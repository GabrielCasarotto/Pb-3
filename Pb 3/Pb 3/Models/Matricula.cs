using System.Globalization;

namespace AreaMembros.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public DateTime DataMatricula { get; set; }

        // novo no TP4, sempre entre 0 e 100
        public double PercentualConclusao { get; set; }

        public string ParaCsv() =>
            $"{Id};{AlunoId};{CursoId};{DataMatricula:yyyy-MM-dd};{PercentualConclusao.ToString("0.00", CultureInfo.InvariantCulture)}";

        public static Matricula DoCsv(string[] c) => new Matricula
        {
            Id = int.Parse(c[0]), AlunoId = int.Parse(c[1]), CursoId = int.Parse(c[2]),
            DataMatricula = DateTime.Parse(c[3], CultureInfo.InvariantCulture),
            PercentualConclusao = double.Parse(c[4], CultureInfo.InvariantCulture)
        };
    }
}
