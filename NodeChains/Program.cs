using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeChains
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(new SinglyLinkedListNode<int>(5));
            list.AddFirst(new SinglyLinkedListNode<int>(3));

            while (list.Head!=null)
            {
                Console.WriteLine();
            }

            //Node first = new Node {Value = 3};

            //Node middle = new Node {Value = 5};

            //first.Next = middle;

            //Node last = new Node {Value = 7};

            //middle.Next = last;

        }
    }

    class  FirstNonDuplicate
    {
        /*
         * input:a b c d
         * Output:a
         * 
         * input:a b c d a
         * output: b
         * 
         * Input: a b c d a d
         * Output: b
         * 
         * Input:a b c d d b a
         * Output: c
         * 
        
        */
        
        private List<char> inpuList;

        //TO imporve Performance Hashtable canbe used below
        private List<char> nonDupes;
        void Run()
        {
             inpuList=new List<char>();
             nonDupes=new List<char>();
            
           
            bool stop = false;
            while (!stop)
            {
                var inp = Console.ReadLine().ToCharArray()[0];
                if (inp == '0')
                    stop = true;
                updateList(inp);
                printLists();
            }
        }

        private void printLists()
        {
            Console.Write("I:");
            foreach (char c in inpuList)
                Console.Write(c + " ");
            if(nonDupes.Count>0)
             Console.WriteLine("\nO:" + nonDupes[0]);
            else
                Console.WriteLine("\n No non dupes");
            //Console.WriteLine("\nfirst NonDuplicate:\n{0}\n",nonDupes[0]);
        }

        private void updateList(char inp)
        {
            if (!inpuList.Contains(inp))
                nonDupes.Add(inp);
            else
                nonDupes.Remove(inp);
            inpuList.Add(inp);
        }

        public static void Main()
        {
            FirstNonDuplicate t = new FirstNonDuplicate();
            t.Run();

        }
        
    }

    class FirstOccuranceOfWords
    {
        /*
            Input: 
            targetList = {"cat", "dog"}; 
            availableTagsList = { "cat", "test", "dog", "get", "spain", "south" }; 

            Output: [0, 2] //'cat' in position 0; 'dog' in position 2 

            Input: 

            targetList = {"east", "in", "south"}; 
            availableTagsList = { "east", "test", "east", "in", "east", "get", "spain", "south" }; 

            Output: [2, 6] //'east' in position 2; 'in' in position 3; 'south' in position 6 (east in position 4 is not outputted as it is coming after 'in') 

            Input: 

            targetList = {"east", "in", "south"}; 
            availableTagsList = { "east", "test", "south" }; 

            Output: [0] //'in' not present in availableTagsList 
         */
        private List<string> targetList;
        private List<string> TagsList;
        private Dictionary<int, int> totalCombinations;
        void Run()
        {
            int strInd = 0;
            int SrcPtr = 0;
           
            while (strInd!=TagsList.Count())
            {
                
                for (int i = strInd; i < TagsList.Count(); i++)
                {
                    if (TagsList[strInd]==targetList[SrcPtr])
                    {
                        if (strInd == 0)
                            totalCombinations.Add(SrcPtr,-1);
                        //if(strInd==TagsList.Count-1)
                        //    totalCombinations.Add();
                        SrcPtr++;
                    }
                }
            }
            
        }

    }


}
