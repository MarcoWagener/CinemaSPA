using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolistenManager.Web.Models
{
    public class RentalModel
    {
        public int ID { get; set; }
        public int ClientId { get; set; }
        public int StockId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public string Status { get; set; }
    }
}