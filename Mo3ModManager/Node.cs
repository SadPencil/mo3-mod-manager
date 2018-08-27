using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mo3ModManager
{
    public class Node
    {
        public override bool Equals(object obj)
        {
            if (obj is Node)
                return this.ID == (obj as Node).ID;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }


        public string ID { get; set; }
        public string Name { get; set; }
        public bool IsRunnable { get { return !String.IsNullOrWhiteSpace(this.MainExecutable); } }
        public string MainExecutable { get; set; }
        public string Arguments { get; set; }
        public bool IsRoot { get { return String.IsNullOrWhiteSpace(this.ParentID); } }
        public string ParentID { get; set; }
        public string Compatibility { get; set; }

        public Node Parent { get; set; }
        public List<Node> Childs { get; set; }

        /// <summary>
        /// The path of the node folder
        /// </summary>
        public string Directory { get; set; }
        public string FilesDirectory { get { return System.IO.Path.Combine(this.Directory, "Files"); } }

        public Node()
        {
            this.Parent = null;
            this.Childs = new List<Node>();

            this.ParentID = String.Empty;
            this.Arguments = String.Empty;
            this.MainExecutable = String.Empty;
            //ID must not be same 
            this.ID = System.Guid.NewGuid().ToString();
            this.Name = String.Empty;
            this.Directory = String.Empty;
        }

        /// <summary>
        /// Parse a Node from the "node.json" file.
        /// </summary>
        /// <param name="Filename">The path of the folder where contains "node.json" file.</param>
        /// <returns>A parsed Node.</returns>
        public static Node Parse(string Directory)
        {
            string fileContent;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(System.IO.Path.Combine(Directory, "node.json")))
            {
                fileContent = reader.ReadToEnd();
            }

            var raw_node = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(fileContent, new
            {
                id = String.Empty,
                name = String.Empty,
                main_executable = String.Empty,
                arguments = String.Empty,
                parent = String.Empty,
                compatibility = String.Empty //https://technet.microsoft.com/en-us/library/mt243980.aspx

            });

            if (String.IsNullOrWhiteSpace(raw_node.name) || String.IsNullOrWhiteSpace(raw_node.id)) throw new FormatException();

            Node node = new Node
            {
                Name = raw_node.name,
                ID = raw_node.id
            };
            if (!String.IsNullOrWhiteSpace(raw_node.main_executable))
            {
                node.MainExecutable = raw_node.main_executable;
            }
            else
            {
                node.MainExecutable = String.Empty;
            }

            if (!String.IsNullOrWhiteSpace(raw_node.arguments))
            {
                node.Arguments = raw_node.arguments;
            }
            else
            {
                node.Arguments = String.Empty;
            }

            if (!String.IsNullOrWhiteSpace(raw_node.compatibility))
            {
                node.Compatibility = raw_node.compatibility;
            }
            else
            {
                node.Compatibility = String.Empty;
            }

            if (!String.IsNullOrWhiteSpace(raw_node.parent))
            {
                node.ParentID = raw_node.parent;
            }
            else
            {
                node.ParentID = String.Empty;
            }
             
            node.Directory = Directory;

            return node;

        }

    }
}
