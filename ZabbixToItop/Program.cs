using ZabbixToItop.Models;
using ZabbixToItop.Services;
using System;
using System.Threading.Tasks;

namespace ZabbixToItop
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                if (args.Length == 10)
                {
                    Itop itopApi = new Itop();
                    ItopConfiguration config = new ItopConfiguration(args);
                    string ticketJson = itopApi.GenerateTicket(config);
                    Console.WriteLine(await itopApi.SaveTicketOnItopAsync(ticketJson));
                }
                else
                {
                    Console.WriteLine("Informe a quantidade correta de argumentos (10)");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
