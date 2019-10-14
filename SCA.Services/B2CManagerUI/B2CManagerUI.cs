using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class B2CManagerUI : IB2CManagerUI
    {

        public B2CManagerUI()
        {
        }

        public async Task<ContentDetailForDetailPageDTO> GetContentDetailForDetailPage(string seoUrl)
        {
            ContentDetailForDetailPageDTO _res = new ContentDetailForDetailPageDTO();
            await Task.Run(() =>
            {

                //var contentDetail = _mapper.Map<ContentDetailForDetailPageDTO>(_contentRepo.GetAll(x=>x.SeoUrl == seoUrl).FirstOrDefault());
                //contentDetail.MostPopularItems = _mapper.Map<List<ContentForHomePageDTO>>(_contentRepo.GetAll(x => x.Category.ToString() == contentDetail.Category).OrderByDescending(x => x.ReadCount).Take(8).ToList());


            });
            return _res;
        }

        public async Task<List<ContentForHomePageDTO>> GetContentsForHomePage()
        {
            List<ContentForHomePageDTO> _res = new List<ContentForHomePageDTO>();
            await Task.Run(() =>
            {
                //var contents = _mapper.Map<List<ContentForHomePageDTO>>(_contentRepo.GetAll(x => x.IsDeleted.Equals(false)).ToList());
            });
            return _res;
        }

    }
}
