using SCA.BLLServices.Generic;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public class SocialMediaService : GenericService<Entity.Entities.SocialMedia>, ISocialMediaService<Entity.Entities.SocialMedia>
    {
        public SocialMediaService(IGenericRepository<SocialMedia> repository) : base(repository)
        {
        }
    }
}
