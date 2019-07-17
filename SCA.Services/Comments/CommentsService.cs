﻿using AutoMapper;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class CommentsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Comments> _commentRepo;
        public CommentsService(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _commentRepo = unitOfWork.GetRepository<Comments>();
        }


        /// <summary>
        /// makale veya test için yorum insert yapar 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CreateComments(CommentsDto dto)
        {
            dto.Approved = false;
            _commentRepo.Add(_mapper.Map<Comments>(dto));
            return _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// onayda beykeleyn bütün yorumları çeker
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> GetAllCommentsPendingApproval()
        {
            var result = _mapper.Map<List<CommentsDto>>((_commentRepo.GetAll(x => x.Approved.Equals(false))));
            return Result.ReturnAsSuccess(null, result);
        }

        /// <summary>
        /// makaleye yazılmış olan yorumu admin tarafından onaylar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> ApproveComment(long Id)
        {
            var data = _commentRepo.Get(x => x.Id == Id);
            data.Approved = true;
            _commentRepo.Update(data);
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess("Yorum onaylandı", null);
        }

        /// <summary>
        /// Makale ye ait tüm yorumları getitir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CommentsDto> GetCommentForContent(long id)
        {
            var data = _mapper.Map<List<CommentsDto>>(_commentRepo.GetAll(x => x.Id == id && x.Approved.Equals(true)));
            return data;
        }

        /// <summary>
        /// Makale yorumunu pasif yapar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> PassiveApproveComment(long Id)
        {
            var data = _commentRepo.Get(x => x.Id == Id);
            data.Approved = false;
            _commentRepo.Update(data);
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess("Yorum onaylandı", null);
        }
    }
}
