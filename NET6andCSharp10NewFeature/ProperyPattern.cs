using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6andCSharp10NewFeature
{
    public class PropertyPerson
    {
        public string? FirstName { get; set; }
        public int YearOfBirth { get; set; }
    }
    public class Developer : PropertyPerson
    {
        public Manager? Manager { get; set; }
    }
    public class Manager : PropertyPerson
    {
    }
}
