using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolistenManager.Entities
{
    public class Accessory : IEntityBase
    {
        public int ID { get; set; }
        public Guid UniqueKey { get; set; }
        public int SolistenId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}
