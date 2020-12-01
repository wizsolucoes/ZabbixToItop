using ZabbixToItop.Services;
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
                if (args.Length == 10) 
                {
                    var log = new Log("ZabbixToItop.log", args);
                    var itopService = new ItopService(args);
                    var response = await itopService.SaveTicketAsync();
                    Log.WriteText("Save ticket response = " + response);
                }
                else
                {
                    throw new Exception("Informe a quantidade correta de argumentos (12)");
                }
            }
            catch (ItopException itopException)
            {
                Log.WriteText("Exception = " + itopException.ToString());
                var teamsService = new TeamsService(itopException, args[3]);
                teamsService.SendError();
            }
            catch (Exception exception)
            {
                Log.WriteText("Exception = " + exception.ToString());
                var teamsService = new TeamsService(exception, args[3]);
                teamsService.SendError();
            }

        }

    }
}
