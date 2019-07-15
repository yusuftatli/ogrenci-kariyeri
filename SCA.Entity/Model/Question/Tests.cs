using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    [Table("Tests")]
    public class Tests : BaseEntities
    {
        public string Url { get; set; }
        public string Header { get; set; }
        public string Topic { get; set; }
        public string PublishData { get; set; }
        public string ImagePath { get; set; }
        public string Label { get; set; }
        public int Readed { get; set; }
        public PublishState PublishState { get; set; }

        public ICollection<Questions> Questions { get; set; }
        public ICollection<TestValue> TestValue { get; set; }
        public ICollection<ReadCountOfTestAndContent> ReadCountOfTestAndContent { get; set; }
    }
}

