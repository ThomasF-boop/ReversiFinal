﻿using ReversiRestApi.Model;

namespace ReversiRestApi.Repository
{
    public class SpelRepository : ISpelRepository
    {
        // Lijst met tijdelijke spellen
        public List<Spel> Spellen { get; set; }

        public SpelRepository()
        {
            Spel spel1 = new Spel();
            Spel spel2 = new Spel();
            Spel spel3 = new Spel();

            spel1.Speler1Token = "abcdef";
            spel1.Omschrijving = "Potje snel reveri, dus niet lang nadenken";
            spel2.Speler1Token = "ghijkl";
            spel2.Speler2Token = "mnopqr";
            spel2.Omschrijving = "Ik zoek een gevorderde tegenspeler!";
            spel3.Speler1Token = "stuvwx";
            spel3.Omschrijving = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander";


            Spellen = new List<Spel> { spel1, spel2, spel3 };
        }

        public void AddSpel(Spel spel)
        {
            Spellen.Add(spel);
        }

        public List<Spel> GetSpellen()
        {
            Console.Write("funny");
            return Spellen;
        }

        public Spel GetSpel(string spelToken)
        {
            return Spellen.Find(spel => spel.Token == spelToken);
        }


        public void SaveSpellen()
        {
            throw new NotImplementedException();
        }

        public void DeleteSpel(Spel spel)
        {
            throw new NotImplementedException();
        }

        public object GetSpelBySpeler(string spelerToken)
        {
            return Spellen.Find(spel => spel.Speler1Token == spelerToken || spel.Speler2Token == spelerToken);
        }

        // ...

    }
}
