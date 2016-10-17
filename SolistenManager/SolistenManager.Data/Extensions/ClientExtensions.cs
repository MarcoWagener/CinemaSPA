using SolistenManager.Data.Repositories;
using SolistenManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolistenManager.Data.Extensions
{
    public static class ClientExtensions
    {
        public static string GetClientFullName(this IEntityBaseRepository<Client> clientRepository, int clientId)
        {
            var client = clientRepository.GetSingle(clientId);

            return string.Format("{0} {1}", client.FirstName, client.LastName);
        }
    }
}
