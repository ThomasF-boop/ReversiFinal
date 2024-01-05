using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using ReversiRestApi.DAL;
using ReversiRestApi.Model;
using ReversiRestApi.Model.Api;
using System;

namespace ReversiRestApi.Repository
{
    public class SpelDatabaseRepository : ISpelRepository
    {
        private readonly SpelContext _dbContext;

        public SpelDatabaseRepository(SpelContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddSpel(Spel spel)
        {
            _dbContext.Spellen.Add(spel);
            _dbContext.SaveChanges();
        }

        public List<Spel> GetSpellen()
        {
            return _dbContext.Spellen.ToList();
        }

        public Spel GetSpel(string spelToken)
        {
            return _dbContext.Spellen.First(spel => spel.Token == spelToken);
        }

        public void SaveSpellen()
        {
            
            _dbContext.SaveChanges();
            Console.WriteLine(_dbContext.Spellen);
        }

        public void DeleteSpel(Spel spel)
        {
            _dbContext.Spellen.Remove(spel);
            _dbContext.SaveChanges();
        }

        public object GetSpelBySpeler(string spelerToken)
        {
            return _dbContext.Spellen.FirstOrDefault(spel =>
                spel.Speler1Token == spelerToken || spel.Speler2Token == spelerToken);
        }



        // ...
    }
}
