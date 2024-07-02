using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class Flight
    {
        string code;
        string airline; // Changed to airline instead of Name
        string origin;
        string destination;
        string day;
        string time;
        int numOfSeats;
        double cost;



        public string Code { get; set; }
        public string Airline { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public int NumOfSeats { get; set; }
        public double Cost { get; set; }

        public Flight() { }
        public Flight(string code, string airline, string origin, string destination, string day, string time, int numOfSeats, double cost)
        {
            this.Code = code;
            this.Airline = airline;
            this.Origin = origin;
            this.Destination = destination;
            this.Day = day;
            this.Time = time;
            this.NumOfSeats = numOfSeats;
            this.Cost = cost;
        }
    }
}

