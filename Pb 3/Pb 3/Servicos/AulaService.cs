using AreaMembros.Dados;
using AreaMembros.Models;

namespace AreaMembros.Servicos
{
    // UC06
    public class AulaService
    {
        private readonly Repositorio _repo;

        public AulaService(Repositorio repo)
        {
            _repo = repo;
        }

        public Aula CriarAula(int moduloId, string titulo, string urlVideo, int ordem, int duracaoMinutos)
        {
            if (!_repo.Modulos.Any(m => m.Id == moduloId))
                throw new InvalidOperationException("Módulo não encontrado");

            if (duracaoMinutos <= 0)
                throw new ArgumentException("A duração deve ser maior que zero");

            var aula = new Aula
            {
                Id = _repo.Aulas.Count == 0 ? 1 : _repo.Aulas.Max(a => a.Id) + 1,
                ModuloId = moduloId,
                Titulo = titulo,
                UrlVideo = urlVideo,
                Ordem = ordem,
                DuracaoMinutos = duracaoMinutos
            };
            _repo.Aulas.Add(aula);
            _repo.SalvarAulas();
            return aula;
        }
    }
}
