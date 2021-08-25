using System;
using System.Collections.Generic;

namespace Project
{
    [Serializable]
    public class Record
    {
        /// <summary>
        /// Class for records in the form
        /// </summary>
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string Type { get; set; }
        public string Heating { get; set; }
        public string Room { get; set; }
        public string Ber { get; set; }

        // Boolean list for facilities checkboxes
        private List<bool> facilitiesSettings = new List<bool>();
        

        // Parameters set for each of the records
        public Record(string hn, string st, string sz, string pr, string area, string district, string type, string heating, string room, string ber)
        {
            HouseNumber = hn;
            Street = st;
            Size = sz;
            Price = pr;
            Area = area;
            District = district;
            Type = type;
            Heating = heating;
            Room = room;
            Ber = ber;
        }


        public List<bool> FacilitiesSettings
        {
            set
            {
                facilitiesSettings = value;
            }
            get { return facilitiesSettings; }
        }        
    }
    // author: https://github.com/GaborIreHun
}
