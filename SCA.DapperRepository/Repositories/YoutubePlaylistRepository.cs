using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public class YoutubePlaylistRepository : GenericRepository<YoutubePlaylist>, IYoutubePlaylist<YoutubePlaylist>
    {
        public YoutubePlaylistRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
