using AreaMembros.Models;

namespace AreaMembros.Dados
{
    // le e grava todos os CSV da pasta "csv"
    public class Repositorio
    {
        private readonly string _pasta;

        public List<Utilizador> Utilizadores { get; private set; } = new();
        public List<Curso> Cursos { get; private set; } = new();
        public List<Modulo> Modulos { get; private set; } = new();
        public List<Aula> Aulas { get; private set; } = new();
        public List<MaterialComplementar> Materiais { get; private set; } = new();
        public List<Matricula> Matriculas { get; private set; } = new();
        public List<ProgressoAula> Progressos { get; private set; } = new();
        public List<Publicacao> Publicacoes { get; private set; } = new();

        public Repositorio(string pasta)
        {
            _pasta = pasta;
            Directory.CreateDirectory(_pasta);

            if (!File.Exists(Caminho("utilizadores.csv")))
                CriarDadosIniciais();

            Carregar();
        }

        private string Caminho(string arquivo) => Path.Combine(_pasta, arquivo);

        // pula o cabecalho e quebra cada linha no ;
        private List<string[]> Ler(string arquivo)
        {
            return File.ReadAllLines(Caminho(arquivo))
                .Skip(1)
                .Where(l => l.Trim() != "")
                .Select(l => l.Split(';'))
                .ToList();
        }

        private void Gravar(string arquivo, string cabecalho, IEnumerable<string> linhas)
        {
            var tudo = new List<string> { cabecalho };
            tudo.AddRange(linhas);
            File.WriteAllLines(Caminho(arquivo), tudo);
        }

        private void Carregar()
        {
            Utilizadores = Ler("utilizadores.csv").Select(Utilizador.DoCsv).ToList();
            Cursos = Ler("cursos.csv").Select(Curso.DoCsv).ToList();
            Modulos = Ler("modulos.csv").Select(Modulo.DoCsv).ToList();
            Aulas = Ler("aulas.csv").Select(Aula.DoCsv).ToList();
            Materiais = Ler("materiais.csv").Select(MaterialComplementar.DoCsv).ToList();
            Matriculas = Ler("matriculas.csv").Select(Matricula.DoCsv).ToList();
            Progressos = Ler("progresso.csv").Select(ProgressoAula.DoCsv).ToList();
            Publicacoes = Ler("publicacoes.csv").Select(Publicacao.DoCsv).ToList();
        }

        // ---- buscas ----

        public Utilizador? BuscarUtilizador(int id) => Utilizadores.FirstOrDefault(u => u.Id == id);

        public Aula? BuscarAula(int id) => Aulas.FirstOrDefault(a => a.Id == id);

        public Matricula? BuscarMatricula(int alunoId, int cursoId) =>
            Matriculas.FirstOrDefault(m => m.AlunoId == alunoId && m.CursoId == cursoId);

        public Publicacao? BuscarPublicacao(int id) => Publicacoes.FirstOrDefault(p => p.Id == id);

        public bool JaConcluiu(int alunoId, int aulaId) =>
            Progressos.Any(p => p.AlunoId == alunoId && p.AulaId == aulaId && p.Concluida);

        public List<Aula> AulasDoCurso(int cursoId)
        {
            var modulos = Modulos.Where(m => m.CursoId == cursoId).Select(m => m.Id).ToList();
            return Aulas.Where(a => modulos.Contains(a.ModuloId)).ToList();
        }

        // ---- inclusoes ----

        public void AdicionarProgresso(ProgressoAula progresso)
        {
            progresso.Id = Progressos.Count == 0 ? 1 : Progressos.Max(p => p.Id) + 1;
            Progressos.Add(progresso);
            SalvarProgressos();
        }

        public void AdicionarMatricula(Matricula matricula)
        {
            matricula.Id = Matriculas.Count == 0 ? 1 : Matriculas.Max(m => m.Id) + 1;
            Matriculas.Add(matricula);
            SalvarMatriculas();
        }

        public void AdicionarPublicacao(Publicacao publicacao)
        {
            publicacao.Id = Publicacoes.Count == 0 ? 1 : Publicacoes.Max(p => p.Id) + 1;
            Publicacoes.Add(publicacao);
            SalvarPublicacoes();
        }

        // ---- gravacao ----

        public void SalvarAulas() =>
            Gravar("aulas.csv", "Id;Titulo;UrlVideo;Ordem;ModuloId;DuracaoMinutos", Aulas.Select(a => a.ParaCsv()));

        public void SalvarMatriculas() =>
            Gravar("matriculas.csv", "Id;AlunoId;CursoId;DataMatricula;PercentualConclusao", Matriculas.Select(m => m.ParaCsv()));

        public void SalvarProgressos() =>
            Gravar("progresso.csv", "Id;AlunoId;AulaId;Concluida;DataConclusao", Progressos.Select(p => p.ParaCsv()));

        public void SalvarPublicacoes() =>
            Gravar("publicacoes.csv", "Id;Titulo;Conteudo;DataCriacao;AutorId;Ativa", Publicacoes.Select(p => p.ParaCsv()));

        // dados de exemplo, so na primeira vez que roda
        private void CriarDadosIniciais()
        {
            File.WriteAllLines(Caminho("utilizadores.csv"), new[]
            {
                "Id;Nome;Email;PalavraPasse;TipoPerfil",
                "1;Gabriel Casarotto;gabriel@email.com;123;Professor",
                "2;Ana Souza;ana@email.com;123;Aluno",
                "3;Bruno Lima;bruno@email.com;123;Aluno"
            });
            File.WriteAllLines(Caminho("cursos.csv"), new[]
            {
                "Id;Titulo;Descricao;ProfessorId",
                "1;C# do zero;Curso basico de C#;1"
            });
            File.WriteAllLines(Caminho("modulos.csv"), new[]
            {
                "Id;Titulo;Ordem;CursoId",
                "1;Fundamentos;1;1",
                "2;Orientacao a objetos;2;1"
            });
            File.WriteAllLines(Caminho("aulas.csv"), new[]
            {
                "Id;Titulo;UrlVideo;Ordem;ModuloId;DuracaoMinutos",
                "1;Instalando o Visual Studio;https://videos/1;1;1;12",
                "2;Variaveis e tipos;https://videos/2;2;1;25",
                "3;Classes e objetos;https://videos/3;1;2;30",
                "4;Heranca;https://videos/4;2;2;28"
            });
            File.WriteAllLines(Caminho("materiais.csv"), new[]
            {
                "Id;NomeArquivo;CaminhoUrl;AulaId",
                "1;apostila-variaveis.pdf;https://arquivos/apostila.pdf;2"
            });
            File.WriteAllLines(Caminho("matriculas.csv"), new[]
            {
                "Id;AlunoId;CursoId;DataMatricula;PercentualConclusao",
                "1;2;1;2026-11-20;0.00"
            });
            File.WriteAllLines(Caminho("progresso.csv"), new[]
            {
                "Id;AlunoId;AulaId;Concluida;DataConclusao"
            });
            File.WriteAllLines(Caminho("publicacoes.csv"), new[]
            {
                "Id;Titulo;Conteudo;DataCriacao;AutorId;Ativa",
                "1;Duvida na aula 2;Nao entendi a diferenca entre int e double;2026-11-21 10:00;2;True",
                "2;COMPRE SEGUIDORES;link suspeito aqui;2026-11-21 11:30;3;True"
            });
        }
    }
}
