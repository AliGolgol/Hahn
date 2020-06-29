using System.ComponentModel;
using Newtonsoft.Json;

namespace Hahn.ApplicationProcess.May2020.Domain.Common.Entities
{
    public class Applicant
    {
        public int ID { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        /// <example>25</example>
        public string Name { get; set; }
        
        /// <summary>
        /// Temperature in celcius
        /// </summary>
        /// <example>Ali</example>
        public string FamilyName { get; set; }
        /// <summary>
        /// Temperature in celcius
        /// </summary>
        /// <example>Ali</example>
        public string Address { get; set; }
        /// <summary>
        /// Temperature in celcius
        /// </summary>
        /// <example>Ali</example>
        public string CountryOfOrigin { get; set; }
        /// <summary>
        /// CountryIsExisted
        /// </summary>
        /// <example>Ali</example>
        public bool CountryIsExisted { get; set; }
        /// <summary>
        /// Temperature in celcius
        /// </summary>
        /// <example>EMailAddress</example>
        public string EMailAddress { get; set; }
        /// <summary>
        /// Age
        /// </summary>
        /// <example>22</example>
        public int Age { get; set; }
        /// <summary>
        /// Hired
        /// </summary>
        /// <example>True</example>
        public bool Hired { get; set; }
    }
}