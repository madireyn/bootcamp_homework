using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, string> dict = new ArrayDictionary<string, string>();
            dict.Add("keyA", "valA");
            dict.Add("keyB", "valB");
            dict.Add("keyC", "valC");

            System.Diagnostics.Debug.WriteLine("initial: " + dict.Count);


            dict.RemoveKeyValuePair("keyC");
            dict.Remove("keyA");

            System.Diagnostics.Debug.WriteLine("final: " + dict.Count);

            foreach (var str in dict)
            {
                System.Diagnostics.Debug.WriteLine(str);
            }
        }
    }
}
