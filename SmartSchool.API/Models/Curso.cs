using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Models
{
    public class Curso
    {
        public Curso() { }

        public int CursoId { get; set; }
        public string Nome { get; set; }

        public Curso(int id,
                     string nome)
        {
            this.CursoId = id;
            this.Nome = nome;
        }
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}
