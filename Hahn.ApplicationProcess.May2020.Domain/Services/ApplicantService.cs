using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hahn.ApplicationProcess.May2020.Domain.Common.Entities;
using Hahn.ApplicationProcess.May2020.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.May2020.Domain.Services
{
    public class ApplicantService:IApplicantService
    {
        private readonly IApplicantRepository _repository;
        private readonly ILogger _logger;

        public ApplicantService(IApplicantRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
            
        }

        public async Task<int> Add(Applicant applicant)
        {
            var validator=new ApplicantValidator();
            ValidationResult result = validator.Validate(applicant);
            if (result.IsValid)
            {
                return await _repository.Add(applicant);
            }

            var exceptionMessage = String.Join(",",
                result.Errors.Select(x => $"{x.ErrorMessage}").ToArray());
            
            _logger.LogError(exceptionMessage);
            
            throw new Exception(exceptionMessage);
            
        }

        public async Task<int> Update(int id,Applicant applicant)
        {
            var validator=new ApplicantValidator();
            ValidationResult result = validator.Validate(applicant);
            if (result.IsValid)
            {
                return await _repository.Update(id,applicant);
            }

            var exceptionMessage = String.Join(",",
                result.Errors.Select(x => $"{x.ErrorMessage}").ToArray());

            _logger.LogError(exceptionMessage);
            
            throw new Exception(exceptionMessage);
        }

        public  int Delete(int id)
        {
            try
            {
                _repository.Delete(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
            return id;
        }

        public async Task<IList<Applicant>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Applicant> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}