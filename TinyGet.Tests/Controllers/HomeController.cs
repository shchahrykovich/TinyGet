﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TinyGet.Tests.Controllers
{
    public class HomeController : ApiController
    {
        public String Get()
        {
            return "Home controller";
        }
    }
}
