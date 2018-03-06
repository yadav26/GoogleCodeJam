using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeInitials
{
    public class CakeRowsCollections
    {
        internal int rowid;

        public string original { get; set; }
        public string modified { get; set; }

    }

    class Program
    {
        
        public static string RemoveDuplicates(string input)
        {
            return new string(input.ToCharArray().Distinct().ToArray());
        }

        public static string BuildSortedNonDuplicates(List<string> ls)
        {
            string onlyInputInitials = string.Empty;
            foreach (string st in ls)
                onlyInputInitials += st;

            //remove all duplicates
            return RemoveDuplicates(onlyInputInitials);
            
        }

        /// <summary>
        /// SuperImposeImageOnSource :
        /// super impose valid initial on invalid initial e.g.
        /// ? ? ? ? 
        /// A B C D
        /// ============ Output as below
        /// A B C D
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string SuperImposeImageOnSource(string first, string second)
        {
            string[] strArray = { string.Empty, string.Empty };

            string toform0 = string.Empty;
            string toform1 = string.Empty;

            for (int j = 0; j < first.Length; ++j)
            {

                if (first[j] == '?' && second[j] == '?')
                {
                    toform0 += '?';
                    
                }
                else
                {
                    if (first[j] == '?')
                        toform0 += second[j];
                    else
                        toform0 += first[j];

                }
                
            }


           // Console.WriteLine("\nInput {0}- {1}- Formed: {2}", first, second, toform0);

            return toform0;

        }




        /// <summary>
        /// Main brain of the solution
        /// Here we figure out which is valid and should be used to replace with invalid = [?]
        /// 
        //find first '?'
        //pass all valid initials
        //record last valid initial
        //push all '?' in list
        //if last valid was null then 
        //valid is the next [+1] character
        //if next valid is [valid]
        // pop all list with valid and append all valid in new list.
        //inputs
        // ????
        // ?A??
        // ??AA
        // ??A?
        // A???
        // ???A
        // ?A?B
        // ?AB?
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <returns></returns>
        public static string SuperImposeImageRows( string first )
        {
            string toform0 = string.Empty;
            string temp = string.Empty;
            char src ='\0';
            for (int i = 0; i < first.Length; ++i)
            {
                
                if (first[i] != '?') //ignore or pass all non '?' e.g. ABA?????
                {
                    src = first[i];
                    toform0 += src;
                    continue;
                }

                
                int p = i; //save the current traversal pointer
                while (first[p] == '?' && p < first.Length) //traverse till next valid initial found or end of string e.g. AB???C?D???
                {

                    if (src != '\0') // possible case start with valid initial e.g. 'A???A'
                        toform0 += src;
                    else // possible case start with '?' e.g. '????A'
                    {
                        temp += '?';

                    }

                    if (++p >= first.Length) 
                        break;

                }

                if (p < first.Length) //*special case* if valid found before eod of string
                {
                    src = first[p];
                    toform0 += src;// we need to append this valid initial
                }

                //if we failed to locate valid src,then push invalid src; for next iteration 
                if (src == '\0')
                    src = '?';

                //now pop all pushed invalid initials with valid src i.e. non '\0'
                for (int x = 0; x < temp.Length; x++)
                    toform0 += src;

                temp = string.Empty; //reset for next iteration

                //move the current pos to where the next valid initial found.
                i = p ;
            }


            return toform0;
        }


        /// <summary>
        /// Cake grids super impose remove invalid initials e.g.
        /// A B ? ? ======src
        /// ? A ? C ======
        /// =============Output for 
        /// A B ? C
        /// </summary>
        /// <param name="collection_rows"></param>
        /// <returns></returns>
        public static List<CakeRowsCollections> UpdateCakeGridColumns(List<string> collection_rows)
        {

            List<CakeRowsCollections> newCollection = new List<CakeRowsCollections>();

            string last_filled_collection = string.Empty;
            string toform0 = string.Empty;
            string toform1 = string.Empty;



            for (int loop = 0; loop < collection_rows.Count; loop++)
            {
                string last = collection_rows[loop];
                string result = string.Empty;


                for (int cnt = loop>=1?loop-1:loop; cnt < collection_rows.Count; cnt++)
                {
                    string recent = collection_rows[cnt];
                    CakeRowsCollections rc = newCollection.Find(x => x.rowid == cnt);
                    if( null != rc )
                        recent = rc.modified;


                    result = SuperImposeImageOnSource(last, recent);

                    if (-1 == result.IndexOf('?'))
                        break;
      
                    last = result;
                   
                }

                CakeRowsCollections cakerow1 = new CakeRowsCollections();
                cakerow1.rowid = loop;
                cakerow1.original = collection_rows[loop]; ;
                cakerow1.modified = result;
                newCollection.Add(cakerow1);

            }

            return newCollection;
        }

        private static List<CakeRowsCollections> UpdateCakeGridRows(List<string> collection_rows)
        {
            List<CakeRowsCollections> newCollection = new List<CakeRowsCollections>();


            string last_filled_collection = string.Empty;
            string toform0 = string.Empty;
            string toform1 = string.Empty;


            for (int cnt = 0; cnt < collection_rows.Count; cnt++)
            {
                string recent = collection_rows[cnt];
                if (-1 == recent.IndexOf('?'))
                    continue;

                string result = SuperImposeImageRows(recent);

                if (-1 == result.IndexOf('?'))
                {
                    CakeRowsCollections cakerow1 = new CakeRowsCollections();
                    cakerow1.rowid = cnt;
                    cakerow1.original = collection_rows[cnt]; ;
                    cakerow1.modified = result;
                    newCollection.Add(cakerow1);

                }

            }

            return newCollection;

        }

        static public List<string> GetStringList( List<CakeRowsCollections> ls)
        {
            return ls.Select(o => o.modified).ToList();
        }

        static public int CheckForInvalidInitial(List<string> ls)
        {
            
            return  BuildSortedNonDuplicates(ls).IndexOf('?');
            
        }

        static public void SolveProblem(TestPlan tp, string outputpath)
        {
            int case_no = 1;

            List<CakeRowsCollections> newCollection = new List<CakeRowsCollections>();
            List<string> temp_st_list = new List<string>();

            foreach (TestCase tc in tp.lsTestCases)
            {
                temp_st_list.Clear();

                temp_st_list = tc.lsRowItems;

                newCollection = UpdateCakeGridColumns(temp_st_list);



                int invalid_index = CheckForInvalidInitial(GetStringList(newCollection));

                while (-1 != CheckForInvalidInitial(GetStringList(newCollection)))
                {
                    newCollection = UpdateCakeGridRows(GetStringList(newCollection));
                }



                using (StreamWriter wr = new StreamWriter(outputpath, true))
                {
                    Console.WriteLine("Case #{0}:", case_no);

                    wr.WriteLine("Case #{0}:", case_no++);
                    foreach (CakeRowsCollections st in newCollection)
                    {
                        wr.WriteLine("{0}", st.modified);
                        //Console.WriteLine("RowID# {0}: Original#  {1}: Modified  {2}", st.rowid, st.original, st.modified);
                    }

                }


            }


            return;
        }

        static void Main(string[] args)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory;


            string small_input_path = path + @"..\..\A-small-practice.in";
            string small_output_path = path + @"..\..\A-small-practice.out";
            TestPlan tp_small = new TestPlan(small_input_path);
            SolveProblem(tp_small, small_output_path);



            string large_input_path = path + @"..\..\A-large-practice.in";
            string large_output_path = path + @"..\..\A-large-practice.out";
            TestPlan tp_large = new TestPlan(large_input_path);
            SolveProblem(tp_large, large_output_path);

  

            return;
        }


    }
}
