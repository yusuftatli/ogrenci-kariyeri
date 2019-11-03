using SCA.BLLServices.Generic;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public class ImageGaleryService : GenericService<ImageGalery>, IImageGaleryService<ImageGalery>
    {
        public ImageGaleryService(IGenericRepository<ImageGalery> repository) : base(repository)
        {
        }
    }
}
