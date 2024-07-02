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

        // PopulateReservations:
        // This method creates a series of Reservation objects from a reservations.csv file.
        // The Flight code is used from the csv file to retrieve the corresponding Flight object.
        // The reservation's code is added to reservationCodeList to ensure subsequent reservation codes are unique.
        // It checks if the file has items on the list and then proceeds with the reservation creation.
        // Once created, the reservation is added to the reservations list.
        public void PopulateReservations()
        {
            if (File.Exists(filePath))
            {
                string fileLines = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(fileLines))
                {
                    foreach (string line in File.ReadAllLines(filePath))
                    {
                        string[] parts = line.Split(",");
                        Flight flight = FlightManager.GetFlights().Where(f => f.Code == parts[1]).FirstOrDefault();
                        Reservation reservation = new Reservation(parts[0], flight, parts[2], parts[3], parts[4]);
                        reservationCodeList.Add(parts[0]);
                        reservations.Add(reservation);
                    }
                }
            }
        }


        // GenerateResCode:
        // This method creates a reservation code that adheres to this format [A-Z][0-9]{4} -> L9999.
        // The Random class is used to generate a random letter and 4 random numbers.
        // The letter and numbers are then combined into a reservationCode string which is returned.
        public static string GenerateResCode()
        {
            Random rng = new Random();
            char rngLetter = (char)('A' + rng.Next(0, 26)); 
            int rngNumber = rng.Next(0, 10);
            int rngNumber2 = rng.Next(0, 10);
            int rngNumber3 = rng.Next(0, 10);
            int rngNumber4 = rng.Next(0, 10);

            string reservationCode = $"{rngLetter}{rngNumber}{rngNumber2}{rngNumber3}{rngNumber4}";
            return reservationCode;
        }

        // MakeReservation:
        // This method receives a Flight object, a client name and citizenship.
        // The GenerateResCode method is called to create a new Reservation code. 
        // The generated code is checked against existing Reservation codes to ensure that each one is unique.
        // An exception is thrown if the number of seats on the Flight object is less than 1.
        // An exception is thrown if the reservation already exists on the reservation list.
        // Once created the reservations list is saved to the reservations csv file via the Persist method.
        public static string MakeReservation(Flight flight, string clientName, string clientCitizenship)
        {
            string reservationCode;

            do 
            {
                reservationCode = GenerateResCode();
            }
            while (reservationCodeList.Contains(reservationCode));
            reservationCodeList.Add(reservationCode);
            string status = "Active";
            if (flight.NumOfSeats < 1)
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

        public static List<Reservation> GetReservations() 
        {
            return reservations; 
        }

        // Persist:
        // A list of type string is creaetd to contain each Reservation objects string file format.
        // The FormatForFile method is called to retrieve each Reservation's code, flight code and client's name, citizenship, and status.
        // Each reservation is then added to the csv file.
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
