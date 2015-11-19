using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolistenManager.Data.Infrastructure
{
    public interface IDbFactory
    {
        SolistenManagerContext Init();
    }
}
