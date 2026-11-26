using AreaMembros.Dados;
using AreaMembros.Models;

namespace AreaMembros.Servicos
{
    // UC04 e UC09
    public class ProgressoService
    {
        private readonly Repositorio _repo;

        public ProgressoService(Repositorio repo)
        {
            _repo = repo;
        }

        public double ConcluirAula(int alunoId, int aulaId)
        {
            var aula = _repo.BuscarAula(aulaId)
                ?? throw new InvalidOperationException("Aula não encontrada");

            var modulo = _repo.Modulos.First(m => m.Id == aula.ModuloId);
            var matricula = _repo.BuscarMatricula(alunoId, modulo.CursoId)
                ?? throw new InvalidOperationException("Você não está matriculado neste curso");

            // aula ja concluida, nao duplica
            if (_repo.JaConcluiu(alunoId, aulaId))
                return matricula.PercentualConclusao;

            _repo.AdicionarProgresso(new ProgressoAula
            {
                AlunoId = alunoId,
                AulaId = aulaId,
                Concluida = true,
                DataConclusao = DateTime.Now
            });

            matricula.PercentualConclusao = CalcularPercentual(alunoId, modulo.CursoId);
            _repo.SalvarMatriculas();
            return matricula.PercentualConclusao;
        }

        public double CalcularPercentual(int alunoId, int cursoId)
        {
            var aulas = _repo.AulasDoCurso(cursoId);
            if (aulas.Count == 0)
                return 0;

            int concluidas = aulas.Count(a => _repo.JaConcluiu(alunoId, a.Id));
            return Math.Round(concluidas * 100.0 / aulas.Count, 2);
        }
    }
}
