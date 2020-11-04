using ZabbixToItop.Services;
using ZabbixToItop.Models;
using System.Threading.Tasks;
using System;

namespace ZabbixToItop
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                if (args.Length >= 7)
                {
                    Itop itopApi = new Itop();
                    ItopConfiguration config = new ItopConfiguration(args);
                    string ticketJson = itopApi.GenerateTicket(config);
                    Console.WriteLine(await itopApi.SaveTicketOnItopAsync(ticketJson));
                }
                else
                {
                    throw new Exception("Informe a quantidade minima correta de argumentos (7)");
                }
            }
            catch(ItopException itopException)
            {
                Teams teams = new Teams(itopException);
                teams.SendError();
            }
            catch (Exception exception)
            {
                Teams teams = new Teams(exception);
                teams.SendError();
            }
            
        }

    }
}
