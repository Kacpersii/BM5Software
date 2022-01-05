using BM5Software.DTOs;
using BM5Software.Models;
using BM5Software.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BM5Software.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorWorkplaceController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IWorkplaceService _workplaceService;
        private readonly IDoctorWorkplaceService _doctorWorkplaceService;
        public DoctorWorkplaceController(IDoctorService doctorService, IWorkplaceService workplaceService, IDoctorWorkplaceService doctorWorkplaceService)
        {
            _doctorService = doctorService;
            _workplaceService = workplaceService;
            _doctorWorkplaceService = doctorWorkplaceService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] DoctorWorkplaceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = _doctorService.GetById(dto.DoctorId);

            List<Workplace> workplaces = new List<Workplace>();
            dto.WorkplaceIds.ForEach(id => workplaces.Add(_workplaceService.GetById(id)));


            if (doctor is null || workplaces.Count == 0)
            {
                return NotFound();
            }

            _doctorWorkplaceService.AddToWorkplace(doctor, workplaces);

            return Created($"api/doctor/{doctor.Id}", doctor);
        }
    }
}
