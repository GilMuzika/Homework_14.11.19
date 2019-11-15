using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _12._11._19_MemoryGame_Itay
{
    static class StaticStaff
    {



        public static void Shuffle_thisProject<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IEnumerable<T> findDuplicates_thisProject<T>(this List<T> list)
        {
            /*var hash = new HashSet<T>();
            var duplicates = list.Where(i => !hash.Add(i));

            return duplicates;*/

            var duplicateKeys = list.GroupBy(x => x)
                        .Where(group => group.Count() > 1)
                        .Select(group => group.Key);

            return duplicateKeys;
        }

        public static char[] numbersToChars_thisProject(this int[] numArray)
        {
            char[] charAttay = new char[numArray.Length];
            for (int i = 33; i < 33 + numArray.Length; i++)
            {
                charAttay[i-33] = (char)i;
            }

            return charAttay;
        }




        public static List<Label_field> allWhichWasOpened = new List<Label_field>();

        public static int indexOfTheSameIdentityFiledsList = 0;

        public static bool isVirtPlayerGuessedRight = false;

        public static bool keepRandomlyDeterminedIndex = false;

        public static int difficultyLevelIndex = 3;

        public static string charsetSelectedItem = "Numbers";

        public static Label_field keep;


    }
}
