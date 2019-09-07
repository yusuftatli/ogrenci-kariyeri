using AutoMapper;
using MySql.Data.MySqlClient;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Category> _categoryRepo;
        private IGenericRepository<CategoryRelation> _categoryRelationRepo;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");


        public CategoryManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepo = unitOfWork.GetRepository<Category>();
            _categoryRelationRepo = unitOfWork.GetRepository<CategoryRelation>();
        }
        #region MainCategory
        /// <summary>
        /// Kategorileri listeler, parent'a göre filtrelenmiş
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryList(long? id)
        {
            return Result.ReturnAsSuccess(null, null, _mapper.Map<List<MainCategoryDto>>(_categoryRepo.GetAll(x => x.IsDeleted.Equals(false) && x.ParentId.Equals(id)).ToList()));
        }
        /// <summary>
        /// Kategorileri listeler, parenttan bağımsız tüm kategoriler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryListWithParents()
        {
            return Result.ReturnAsSuccess(null, null, _mapper.Map<List<MainCategoryDto>>(_categoryRepo.GetAll(x => x.IsDeleted.Equals(false))));
        }
        /// <summary>
        /// Kategori ekler
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryCreate(MainCategoryDto dto)
        {
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            string query = GetCategoryQuery(CrudType.Insert, null, null);


            return null;
        }
        /// <summary>
        /// kategori bilgileri güncellenir
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryUpdate(MainCategoryDto dto)
        {
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            _categoryRepo.Update(_mapper.Map<Category>(dto));
            return _unitOfWork.SaveChanges();
        }
        /// <summary>
        /// Kategori siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryDelete(long id)
        {
            var deleteData = _categoryRepo.GetAll().Where(x => x.Id == id);
            var result = _unitOfWork.SaveChanges();
            return result;
        }
        /// <summary>
        /// Kategori durumunu aktif pasif yapar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryStatusUpdate(int id, bool state)
        {
            var data = _categoryRepo.Get(x => x.Id == id);
            data.IsActive = state;
            _categoryRepo.Update(data);
            var result = _unitOfWork.SaveChanges();
            return result;
        }
        #endregion
        public async Task<ServiceResult> CreateCategoryRelation(List<CategoryRelationDto> listData)
        {


            _categoryRelationRepo.AddRange(_mapper.Map<List<CategoryRelation>>(listData));
            return _unitOfWork.SaveChanges();
        }

        private static string GetCategoryQuery(CrudType crudType, CategoriesDto dto, CategoriesDto session)
        {
            string query = "";
            if (crudType == CrudType.Insert)
            {
                query = $"Insert Into Category (Description,IsActive,CreatedUserId,CreatedDate) VALUES({dto.Description},{true},{session.Id},{DateTime.Now})";
            }

            if (crudType == CrudType.Update)
            {
                query = $"Update Category set Description={dto.Description},IsActive={dto.IsActive},UpdatedUserId={session.IsActive},UpdatedDate={DateTime.Now}";
            }
            return query;
        }

        private static string GetCategoryRelationQuery(CrudType crudType, CategoryRelationDto dto, UserSession session)
        {
            string query = "";
            if (crudType == CrudType.Insert)
            {
                query = $"Insert Into CategoryRelation  (CategoryId,TagContentId,ReadType,CreatedUserId,CreatedDate) Values" +
                    $"({dto.CategoryId},{dto.TagContentId},{dto.ReadType},{session.Id},{DateTime.Now})";
            }

            if (crudType == CrudType.Update)
            {
                query = $"Update CategoryRelation set CategoryId={dto.CategoryId},TagContentId={dto.TagContentId},ReadType={dto.ReadType},UpdatedUserId={session.Id},UpdatedDate={DateTime.Now}";
            }
            return query;
        }

        public List<CategoryRelationDto> GetCategoryRelation(string data, long Id, ReadType readType)
        {
            List<CategoryRelationDto> listData = new List<CategoryRelationDto>();
            string[] data_ = data.Replace("[", "").Replace("]", "").Replace("\"", "").Replace("\"", "").Split(',');
            foreach (var item in data_)
            {
                var relationData = new CategoryRelationDto()
                {
                    CategoryId = Convert.ToInt64(item),
                    TagContentId = Id,
                    ReadType = readType
                };
                listData.Add(relationData);
            }
            return listData;
        }
    }
}
