using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class Flight
    {
        string code;
        string name;
        string origin;
        string destination;
        string day;
        string time;
        int numOfSeats;
        double cost;



        public string Code { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public int NumOfSeats { get; set; }
        public double Cost { get; set; }

        public Flight() { }
        public Flight(string code, string name, string origin, string destination, string day, string time, int numOfSeats, double cost)
        {
            this.Code = code;
            this.Name = name;
            this.Origin = origin;
            this.Destination = destination;
            this.Day = day;
            this.Time = time;
            this.NumOfSeats = numOfSeats;
            this.Cost = cost;
        }
    }
}

