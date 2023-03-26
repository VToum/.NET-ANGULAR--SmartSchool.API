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
        Aluno[] GetAllAlunos(bool includeDisciplina);
        Aluno[] GetAlunoDisciplinaId(int disciplinaId, bool includeDisciplina);
        Aluno GetAlunoId(int alunoId, bool includeDisciplina);

        //PROFESSORES
        Professor[] GetAllProfessores( bool includeDisciplina);
        Professor[] GetProfessorDisciplinaId(int disciplinaId, bool includeProfessor);
        Professor GetProfessorId(int professorId, bool includeDisciplina);

    }
}
