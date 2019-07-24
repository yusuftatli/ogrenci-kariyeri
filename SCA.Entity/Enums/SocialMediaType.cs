using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SCA.Entity.Enums
{
    public enum SocialMediaType
    {
        [Description("facebook, facebook, Beğeni")]
        Facebook = 1,

        [Description("google-plus, google-plus, Takipçi")]
        GooglePlus = 2,

        [Description("twitter, twitter, Takipçi")]
        Twitter = 3,

        [Description("pinterest, pinterest-p, Fotoğraf")]
        Pinterest = 4,

        [Description("linkedin, linkedin, Takipçi")]
        Linkedin = 5,

        [Description("youtube, youtube, Abone")]
        Youtube = 6,

        [Description("instragram, instagram, Takipçi")]
        Instagram = 7,

        [Description("dribble, dribbble, Takipçi")]
        Dribble = 8,

        [Description("behance, behance, Takipçi")]
        Behance = 9
    }
}
