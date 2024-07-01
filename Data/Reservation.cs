using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2OOP.Data
{
    public class Reservation 
    {
        private string code;
        private Flight resFlight;
        private string name;
        private string citizenship;
        private string status;

        public string Code { get; set; }
        public Flight ResFlight { get; set; }
        public string Name { get; set; }
        public string Citizenship { get; set; }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                switch (value.ToLower())
                {
                    case "active":
                        ResFlight.NumOfSeats -= 1;
                        status = "Active";
                        break;
                    default:
                        ResFlight.NumOfSeats += 1;
                        status = "Inactive"; 
                        break;
                }
            }
        }

        public Reservation() {}

        public Reservation(string reservationCode, Flight flight, string clientName, string clientCitizenship, string status)
        {
            this.Code = reservationCode;
            this.ResFlight = flight;
            this.Name = clientName;
            this.Citizenship = clientCitizenship;
            this.Status = status;
        }

        public string FormatForFile()
        {
            return $"{Code},{ResFlight.Code},{Name},{Citizenship},{Status}";
        }
    }
}
