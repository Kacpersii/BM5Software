using BM5Software.Models;
using BM5Software.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BM5Software.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
        public SpecializationController(ISpecializationService _pecializationService)
        {
            _specializationService = _pecializationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Specialization>> Get()
        {
            var specializations = _specializationService.GetAll();

            return Ok(specializations);
        }
    }
}
