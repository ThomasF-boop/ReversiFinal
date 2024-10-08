using System.Drawing;

namespace ReversiMvcApp.Models
{
    public enum Kleur { Geen, Wit, Zwart };
    public class Spel
    {
        public string Token { get; set; }
        public string Omschrijving { get; set; }
        public string? Speler1Token { get; set; }
        public string? Speler2Token { get; set; }
        public Kleur[,] Bord { get; set; }
        public Kleur AandeBeurt { get; set; }
        public bool Finished { get; set; } = false;
        public string Winnaar { get; set; }
        public bool PuntenGegeven { get; set; } = false;
    }
}
