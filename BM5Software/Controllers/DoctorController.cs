using BM5Software.DAL;
using BM5Software.Models;
using BM5Software.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BM5Software.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> Get([FromQuery] string specialization = null)
        {
            IEnumerable<Doctor> doctors;
            if(specialization is null)
            {
                doctors = _doctorService.GetAll();
            }
            else
            {
                doctors = _doctorService.GetAll(specialization);
            }

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> Get([FromRoute] int id)
        {
            var doctor = _doctorService.GetById(id);

            if (doctor is null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var d = _doctorService.Create(doctor);

            return Created($"api/doctor/{d.Id}", d);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] Doctor doctor, [FromRoute] int id)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _doctorService.Update(doctor);

            if (isUpdated)
            {
                return Ok( );
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _doctorService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
