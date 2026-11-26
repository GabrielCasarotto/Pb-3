using System.Text;
using AreaMembros.Dados;
using AreaMembros.Servicos;

namespace AreaMembros
{
    class Program
    {
        static Repositorio repo = null!;
        static ProgressoService progresso = null!;
        static ComunidadeService comunidade = null!;
        static MatriculaService matriculas = null!;
        static AulaService aulas = null!;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // os CSV ficam na pasta "csv" do projeto
            string pasta = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "csv"));

            // apaga pra cada execucao comecar com os mesmos dados de exemplo
            if (Directory.Exists(pasta))
                Directory.Delete(pasta, true);

            repo = new Repositorio(pasta);
            progresso = new ProgressoService(repo);
            comunidade = new ComunidadeService(repo);
            matriculas = new MatriculaService(repo);
            aulas = new AulaService(repo);

            Console.WriteLine("Arquivos CSV em: " + pasta);

            // ---------- UC04 / UC09 ----------
            Titulo("UC04 - Concluir aula (aluna Ana, curso com 4 aulas)");
            Tentar("Ana conclui a aula 1", () => Console.WriteLine($"Progresso: {progresso.ConcluirAula(2, 1)}%"));
            Tentar("Ana conclui a aula 1 de novo", () => Console.WriteLine($"Progresso: {progresso.ConcluirAula(2, 1)}%"));
            Console.WriteLine($"Registros em progresso.csv: {repo.Progressos.Count} (não duplicou)");
            Tentar("Ana conclui a aula 2", () => Console.WriteLine($"Progresso: {progresso.ConcluirAula(2, 2)}%"));
            Tentar("Bruno (não matriculado) conclui a aula 1", () => progresso.ConcluirAula(3, 1));
            Tentar("Ana conclui a aula 99", () => progresso.ConcluirAula(2, 99));

            // ---------- UC07 ----------
            Titulo("UC07 - Matricular aluno");
            Tentar("Professor matricula o Bruno", () =>
            {
                var m = matriculas.Matricular(3, 1);
                Console.WriteLine($"Matrícula {m.Id} criada com {m.PercentualConclusao}%");
            });
            Tentar("Professor matricula o Bruno de novo", () => matriculas.Matricular(3, 1));
            Tentar("Matricular o professor como aluno", () => matriculas.Matricular(1, 1));

            // ---------- UC06 ----------
            Titulo("UC06 - Criar aula com duração");
            Tentar("Criar aula com duração 0", () => aulas.CriarAula(2, "Polimorfismo", "https://videos/5", 3, 0));
            Tentar("Criar aula com 35 minutos", () =>
            {
                var a = aulas.CriarAula(2, "Polimorfismo", "https://videos/5", 3, 35);
                Console.WriteLine($"Aula {a.Id} '{a.Titulo}' criada com {a.DuracaoMinutos} min");
            });
            Tentar("Ana conclui a aula 3 (agora o curso tem 5 aulas)",
                () => Console.WriteLine($"Progresso: {progresso.ConcluirAula(2, 3)}%"));

            // ---------- UC05 / UC10 ----------
            Titulo("UC05 / UC10 - Comunidade e moderação");
            MostrarComunidade();
            Tentar("Ana tenta ocultar a publicação 2", () => comunidade.OcultarPublicacao(2, 2));
            Tentar("Professor oculta a publicação 2", () =>
            {
                comunidade.OcultarPublicacao(2, 1);
                Console.WriteLine("Publicação 2 oculta");
            });
            MostrarComunidade();
            Tentar("Professor oculta a publicação 50", () => comunidade.OcultarPublicacao(50, 1));
            Tentar("Publicar sem conteúdo", () => comunidade.Publicar(2, "Oi", ""));

            // ---------- arquivos ----------
            Titulo("Conteúdo dos CSV");
            MostrarArquivo(pasta, "matriculas.csv");
            MostrarArquivo(pasta, "progresso.csv");
            MostrarArquivo(pasta, "publicacoes.csv");
            MostrarArquivo(pasta, "aulas.csv");
        }

        static void Titulo(string texto)
        {
            Console.WriteLine();
            Console.WriteLine("=== " + texto + " ===");
        }

        // roda a acao e mostra o erro se der excecao
        static void Tentar(string descricao, Action acao)
        {
            Console.WriteLine("> " + descricao);
            try
            {
                acao();
            }
            catch (Exception ex)
            {
                Console.WriteLine("  ERRO: " + ex.Message);
            }
        }

        static void MostrarComunidade()
        {
            Console.WriteLine("Publicações visíveis:");
            foreach (var p in comunidade.ListarAtivas())
                Console.WriteLine($"  [{p.Id}] {p.Titulo}");
        }

        static void MostrarArquivo(string pasta, string arquivo)
        {
            Console.WriteLine("-- " + arquivo);
            foreach (var linha in File.ReadAllLines(Path.Combine(pasta, arquivo)))
                Console.WriteLine("  " + linha);
        }
    }
}
