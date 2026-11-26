namespace AreaMembros.Models
{
    public class Modulo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public int Ordem { get; set; }
        public int CursoId { get; set; }

        public string ParaCsv() => $"{Id};{Titulo};{Ordem};{CursoId}";

        public static Modulo DoCsv(string[] c) => new Modulo
        {
            Id = int.Parse(c[0]), Titulo = c[1], Ordem = int.Parse(c[2]), CursoId = int.Parse(c[3])
        };
    }
}
