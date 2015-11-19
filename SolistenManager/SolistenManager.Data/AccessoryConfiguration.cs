using SolistenManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolistenManager.Data
{
    public class AccessoryConfiguration : EntityBaseConfiguration<Accessory>
    {
        public AccessoryConfiguration()
        {
            Property(a => a.Name).IsRequired().HasMaxLength(50);
            Property(a => a.SerialNumber).IsRequired().HasMaxLength(150);
        }
    }
}
