using ZabbixToItop.Services;
using ZabbixToItop.Settings;
using ZabbixToItop.Exceptions;
using System.Threading.Tasks;
using System;
using ZabbixToItop.Util;

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
                    var log = new Log("ZabbixToItop.log", args);
                    var settings = new RequestSettings(args);
                    var itop = new Itop(args);
                    string ticketJson = Helper.ObjectToJson(await itop.GenerateTicketAsync(settings));
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
