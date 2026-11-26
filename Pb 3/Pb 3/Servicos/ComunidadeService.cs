using AreaMembros.Dados;
using AreaMembros.Models;

namespace AreaMembros.Servicos
{
    // UC05 e UC10
    public class ComunidadeService
    {
        private readonly Repositorio _repo;

        public ComunidadeService(Repositorio repo)
        {
            _repo = repo;
        }

        public Publicacao Publicar(int autorId, string titulo, string conteudo)
        {
            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(conteudo))
                throw new ArgumentException("Título e conteúdo são obrigatórios");

            var pub = new Publicacao
            {
                AutorId = autorId,
                Titulo = titulo,
                Conteudo = conteudo,
                DataCriacao = DateTime.Now,
                Ativa = true
            };
            _repo.AdicionarPublicacao(pub);
            return pub;
        }

        public void OcultarPublicacao(int publicacaoId, int professorId)
        {
            MudarStatus(publicacaoId, professorId, false);
        }

        public void ReativarPublicacao(int publicacaoId, int professorId)
        {
            MudarStatus(publicacaoId, professorId, true);
        }

        private void MudarStatus(int publicacaoId, int professorId, bool ativa)
        {
            var usuario = _repo.BuscarUtilizador(professorId);
            if (usuario == null || usuario.TipoPerfil != "Professor")
                throw new InvalidOperationException("Apenas professores podem moderar");

            var pub = _repo.BuscarPublicacao(publicacaoId)
                ?? throw new InvalidOperationException("Publicação não encontrada");

            pub.Ativa = ativa;
            _repo.SalvarPublicacoes();
        }

        public List<Publicacao> ListarAtivas() =>
            _repo.Publicacoes.Where(p => p.Ativa).ToList();
    }
}
