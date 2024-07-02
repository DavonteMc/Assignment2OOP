using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class FlightManager
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Res\\flights.csv");
        public static List<Flight> flights = new List<Flight>();

        public FlightManager()
        {
            PopulateFlights();
        }

        public void PopulateFlights()
        {
            Flight flight;
            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(",");
                flight = new Flight(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], int.Parse(parts[6]), double.Parse(parts[7]));
                flights.Add(flight);
            }
        }

        public static List<Flight> GetFlights() 
        { 
            return flights; 
        }

        public static Flight getFlightViaCode(string code)
        {
            return flights.FirstOrDefault(p => p.Code == code);
        }
    }
}
