using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class ReservationManager
    {
        //reservations Dictionary: Stores all created reservations while only allowing unique reservations to be created
        public static Dictionary<string, Reservation> reservations = new Dictionary<string, Reservation>();
        public static List<string> reservationCodeList = new List<string>();
        public static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Res\\reservations.csv");


        public ReservationManager()
        {
            PopulateReservations();
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
            while (reservationCodeList.Contains(reservationCode) == true);
            reservationCodeList.Add(reservationCode);

            string status = "Active";
            if (flight.NumOfSeats < 1)
            {
                throw new InvalidOperationException("No seats available.");
            }
            else
            {
                Reservation reservation = new Reservation(reservationCode, flight, clientName, clientCitizenship, status);
                if (reservations.Values.Contains(reservation) == false)
                {
                    reservations.Add(reservationCode, reservation);
                }
                else
                {
                    throw new ReservationAlreadyExisitsException();
                }
            }
            ReservationManager.Persist();
            return reservationCode;
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

        // Persist:
        // A list of type string is created to contain each Reservation objects string file format.
        // The Reservation objects stored in each value is added to the list.
        // The FormatForFile method is called to retrieve each Reservation's code, flight code and client's name, citizenship, and status.
        // Each reservation is then added to the csv file.
        public static void Persist()  
        {
            List<string> saveReservationsToFile = new List<string>();
            foreach (KeyValuePair<string, Reservation> kvp in reservations)
            {
                saveReservationsToFile.Add(kvp.Value.FormatForFile());
            }
            File.WriteAllLines(filePath, saveReservationsToFile); 
        }

        // PopulateReservations:
        // This method creates a series of Reservation objects from a reservations.csv file.
        // The Flight code is used from the csv file to retrieve the corresponding Flight object.
        // The reservation's code is added to reservationCodeList to ensure subsequent reservation codes are unique.
        // The reservation code and reservataion object are added to the reservation dictionary to ensure that it doesn't generate duplicate reservations.
        // A try-catch block is used to catch potential exceptions that are generated when a reservation code that already exists is added as a key.
        public static void PopulateReservations()
        {
            if (File.Exists(filePath))
            {
                string fileLines = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(fileLines))
                {
                    Reservation resPerList;
                    foreach (string line in File.ReadAllLines(filePath))
                    {
                        string[] parts = line.Split(",");
                        string resCode = parts[0];
                        string flightCode = parts[1];
                        Flight flight = FlightManager.getFlightViaCode(flightCode);
                        resPerList = new Reservation(resCode, flight, parts[2], parts[3], parts[4]);
                        try
                        {
                            reservations.Add(parts[0], resPerList);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }

        // GetReservations:
        // This method creates a list of type Reservation from the reservation Dictionary values.
        // The list of reservtions is returned.
        public static List<Reservation> GetReservations()
        {
            List<Reservation> listOfReservations = new List<Reservation>();
            foreach (KeyValuePair<string, Reservation> kvp in reservations)
            {
                listOfReservations.Add(kvp.Value);
            }
            return listOfReservations;
        }

    }
}

