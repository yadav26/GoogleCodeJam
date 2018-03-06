using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// CopyRight Anshul Yadav ; LinkedIn www.linkedin.com/in/anshul-yadav-2289b734
/// </summary>
namespace PurePrime
{
    class PureNumber
    {
        long value;
        long index;

        public PureNumber(long i, long v)
        {
            this.index = i;
            this.value = v;
        }

        public long getValue() { return value; }
        public long getIndex() { return index; }
        
    }

    //class Subset
    //{
    //    List<int> subset = new List<int>();
    //    public int Count { get; set; }

    //}


    class Program
    {
        //static public List<Subset> SubsetCollection = new List<Subset>();

        static int index = 1;
        static long countPure = 0;


        static List<int> ls_TestCases = null;
        static List<int> ls_InputSet = new List<int>();

        /// <summary>
        /// https://code.google.com/codejam/contest/635101/dashboard#s=p2
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory;


            string small_input_path = path + @"..\..\C-small-practice.in";
            string small_output_path = path + @"..\..\C-small-practice.out";
            TestCase tp_small = new TestCase(small_input_path);
            SolveProblem(tp_small, small_output_path);



            //string large_input_path = path + @"..\..\C-large-practice.in";
            //string large_output_path = path + @"..\..\C-large-practice.out";
            //TestCase tp_large = new TestCase(large_input_path);
            //SolveProblem(tp_large, large_output_path);



            return;
        }


        /// <summary>
        /// 
        /// Using DYNAMIC PROGRAMING TO QUICKLY FIND THEIR SOLUTION OF ALREADY CALCULATED SOLUTION.
        /// 
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="outputPath"></param>
        static void SolveProblem(TestCase tc, string outputPath)
        {


            ls_TestCases = tc.lsTestCases;

            List<PureNumber> lsResults = new List<PureNumber>();
            
            int testcase_id = 0;

            foreach (int number in ls_TestCases)
            {

                countPure = 0;

                ++testcase_id;

                using (StreamWriter wr = new StreamWriter(outputPath, true))
                {
                    PureNumber objectfound = lsResults.Find(x => x.getIndex() == number);
                    if (objectfound == null)
                    {
                        CreateAllSubsetsCollection(number);
                        
                        long mod = countPure % 100003;

                        wr.WriteLine("Case #{0}: {1}", testcase_id, mod);

                        lsResults.Add(new PureNumber(number, mod));

                        Console.WriteLine("\nCase #{0}: {1}", testcase_id, mod);
                    }
                    else
                    {

                        wr.WriteLine("Case #{0}: {1}", testcase_id, objectfound.getValue());

                        Console.WriteLine("Case #{0}: {1}", testcase_id, objectfound.getValue());
                    }

                }
            }


            return;

        }

        /// <summary>
        /// UNUSED - PRIME NUMBER CHECKER
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static bool IsPrime( long number )
        {
            for (long i = 2; i <= (number / 2) + 2; i++)
            {

                if (0 == (number % i) && i != number)
                {
                    return false;
                }

                if (i == ((number / 2)) ||
                         (0 == (number % i) ||
                         i == number))
                {

                    return true;
                }

            }
            return false;
        }

        
        static List<PureNumber> PrimeList = new List<PureNumber>();

        /// <summary>
        /// NOT IN USE, UNUSED
        /// 
        /// CREATES LIST OF ALL PURE PRIME NUMBERS
        /// 
        /// </summary>
        static void CreatePurePrimList()
        {
            
            long Max = 1000000;
            long counter = 2;
            PrimeList.Add(new PureNumber(0,0));

            long rank = 0;
            int prevPrimeRank = 0;
            while (counter < Max)
            {
                if (IsPrime(counter))
                {
                    
                    rank = ++prevPrimeRank;//PrimeList.Count;
                    long newRank = rank;
                    while( rank != 1 )
                    {
                        PureNumber ppO = PrimeList.Find(x =>x.getValue() == rank);
                        if (null ==ppO)
                            break;

                        rank = ppO.getIndex();
                    }
                    if (rank == 1 || PrimeList.Count <= 1)
                    {
                        PrimeList.Add(new PureNumber(prevPrimeRank, counter)) ;
                        Console.WriteLine("{0}:{1}", prevPrimeRank, counter);
                    }

                }
                counter++;
            }
        }

        /// <summary>
        /// WE TRAVERSE TO ALL THE WAY BACK TO GET THE RANK AND FIND THE ELEMENT AND THEN
        /// ITS RANK TO REACH 1
        /// 
        /// IF 1 IS LAST REACHED, THEN ITS PURE
        /// 
        /// </summary>
        /// <param name="testcase_id"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool PureNumber(int testcase_id, int number)
        {

            if(number <= 0)
                return false;

            int prevRank = ls_InputSet.IndexOf(number)+1;

            while (prevRank != 1)
            {
                int rank = ls_InputSet.IndexOf(prevRank)+1;
                if (rank == -1 || prevRank == rank)
                    break;

                prevRank = rank;
            }

            if (prevRank == 1 )
            {
                
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// HERE WE WILL CREATE ALL
        /// POSSIBLE SUBSETS OF A GIVEN SET; 
        /// TOTAL SUBSETS EQUAL TO 
        /// 
        /// [ 2 POW N ]
        /// 
        /// </summary>
        /// <param name="number"></param>
        private static void CreateAllSubsetsCollection( int number )
        {
            //total subsets of a Sets are 2*power n
            //int[] arr = { 1, 2, 3, 4 };

            //because number should be the highest # in last
            //array will start from 2
            //so first index val will be 2,3,4,5,6..... onwards
            //
            if (number < 2)
                return;


            int sizeofarray = number - 1;
            int start_number = 2;
            int[] arr = new int[sizeofarray];
            
            //Console.Write("\nInput SET for [{0}]- ", number);
            for (int k=0;k< sizeofarray; ++k)
            {
                arr[k] = start_number+k;

            //    Console.Write("[{0}:{1}] ", k, arr[k]);
            }


            int nCount = 1 << (sizeofarray);

            int subsetCount = 1;
            for ( int i = nCount-1; i >=0; --i)
            {
                BitArray b = new BitArray(BitConverter.GetBytes(i));

               // Console.Write("\n{0} #[", subsetCount++);

                ls_InputSet.Clear();

                for (int bit = 0; bit <= arr.Length ; bit++)
                {
                    if (b[bit] == true)
                    {
                        ls_InputSet.Add(arr[bit]);
                     //   Console.Write("{0} ", arr[bit]);
                    }
                    
                }

             //   Console.Write("]");

                if (PureNumber(number, number))
                {
                    //Console.Write("\n[");
                    //foreach (int x in ls_InputSet)
                    //    Console.Write("{0} ", x);
                    //Console.Write("]");
                    countPure++;
                }
                   
            }

        }

        
    }
}
