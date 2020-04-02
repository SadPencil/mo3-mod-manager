using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Mo3ModManager
{
    static class IO
    {
        /// <summary>
        /// Delete everything in the directory.
        /// </summary>
        /// <param name="Directory">The directory to be cleared</param>
        public static void ClearDirectory(string Directory)
        {

            var directoryInfo = new DirectoryInfo(Directory);
            foreach (var file in directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly))
            {
                file.Delete();
            }
            foreach (var folder in directoryInfo.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                folder.Delete(true);
            }
        }
        /// <summary>
        /// Determine whether the two files are actually the same.
        /// Note that in the ReFS file system, the method might not work properly. Must use GetFileInformationByHandleEx() to support ReFS.
        /// </summary>
        /// <param name="FileA">A file's path.</param>
        /// <param name="FileB">Another file's path</param>
        /// <returns></returns>
        public static bool IsSameFile(string FileA, string FileB)
        {
            IntPtr fileAHandle = Win32.NativeMethods.CreateFileW(
                FileA,
                Win32.NativeConstants.GENERIC_READ,
                Win32.NativeConstants.FILE_SHARE_READ | Win32.NativeConstants.FILE_SHARE_WRITE,
                IntPtr.Zero,
                Win32.NativeConstants.OPEN_EXISTING,
                Win32.NativeConstants.FILE_ATTRIBUTE_NORMAL,
                IntPtr.Zero
                );

            if (fileAHandle == Win32.NativeConstants.INVALID_HANDLE_VALUE)
            {
                Trace.WriteLine("[Warn] Can not open file " + FileA + ". Error " + Win32.NativeMethods.GetLastError() + ".");
                return false;
            }

            IntPtr fileBHandle = Win32.NativeMethods.CreateFileW(
                FileB,
                Win32.NativeConstants.GENERIC_READ,
                Win32.NativeConstants.FILE_SHARE_READ | Win32.NativeConstants.FILE_SHARE_WRITE,
                IntPtr.Zero,
                Win32.NativeConstants.OPEN_EXISTING,
                Win32.NativeConstants.FILE_ATTRIBUTE_NORMAL,
                IntPtr.Zero
                );

            if (fileBHandle == Win32.NativeConstants.INVALID_HANDLE_VALUE)
            {
                Trace.WriteLine("[Warn] Can not open file " + FileB + ". Error " + Win32.NativeMethods.GetLastError() + ".");
                return false;
            }

            if (!Win32.NativeMethods.GetFileInformationByHandle(fileAHandle, out var fileAInfo))
            {
                Trace.WriteLine("[Warn] Can not get information of file " + FileA + ". Error " + Win32.NativeMethods.GetLastError() + ".");
                return false;
            }

            if (!Win32.NativeMethods.GetFileInformationByHandle(fileBHandle, out var fileBInfo))
            {
                Trace.WriteLine("[Warn] Can not get information of file " + FileB + ". Error " + Win32.NativeMethods.GetLastError() + ".");
                return false;
            }

            Win32.NativeMethods.CloseHandle(fileAHandle);
            Win32.NativeMethods.CloseHandle(fileBHandle);

            return (fileAInfo.dwVolumeSerialNumber == fileBInfo.dwVolumeSerialNumber) && (fileAInfo.nFileIndexHigh == fileBInfo.nFileIndexHigh) && (fileAInfo.nFileIndexLow == fileBInfo.nFileIndexLow);

        }


        /// <summary>
        /// Delete all empty subfolders.
        /// </summary>
        /// <param name="directory">The folder</param>
        /// <returns> Whether the folder is deleted or not.</returns>
        public static bool RemoveEmptyFolders(string directory)
        {
            bool status = true;
            foreach (var folder in Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly))
            {
                bool result = RemoveEmptyFolders(folder);
                status = status && result;
            }

            if (status && (Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly).Count() == 0))
            {
                Directory.Delete(directory);
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Create hard links for every files, just like copying folders.
        /// </summary>
        /// <param name="SrcDirectory">The source folder.</param>
        /// <param name="DestDirectory">The destination folder.</param>
        public static void CreateHardLinksOfFiles(string SrcDirectory, string DestDirectory)
        {
            CreateHardLinksOfFiles(SrcDirectory, DestDirectory, false, null);
        }
        /// <summary>
        /// Create hard links for every files, just like copying folders.
        /// </summary>
        /// <param name="SrcDirectory">The source folder.</param>
        /// <param name="DestDirectory">The destination folder.</param>
        /// <param name="Override">Whether override files if exist</param>
        public static void CreateHardLinksOfFiles(string SrcDirectory, string DestDirectory, bool Override)
        {
            CreateHardLinksOfFiles(SrcDirectory, DestDirectory, Override);
        }
        public static void CreateHardLinksOfFiles(string SrcDirectory, string DestDirectory, bool Override, List<string> skipExtensionsWithDotUpper)
        {
            Debug.WriteLine("Source: " + SrcDirectory);
            //Create all folders
            foreach (string subDirectory in Directory.GetDirectories(SrcDirectory, "*", SearchOption.AllDirectories))
            {
                string relativeName = subDirectory.Substring(SrcDirectory.Length + 1);
                //no matter this folder exists or not, the following method don't throw exception 
                Directory.CreateDirectory(Path.Combine(DestDirectory, relativeName));

                Debug.WriteLine("CreateDirectory: " + relativeName);
            }

            //Create NTFS hard link if not existed
            foreach (string srcFullName in Directory.GetFiles(SrcDirectory, "*", SearchOption.AllDirectories))
            {
                string relativeName = srcFullName.Substring(SrcDirectory.Length + 1);
                string destFullName = Path.Combine(DestDirectory, relativeName);

                CreateHardLinkOrCopy(destFullName, srcFullName, Override, skipExtensionsWithDotUpper);
                //if (!File.Exists(destFullName))
                //{
                //    Win32.NativeMethods.CreateHardLinkW(destFullName, srcFullName, IntPtr.Zero);
                //}
                //else
                //{
                //    if (Override)
                //    {
                //        //File exists
                //        Debug.WriteLine("Overrided: " + relativeName);
                //        File.Delete(destFullName);
                //        Win32.NativeMethods.CreateHardLinkW(destFullName, srcFullName, IntPtr.Zero);
                //    }
                //}
            }
        }

        private static void CreateHardLinkOrCopy(string destFile, string srcFile, bool Override, List<string> skipExtensionsWithDot)
        {
            var ext = Path.GetExtension(destFile).ToUpperInvariant();
            if (File.Exists(destFile) && Override)
            {
                Debug.WriteLine("Overrided: " + destFile);
                File.Delete(destFile);
            }
            
            if (!File.Exists(destFile)) {

                if (skipExtensionsWithDot != null && skipExtensionsWithDot.Contains(ext))
                {
                    File.Copy(srcFile, destFile, Override);
                }
                else
                {
                    Win32.NativeMethods.CreateHardLinkW(destFile, srcFile, IntPtr.Zero);
                }
            } 
                
        }

    }
}
