using System;

namespace AEP_WebApi.Models
{
    public class InfoMessage
    {
        public DateTime Data { get; set; }
        public string Message { get; set; }

        public InfoMessage(DateTime Data, String Message)
        {
            this.Data = Data;
            this.Message = Message;
        }
    }
}