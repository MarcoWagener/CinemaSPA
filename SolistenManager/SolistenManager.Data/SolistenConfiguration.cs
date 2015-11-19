using SolistenManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolistenManager.Data
{
    public class SolistenConfiguration : EntityBaseConfiguration<Solisten>
    {
        public SolistenConfiguration()
        {
            Property(s => s.Description).IsRequired().HasMaxLength(50);
            Property(s => s.Image).IsRequired().HasMaxLength(150);
            Property(s => s.SerialNumber).IsRequired().HasMaxLength(150);
            Property(s => s.Owner).IsRequired().HasMaxLength(50);
            Property(s => s.PurchaseDate).IsRequired();

            HasMany(s => s.Stocks).WithRequired().HasForeignKey(s => s.SolistenId);
            HasMany(s => s.Accessories).WithRequired().HasForeignKey(a => a.SolistenId);
        }
    }
}
