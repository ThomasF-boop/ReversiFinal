﻿using ReversiRestApi.DAL;
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
            return _dbContext.Spellen.FirstOrDefault(spel => spel.Token == spelToken);
        }

        public void SaveSpellen()
        {
            _dbContext.SaveChanges();
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
