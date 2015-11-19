using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolistenManager.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        SolistenManagerContext dbContext;

        public SolistenManagerContext Init()
        {
            return dbContext ?? (dbContext = new SolistenManagerContext());
        }

        protected override void DisposeCore()
        {
            if(dbContext != null)
                dbContext.Dispose();
        }
    }
}
