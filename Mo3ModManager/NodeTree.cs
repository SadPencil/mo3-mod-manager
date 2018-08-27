using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mo3ModManager
{
    class NodeTree
    {
        public Dictionary<string, Node> NodesDictionary { get; private set; }
        public List<Node> RootNodes { get; private set; }


        private List<Node> GetNodesFromDirectory(string Directory)
        {
            System.IO.DirectoryInfo modsParentFolder = new System.IO.DirectoryInfo(Directory);
            System.IO.DirectoryInfo[] modsFolders = modsParentFolder.GetDirectories();


            List<Node> nodes = new List<Node>();
            //Find "node.json" file in the folders and try to parse them.
            foreach (var modFolder in modsFolders)
            {
                // node.json must exist
                var nodeFiles = modFolder.GetFiles("node.json", System.IO.SearchOption.TopDirectoryOnly);
                System.Diagnostics.Debug.Assert(nodeFiles.Length <= 1);
                if (nodeFiles.Length == 0) continue;
                var nodeFile = nodeFiles[0];

                // Files folder must exist
                var filesFolders = modFolder.GetDirectories("Files", System.IO.SearchOption.TopDirectoryOnly);
                System.Diagnostics.Debug.Assert(nodeFiles.Length <= 1);
                if (nodeFiles.Length == 0) continue;

                //parse
                try
                {
                    Node node = Node.Parse(modFolder.FullName);
                    nodes.Add(node);
                }
                catch (Exception)
                {
                    continue;
                }


            }
            return nodes;
        }

        private void BuildTree(List<Node> Nodes)
        {
            foreach (var node in Nodes)
            {
                if (NodesDictionary.ContainsKey(node.ID)) throw new Exception("Node " + node.ID + " already exists.");
                NodesDictionary[node.ID] = node;
            }


            foreach (var node in Nodes)
            {
                if (!node.IsRoot)
                {
                    if (!NodesDictionary.ContainsKey(node.ParentID)) throw new Exception("Node " + node.ParentID + " not exists.");
                    node.Parent = NodesDictionary[node.ParentID];
                    node.Parent.Childs.Add(node);
                }
                else
                {
                    RootNodes.Add(node);
                    node.Parent = null;
                }
            }

        }

        public NodeTree()
        {
            this.NodesDictionary = new Dictionary<string, Node>();
            this.RootNodes = new List<Node>();
        }

        public NodeTree(NodeTree NodeTree)
        {
            this.NodesDictionary = new Dictionary<string, Node>(NodeTree.NodesDictionary);
            this.RootNodes = new List<Node>(NodeTree.RootNodes);
        }

        public void AddNodes(string Directory)
        {
            var nodes = GetNodesFromDirectory(Directory);
            BuildTree(nodes);
        }

        public void RemoveNode(Node OldNode)
        {
            //only leaf node is allowed to be removed
            System.Diagnostics.Debug.Assert(OldNode.Childs.Count == 0);

            if (OldNode.Parent != null)
            {
                OldNode.Parent.Childs.Remove(OldNode);
            }
            else
            {
                this.RootNodes.Remove(OldNode);
            }

            this.NodesDictionary.Remove(OldNode.ID);
        }

    }
}
