using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class ReservationManager
    {
        public static List<Reservation> reservations = new List<Reservation>();

        public ReservationManager() { }

        public static string AddReservation(Flight flight, string clientName, string clientCitizenship) 
        {
            Random rng = new Random();
            char rngLetter = (char)('A' + rng.Next(0, 26)); // Uses ASCII character values Where 'A' starts at 63, 63 is then added to a number between 0-25 to produce a random letter
            int rngNumber = rng.Next(0, 10);
            int rngNumber2 = rng.Next(0, 10);
            int rngNumber3 = rng.Next(0, 10);
            int rngNumber4 = rng.Next(0, 10);

            string reservationCode = $"{rngLetter}{rngNumber}{rngNumber2}{rngNumber3}{rngNumber4}";
            flight.NumOfSeats -= 1;
            string status = "Active";
            Reservation reservation = new Reservation(reservationCode, flight, clientName, clientCitizenship, status);
            reservations.Add(reservation);
            return reservationCode;
        }

        public static List<Reservation> GetReservations() 
        {
            return reservations; 
        }

        // List<Reservation> reservedFlights = reservations.Where(r => r.Flight.Code == flightCode && r.ClientName == clientName && r.ClientCitizenship == clientCitizenship).ToList();

        // Implement the SaveReservation method
        public static void SaveReservation(Reservation selectedReservation)
        {
            // reservationManager.Save(selectedReservation);
        }

    }
}
