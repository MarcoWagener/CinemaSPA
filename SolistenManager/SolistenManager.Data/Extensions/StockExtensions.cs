using SolistenManager.Data.Repositories;
using SolistenManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolistenManager.Data.Extensions
{
    public static class StockExtensions
    {
        public static IEnumerable<Stock> GetAvailableItems(this IEntityBaseRepository<Stock> stockRepository, int solistenId)
        {
            return stockRepository
                    .GetAll()
                    .Where(s => s.SolistenId == solistenId)
                    .ToList();
        }
    }
}
