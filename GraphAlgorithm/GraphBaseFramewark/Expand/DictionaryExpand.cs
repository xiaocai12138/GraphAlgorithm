using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphBaseFramewark
{
    public static class DictionaryExpand
    {
        public static void Put<keyT, valueT>(this Dictionary<keyT, valueT> dic, keyT key, valueT value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
    }
}
