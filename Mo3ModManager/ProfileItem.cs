using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mo3ModManager
{
    class ProfileItem : INotifyPropertyChanged
    {
        private string name;
        public string Name {
            get { return this.name; }
            set {
                this.name = value;
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Title");
            }
        }

        private string directory;
        public string Directory {
            get { return this.directory; }
            set { this.directory = value; NotifyPropertyChanged("Directory"); }
        }

        private long size;
        public long Size {
            get {
                return this.size;
            }
            set {
                this.size = value;
                NotifyPropertyChanged("Size");
                NotifyPropertyChanged("SizeInMB");
            }
        }
        public string SizeInMB {
            get {
                return (this.Size / 1024.0 / 1024.0).ToString("#0.00") + " MB";
            }
        }

        public string Title { get { return this.Name; } }

        public ProfileItem()
        {
            this.Name = String.Empty;
            this.Directory = String.Empty;
            this.Size = 0;
        }

        public ProfileItem(System.IO.DirectoryInfo DirectoryInfo)
        {
            this.Name = DirectoryInfo.Name;
            this.Directory = DirectoryInfo.FullName;
            this.Size = this.GetDirectorySize(DirectoryInfo);
        }

        public ProfileItem(ProfileItem ProfileItem)
        {
            this.ReplaceFrom(ProfileItem);
        }

        public void ReplaceFrom(ProfileItem ProfileItem)
        {
            this.Name = ProfileItem.Name;
            this.Directory = ProfileItem.Directory;
            this.Size = ProfileItem.Size;
        }

        /// <summary>
        /// Recalculate Size.
        /// </summary>
        public void Refresh()
        {
            this.ReplaceFrom(new ProfileItem(new System.IO.DirectoryInfo(this.Directory)));
        }

        private long GetDirectorySize(System.IO.DirectoryInfo DirectoryInfo)
        {
            var files = DirectoryInfo.GetFiles("*", System.IO.SearchOption.AllDirectories);
            long size = 0;
            foreach (var file in files)
            {
                size += file.Length;
            }
            return size;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
