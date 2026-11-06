using System;
using System.Collections.Generic;
using System.IO;

namespace AreaMembrosApp
{
    public class CsvManager
    {
        private readonly string _caminhoArquivo;

        public CsvManager(string caminhoArquivo)
        {
            _caminhoArquivo = caminhoArquivo;

            // Cria o ficheiro com um cabeçalho caso ainda não exista
            if (!File.Exists(_caminhoArquivo))
            {
                File.WriteAllText(_caminhoArquivo, "Id,Nome,Email,TipoPerfil\n");
            }
        }

        public List<string[]>
    LerDados()
    {
    var registros = new List<string[]>
        ();
        var linhas = File.ReadAllLines(_caminhoArquivo);

        foreach (var linha in linhas)
        {
        var colunas = linha.Split(',');
        registros.Add(colunas);
        }

        return registros;
        }

        public void EscreverDado(int id, string nome, string email, string tipoPerfil)
        {
        string novaLinha = $"{id},{nome},{email},{tipoPerfil}";
        File.AppendAllText(_caminhoArquivo, novaLinha + Environment.NewLine);
        }
        }

        class Program
        {
        static void Main(string[] args)
        {
        string caminho = "usuarios.csv";
        CsvManager csvManager = new CsvManager(caminho);

        // Exemplo de escrita de dados
        csvManager.EscreverDado(1, "Gabriel Casarotto", "gabriel@email.com", "Professor");
        csvManager.EscreverDado(2, "Aluno Exemplo", "aluno@email.com", "Aluno");

        // Exemplo de leitura de dados
        Console.WriteLine("Ficheiro CSV: ");
        var dados = csvManager.LerDados();
        foreach (var linha in dados)
        {
        Console.WriteLine(string.Join(" | ", linha));
        }
        }
        }
        }
