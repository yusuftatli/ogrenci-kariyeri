﻿using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Base;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Settings
{
    public class SettingsManager : BaseClass,ISettingsManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);

        public SettingsManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }

        /// <summary>
        /// Haber okunma sayısı çarpan değeri ayarı
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> GetContentMultipleCount()
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select * from Settings where Code = 'HABERCOUNT';";

                var data = await _db.QueryFirstAsync<MultipleCountDto>(query);

                res = Result.ReturnAsSuccess(data: data);

            }
            catch (Exception _ex)
            {
                await _errorManagement.SaveError(_ex, null, "ContentShortListByMobil", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Mobil Content bilgileri yüklenirken hata meydana geldi");
            }
            return res;
        }

        /// <summary>
        /// haber okuma count set etme 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> SetContentMultipleCount(long value)
        {
            ServiceResult res = new ServiceResult();

            if (value == 0)
            {
                res = Result.ReturnAsFail(message: "Çarpan değeri sıfır olamaz");
            }
            try
            {
                string query = $"update Settings set Value = @count  where Code = 'HABERCOUNT';";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("count", value);


                var data = await _db.ExecuteAsync(query, filter);

                res = Result.ReturnAsSuccess(message: "Haber okuma sayısı çarpan değeri başarılı bir şekilde güncellendi");

            }
            catch (Exception _ex)
            {
                await _errorManagement.SaveError(_ex, null, "ContentShortListByMobil", PlatformType.Web);
                res = Result.ReturnAsFail(message: "İşelm sırasında hata meydana geldi");
            }
            return res;
        }

        /// <summary>
        /// Haber okunma sayısı çarpan değeri ayarı haber bazında
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> GetContentMultipleCountOnly(long id)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = " select Multiplier as value from Content where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", id);

                var data = await _db.QueryFirstAsync<MultipleCountDto>(query, filter);

                res = Result.ReturnAsSuccess(data: data);

            }
            catch (Exception _ex)
            {
                await _errorManagement.SaveError(_ex, null, "GetContentMultipleCountOnly", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Mobil Content bilgileri yüklenirken hata meydana geldi");
            }
            return res;
        }

        /// <summary>
        /// haber okuma count set etme 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> SetContentMultipleCountOnly(MultipleCountDto dto)
        {
            ServiceResult res = new ServiceResult();

            if (dto.Value == "")
            {
                res = Result.ReturnAsFail(message: "Çarpan değeri sıfır olamaz");
            }
            try
            {
                string query = $" update Content set Multiplier = @Multiplier where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", Convert.ToInt64(dto.Id));
                filter.Add("Multiplier", dto.Value);


                var data = await _db.ExecuteAsync(query, filter);

                res = Result.ReturnAsSuccess(message: "Haber okuma sayısı çarpan değeri başarılı bir şekilde güncellendi");

            }
            catch (Exception _ex)
            {
                await _errorManagement.SaveError(_ex, null, "ContentShortListByMobil", PlatformType.Web);
                res = Result.ReturnAsFail(message: "İşelm sırasında hata meydana geldi");
            }
            return res;
        }
    }
}
