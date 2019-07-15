using SCA.Common.Result;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface
{
    public interface ICategoryManager
    {


        /// <summary>
        /// Kategorileri listeler
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> MainCategoryList(long? id);

        /// <summary>
        /// Kategorileri listeler, parenttan bağımsız tüm kategoriler
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> MainCategoryListWithParents();

        /// <summary>
        /// Kategori ekler
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns> 
        Task<ServiceResult> MainCategoryCreate(MainCategoryDto dto);

        /// <summary>
        /// kategori bilgileri güncellenir
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServiceResult> MainCategoryUpdate(MainCategoryDto dto);

        /// <summary>
        /// Kategori siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult> MainCategoryDelete(long id);

        /// <summary>
        /// Kategori durumunu aktif pasif yapar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<ServiceResult> MainCategoryStatusUpdate(int id, bool state);
        Task<ServiceResult> CreateCategoryRelation(List<CategoryRelationDto> listData);
        List<CategoryRelationDto> GetCategoryRelation(string data, long Id, ReadType readType);

    }
}
