using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.May2020.Domain.Common.Entities;
using Hahn.ApplicationProcess.May2020.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.May2020.Data.Repository
{
    public class ApplicantRepository:IApplicantRepository
    {
        private readonly ApplicationContext _context;

        public ApplicantRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Applicant applicant)
        {
             await _context.Applicants.AddAsync(applicant);
             int id = await _context.SaveChangesAsync();
             return  applicant.ID;
        }

        public async Task<int> Update(int id,Applicant applicant)
        {
            _context.Entry(applicant).State = EntityState.Modified;
            _context.Applicants.Update(applicant);
            try
            {
                 await _context.SaveChangesAsync();
                 return applicant.ID;
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            var applicant = await _context.Applicants.FindAsync( id);
            if (applicant==null)
            {
                throw  new Exception("Bad request");
            }
            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();
            return  id;
        }

        public async Task<IList<Applicant>> GetAll()
        {
            return await _context.Applicants.ToListAsync();
        }

        public async Task<Applicant> GetById(int id)
        {
            return await _context.Applicants.FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}