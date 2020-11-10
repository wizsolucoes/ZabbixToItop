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
        
        public Teams(Exception exception)
        {
            Message = exception.Message;
            ErrorCode = "";
            Body = exception.ToString();
        }

        public Teams(ItopException exception)
        {
            Message = exception.Message;
            ErrorCode = exception.ErrorCode.ToString();
            Body = exception.ToString();
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
                        },
                        Markdown = true
                    }
                }
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://outlook.office.com/webhook/33621ba1-f736-4a59-aeef-9a2affc3b2bb@7c416a2f-a987-4337-bb0a-94e57c1f32e7/IncomingWebhook/5d28bf49ab734814954632eda45eb568/4070e9f8-9676-4659-bfc4-2138659f8145");
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