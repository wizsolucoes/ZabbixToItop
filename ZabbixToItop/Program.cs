using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Http;
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
                    var config = new ItopConfiguration(args);
                    string ticketJson = Utils.ObjectToJson(await Itop.GenerateTicketAsync(config));
                    await Itop.SaveTicketOnItopAsync(ticketJson, args);
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
