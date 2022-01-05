using BM5Software.DAL;
using BM5Software.DTOs;
using BM5Software.Models;
using BM5Software.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BM5Software.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplaceController : ControllerBase
    {
        private readonly IWorkplaceService _workplaceService;
        public WorkplaceController(IWorkplaceService workplaceService)
        {
            _workplaceService = workplaceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Workplace>> Get()
        {
            var workplaces = _workplaceService.GetAll();

            return Ok(workplaces);
        }

        [HttpGet("{id}")]
        public ActionResult<Workplace> Get([FromRoute] int id)
        {
            var workplace = _workplaceService.GetById(id);

            if (workplace is null)
            {
                return NotFound();
            }

            return Ok(workplace);
        }

        [HttpPost]
        public ActionResult Create([FromBody] WorkplaceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workplace = _workplaceService.Create(dto);

            return Created($"api/workplace/{workplace.Id}", workplace);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] WorkplaceDto dto, [FromRoute] int id)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _workplaceService.Update(dto);

            if (isUpdated)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _workplaceService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
