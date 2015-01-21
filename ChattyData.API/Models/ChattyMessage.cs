using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChattyData.Models
{
    public class ChattyMessage
    {
        public override string ToString()
        {
            return string.Format("{0}-{1}", this.ActionName, this.Id);
        }
        
        private Guid _id = Guid.NewGuid();
        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        public ChattyMessage()
        {

        }

        public string ActionName { get; set; }

        public string Source { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public byte[] AttachedContent { get; set; }

        public string ContentType { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}