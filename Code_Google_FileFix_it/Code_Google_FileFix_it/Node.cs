using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Google_FileFix_it
{
    public class Node
    {
        public string name { get; set; }
        public int id { get; set; }
        public int depth { get; set; }
        public string parent_name { get; set; }
        public int parent_id { get; set; }

        public Node( string nodeName, int node_id, int d, string pname, int pid )
        {
            name = nodeName;
            id = node_id;
            depth = d;
            parent_name = pname;
            parent_id = pid;
        }
    }
}
