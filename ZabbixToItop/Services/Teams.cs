using System.Net.Http;
using System.Collections.Generic;
using ZabbixToItop.Models;
using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ZabbixToItop.Services
{
    public class Teams
    {
        private string Message { get; set; }
        private string ErrorCode { get; set; }
        private string Body { get; set; }
        private string TeamsUrl { get; set; }
        
        public Teams(Exception exception, string teamsUrl)
        {
            Message = exception.Message;
            ErrorCode = "";
            Body = exception.ToString();
            TeamsUrl = teamsUrl;
        }

        public Teams(ItopException exception, string teamsUrl )
        {
            Message = exception.Message;
            ErrorCode = exception.ErrorCode.ToString();
            Body = exception.ToString();
            TeamsUrl = teamsUrl;
        }
        public void SendError()
        {
            Log.WriteText("Exception = " + Body);
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
                        },
                        Markdown = true
                    }
                }
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(TeamsUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = Utils.ObjectToJson(teamsHook);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

        }
    }
}