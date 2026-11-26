namespace AreaMembros.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Descricao { get; set; } = "";
        public int ProfessorId { get; set; }

        public string ParaCsv() => $"{Id};{Titulo};{Descricao};{ProfessorId}";

        public static Curso DoCsv(string[] c) => new Curso
        {
            Id = int.Parse(c[0]), Titulo = c[1], Descricao = c[2], ProfessorId = int.Parse(c[3])
        };
    }
}
