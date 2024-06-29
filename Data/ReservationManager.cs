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
                foreach (string line in File.ReadAllLines(filePath))
                {
                    string[] parts = line.Split(",");
                    Reservation reservation = new Reservation();
                    reservations.Add(reservation);
                }
            }
        }
        public static string AddReservation(Flight flight, string clientName, string clientCitizenship) 
        {
            string reservationCode;

            do // ensure the reservation code is unique 
            {
                reservationCode = GenerateResCode();
            }
            while (reservationCodeList.Contains(reservationCode) == true);
            reservationCodeList.Add(reservationCode);

            flight.NumOfSeats -= 1;
            string status = "Active";
            Reservation reservation = new Reservation(reservationCode, flight, clientName, clientCitizenship, status);
            reservations.Add(reservation);
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
                string filePath = "..\\..\\..\\..\\..\\Resources\\Res\\reservations.csv";
                saveReservationsToFile.Add(res.FormatForFile());
                File.WriteAllLines(filePath, saveReservationsToFile);
            }
        }

    }
}
