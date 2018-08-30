using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mo3ModManager
{
    public class ModItem : INotifyPropertyChanged
    {
        private string name;
        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Title");
            }
        }

        public string Title { get { return this.Name; } }

        public System.Collections.ObjectModel.ObservableCollection<ModItem> Items { get; set; }

        //the following properties is not used by UI
        public Node Node { get; set; }
        public ModItem Parent { get; set; }

        public ModItem()
        {
            this.Items = new System.Collections.ObjectModel.ObservableCollection<ModItem>();
            this.Parent = null;
            this.Node = null;
        }
        public ModItem(Node Node)
        {
            this.Items = new System.Collections.ObjectModel.ObservableCollection<ModItem>();
            this.Name = Node.Name;
            this.Node = Node;
            foreach (var child in Node.Childs)
            {
                var childItem = new ModItem(child)
                {
                    Parent = this
                };
                this.Items.Add(childItem);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
