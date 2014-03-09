using System.Net.Http;
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

        public async Task Run()
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < _context.Arguments.Loop; i++)
                {
                    HttpRequestMessage request = new HttpRequestMessage(_context.Arguments.Method, _context.Arguments.GetUrl());
                    HttpResponseMessage result = await client.SendAsync(request, _context.Token);
                }
            }
        }
    }
}