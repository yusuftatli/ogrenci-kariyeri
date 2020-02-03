using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Base;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class CategoryManager : BaseClass, ICategoryManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);


        public CategoryManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }
        #region MainCategory
        /// <summary>
        /// Kategorileri listeler, parent'a göre filtrelenmiş
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryList(long? id)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select * from Category where ParentId=@_Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("_Id", id);
                var lisData = await _db.QueryAsync<MainCategoryDto>(query, filter) as List<MainCategoryDto>;
                res = Result.ReturnAsSuccess(data: lisData);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryList " + id, Entity.Enums.PlatformType.Web);
            }
            return res;
        }

        public async Task<string> GetCategoryById(long id)
        {
            string res = "";
            try
            {
                string query = $"select r.TagContentId as Id, c.Description from  CategoryRelation r left join Category c on r.CategoryId = c.Id where r.TagContentId = @id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("id", id);

                var lisData = await _db.QueryAsync<CategoriesDto>(query, filter) as List<CategoriesDto>;
                for (int i = 0; i < lisData.Count; i++)
                {
                    if (i + 1 == lisData.Count)
                    {
                        res += lisData[i].Description.ToString();
                    }
                    else
                    {
                        res += lisData[i].Description.ToString() + ", ";
                    }
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetCategoryById", PlatformType.Web);
            }

            return res;
        }

        public async Task<List<CategoriesDto>> GetCategoryListById(List<long> contentList)
        {
            List<CategoriesDto> res = new List<CategoriesDto>();
            string ids = string.Empty;
            try
            {
                for (int i = 0; i < contentList.Count; i++)
                {
                    if (i + 1 == contentList.Count)
                    {
                        ids += contentList[i].ToString();
                    }
                    else
                    {
                        ids += contentList[i] + ",";
                    }
                }

                string query = $"select r.TagContentId as Id, c.Description from  CategoryRelation r left join Category c on r.CategoryId = c.Id where r.TagContentId in({ids})";
                var lisData = await _db.QueryAsync<CategoriesDto>(query) as List<CategoriesDto>;
                res = lisData;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetCategoryListById", PlatformType.Web);
            }

            return res;
        }
        /// <summary>
        /// Kategorileri listeler, parenttan bağımsız tüm kategoriler
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryListWithParents()
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select * from Category where IsActive = true";
                var lisData = await _db.QueryAsync<MainCategoryDto>(query) as List<MainCategoryDto>;
                res = Result.ReturnAsSuccess(data: lisData);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryListWithParents ", Entity.Enums.PlatformType.Web);
            }
            return res;
        }
        /// <summary>
        /// Kategori ekler
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryCreate(MainCategoryDto dto)
        {
            ServiceResult res = new ServiceResult();
            if (dto == null)
            {
                res = Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            try
            {
                string query = GetCategoryQuery(CrudType.Insert, null, null);
                var result = _db.Execute(query);
                res = Result.ReturnAsSuccess(message: "Kategori Kayıt İşlemi Başarılı");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryCreate ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: "Kategori kayıt İşlemi Sırasında Hata Meydana Geldi.");
            }
            return res;
        }
        /// <summary>
        /// kategori bilgileri güncellenir
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryUpdate(MainCategoryDto dto)
        {
            ServiceResult res = new ServiceResult();
            if (dto == null)
            {
                res = Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            try
            {
                string query = GetCategoryQuery(CrudType.Update, null, null);
                var result = _db.Execute(query);
                res = Result.ReturnAsSuccess(message: "Kategori Güncelleme İşlem Başarılı");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryUpdate ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: "Kategori Güncelleme İşlemi Sırasında Hata Meydana Geldi");
            }
            return res;
        }

        /// <summary>
        /// Kategori durumunu aktif pasif yapar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<ServiceResult> MainCategoryStatusUpdate(int id, bool state)
        {
            ServiceResult res = new ServiceResult();
            string flag = state == true ? "Aktif" : "Pasif";
            try
            {
                string query = $"Update Category set IsActive={state} where Id={id}";
                var result = _db.Execute(query);
                res = Result.ReturnAsSuccess(message: $"Kategori Başarılı Bir Şekilde {flag} Edildi");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryStatusUpdate ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: $"Kategori {flag} Edilirken hata meydane geldi");
            }
            return res;
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
                query = $"Insert Into Category (Description,IsActive,CreatedUserId,CreatedDate) VALUES({dto.Description},{true},{session.Id},{DateTime.Now.ToString()})";
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
                    $"({dto.CategoryId},{dto.TagContentId},'{dto.ReadType}')";
            }

            if (crudType == CrudType.Update)
            {
                query = $"Update CategoryRelation set CategoryId={dto.CategoryId},TagContentId={dto.TagContentId},ReadType={dto.ReadType}";
            }
            return query;
        }

        private static string GetCategoryRelationQuery(CrudType crudType, CategoryRelationDto dto)
        {
            string query = "";
            if (crudType == CrudType.Insert)
            {
                query = $"Insert Into CategoryRelation  (CategoryId,TagContentId,ReadType) Values" +
                    $"({dto.CategoryId},{dto.TagContentId},'{dto.ReadType}')";
            }

            if (crudType == CrudType.Update)
            {
                query = $"Update CategoryRelation set CategoryId={dto.CategoryId},TagContentId={dto.TagContentId},ReadType={dto.ReadType}";
            }
            return query;
        }

        public async Task<string> CreateCategoryRelation(string data, long Id, ReadType readType, UserSession session)
        {
            List<long> idList = new List<long>();
            string res = string.Empty;
            try
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
                    var result = await _db.ExecuteAsync(query);
                    idList.Add(result);
                }
                foreach (long item in idList)
                {
                    res += item.ToString();
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryStatusUpdate ", Entity.Enums.PlatformType.Web);
            }
            return res;
        }

        public async Task<string> CreateCategoryRelation(CategoryRelationDto item, long Id, ReadType readType)
        {
            string res = string.Empty;
            try
            {
                string query = GetCategoryRelationQuery(CrudType.Insert, item);
                var result = await _db.ExecuteAsync(query);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryStatusUpdate ", Entity.Enums.PlatformType.Web);
            }
            return res;
        }
    }
}
