using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            return Ok(iRepository.GetSpellen().Where(spel => spel.Speler2Token == null));
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

        // GET api/spel/speler/{spelertoken}
        [HttpGet("speler/{spelertoken}")]
        public ActionResult<Spel> HaalSpelOpViaSpeler(string spelertoken)
        {
            var spel = iRepository.GetSpelBySpeler(spelertoken);

            if (spel == null)
            {
                return NotFound($"Spel voor speler met token {spelertoken} niet gevonden.");
            }

            return Ok(spel);
        }
    }

    // ...

}

