using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class TestsDto
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Header { get; set; }
        public string Topic { get; set; }
        public string PublishData { get; set; }
        public string ImagePath { get; set; }
        public string Label { get; set; }
        public int Readed { get; set; }
        public PublishState PublishState { get; set; }
    }
}
