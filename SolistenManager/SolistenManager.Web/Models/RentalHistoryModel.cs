using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolistenManager.Web.Models
{
    public class RentalHistoryModel
    {
        public int ID { get; set; }
        public int StockId { get; set; }
        public string Client { get; set; }
        public string Status { get; set; }
        public DateTime RentalDate { get; set; }
        public Nullable<DateTime> ReturnedDate { get; set; }
    }
}