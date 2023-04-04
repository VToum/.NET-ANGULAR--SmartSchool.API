using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext context) 
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool includeDisciplina)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeDisciplina)
            {
                query = query.Include(ad => ad.alunoDisciplinas)
                    .ThenInclude(d => d.Disciplina)
                    .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(i => i.Id);

            return query.ToArray();
        }

        public Aluno[] GetAlunoDisciplinaId(int disciplinaId, bool includeAluno = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeAluno)
            {
                query = query.Include(ad => ad.alunoDisciplinas)
                    .ThenInclude(d => d.Disciplina)
                    .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking()
                .OrderBy(i => i.Id)
                .Where(aluno => aluno.alunoDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoId(int alunoId, bool includeProfessor)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(p => p.alunoDisciplinas)
                    .ThenInclude(d => d.Disciplina)
                    .ThenInclude(a => a.Professor);
            }

            query = query.AsNoTracking()
                .OrderBy(ad => ad.Id)
                .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeProfessor = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeProfessor)
            {
                query = query.Include(p => p.Disciplinas);
            }
            query = query.AsNoTracking().OrderBy(i => i.Id);

            return query.ToArray();
        }

        public Professor[] GetProfessorDisciplinaId(int disciplinaId, bool includeProfessor)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeProfessor)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunoDisciplinas);
            }
            query = query.AsNoTracking()
                .OrderBy(aluno => aluno.Id)
                .Where(aluno => aluno.Disciplinas.Any(
                    d => d.AlunoDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));

            return query.ToArray();
        }

        public Professor GetProfessorId(int professorId, bool includeProfessor = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeProfessor)
            {
                query = query.Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunoDisciplinas);
            }
            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(professor => professor.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}
