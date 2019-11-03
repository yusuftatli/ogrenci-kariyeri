using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public class ImageGaleryRepository : GenericRepository<Entity.Entities.ImageGalery>, IImageGalery<Entity.Entities.ImageGalery>
    {
        public ImageGaleryRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
