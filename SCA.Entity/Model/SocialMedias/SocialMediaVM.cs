using SCA.Entity.Enums;

namespace SCA.Entity.Model.SocialMedias
{
    public class SocialMediaVM
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long CompanyClupId { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        public SocialMediaType SocialMediaType { get; set; }
    }
}
