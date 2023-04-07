using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using SmartSchool.API.V2.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V2Controllers
{
    /// <summary>
    /// Versão 2 <ProfessorController>
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ProfessorController : ControllerBase
    {
        private readonly IMapper _mapper;
        public readonly IRepository _repo;
        public ProfessorController(IRepository repo, IMapper mapper) 
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método responsável para retornar todos meus Professores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() 
        {
            var professor = _repo.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        /// <summary>
        /// Método responsável para retornar um Professor por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            var prof = _repo.GetProfessorId(id, true);
            if (prof == null) return BadRequest("Id não existe");

            var professor = _mapper.Map<ProfessorDto>(prof);

            return Ok(professor);
            
        }

        /// <summary>
        /// Método responsável para Adicionar um Professor.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável para Atualizar dados do Professor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsável para Remover um Professor.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
