﻿using SCA.DapperRepository.Generic;

namespace SCA.DapperRepository
{
    public interface IUser<U> : IGenericRepository<U> where U : class
    {
    }
}
