using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class RecentAndFavoritesContentForUIDto
    {
        public List<ContentForHomePageDTO> Recents { get; set; }
        public List<ContentForHomePageDTO> Favorites { get; set; }
    }
}
