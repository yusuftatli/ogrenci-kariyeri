using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ContentForHomePageDTO
    {
        public string SeoUrl { get; set; }
        public string ImagePath { get; set; }
        public string Header { get; set; }
        public DateTime PublishDate { get; set; }
        public string Category { get; set; }
        public bool IsHeadLine { get; set; }
        public bool IsManset { get; set; }
        public string Writer { get; set; }
        public string WriterImage { get; set; }
        public long ReadCount { get; set; }
    }


}
