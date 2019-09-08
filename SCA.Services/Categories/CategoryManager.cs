using AutoMapper;
using Dapper;
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
        private readonly IErrorManagement _errorManagent;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");


        public CategoryManager(IUnitofWork unitOfWork, IMapper mapper, IErrorManagement errorManagement)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepo = unitOfWork.GetRepository<Category>();
            _categoryRelationRepo = unitOfWork.GetRepository<CategoryRelation>();
            _errorManagent = errorManagement;
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
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from Category where IsActive=true";
                var lisData = await _db.QueryAsync<MainCategoryDto>(query) as List<MainCategoryDto>;
                _res = Result.ReturnAsSuccess(data: lisData);
            }
            catch (Exception ex)
            {
                await _errorManagent.SaveError(ex.ToString());
            }
            return _res;
        }
        /// <summary>
        /// Kategori ekler
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryCreate(MainCategoryDto dto)
        {
            ServiceResult _res = new ServiceResult();
            if (dto == null)
            {
                _res = Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            try
            {
                string query = GetCategoryQuery(CrudType.Insert, null, null);
                var result = _db.Execute(query);
                _res = Result.ReturnAsSuccess(message: "Kategori Kayıt İşlemi Başarılı");
            }
            catch (Exception ex)
            {
                await _errorManagent.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Kategori kayıt İşlemi Sırasında Hata Meydana Geldi.");
            }
            return _res;
        }
        /// <summary>
        /// kategori bilgileri güncellenir
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryUpdate(MainCategoryDto dto)
        {
            ServiceResult _res = new ServiceResult();
            if (dto == null)
            {
                _res = Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            try
            {
                string query = GetCategoryQuery(CrudType.Update, null, null);
                var result = _db.Execute(query);
                _res = Result.ReturnAsSuccess(message: "Kategori Güncelleme İşlem Başarılı");
            }
            catch (Exception ex)
            {
                await _errorManagent.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Kategori Güncelleme İşlemi Sırasında Hata Meydana Geldi");
            }
            return _res;
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
            ServiceResult _res = new ServiceResult();
            string flag = state == true ? "Aktif" : "Pasif";
            try
            {
                string query = $"Update Category set IsActive={state} where Id={id}";
                var result = _db.Execute(query);
                _res = Result.ReturnAsSuccess(message: $"Kategori Başarılı Bir Şekilde {flag} Edildi");
            }
            catch (Exception ex)
            {
                await _errorManagent.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: $"Kategori {flag} Edilirken hata meydane geldi");
            }
            return _res;
        }
        #endregion


        private static string GetCategoryQuery(CrudType crudType, CategoriesDto dto, CategoriesDto session)
        {
            string query = "";
            if (crudType == CrudType.List)
            {
                query = "";
            }

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
                query = $"Insert Into CategoryRelation  (CategoryId,TagContentId,ReadType) Values" +
                    $"({dto.CategoryId},{dto.TagContentId},{dto.ReadType},{session.Id},{DateTime.Now})";
            }

            if (crudType == CrudType.Update)
            {
                query = $"Update CategoryRelation set CategoryId={dto.CategoryId},TagContentId={dto.TagContentId},ReadType={dto.ReadType}";
            }
            return query;
        }

        public async Task<bool> CreateCategoryRelation(string data, long Id, ReadType readType, UserSession session)
        {
            string[] data_ = data.Replace("[", "").Replace("]", "").Replace("\"", "").Replace("\"", "").Split(',');
            foreach (var item in data_)
            {
                var relationData = new CategoryRelationDto()
                {
                    CategoryId = Convert.ToInt64(item),
                    TagContentId = Id,
                    ReadType = readType
                };
                string query = GetCategoryRelationQuery(CrudType.Insert, relationData, session);
                var res = _db.Execute(query);
            }
            return true;
        }
    }
}
