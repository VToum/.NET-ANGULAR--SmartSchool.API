using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        public void Add<T>(T entity) where T : class;
        public void Remove<T>(T entity) where T : class;
        public void Update<T>(T entity) where T : class;
        public bool SaveChanges();

        //ALUNOS
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeDisciplina = false);
        Aluno[] GetAllAlunos(bool includeDisciplina = false);

        Aluno[] GetAlunoDisciplinaId(int disciplinaId, bool includeDisciplina = false);
        Aluno GetAlunoId(int alunoId, bool includeDisciplina = false);

        //PROFESSORES
        Professor[] GetAllProfessores(bool includeDisciplina = false);
        Professor[] GetProfessorDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Professor GetProfessorId(int professorId, bool includeDisciplina = false);

    }
}
