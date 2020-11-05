using System.Collections.Generic;
using ZabbixToItop.Models;
using System;

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
                                Name ="Body",
                                Value = Body
                            },
                        },
                        Markdown = true
                    }
                }
            };

            var json = Utils.ObjectToJson(teamsHook);
            Console.WriteLine(json);
        }
    }
}