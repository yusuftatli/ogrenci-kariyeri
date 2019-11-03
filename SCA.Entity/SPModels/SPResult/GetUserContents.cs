using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.SPModels.SPResult
{
    public class GetUserContents
    {
        public long Id { get; set; }

        public int ReadCount { get; set; }

        public string ImagePath { get; set; }

        public string Header { get; set; }

        public string Writer { get; set; }

        public DateTime PublishDate { get; set; }

        public string CategoryName { get; set; }
    }
}
