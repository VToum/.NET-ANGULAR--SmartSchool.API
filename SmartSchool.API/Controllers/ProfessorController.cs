﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly SmartContext _context;
        public ProfessorController(SmartContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok(_context.Professores);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id) 
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("Id não existe");

            return Ok(professor);

        }

        [HttpGet("byName")]
        public IActionResult GetName(string nome) 
        {
            var professor = _context.Professores.FirstOrDefault(p =>
                p.Nome.Contains(nome));
            if (professor == null) return BadRequest("Nome não existe");

            return Ok(professor);

        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("Nome não existe");
            
            _context.Remove(prof);
            _context.SaveChanges();

            return Ok(prof);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor) 
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("Nome não existe");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);

        }
    }
}