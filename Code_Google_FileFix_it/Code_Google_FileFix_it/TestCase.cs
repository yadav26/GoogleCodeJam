using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Google_FileFix_it
{
    public class TestCase
    {
        public int mkdir_count { get; set; }
        public int Id { get; set; }

        public StringCollection exists { get; set; }
        public StringCollection toCreate { get; set; }
        public TestCase (int id, int N, int M, StringCollection ex, StringCollection create )
        {
            Console.WriteLine("\n TEST CASE #" + id);

            Console.WriteLine("Exists:");
            for (int i = 0; i < ex.Count; i++)
            {
                Console.WriteLine("   [{0}] {1}", i, ex[i]);
            }

            Console.WriteLine("To Create:");
            for (int i = 0; i < create.Count; i++)
            {
                Console.WriteLine("   [{0}] {1}", i, create[i]);
            }

            exists = ex;
            toCreate = create;

            Id = id;
        }
    }
}
