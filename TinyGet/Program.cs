using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyGet.Config;

namespace TinyGet
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = args.ToNameValueCollection();
        }
    }
}
