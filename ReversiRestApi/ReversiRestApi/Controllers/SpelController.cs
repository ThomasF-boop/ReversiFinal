using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace ReversiRestApi.Controllers
{
    [Route("api/Spel")]
    [ApiController]
    public class SpelController : ControllerBase
    {
        private readonly ISpelRepository iRepository;
     

        public SpelController(ISpelRepository repository)
        {
            iRepository = repository;
        }


        // GET api/spel
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            return Ok(iRepository.GetSpellen().Where(spel => spel.Speler2Token == null).ToList());
        }



        // POST api/spel
        [HttpPost]
        public ActionResult<string> PlaatsNieuwSpel( string spelerToken, string omschrijving)
        {
            try
            {
                // Genereer een nieuw speltoken (GUID)
                var speltoken = Guid.NewGuid().ToString();

                // Maak een nieuw spel aan met de opgegeven gegevens
                var nieuwSpel = new Spel
                {
                    Token = speltoken,
                    Speler1Token = spelerToken,
                    Omschrijving = omschrijving
                };

                // Voeg het nieuwe spel toe aan de repository
                iRepository.AddSpel(nieuwSpel);

                return Ok($"Nieuw spel geplaatst met speltoken: {speltoken}");
            }
            catch (Exception ex)
            {
                // Handle eventuele fouten
                return BadRequest($"Fout bij het plaatsen van het nieuwe spel: {ex.Message}");
            }
        }

        // GET api/Spel/{speltoken}
        [HttpGet("{speltoken}")]
        public ActionResult<Spel> HaalSpelOp(string speltoken)
        {
            var spel = iRepository.GetSpel(speltoken);

            if (spel == null)
            {
                return NotFound($"Spel met speltoken {speltoken} niet gevonden.");
            }

            return Ok(spel);
        }

        // GET api/SpelSpeler/{spelertoken}
        [HttpGet("SpelSpeler/{spelertoken}")]
        public ActionResult<Spel> HaalSpelOpViaSpeler(string spelertoken)
        {
            var spel = iRepository.GetSpelBySpeler(spelertoken);

            if (spel == null)
            {
                return NotFound($"Spel voor speler met token {spelertoken} niet gevonden.");
            }

            return Ok(spel);
        }

        // GET api/Spel/Beurt
        [HttpGet("Beurt")]
        public ActionResult GetSpelerAanDeBeurt(string speltoken)
        {
            // Voer authenticatie uit op basis van het speltoken. Implementeer je eigen logica.
            var spel = iRepository.GetSpel(speltoken);

            if (spel == null)
            {
                return NotFound($"Spel met speltoken {speltoken} niet gevonden.");
            }

            return Ok(spel.AandeBeurt);
        }

        // PUT 
        [HttpPut("Zet")]
        public ActionResult PlaatsZet(string speltoken, string spelertoken, int rij, int colom) 
        {
            if(string.IsNullOrEmpty(speltoken) || string.IsNullOrEmpty(spelertoken)) return BadRequest();

            var spel = iRepository.GetSpel(speltoken);

            if (spel == null)
            {
                return NotFound($"Spel met speltoken {speltoken} niet gevonden.");
            }
            if(!(spel.Speler1Token == speltoken || spel.Speler2Token == spelertoken)) return BadRequest("Speler zit niet in deze game");
            if (!(spel.Speler1Token == spelertoken && spel.AandeBeurt == Kleur.Wit || spel.Speler2Token == spelertoken && spel.AandeBeurt == Kleur.Zwart)) return BadRequest("Speler niet aan de beurt");

            if (spel.ZetMogelijk(rij,colom))
            {
                spel.DoeZet(rij, colom);

                if (spel.Afgelopen())
                {
                    return Ok("winnaar is " + spel.OverwegendeKleur());
                }
                return Ok(spel.Bord);
            }

            return BadRequest();    
                    
                    
        }

        // PUT
        [HttpPut("opgeven")]
        public ActionResult Opgeven(string speltoken, string spelertoken)
        {
            if (string.IsNullOrEmpty(speltoken) || string.IsNullOrEmpty(spelertoken)) return BadRequest();

            var spel = iRepository.GetSpel(speltoken);

            if (spel == null)
            {
                return NotFound($"Spel met speltoken {speltoken} niet gevonden.");
            }
            if (!(spel.Speler1Token == speltoken || spel.Speler2Token == spelertoken)) return BadRequest("Speler zit niet in deze game");

            spel.Opgeven();

            return Ok(spel.Bord);
        }

    }


}

