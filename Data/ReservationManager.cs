using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class ReservationManager
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Res\\reservations.csv");
        public static List<Reservation> reservations = new List<Reservation>();

        public static List<string> reservationCodeList = new List<string>();

        public ReservationManager()
        {
            PopulateReservations();
        }

        public void PopulateReservations()
        {
            if (File.Exists(filePath))
            {
                string fileLines = File.ReadAllText(filePath);
                if (string.IsNullOrEmpty(fileLines))
                {
                    foreach (string line in File.ReadAllLines(filePath))
                    {
                        string[] parts = line.Split(",");
                        Flight flight = FlightManager.GetFlights().Where(f => f.Code == parts[1]).FirstOrDefault();
                        Reservation reservation = new Reservation(parts[0], flight, parts[2], parts[3], parts[4]);
                        reservations.Add(reservation);
                    }
                }
            }
        }
        public static string MakeReservation(Flight flight, string clientName, string clientCitizenship) 
        {
            string reservationCode;

            do // ensure the reservation code is unique 
            {
                reservationCode = GenerateResCode();
            }
            while (reservationCodeList.Contains(reservationCode));
            reservationCodeList.Add(reservationCode);
            string status = "Active";
            if (flight.NumOfSeats <= 0)
            {
                throw new InvalidOperationException("No seats available.");
            }
            else
            {
                Reservation reservation = new Reservation(reservationCode, flight, clientName, clientCitizenship, status);
                if (reservations.Contains(reservation) == false)
                {
                    reservations.Add(reservation);
                }
                else
                {
                    throw new ReservationAlreadyExisitsException();
                }

            }
            Persist();
            return reservationCode;
        }

        public static string GenerateResCode()
        {
            Random rng = new Random();
            char rngLetter = (char)('A' + rng.Next(0, 26)); // Uses ASCII character values Where 'A' starts at 63, 63 is then added to a number between 0-25 to produce a random letter
            int rngNumber = rng.Next(0, 10);
            int rngNumber2 = rng.Next(0, 10);
            int rngNumber3 = rng.Next(0, 10);
            int rngNumber4 = rng.Next(0, 10);

            string reservationCode = $"{rngLetter}{rngNumber}{rngNumber2}{rngNumber3}{rngNumber4}";
            return reservationCode;
        }

        public static List<Reservation> GetReservations() 
        {
            return reservations; 
        }

        public static void Persist()
        {
            List<string> saveReservationsToFile = new List<string>();
            foreach (Reservation res in reservations)
            {
                saveReservationsToFile.Add(res.FormatForFile());
            }
            string filePath = "..\\..\\..\\..\\..\\Resources\\Res\\reservations.csv";

            File.WriteAllLines(filePath, saveReservationsToFile);
        }

    }
}
