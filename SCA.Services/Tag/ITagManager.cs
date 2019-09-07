﻿using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface ITagManager
    {
        Task<ServiceResult> GetTags();
        Task<ServiceResult> CreateTag(string tags, long tagContentId, ReadType ReadType, UserSession session);
        void CreateTagRelation(List<TagRelationDto> dto);
        void UpdateTagRelation(List<TagRelationDto> dto);
    }
}
