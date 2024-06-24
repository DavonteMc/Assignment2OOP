using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class Airport
    {
        string code;
        string name;

        public string Code { get; set; }
        public string Name { get; set; }

        public Airport() { }
        public Airport(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
