using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public class SocialMediaRepository : GenericRepository<Entity.Entities.SocialMedia>, ISocialMedia<Entity.Entities.SocialMedia>
    {
        public SocialMediaRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
