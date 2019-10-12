using AutoMapper;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IMapper _mapper;

        public QuestionManager()
        {
        }

        public async Task<ServiceResult> GetTestShortList()
        {
            ServiceResult res = new ServiceResult();
            Task t = new Task(() =>
            {
                //var res = (from _test in _testRepo.GetAll().Where(x => x.IsDeleted.Equals(false) && x.PublishState == PublishState.Publish)
                //           join
                //           select new TestShortListDto
                //           {
                //               Id = _test.Id,
                //               Url = _test.Url,
                //               Header = _test.Header,
                //               Count = _analysisManager.GetCountValue(ReadType.Test, _test.Id)
                //           }).ToList();

                res = Result.ReturnAsSuccess(null, null, null);
            });
            t.Start();
            return res;
        }

        public async Task<ServiceResult> GetQuesitons(QuestionCrudDto dto)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                //var res = (from _test in _testRepo.GetAll().Where(x => x.IsDeleted.Equals(false) && x.PublishState == PublishState.Publish)
                //           select new QuestionCrudDto
                //           {
                //               Id = _test.Id,
                //               Url = _test.Url,
                //               Header = _test.Header,
                //               Topic = _test.Topic,
                //               PublishData = _test.PublishData,
                //               ImagePath = _test.ImagePath,
                //               Label = _test.Label,
                //               Readed = _test.Readed,
                //               QuestionList = _questionsRepo.GetAll().Where(a => a.IsDeleted.Equals(false) && _test.Id == a.TestId).Select(s => new QuestionsDto
                //               {
                //                   Id = s.Id,
                //                   TestId = s.TestId,
                //                   ImagePath = s.ImagePath,
                //                   Description = s.Description,
                //                   QuestionOptionList = _questionOptionsRepo.GetAll().Where(d => d.IsDeleted.Equals(false) && d.Id == s.Id).Select(sq => new QuestionOptionsDto
                //                   {
                //                       Id = sq.Id,
                //                       QuestionId = sq.QuestionId,
                //                       CheckOption = sq.CheckOption,
                //                       Description = sq.Description,
                //                       ImagePath = sq.ImagePath,
                //                       FreeText = sq.FreeText,
                //                       Answer = sq.Answer
                //                   }).ToList()
                //               }).ToList()
                //           }).ToList();
                //res = Result.ReturnAsSuccess(null, null, res);
            });
            return res;
        }

        public async Task<ServiceResult> CreateQuestion(QuestionCrudDto dto)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                TestsDto test = new TestsDto();
                test.Url = dto.Url;
                test.Header = dto.Header;
                test.Topic = dto.Topic;
                test.PublishData = dto.PublishData;
                test.ImagePath = dto.ImagePath;
                test.Label = dto.Label;
                test.Readed = dto.Readed;

                //var testId = _testRepo.Add((_mapper.Map<Tests>(test)));
                //_unitOfWork.SaveChanges();
                //foreach (QuestionsDto item in dto.QuestionList)
                //{
                //    item.TestId = testId.Id;
                //    var quesitonId = _questionsRepo.Add((_mapper.Map<Questions>(item)));
                //    _unitOfWork.SaveChanges();
                //    foreach (QuestionOptionsDto options in item.QuestionOptionList)
                //    {
                //        options.QuestionId = quesitonId.Id;
                //        _questionOptionsRepo.Add((_mapper.Map<QuestionOptions>(options)));
                //    }

                //    foreach (TestValueDto value in dto.TestValues)
                //    {
                //        value.TestId = testId.Id;
                //        _testValueRepo.Add((_mapper.Map<TestValue>(value)));
                //    }

                //}
                //_unitOfWork.SaveChanges();
                res = Result.ReturnAsSuccess(null, AlertResource.SuccessfulOperation, null);
            });
            return res;
        }

        public async Task<ServiceResult> UpdateQuestion(QuestionCrudDto dto)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                //TestsDto test = _mapper.Map<TestsDto>(_testRepo.Get(x => x.Id == dto.Id));
                //test.Url = dto.Url;
                //test.Header = dto.Header;
                //test.Topic = dto.Topic;
                //test.PublishData = dto.PublishData;
                //test.ImagePath = dto.ImagePath;
                //test.Label = dto.Label;
                //test.Readed = dto.Readed;

                //_testRepo.Update((_mapper.Map<Tests>(test)));

                //foreach (QuestionsDto item in dto.QuestionList)
                //{
                //    QuestionsDto question = _mapper.Map<QuestionsDto>(_questionsRepo.Get(x => x.Id == item.Id));
                //    question.ImagePath = item.ImagePath;
                //    question.Description = item.Description;

                //    _questionsRepo.Update((_mapper.Map<Questions>(question)));

                //    foreach (QuestionOptionsDto options in item.QuestionOptionList)
                //    {
                //        QuestionOptionsDto questionOption = _mapper.Map<QuestionOptionsDto>(_questionsRepo.Get(x => x.Id == options.Id));
                //        questionOption.CheckOption = options.CheckOption;
                //        questionOption.Description = options.Description;
                //        questionOption.ImagePath = options.ImagePath;
                //        questionOption.FreeText = options.FreeText;
                //        questionOption.Answer = options.Answer;

                //        _questionOptionsRepo.Update((_mapper.Map<QuestionOptions>(questionOption)));
                //    }
                //}

                //var res = _unitOfWork.SaveChanges();

                res = Result.ReturnAsSuccess(null, AlertResource.SuccessfulOperation, null);
            });
            return res;
        }

    }
}
