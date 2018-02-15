using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Google_FileFix_it
{
    public class Tree
    {
        public int CountMKDIR { get; set; }

        public static int depth = 0;
        public static int Depth { get; set; }

        public List<Node> DirTree = new List<Node>();
        public Tree( StringCollection dirpathsExists )
        {
            //Add root node
            DirTree.Add(new Node("ROOT", DirTree.Count, 0, "", -1));
            foreach ( string path in dirpathsExists)
            {

                string[] dirnames = path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                GenerateTree( path );

            }

            //set the depth ;if 0 no tree
            Depth = depth;

        }

        internal int CountMkDir(StringCollection toCreate)
        {
            
            foreach( string str in toCreate)
            {
                CreateNewDir(str);
            }

            return CountMKDIR;
        }

        private int CreateNewDir( string path )
        {

            string[] dirnames = path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            int prevParentId = 0;
            string prevParentName = DirTree[0].name;


            for (int i = 0; i < dirnames.Length; ++i)
            {
                string tempNodeName = dirnames[i];
                string parentName = string.Empty;
                int currDepth = i + 1;
                //int parent_id = prevParentId;
                int newNodeID = DirTree.Count;
                Node foundNode = FindNode(tempNodeName, currDepth, prevParentId, prevParentName);
                if (null == foundNode)
                {// here means, this node 
                    //if depth is 0, means a new root child
                    if (currDepth == 1) // 0 is ROOT; next level is RC1 - depth =1
                    {
                        parentName = DirTree[0].name;

                    }
                    else
                    {
                        parentName = prevParentName;
                    }

                    DirTree.Add(new Node(tempNodeName, newNodeID, currDepth, parentName, prevParentId));

                    prevParentId = newNodeID;
                    prevParentName = tempNodeName;
                    ++CountMKDIR;
                }
                else
                { // here means we have found the same [dirname, depth, parentId, parentName]
                    //if that path is okay skip to next child if any
                    prevParentId = foundNode.id;
                    prevParentName = foundNode.name;
                }

            }

            return CountMKDIR; // Total tree nodes

        }

        private int GenerateTree(string path)
        {

            string[] dirnames = path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            int prevParentId = 0;
            string prevParentName = DirTree[0].name;


            for (int i = 0; i < dirnames.Length; ++i)
            {
                string tempNodeName = dirnames[i];
                string parentName = string.Empty;
                int currDepth = i+1;
                //int parent_id = prevParentId;
                int newNodeID = DirTree.Count;
                Node foundNode = FindNode(tempNodeName, currDepth, prevParentId, prevParentName);
                if(null == foundNode)
                {// here means, this node 
                    //if depth is 0, means a new root child
                    if( currDepth == 1 ) // 0 is ROOT; next level is RC1 - depth =1
                    {
                        parentName = DirTree[0].name;
                        
                    }
                    else
                    {
                        parentName = prevParentName;
                    }

                    DirTree.Add(new Node(tempNodeName, newNodeID, currDepth,  parentName, prevParentId));

                    prevParentId = newNodeID;
                    prevParentName = tempNodeName;
                }
                else
                { // here means we have found the same [dirname, depth, parentId, parentName]
                    //if that path is okay skip to next child if any
                    prevParentId = foundNode.id;
                    prevParentName = foundNode.name;
                }

            }

            return DirTree.Count; // Total tree nodes

        }

        private Node FindNode( string searchstr, int depth, int pid, string pname )
        {

            Node node = DirTree.Find(item => item.name == searchstr && item.depth == depth && item.parent_id == pid && item.parent_name == pname)  ;
            
            if (node == null)
                return null;

            return node;
        }
    }
}
