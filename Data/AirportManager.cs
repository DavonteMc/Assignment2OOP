using Assignment2OOP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class AirportManager
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Res\\airports.csv");
        public static List<Airport> airports = new List<Airport>();

        public AirportManager()
        {
            PopulateAirports();
        }

        public void PopulateAirports()
        {
            Airport airport;
            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(",");
                airport = new Airport(parts[0], parts[1]);
                airports.Add(airport);
            }
        }

        public static List<Airport> GetAirports() { return airports; }
    }
}
