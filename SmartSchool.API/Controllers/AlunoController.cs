using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;
        public AlunoController(IRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos =_repo.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("getRegistro")]
        public IActionResult GetRegistro()
        {

            return Ok(new AlunoRegistrarDto());
        }

        //api/aluno/byId/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoId(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            var alunoDto =_mapper.Map<AlunoDto>(aluno);


            return Ok(alunoDto);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model) 
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não foi Adicionado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoId(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);


            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não foi Atualizado");          
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model) 
        {
            var aluno = _repo.GetAlunoId(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            }
            return BadRequest("Aluno não foi Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id) 
        {
            var aluno = _repo.GetAlunoId(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");


            _repo.Remove(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno removido com sucesso");
            }
            return BadRequest("Aluno não foi Removido");
        }
    }
}
