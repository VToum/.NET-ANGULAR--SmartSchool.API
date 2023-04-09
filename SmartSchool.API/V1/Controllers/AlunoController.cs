using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using SmartSchool.API.V2.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// Versão 2 <AlunoController>
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Método responsável para retornar todos meus alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));

        }

        /// <summary>
        /// Método responsável para retornar apenas um aluno por codigo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //api/aluno/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoId(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            var alunoDto =_mapper.Map<AlunoDto>(aluno);


            return Ok(alunoDto);

        }
        /// <summary>
        /// Método responsável para Adicionar um aluno.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável para atualizar dados de um aluno.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável para atualizar dados de um aluno.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável para Deletar um aluno.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
