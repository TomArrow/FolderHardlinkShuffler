using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderHardlinkShuffler
{
    static class Helpers
    {

        // Fisher-Yates algorithm AKA the Knuth Shuffle
        // Found here: https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
