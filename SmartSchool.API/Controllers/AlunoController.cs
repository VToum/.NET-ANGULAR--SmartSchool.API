using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        public AlunoController(IRepository repo) 
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var aluno =_repo.GetAllAlunos(true);
            return Ok(aluno);
        }
        //api/aluno/byId/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoId(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno) 
        {
            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não foi Adicionado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id)
        {
            var aluno = _repo.GetAlunoId(id, false);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não foi Atualizado");          
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id) 
        {
            var aluno = _repo.GetAlunoId(id, false);
            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não foi Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id) 
        {
            var aluno = _repo.GetAlunoId(id, false);
            _repo.Remove(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não foi Removido");
        }
    }
}
