using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ContentDetailForDetailPageDTO
    {
        public long Id { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }
        public string Header { get; set; }
        public string Writer { get; set; }
        public string WriterImagePath { get; set; }
        public string WriterBiography { get; set; }
        public GenderType GenderId { get; set; }
        public DateTime PublishDate { get; set; }
        public int ReadCount { get; set; }
        public string Tags { get; set; }
        public bool IsFavoriteContent { get; set; }
        public string ContentDescription { get; set; }
        public List<ContentForHomePageDTO> MostPopularItems { get; set; }
        public List<CommentForUIDto> CommentList{ get; set; }
    }
}
