using System;
using System.Net;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.May2020.Domain.Common.Entities;
using Hahn.ApplicationProcess.May2020.Domain.Services;
using Hahn.ApplicationProcess.May2020.Web.Common.Swagger;
using Hahn.ApplicationProcess.May2020.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.May2020.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }
        
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Resource created", typeof(ApplicantModelExample))]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ApplicantModelExample))]
        public async Task<ActionResult<string>> Create([FromBody]Applicant applicant)
        {
            try
            {
                applicant.CountryIsExisted = await Countries.IsExisted(applicant.CountryOfOrigin);
                var id=await _applicantService.Add(applicant);
                var result = new JsonResult($"/api/applicant/{id}") {StatusCode = 201};
                return result;
            }
            catch (Exception e)
            {
                return  BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<JsonResult> Get(int id)
        {
            var result = await _applicantService.GetById(id);
            return new JsonResult(result);
        } 
        
        [HttpGet()]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<JsonResult> Get()
        {
            var result = await _applicantService.GetAll();
            return new JsonResult(result);
        } 
        
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult> Put(int id,[FromBody]Applicant applicant)
        {
            if (id!=applicant.ID)
            {
                return BadRequest();
            }

            try
            {
                var result = await _applicantService.Update(id,applicant);
                return new OkResult();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }  
        
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            { 
                 _applicantService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return new OkResult();
        }
    }
}