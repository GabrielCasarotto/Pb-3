namespace AreaMembros.Models
{
    public class Aula
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string UrlVideo { get; set; } = "";
        public int Ordem { get; set; }
        public int ModuloId { get; set; }

        // novo no TP4
        public int DuracaoMinutos { get; set; }

        public string ParaCsv() => $"{Id};{Titulo};{UrlVideo};{Ordem};{ModuloId};{DuracaoMinutos}";

        public static Aula DoCsv(string[] c) => new Aula
        {
            Id = int.Parse(c[0]), Titulo = c[1], UrlVideo = c[2], Ordem = int.Parse(c[3]),
            ModuloId = int.Parse(c[4]), DuracaoMinutos = int.Parse(c[5])
        };
    }
}
