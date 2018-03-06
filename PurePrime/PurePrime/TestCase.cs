using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurePrime
{
    public class TestCase
    {
        int nTestCases { get; set; }
        public List<int> lsTestCases { get; set; }
        public TestCase( string infile)
        {
            using (StreamReader reader = new StreamReader(infile))
            {
                string lineRead = reader.ReadLine();
                nTestCases = int.Parse(lineRead);
                if (null == lsTestCases)
                {
                    lsTestCases = new List<int>();
                    //lsTestCases.Add(0);
                }

                for (int i = 0; i < nTestCases; ++i)
                {
                    string str = reader.ReadLine();
                    lsTestCases.Add(int.Parse(str));
                }
            }

        }
    }
}
