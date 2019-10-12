using SCA.BLLServices.Generic;
using SCA.DapperRepository;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public class YoutubePlaylistService : GenericService<YoutubePlaylist>, IYoutubePlaylistService<YoutubePlaylist>
    {
        public YoutubePlaylistService(IYoutubePlaylist<YoutubePlaylist> repository) : base(repository)
        {
        }
    }
}
