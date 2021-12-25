using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6andCSharp10NewFeature
{
    public class Example : Attribute
    {
        public int Value { get; set; }
        public Example(int value)
        {
            Value = value;
        }
    }
}
