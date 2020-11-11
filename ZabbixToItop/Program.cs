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
                    var log = new Log("ZabbixToItop.log");
                    Log.WriteText("-------------------- " + DateTime.Now + " --------------------");
                    var config = new ItopConfiguration(args);
                    var itop = new Itop(args);
                    string ticketJson = Utils.ObjectToJson(await itop.GenerateTicketAsync(config));
                    await itop.SaveTicketAsync(ticketJson);
                }
                else
                {
                    throw new Exception("Informe a quantidade minima correta de argumentos (7)");
                }
            }
            catch (ItopException itopException)
            {
                Teams teams = new Teams(itopException, args[3]);
                teams.SendError();
            }
            catch (Exception exception)
            {
                Teams teams = new Teams(exception, args[3]);
                teams.SendError();
            }

        }

    }
}
