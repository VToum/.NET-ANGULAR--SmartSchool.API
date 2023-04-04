using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IMapper _mapper;
        public readonly IRepository _repo;
        public ProfessorController(IRepository repo, IMapper mapper) 
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var professor = _repo.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            var prof = _repo.GetProfessorId(id, true);
            if (prof == null) return BadRequest("Id não existe");

            var professor = _mapper.Map<ProfessorDto>(prof);

            return Ok(professor);
            
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<ProfessorDto>(model);

            _repo.Add(professor);
            if (_repo.SaveChanges()) 
            {
                return Created($"/api/professor{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não adiconado");   
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorId(id, false);
            if (professor == null) return BadRequest("Nome não existe");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor{model.Id}", _mapper.Map<ProfessorDto>(model));
            }
            return BadRequest("Professor não Atualizado");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var professor = _repo.GetProfessorId(id, false);
            if (professor == null) return BadRequest("Nome não existe");

            _repo.Remove(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor Removido com sucesso");
            }
            return BadRequest("Professor não Removido");
        }
    }
}
