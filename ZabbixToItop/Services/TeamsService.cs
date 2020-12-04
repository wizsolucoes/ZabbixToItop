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
            this.Message = exception.Message;
            this.ErrorCode = "";
            this.Body = exception.ToString();
            this.TeamsUrl = teamsUrl;

            this.Client = new HttpClient();
        }

        public TeamsService(ItopException exception, string teamsUrl)
        {
            this.Message = exception.Message;
            this.ErrorCode = exception.ErrorCode.ToString();
            this.Body = exception.ToString();
            this.TeamsUrl = teamsUrl;

            this.Client = new HttpClient();
        }


        public TeamsService(Exception exception, string teamsUrl, HttpClient client)
        {
            this.Message = exception.Message;
            this.ErrorCode = "";
            this.Body = exception.ToString();
            this.TeamsUrl = teamsUrl;

            this.Client = client;
        }

        public TeamsService(ItopException exception, string teamsUrl, HttpClient client)
        {
            this.Message = exception.Message;
            this.ErrorCode = exception.ErrorCode.ToString();
            this.Body = exception.ToString();
            this.TeamsUrl = teamsUrl;

            this.Client = client;
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
                        ActivityTitle = this.Message,
                        ActivitySubtitle = this.ErrorCode,
                        Facts = new List<Fact>{
                            new Fact
                            {
                                Name = "mensagem",
                                Value = this.Message
                            },
                            new Fact
                            {
                                Name = "Quando",
                                Value = DateTime.Now.ToString("MM/dd/yyyy h:mm tt")
                            },
                            new Fact
                            {
                                Name = "Status",
                                Value = this.ErrorCode
                            },
                            new Fact
                            {
                                Name = "Origem",
                                Value = "Zabbix"
                            },
                            new Fact
                            {
                                Name = "Body",
                                Value = this.Body
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

            var json = Helper.ObjectToJson(teamsHook);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = Client.PostAsync(TeamsUrl, content).Result;

            Log.WriteText(result.Content.ReadAsStringAsync().Result);
        }
    }
}