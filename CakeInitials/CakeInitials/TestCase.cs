using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeInitials
{

    /*
     * 
3
3 3
G??
?C?
??J
3 4
CODE
????
?JAM
2 2
CA
KE


     */

    public class TestCase
    {
        public List <string> lsRowItems { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }
        public TestCase(int r, int c )
        {
            rows = r;
            cols = c;

            if(lsRowItems==null)
            {
                lsRowItems = new List<string>();
            }

        }

    }
    public class TestPlan
    {
        int nTestCases { get; set; }
        public List<TestCase> lsTestCases { get; set; }
        public TestPlan( string infile)
        {
            using (StreamReader reader = new StreamReader(infile))
            {
                string lineRead = reader.ReadLine();
                nTestCases = int.Parse(lineRead);
                if (lsTestCases == null)
                {
                    lsTestCases = new List<TestCase>();
                }

                for (int i = 0; i < nTestCases; ++i)
                {
                    string str = reader.ReadLine();
                    string[] rc = str.Split(' ');
                    int rows = int.Parse(rc[0]);
                    int cols = int.Parse(rc[1]);
                    TestCase tc = new TestCase(rows, cols);
                    for(int cnt = 0; cnt < rows; cnt++)
                    {
                        tc.lsRowItems.Add(reader.ReadLine());
                    }
                    lsTestCases.Add(tc);
                }
            }

        }
    }


}
