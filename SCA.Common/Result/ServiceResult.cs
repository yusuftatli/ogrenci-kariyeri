using SCA.Common.Resource;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SCA.Common.Result
{
    public class ServiceResult<T>
    {
        public ServiceResult(T data, string roleTypeId = null, string message = null, HttpStatusCode? resultCode = null)
        {
            ResultCode = resultCode ?? HttpStatusCode.SeeOther;
            Data = data;
            Message = string.IsNullOrEmpty(message) ? AlertResource.OperationFailed : message;
            RoleTypeId = roleTypeId;
        }
        public HttpStatusCode ResultCode { get; }
        public string Message { get; }
        public T Data { get; set; }
        public string RoleTypeId { get; set; }

        public bool IsSuccess()
        {
            return ResultCode == HttpStatusCode.OK;
        }
    }

    public class ServiceResult
    {
        public ServiceResult(string roleTypeId = null, string message = null, HttpStatusCode? resultCode = null, object data = null)
        {
            ResultCode = resultCode ?? HttpStatusCode.SeeOther;
            Message = string.IsNullOrEmpty(message) ? AlertResource.OperationFailed : message;
            RoleTypeId = roleTypeId;
            Data = data;
        }

        public HttpStatusCode ResultCode { get; }
        public string Message { get; }
        public string RoleTypeId { get; set; }
        public object Data { get; set; }


        public bool IsSuccess()
        {
            return ResultCode == HttpStatusCode.OK;
        }
    }


    public abstract class Result<T>
    {
        public static ServiceResult<T> ReturnAsSuccess(T data, string roleTypeId = null, string message = null)
        {
            return new ServiceResult<T>(data, roleTypeId, message ?? AlertResource.SuccessfulOperation, HttpStatusCode.OK);
        }
        public static ServiceResult<T> ReturnAsFail(T data, string message = null)
        {
            return new ServiceResult<T>(data, null, message ?? AlertResource.AnErrorOccurredWhenProcess, HttpStatusCode.BadRequest);
        }
        public static ServiceResult<T> ReturnAsUnAuth(T data, string message = null)
        {
            return new ServiceResult<T>(data, null, message ?? AlertResource.AnErrorOccurredWhenProcess, HttpStatusCode.Unauthorized);
        }
        public static ServiceResult<T> ReturnAsInformation(T data, long roleTypeId, string message = null)
        {
            return new ServiceResult<T>(data, null, message ?? AlertResource.NoChanges, HttpStatusCode.SeeOther);
        }
    }

    public abstract class Result
    {
        public static ServiceResult ReturnAsSuccess(string roleTypeId = null, string message = null, object data = null)
        {
            return new ServiceResult(roleTypeId, message ?? AlertResource.SuccessfulOperation, HttpStatusCode.OK, data);
        }
        public static ServiceResult ReturnAsFail(string message = null, object data = null)
        {
            return new ServiceResult(null, message ?? AlertResource.AnErrorOccurredWhenProcess, HttpStatusCode.BadRequest, data);
        }
        public static ServiceResult ReturnAsUnAuth(string message = null, object data = null)
        {
            return new ServiceResult(null, message ?? AlertResource.AnErrorOccurredWhenProcess, HttpStatusCode.Unauthorized, data);
        }
        public static ServiceResult ReturnAsInformation(long roleTypeId, string message = null, object data = null)
        {
            return new ServiceResult(null, message ?? AlertResource.NoChanges, HttpStatusCode.SeeOther, data);
        }

        public static ServiceResult ReturnAsInformation(object productNameCanNotEmpty)
        {
            throw new NotImplementedException();
        }
    }
}
