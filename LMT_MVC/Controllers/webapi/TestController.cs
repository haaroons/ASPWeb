using LMT_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMT_MVC.Controllers.webapi
{
    public class TestController : ApiController
    {
        public IEnumerable<Test> Get()
        {
            List<Test> P = new List<Test>();
            for (int i = 0; i < 10; i++)
            {
                P.Add(new Test { t = "test" + i, d = "Ecommerce desc" + i });
            }

            P.Add(new Test { t = "test", d = "test description" });
            return P;
        }
    }
}