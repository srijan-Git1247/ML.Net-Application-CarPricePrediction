using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPricePrediction.Common
{
    public static class ExtensionMethods
    {
        public static string[] ToPropertyList<T>(this Type objType, string labelName) => objType.GetProperties().Where(a => a.Name != labelName).Select(a => a.Name).ToArray();
    }
}
