using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20
{
    class Program
    {
        static void Main(string[] args)
        {
            FindFirstDuplicate duplicate=new FindFirstDuplicate();
            int[] array =new int[10]{9,8,7,6,2,4,3,2,1,5};
            Console.WriteLine(duplicate.Meth1(array));
            Console.WriteLine(duplicate.Meth2(array));
            Console.WriteLine(duplicate.Meth3(array));
            Console.WriteLine(duplicate.Meth4(array));
        }
    }

    class FindFirstDuplicate
    {
        /*
         * structure:int Array
         * find if n is duplicate.         
         * Num of elements: N
         * Constraints 0<n<N  : 
         */

        /// <summary>
        /// Adhoc Scan Each element 
        /// T: n+ (n-1) + (n-2) + .....1 ==>(n*n-n)/2
        /// </summary>
        /// <returns></returns>
       public int  Meth1(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                    if (array[i] == array[j])
                        return array[i];
            }
            return -1;
        }
        /// <summary>
        /// Sort and scan
        /// T:nlogn Comparisions + n Comparisions
        /// S:0
        /// </summary>
        /// <returns></returns>
       public int Meth2(int[] array)
        {
            Array.Sort(array);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == array[i + 1])
                    return array[i];
            }
            return -1;
        }
        /// <summary>
        /// using data structure
        /// T:n
        /// S:n
        /// </summary>
        /// <returns></returns>
       public int Meth3(int[] array)
        {
            HashSet<int> buff=new HashSet<int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (buff.Contains(array[i]))
                    return array[i];
                else
                    buff.Add(array[i]);
            }
            return -1;
        }

        /// <summary>
        /// Inplace Negate
        /// </summary>
        /// <returns></returns>
       public int Meth4(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[Math.Abs(array[i])] <0)
                    return Math.Abs(array[i]);
                else
                    array[Math.Abs(array[i])] = -1 * array[Math.Abs(array[i])];
            }
            return -1;
        }
    }

    class FindElementInSortedArray
    {
        /// <summary>
        /// Adhoc Linear search
        /// TC:O(m*n)
        /// SC:O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool FindElement1(int[,] arr, int target)
        {
            int rws = arr.GetLength(0);//Num of rows
            int cols = arr.GetLength(1);//Num of cols

            for (int i = 0; i < rws-1; i++)
            {
                for (int j = 0; j < cols-1; j++)
                {
                    if (arr[i, j] == target)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Linear search on rows. once found the row then binary search on cols
        /// TC:m + mlogm=> O(nlogn)
        /// SC:O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool FindElement2(int[,] arr, int target)
        {
            int m = arr.GetLength(0);//Num of rows
            int n = arr.GetLength(1);//Num of cols
            int row = -1;

            for (int i = 1; i < m; i++)
                if (arr[i - 1, 0] > target && arr[i, 0] < target)
                    row = i - 1;

            int min = 0;
            int max = n;

            while (min<max)
            {
                int mid =(min+ max) / 2;
                if (arr[row,mid]>target)
                    max = mid-1;
                else if (arr[row, mid]< target)
                    min = mid + 1;
                else
                    return true;
            }
            return false;
        }


        public static bool FindElement3(int[,] arr, int target)
        {
            int m = arr.GetLength(0);
            int n=arr.GetLength(1);

            for (int i = 0; i < UPPER; i++)
            {
                
            }

            return false;
        }
    }
}
