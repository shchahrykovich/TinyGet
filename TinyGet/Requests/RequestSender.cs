using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyGet.Requests
{
    internal class RequestSender : IRequestSender
    {
        private readonly Context _context;

        public RequestSender(Context context)
        {
            _context = context;
        }

        public Task Run()
        {
            return Task.Run(() => { }, _context.Token);
        }
    }
}
