using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Mo3ModManager
{
    class ModProcessManager
    {
        public ModProcessManager(ModProcessManagerArguments Arguments)
        {
            this.ProfileDirectory = Arguments.ProfileDirectory;
            this.RunningDirectory = Arguments.RunningDirectory;
            this.Node = Arguments.Node;
        }


        /*
        /// <summary>
        /// Only make sense in multi-threading. Not implemented now.
        /// </summary>
        public enum ProcessStatus
        {
            Prepairing,
            Running,
            Cleaning,
            Finished
        };

        /// <summary>
        /// Only make sense in multi-threading. Not implemented now.
        /// </summary>
        public ProcessStatus Status { get; set; }
        */

        public string RunningDirectory { get; set; }
        public string ProfileDirectory { get; set; }

        //public ModItem ModItem { get; set; }
        public Node Node { get; set; }



        /// <summary>
        /// Delete symlink for both the node and its ancestor.
        /// Note, if a file was deleted and rebuilt, they will be considered as respective files, and the file will be reserved.
        /// </summary>
        /// <param name="ModItem">The node</param>
        private void CleanNode(Node Node,object addition =null)
        {
            //source and destination is the same with "PrepareNode"
            string sourceDirectory = Node.FilesDirectory;
            Debug.WriteLine(sourceDirectory);
            string destinationDirectory = this.RunningDirectory;

            //Remove a NTFS hard link is equal to remove a file
            Exception ex = null;
            if (addition != null)
            {
                ex = (Exception)addition;
            }
            foreach (string srcFullName in Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                string relativeName = srcFullName.Substring(sourceDirectory.Length + 1);
                string destFullName = Path.Combine(destinationDirectory, relativeName);
                if (File.Exists(destFullName))
                {
                    //Determine whether the two files are actually the same.
                    if (IO.IsSameFile(srcFullName, destFullName))
                    {
                        //Debug.WriteLine("Delete file: " + destFullName);
                        try
                        {
                            File.Delete(destFullName);
                        }
                        catch (Exception e)
                        {
                            ex = e;
                        }

                    }
                    else
                    {
                        Debug.WriteLine("Reserved: " + relativeName);
                    }

                }
            }

            //Recursive for its parent
            if (!Node.IsRoot)
            {
                this.CleanNode(Node.Parent,ex);
            }
            if (ex != null)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Create symlink for both the node and its ancestor
        /// </summary>
        /// <param name="ModItem">The node</param>
        private void PrepareNode(Node Node)
        {
            string srcDirectory = Node.FilesDirectory;
            string destDirectory = this.RunningDirectory;

            IO.CreateHardLinksOfFiles(srcDirectory, destDirectory, false, new List<string>() { ".INI" });

            //Recursive for its parent
            if (!Node.IsRoot)
            {
                this.PrepareNode(Node.Parent);
            }
        }


        /// <summary>
        /// Run a program and wait till all of its descendants to exit. Even if the program exited early than its descendants, this method can still work properly.
        /// Only supports Windows 8 and newer.
        /// </summary>
        private void RunStep2_RunAndWait()
        {
            this.RunStep2_RunAndWait(Path.Combine(this.RunningDirectory, this.Node.MainExecutable), this.Node.Arguments, this.RunningDirectory);
        }

        /// <summary>
        /// Run a program and wait till all of its descendants to exit. Even if the program exited early than its descendants, this method can still work properly.
        /// Only supports Windows 8 and newer.
        /// </summary>
        /// <param name="Fullname">The program's path.</param>
        /// <param name="Arguments">The arguments.</param>
        /// <param name="WorkingDirectory">The working directory.</param>
        private void RunStep2_RunAndWait(string Fullname, string Arguments, string WorkingDirectory)
        {
            string commandLine = '"' + Fullname + '"' + (String.IsNullOrEmpty(Arguments) ? string.Empty : ' ' + Arguments);

            //See : https://blogs.msdn.microsoft.com/oldnewthing/20130405-00/?p=4743

            Trace.WriteLine("[Note] Start up the game...");

            IntPtr jobHandle = Win32.NativeMethods.CreateJobObjectW(IntPtr.Zero, String.Empty);
            if (jobHandle == IntPtr.Zero)
            {
                throw new Exception("WinAPI CreateJobObjectW failed. Error " + Win32.NativeMethods.GetLastError());
            }


            IntPtr ioPortHandle = Win32.NativeMethods.CreateIoCompletionPort(Win32.NativeConstants.INVALID_HANDLE_VALUE, IntPtr.Zero, 0, 1);
            if (ioPortHandle == IntPtr.Zero)
            {
                throw new Exception("WinAPI CreateIoCompletionPort failed. Error " + Win32.NativeMethods.GetLastError());
            }

            var portStruct = new Win32.JOBOBJECT_ASSOCIATE_COMPLETION_PORT()
            {
                CompletionKey = jobHandle,
                CompletionPort = ioPortHandle
            };

            IntPtr portStructIntPtr = IntPtr.Zero;
            try
            {
                int portStructLength = Marshal.SizeOf(portStruct);
                portStructIntPtr = Marshal.AllocHGlobal(portStructLength);
                Marshal.StructureToPtr(portStruct, portStructIntPtr, false);
                if (!Win32.NativeMethods.SetInformationJobObject(
                    jobHandle,
                    Win32.JOBOBJECTINFOCLASS.JobObjectAssociateCompletionPortInformation,
                    portStructIntPtr,
                    (uint)portStructLength
                    ))
                {
                    throw new Exception("WinAPI SetInformationJobObject failed. Error " + Win32.NativeMethods.GetLastError());
                }

            }
            finally
            {
                if (portStructIntPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(portStructIntPtr);
            }

            var processInformationStruct = new Win32.PROCESS_INFORMATION() { };

            var startupInfoStruct = new Win32.STARTUPINFOW() { };

            //show the Working in Background cursor
            startupInfoStruct.dwFlags = Win32.NativeConstants.STARTF_FORCEONFEEDBACK;

            startupInfoStruct.cb = (uint)Marshal.SizeOf(startupInfoStruct);

            // Win32.NativeConstants.CREATE_BREAKAWAY_FROM_JOB might have some problems
            // https://blog.csdn.net/jpexe/article/details/49661479

            if (!Win32.NativeMethods.CreateProcessW(null, new StringBuilder(commandLine), IntPtr.Zero, IntPtr.Zero, false, Win32.NativeConstants.CREATE_SUSPENDED, IntPtr.Zero, WorkingDirectory, ref startupInfoStruct, out processInformationStruct))
            {
                throw new Exception("WinAPI CreateProcessW failed.  Error " + Win32.NativeMethods.GetLastError());
            }


            if (!Win32.NativeMethods.AssignProcessToJobObject(jobHandle, processInformationStruct.hProcess))
            {
                throw new Exception("WinAPI AssignProcessToJobObject failed. Error " + Win32.NativeMethods.GetLastError());
            }

            Win32.NativeMethods.ResumeThread(processInformationStruct.hThread);

            //Close unnecessary handles
            Win32.NativeMethods.CloseHandle(processInformationStruct.hThread);
            Win32.NativeMethods.CloseHandle(processInformationStruct.hProcess);

            // DWORD->unsigned int
            uint completionCode;

            // ULONG_PTR->unsigned int
            IntPtr completionKey;

            ///LPOVERLAPPED->_OVERLAPPED*
            IntPtr overlapped;

            while (
                Win32.NativeMethods.GetQueuedCompletionStatus(
                    ioPortHandle,
                    out completionCode,
                    out completionKey,
                    out overlapped,
                    Win32.NativeConstants.INFINITE
                    ) && !(
                    completionKey == jobHandle &&
                    completionCode == Win32.NativeConstants.JOB_OBJECT_MSG_ACTIVE_PROCESS_ZERO
                    )
                    )
            {
                //do nothing
            }

            //all set
            Win32.NativeMethods.CloseHandle(jobHandle);
            Win32.NativeMethods.CloseHandle(ioPortHandle);

            Trace.WriteLine("[Note] Game exited.");

        }






        private void SetCompatibility(string Fullname, string Compatibility)
        {
            if (!String.IsNullOrWhiteSpace(Compatibility))
                using (var registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"))
                {
                    registryKey.SetValue(Fullname, Compatibility);
                }
        }


        private void RunStep1_Prepare()
        {
            if (!Directory.Exists(this.ProfileDirectory))
            {
                Directory.CreateDirectory(this.ProfileDirectory);
            }
            Trace.WriteLine("[Note] Clearing Running Directory...");
            //RunningDirectory should be empty. Delete all of them otherwise
            if (Directory.Exists(this.RunningDirectory))
            {
                IO.ClearDirectory(this.RunningDirectory);
            }
            else
            {
                Directory.CreateDirectory(this.RunningDirectory);
            }

            Trace.WriteLine("[Note] Create hard links for profiles...");
            //Move profiles
            IO.CreateHardLinksOfFiles(this.ProfileDirectory, this.RunningDirectory, false, new List<string>() { ".INI" });

            Trace.WriteLine("[Note] Create hard links for game files...");
            //Create hard links recursively - from leaf node to root
            this.PrepareNode(this.Node);


            this.SetCompatibility(
                Path.Combine(this.RunningDirectory, this.Node.MainExecutable),
                this.Node.Compatibility);

            //TODO: Set the registry to avoid firewall

        }

        private void RunStep3_Clean()
        {
            Trace.WriteLine("[Note] Remove game files...");
            //Clean the node
            this.CleanNode(this.Node);

            Trace.WriteLine("[Note] Remove empty folders...");
            //Remove unnecessary folders
            if (IO.RemoveEmptyFolders(this.RunningDirectory))
            {
                Directory.CreateDirectory(this.RunningDirectory);
            }
            else
            {
                Trace.WriteLine("[Note] Save profiles...");
                //Save profiles
                IO.CreateHardLinksOfFiles(this.RunningDirectory, this.ProfileDirectory, true, new List<string>() { ".INI" });
            }

            IO.ClearDirectory(this.RunningDirectory);

            /*
            //Remove the directory
            System.IO.Directory.Delete(this.RunningDirectory, true);
            */

        }

        /// <summary>
        /// Do everything to run the game from specified arguments. 
        /// Run and wait for user's confirmation that the game has exited. Will not block the thread.
        /// This is a workaround for Windows 7 and earlier.
        /// This method MUST be run in the UI thread.
        /// </summary> 
        /// <param name="parent">The parent window of MessageBox.</param>
        public void RunLegacyAsync(System.Windows.Window parent)
        {
            System.ComponentModel.BackgroundWorker worker1 = new System.ComponentModel.BackgroundWorker();

            worker1.DoWork += (object worker1_sender, System.ComponentModel.DoWorkEventArgs worker1_e) =>
            {
                RunStep1_Prepare();

                new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = Path.Combine(this.RunningDirectory, this.Node.MainExecutable),
                        Arguments = this.Node.Arguments,
                        WorkingDirectory = this.RunningDirectory
                    }
                }.Start();

                //sleep for 10s
                System.Threading.Thread.Sleep(10000);
            };
            worker1.RunWorkerCompleted += (object worker1_sender, System.ComponentModel.RunWorkerCompletedEventArgs worker1_e) =>
            {
                if (worker1_e.Error != null)
                {
                    this.RunWorkerCompleted(worker1_sender, worker1_e);
                }
                else
                {
                    //continue
                    System.Windows.MessageBox.Show(parent, "You are still running Windows 7 or earlier.\n It's too old so we can't know whether the game has exited or not.\n Click the OK button when the game has exited.",
                   "You should consider upgrading to Windows 10", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                    System.Windows.MessageBox.Show(parent, "Only click the OK button when the game has exited.", "Double check",
                        System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);

                    // notice for dumbs
                    for (var i = 0; i < 5; ++i)
                    {
                        Process[] ps = Process.GetProcesses();
                        if (ps.Count() == 0) break;

                        foreach (Process p in ps)
                        {
                            string exePath;
                            try {
                                exePath = Path.GetDirectoryName(p.MainModule.FileName);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                            
                            System.Diagnostics.Debug.WriteLine(exePath);
                            System.Diagnostics.Debug.WriteLine(this.RunningDirectory);

                            if (this.RunningDirectory.Contains(exePath))
                            {
                                System.Windows.MessageBox.Show(parent, "Only click the OK button when the game has exited.", "Double check",
                                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                                break;
                            }
                        }
                    }

                    Trace.WriteLine("[Note] Game exited.");

                    System.ComponentModel.BackgroundWorker worker2 = new System.ComponentModel.BackgroundWorker();

                    worker2.DoWork += (object worker2_sender, System.ComponentModel.DoWorkEventArgs worker2_e) =>
                    {
                        //wait for 3s. just in case 
                        System.Threading.Thread.Sleep(3000);
                        RunStep3_Clean();
                    };

                    worker2.RunWorkerCompleted += (object worker2_sender, System.ComponentModel.RunWorkerCompletedEventArgs worker2_e) =>
                    {
                        this.RunWorkerCompleted(worker2_sender, worker2_e);
                    };

                    worker2.RunWorkerAsync();
                }

            };

            worker1.RunWorkerAsync();

        }

        /// <summary>
        /// Do everything to run the game from specified arguments. 
        /// Wait till game exited. Will not block the thread.
        /// Only supports Windows 8 or newer. 
        /// </summary>
        public void RunAsync()
        {
            System.ComponentModel.BackgroundWorker worker1 = new System.ComponentModel.BackgroundWorker();

            worker1.DoWork += (object worker1_sender, System.ComponentModel.DoWorkEventArgs worker1_e) =>
            {
                Run();
            };

            worker1.RunWorkerCompleted += (object worker1_sender, System.ComponentModel.RunWorkerCompletedEventArgs worker1_e) =>
            {
                this.RunWorkerCompleted(worker1_sender, worker1_e);
            };

            worker1.RunWorkerAsync();

        }

        /// <summary>
        /// Occurs when the background operation has completed, has been canceled, or has raised an exception.
        /// </summary>
        public event System.ComponentModel.RunWorkerCompletedEventHandler RunWorkerCompleted;

        /// <summary>
        /// Do everything to run the game from specified arguments. 
        /// Wait till game exited. Will block the thread.
        /// Only supports Windows 8 or newer. 
        /// </summary>
        public void Run()
        {
            RunStep1_Prepare();
            RunStep2_RunAndWait();

            //wait for 3s. just in case 
            System.Threading.Thread.Sleep(3000);
            RunStep3_Clean();
        }


    }
}



