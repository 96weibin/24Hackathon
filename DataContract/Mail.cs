using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledgeBase.DataContract
{
    public class Mail
    {
        public string FromPerson { get; set; }
        public List<string> ReceiptArray { get; set; }
        public List<string> MailCcArray { get; set; }
        public string Title { get; set; }
        public string MailBody { get; set; }
        public string FilePath { get; set; }
        public string Password { get; set; }
        public bool IsBodyHtml { get; set; }

    }
}