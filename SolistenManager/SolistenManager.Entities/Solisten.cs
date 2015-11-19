using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolistenManager.Entities
{
    public class Solisten : IEntityBase
    {
        public Solisten()
        {
            Stocks = new List<Stock>();
            Accessories = new List<Accessory>();
        }

        public int ID { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string SerialNumber { get; set; }
        public string Owner { get; set; }
        public DateTime PurchaseDate { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Accessory> Accessories { get; set; }
    }
}
