using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ContenUIDto
    {
        public string Tags { get; set; }
        public long ReadCount { get; set; }
        public string ImagePath { get; set; }
        public string SeoUrl { get; set; }
        public string Header { get; set; }
        public string Writer { get; set; }
        public string ContentDescription { get; set; }
        public DateTime PublishDate { get; set; }
        public long CreatedUserId { get; set; }
        public string WriterName { get; set; }
        public string WriterImagePath { get; set; }
    }
}
