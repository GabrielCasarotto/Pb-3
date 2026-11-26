using System.Globalization;

namespace AreaMembros.Models
{
    public class Publicacao
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Conteudo { get; set; } = "";
        public DateTime DataCriacao { get; set; }
        public int AutorId { get; set; }

        // novo no TP4: false = oculta pelo professor (antigo UC08)
        public bool Ativa { get; set; } = true;

        public string ParaCsv() => $"{Id};{Titulo};{Conteudo};{DataCriacao:yyyy-MM-dd HH:mm};{AutorId};{Ativa}";

        public static Publicacao DoCsv(string[] c) => new Publicacao
        {
            Id = int.Parse(c[0]), Titulo = c[1], Conteudo = c[2],
            DataCriacao = DateTime.Parse(c[3], CultureInfo.InvariantCulture),
            AutorId = int.Parse(c[4]), Ativa = bool.Parse(c[5])
        };
    }
}
