using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        public ProfessorController(IRepository repo) 
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            _repo.GetAllProfessores(true);
            return Ok(_repo);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            var professor = _repo.GetProfessorId(id, true);
            if (professor == null) return BadRequest("Id não existe");

            return Ok(professor);

        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if (_repo.SaveChanges()) 
            {
                return Ok(professor);
            }
            return BadRequest("Professor não adiconado");   
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var professor = _repo.GetProfessorId(id, false);
            if (professor == null) return BadRequest("Nome não existe");

            _repo.Remove(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não Removido");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor) 
        {
            var prof = _repo.GetProfessorId(id, false);
            if (prof == null) return BadRequest("Nome não existe");

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não Atualizado");

        }
    }
}
