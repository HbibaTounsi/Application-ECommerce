using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Not_Mapped_Entities
{
    public class EmailSetting
    {
        public string SmptpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public bool EnableSsl { get; set; }

    }
}
