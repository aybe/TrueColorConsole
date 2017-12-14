using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class2
    {
        public Class2()
        {

            Dictionary<Color, string> dictionary = new Dictionary<Color, string>();

            for (uint u = uint.MinValue; u < uint.MaxValue; u++)
            {
                var color = Color.FromArgb(unchecked ((int)u));
                 dictionary.Add(color,"");
            }
        }
    }
}
