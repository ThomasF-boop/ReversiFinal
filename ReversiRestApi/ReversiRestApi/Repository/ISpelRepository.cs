using ReversiRestApi.Model;

namespace ReversiRestApi.Repository
{
    public interface ISpelRepository
    {
        void AddSpel(Spel spel);

        public List<Spel> GetSpellen();

        Spel GetSpel(string spelToken);

        public void SaveSpellen();

        public void DeleteSpel(Spel spel);
        object GetSpelBySpeler(string spelertoken);
    }
}
