using System;
using System.Collections.Generic;
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
                if ((status == "active") && (value.ToLower() == "active"))
                {
                    status = "active";
                }
                else if ((status == "inactive") && (value.ToLower() == "inactive"))
                {
                    status = "inactive";
                }
                else
                {
                    if (value.ToLower() == "active")
                    {
                        ResFlight.NumOfSeats -= 1;
                        status = "active";
                    }
                    else
                    {
                        ResFlight.NumOfSeats += 1;
                        status = "inactive";
                    }
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
