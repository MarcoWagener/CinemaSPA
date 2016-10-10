using SolistenManager.Entities;
using SolistenManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolistenManager.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void AsClient(this Client client, ClientModel clientModel)
        {
            client.FirstName = clientModel.FirstName;
            client.LastName = clientModel.LastName;
            client.Email = clientModel.Email;
            client.Mobile = clientModel.Mobile;
        }
    }
}