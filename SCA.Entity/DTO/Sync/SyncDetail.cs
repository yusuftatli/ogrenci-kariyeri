using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class SyncDetail
    {
        public string ID { get; set; }
        public string post_title { get; set; }
        public string post_content { get; set; }
        public string post_author { get; set; }
        public string post_date { get; set; }
        public string appKat { get; set; }
        public string appEtkinlik { get; set; }
        public string appStaj { get; set; }
        public string guid { get; set; }
        public string yazar { get; set; }
        public string foto { get; set; }
        public string app { get; set; }
        public bool favori { get; set; }
        public List<kategorilerDto> kategoriler { get; set; }


    }
}
