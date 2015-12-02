using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace LineCat.Web.Repository
{
    public class DbContextFactory
    {
        public static LineCatDb GetCurrentContext()
        {
            LineCatDb _nContext = CallContext.GetData("LineCatDbContext") as LineCatDb;
            if (_nContext == null)
            {
                _nContext = new LineCatDb();
                CallContext.SetData("LineCatDbContext", _nContext);
            }
            return _nContext;
        }
    }
}