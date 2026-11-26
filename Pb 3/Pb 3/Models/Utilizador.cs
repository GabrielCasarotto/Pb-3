namespace AreaMembros.Models
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
        public string PalavraPasse { get; set; } = "";
        public string TipoPerfil { get; set; } = "Aluno"; // Aluno ou Professor

        public bool EhProfessor() => TipoPerfil == "Professor";

        public string ParaCsv() => $"{Id};{Nome};{Email};{PalavraPasse};{TipoPerfil}";

        public static Utilizador DoCsv(string[] c) => new Utilizador
        {
            Id = int.Parse(c[0]), Nome = c[1], Email = c[2], PalavraPasse = c[3], TipoPerfil = c[4]
        };
    }
}
