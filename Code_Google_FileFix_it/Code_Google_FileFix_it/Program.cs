using Code_Google_FileFix_it;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Google_FileFix_it
{
    class Program
    {
        static void Main(string[] args)
        {

            //TestPlan tp = new TestPlan(@"C:\MySpace\work\myprojects\Code_Google_FileFix_it\Code_Google_FileFix_it\Inputfile.txt");
           //TestPlan tp = new TestPlan(@"C:\MySpace\work\myprojects\Code_Google_FileFix_it\Code_Google_FileFix_it\A-small-practice.in");
            TestPlan tp = new TestPlan(@"C:\MySpace\work\myprojects\Code_Google_FileFix_it\Code_Google_FileFix_it\A-large-practice.in"); 
            List<TestCase > list_cases = tp.ListTC_array;

            foreach (TestCase tc in list_cases.ToArray())
            {
                Tree dirTree = new Tree(tc.exists);

                
                int count = dirTree.CountMkDir(tc.toCreate);//dirTree.CountMKDIR;

                //Write to a file
                using (StreamWriter writer = new StreamWriter(@"C:\MySpace\work\myprojects\Code_Google_FileFix_it\Code_Google_FileFix_it\A-large-practice.out", true))
                {
                    writer.WriteLine("Case #{0}: {1}", tc.Id + 1, count);
                }
                Console.WriteLine("Case #{0}: {1}", tc.Id+1, count);

            }
        }
    }
}
