using Hahn.ApplicationProcess.May2020.Domain.Common.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.May2020.Web.Common.Swagger
{
    public class ApplicantModelExample:IExamplesProvider<Applicant>
    {
        public Applicant GetExamples()
        {
            return new Applicant()
            {
                Address = "St. No. 12",
                Age = 28,
                CountryOfOrigin = "aruba",
                EMailAddress = "example@gmail.com",
                FamilyName = "Solly",
                Hired = true,
                Name = "Alex"
            };
        }
    }
}