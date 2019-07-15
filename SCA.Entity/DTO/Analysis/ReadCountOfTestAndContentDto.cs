using SCA.Entity.Enums;
using System;

namespace SCA.Entity.DTO
{
    public class ReadCountOfTestAndContentDto
    {
        public long TestId { get; set; }
        public long UserId { get; set; }
        public ReadType ReadType { get; set; }
        public long ReadId { get; set; }
        public PlatformType PlatformType { get; set; }
        public long Count { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
