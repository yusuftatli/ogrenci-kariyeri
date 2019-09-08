using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity
{
    public class NewsletterDto
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string EmailAddress { get; set; }
    }
}
