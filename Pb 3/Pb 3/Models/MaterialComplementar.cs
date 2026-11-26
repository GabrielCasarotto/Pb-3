namespace AreaMembros.Models
{
    public class MaterialComplementar
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; } = "";
        public string CaminhoUrl { get; set; } = "";
        public int AulaId { get; set; }

        public string ParaCsv() => $"{Id};{NomeArquivo};{CaminhoUrl};{AulaId}";

        public static MaterialComplementar DoCsv(string[] c) => new MaterialComplementar
        {
            Id = int.Parse(c[0]), NomeArquivo = c[1], CaminhoUrl = c[2], AulaId = int.Parse(c[3])
        };
    }
}
