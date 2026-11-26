using AreaMembros.Dados;
using AreaMembros.Models;

namespace AreaMembros.Servicos
{
    // UC07
    public class MatriculaService
    {
        private readonly Repositorio _repo;

        public MatriculaService(Repositorio repo)
        {
            _repo = repo;
        }

        public Matricula Matricular(int alunoId, int cursoId)
        {
            var aluno = _repo.BuscarUtilizador(alunoId);
            if (aluno == null || aluno.TipoPerfil != "Aluno")
                throw new InvalidOperationException("Usuário não é aluno");

            if (_repo.BuscarMatricula(alunoId, cursoId) != null)
                throw new InvalidOperationException("Aluno já matriculado");

            var matricula = new Matricula
            {
                AlunoId = alunoId,
                CursoId = cursoId,
                DataMatricula = DateTime.Today,
                PercentualConclusao = 0
            };
            _repo.AdicionarMatricula(matricula);
            return matricula;
        }

        public void RemoverMatricula(int alunoId, int cursoId)
        {
            var matricula = _repo.BuscarMatricula(alunoId, cursoId)
                ?? throw new InvalidOperationException("Matrícula não encontrada");

            _repo.Matriculas.Remove(matricula);
            _repo.SalvarMatriculas();
        }
    }
}
