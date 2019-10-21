using Newtonsoft.Json.Linq;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(async () =>
            {
                var repoOwner = "takenet";               

                List<Repositorio> repo = await ListContentsOctokit(repoOwner);

            }).Wait();
            Console.ReadKey();
        }

        static async Task<List<Repositorio>> ListContentsOctokit(string repoOwner)
        {
            var client = new GitHubClient(new ProductHeaderValue("Github-API-Test"));

            var contents = await client.Repository.GetAllForUser("takenet");
            var contentsList =  contents.OrderBy(x => x.CreatedAt.Date).Where(x => x.FullName.Contains("csharp")).Take(5).ToList();

            List<Repositorio> repositorio = new List<Repositorio>();

            for(int i = 0; i < contentsList.Count(); i++)
            {
                var repo = new Repositorio
                {
                    NomeRepositorio = contentsList[i].FullName,
                    Subtitulo = contentsList[i].Description
                };

                repositorio.Add(repo);
            }

            return repositorio;
        }

    }
}
