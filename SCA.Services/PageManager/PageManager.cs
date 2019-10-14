using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class PageManager : IPageManager
    {
        public PageManager()
        {
        }

        public async Task<ServiceResult> GetAllBasicPages()
        {
            List<BasicPagesDto> listData = new List<BasicPagesDto>();
            await Task.Run(() =>
            {
               // listData = _mapper.Map<List<BasicPagesDto>>(_basicPageRepo.GetAll());
                //listData.ForEach(x =>
                //{
                //    x.ActiveDescription = x.IsActive ? "Aktif" : "Pasif";
                //});
            });
            return Result.ReturnAsSuccess(data: listData);
        }

        public async Task<List<BasicPagesDto>> GetAllBasicPageForUI()
        {
            List<BasicPagesDto> listData = new List<BasicPagesDto>();
            //await Task.Run(() =>
            //{
            //    listData = _mapper.Map<List<BasicPagesDto>>(_basicPageRepo.GetAll());
            //});
            return listData;
        }

        public async Task<ServiceResult> CreateBasicPage(BasicPagesDto dto)
        {
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail(message: "model null olamaz");
            }
            string resultMessage = "";
            //await Task.Run(() =>
            //{
            //    if (dto.Id == 0)
            //    {
            //        var data = _mapper.Map<BasicPages>(dto);
            //        _basicPageRepo.Add(data);
            //        _unitOfWork.SaveChanges();
            //        resultMessage = "Kayıt işlemi başarılı";
            //    }
            //    else
            //    {
            //        var data = _mapper.Map<BasicPages>(dto);
            //        _basicPageRepo.Update(data);
            //        _unitOfWork.SaveChanges();
            //        resultMessage = "Güncelleme işlemi başarılı";
            //    }
            //});
            return Result.ReturnAsSuccess(message: resultMessage);
        }

        public async Task<ServiceResult> UpdateState(long id, bool state)
        {
            if (id == 0)
            {
                return Result.ReturnAsFail(message: "id değeri null olamaz");
            }
            await Task.Run(() =>
            {
                //var data = _basicPageRepo.Get(x => x.Id == id);
                //data.IsActive = state;
                //_unitOfWork.SaveChanges();
            });
            return Result.ReturnAsSuccess(message: "Ekran aktif/Pasif durumu güncellendi.");
        }
    }
}
