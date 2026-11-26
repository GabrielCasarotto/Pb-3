using System.Globalization;

namespace AreaMembros.Models
{
    public class ProgressoAula
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int AulaId { get; set; }
        public bool Concluida { get; set; }
        public DateTime DataConclusao { get; set; }

        public string ParaCsv() => $"{Id};{AlunoId};{AulaId};{Concluida};{DataConclusao:yyyy-MM-dd HH:mm}";

        public static ProgressoAula DoCsv(string[] c) => new ProgressoAula
        {
            Id = int.Parse(c[0]), AlunoId = int.Parse(c[1]), AulaId = int.Parse(c[2]),
            Concluida = bool.Parse(c[3]), DataConclusao = DateTime.Parse(c[4], CultureInfo.InvariantCulture)
        };
    }
}
