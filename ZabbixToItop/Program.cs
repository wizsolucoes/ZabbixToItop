using System.Net.Http;
using ZabbixToItop.Services;
using ZabbixToItop.Models;
using System.Threading.Tasks;
using System;

namespace ZabbixToItop
{
    class Program
    {
        public static HttpClient client { get; set; }
        public Program()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            client = new HttpClient(httpClientHandler);
        }

        public Program(HttpClient clientMock)
        {
            client = clientMock;
        }

        public static async Task Main(string[] args)
        {
            try
            {
                if (args.Length >= 7)
                {
                    Itop itopApi = new Itop();
                    ItopConfiguration config = new ItopConfiguration(args);
                    string ticketJson = Utils.ObjectToJson(itopApi.GenerateTicket(config));
                    await itopApi.SaveTicketOnItopAsync(ticketJson, client);
                }
                else
                {
                    throw new Exception("Informe a quantidade minima correta de argumentos (7)");
                }
            }
            catch (ItopException itopException)
            {
                Teams teams = new Teams(itopException);
                await teams.SendErrorAsync();
            }
            catch (Exception exception)
            {
                Teams teams = new Teams(exception);
                await teams.SendErrorAsync();
            }

        }

    }
}
