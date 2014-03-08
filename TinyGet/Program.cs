using System;
using System.Collections.Specialized;
using System.Threading;
using TinyGet.Config;
using TinyGet.Requests;

namespace TinyGet
{
    class Program
    {
        private static readonly CancellationTokenSource Cancellation = new CancellationTokenSource();

        private static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eventArgs) => Cancellation.Cancel();

            if (0 == args.Length)
            {
                Console.WriteLine(CommandLineArguments.HelpMessage);
            }
            else
            {
                try
                {
                    NameValueCollection settings = args.ToNameValueCollection();
                    AppArguments arguments = AppArguments.Parse(settings);

                    Context context = new Context(arguments, Cancellation.Token, Console.Out);
                    AppHost host = new AppHost(context, new RequestSenderCreator());
                    host.Run();

                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
