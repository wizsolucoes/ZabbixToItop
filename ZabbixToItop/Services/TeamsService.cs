using System.Text;
using System.Collections.Generic;
using ZabbixToItop.Models;
using ZabbixToItop.Exceptions;
using System;
using System.Net.Http;
using ZabbixToItop.Util;

namespace ZabbixToItop.Services
{
    public class TeamsService
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string Body { get; set; }
        public string TeamsUrl { get; set; }
        private HttpClient Client { get; set; }

        public TeamsService(Exception exception, string teamsUrl)
        {
            Message = exception.Message;
            ErrorCode = "";
            Body = exception.ToString();
            TeamsUrl = teamsUrl;

            Client = new HttpClient();
        }

        public TeamsService(ItopException exception, string teamsUrl)
        {
            Message = exception.Message;
            ErrorCode = exception.ErrorCode.ToString();
            Body = exception.ToString();
            TeamsUrl = teamsUrl;

            Client = new HttpClient();
        }


        public TeamsService(Exception exception, string teamsUrl, HttpClient client)
        {
            Message = exception.Message;
            ErrorCode = "";
            Body = exception.ToString();
            TeamsUrl = teamsUrl;

            Client = client;
        }

        public TeamsService(ItopException exception, string teamsUrl, HttpClient client)
        {
            Message = exception.Message;
            ErrorCode = exception.ErrorCode.ToString();
            Body = exception.ToString();
            TeamsUrl = teamsUrl;

            Client = client;
        }

        public void SendError()
        {
            TeamsHook teamsHook = new TeamsHook
            {
                Summary = "Erro ao salvar Ticket do Zabbix no Itop",
                ThemeColor = "9E1414",
                Sections = new List<Section> {
                    new Section
                    {
                        ActivityTitle = Message,
                        ActivitySubtitle = ErrorCode,
                        Facts = new List<Fact>{
                            new Fact
                            {
                                Name = "mensagem",
                                Value = Message
                            },
                            new Fact
                            {
                                Name = "Quando",
                                Value = DateTime.Now.ToString("MM/dd/yyyy h:mm tt")
                            },
                            new Fact
                            {
                                Name = "Status",
                                Value = ErrorCode
                            },
                            new Fact
                            {
                                Name = "Origem",
                                Value = "Zabbix"
                            },
                            new Fact
                            {
                                Name = "Body",
                                Value = Body
                            },
                            new Fact
                            {
                                Name = "Log",
                                Value = Log.ReadText()
                            },
                        },
                        Markdown = true
                    }
                }
            };

            /* var json = Helper.ObjectToJson(teamsHook);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = Client.PostAsync(TeamsUrl, content).Result;

            Log.WriteText(result.Content.ReadAsStringAsync().Result); */
        }
    }
}