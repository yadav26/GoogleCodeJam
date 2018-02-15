using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Google_FileFix_it
{
    public class TestPlan
    {
        public int TotalTC { get; set; }
        public List<TestCase> ListTC_array { get; set; }

        public List<TestCase> list_tc = new List<TestCase>();

        public TestPlan( string inputfile )
        {
            string[] lines = System.IO.File.ReadAllLines(inputfile);

            // Display the file contents by using a foreach loop.
           // System.Console.WriteLine("Contents of inputfile.txt = ");

            TotalTC = int.Parse(lines[0]);
            int total_input_lines = lines.Length;
            int counter = 1;
            for( int i=0; i < TotalTC; ++i)
            {
                
                string[] vals = lines[counter].Split(' ').ToArray();
                int N = int.Parse(vals[0]);
                int M = int.Parse(vals[1]);

                counter += 1;

                StringCollection exists = new StringCollection();
                for (int j = 0; j < N; ++j)
                    exists.Add( lines[counter + j] );

                counter += N;
                StringCollection toCreate = new StringCollection();

                for (int j = 0 ; j < M; ++j)
                    toCreate.Add(lines[counter + j]);

                list_tc.Add( new TestCase(i, N, M, exists, toCreate) );

                counter += M;

            }

            ListTC_array = list_tc;

    
        }
         

    }
}
