using SCA.Entity.Enums;

namespace SCA.Entity.Model
{
    public class BasicPageList
    {
        public long Id { get; set; }

        public string SeoUrl { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public byte OrderNo { get; set; }

        public PageType TypeOfPage { get; set; }
    }
}
