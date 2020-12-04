using System.IO;
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
                if (args.Length == 8) 
                {
                    Log.LogPath = Path.Combine(args[4], "ZabbixToItop.log");
                    Log.WriteText("-------------------- " + DateTime.Now + " --------------------");
                    Log.WriteText("arg1 = " + args[5]);
                    Log.WriteText("arg2 = " + args[6]);
                    Log.WriteText("arg3 = " + args[7]);
                    Log.WriteText("args = " + String.Join(" ", args));

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
