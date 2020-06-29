using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.May2020.Domain.Common.Entities;

namespace Hahn.ApplicationProcess.May2020.Domain.Services
{
    public interface IApplicantService
    {
        Task<int> Add(Applicant applicant);
        Task<int> Update(int id,Applicant applicant);
        int Delete(int id);
        Task<IList<Applicant>> GetAll();
        Task<Applicant> GetById(int id);
    }
}