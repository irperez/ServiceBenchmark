using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.WebHost.Endpoints;
using ServiceBenchmark.Common;

namespace ServiceBenchmark.SvcStack
{
    #region Item Service

    public class ItemService : ServiceBase<ItemRequest>
    {
        protected override object Run(ItemRequest request)
        {
            // simulate call to slow service
            var s = new WebClient().DownloadString("http://localhost:21057/api/slow");

            return new ItemResponse
            {
                Item = new Item(request.ItemID, s)
            };
        }
    }

    #endregion
}
