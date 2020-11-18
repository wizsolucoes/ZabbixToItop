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
                if (args.Length == 12) 
                {
                    var log = new Log("ZabbixToItop.log", args[4]);
                    Log.WriteText("-------------------- " + DateTime.Now + " --------------------");
                    var config = new ItopConfiguration(args);
                    var itop = new Itop(args);
                    string ticketJson = Utils.ObjectToJson(await itop.GenerateTicketAsync(config));
                    Log.WriteText("Save ticket response = " + await itop.SaveTicketAsync(ticketJson));
                }
                else
                {
                    throw new Exception("Informe a quantidade correta de argumentos (12)");
                }
            }
            catch (ItopException itopException)
            {
                Log.WriteText("Exception = " + itopException.ToString());
                Teams teams = new Teams(itopException, args[3]);
                teams.SendErrorAsync();
            }
            catch (Exception exception)
            {
                Log.WriteText("Exception = " + exception.ToString());
                Teams teams = new Teams(exception, args[3]);
                teams.SendErrorAsync();
            }

        }

    }
}
