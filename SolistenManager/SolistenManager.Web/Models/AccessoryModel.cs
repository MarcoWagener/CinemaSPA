using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolistenManager.Web.Models
{
    public class AccessoryModel
    {
        public int ID { get; set; }
        public Guid UniqueKey { get; set; }
        public int SolistenId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}