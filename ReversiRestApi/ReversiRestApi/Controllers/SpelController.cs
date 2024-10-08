using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReversiRestApi.Migrations;
using ReversiRestApi.Model;
using ReversiRestApi.Model.Api;
using ReversiRestApi.Repository;
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


        [HttpGet("all")]
        public ActionResult<IEnumerable<Spel>> GetAllSpellen()
        {
            var allSpellen = iRepository.GetSpellen().ToList();
            return Ok(allSpellen);
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
        public ActionResult PlaatsZet(PlayerZet playerZet) 
        {
            if(string.IsNullOrEmpty(playerZet.speltoken) || string.IsNullOrEmpty(playerZet.spelertoken)) return BadRequest();

            var spel = iRepository.GetSpel(playerZet.speltoken);

            if (spel == null)
            {
                return NotFound($"Spel met speltoken {playerZet.speltoken} niet gevonden.");
            }
            if(!(spel.Speler1Token == playerZet.spelertoken || spel.Speler2Token == playerZet.spelertoken)) return BadRequest("Speler zit niet in deze game");
            if (!(spel.Speler1Token == playerZet.spelertoken && spel.AandeBeurt == Kleur.Wit || spel.Speler2Token == playerZet.spelertoken && spel.AandeBeurt == Kleur.Zwart)) return BadRequest("Speler niet aan de beurt");

            if (spel.ZetMogelijk(playerZet.rij, playerZet.colom))
            {
                spel.DoeZet(playerZet.rij, playerZet.colom);
                
                iRepository.SaveSpellen();
                if (spel.Afgelopen())
                {
                    spel.Finished = true;
                    var winnaar = spel.OverwegendeKleur();
                    iRepository.SaveSpellen();

                    return Ok("winnaar is " + winnaar);
                }
                return Ok(spel.Bord);
            }
            else
            {
                return BadRequest("zet niet mogelijk");
            }
          
                    
        }
        // PUT
        [HttpPut("pas")]
        public ActionResult Pas(PlayerGameData data)
        {
            if (string.IsNullOrEmpty(data.SpelToken) || string.IsNullOrEmpty(data.SpelerToken)) return BadRequest();

            var spel = iRepository.GetSpel(data.SpelToken);

            if (spel == null)
            {
                return NotFound($"Spel met speltoken {data.SpelToken} niet gevonden.");
            }
            if (!(spel.Speler1Token == data.SpelerToken || spel.Speler2Token == data.SpelerToken)) return BadRequest("Speler zit niet in deze game");

            spel.Pas();
            iRepository.SaveSpellen();
            return Ok(spel);
        }

        // PUT
        [HttpPut("opgeven")]
        public ActionResult Opgeven(PlayerGameData data)
        {
            if (string.IsNullOrEmpty(data.SpelToken) || string.IsNullOrEmpty(data.SpelerToken)) return BadRequest();

            var spel = iRepository.GetSpel(data.SpelToken);

            if (spel == null)
            {
                return NotFound($"Spel met speltoken {data.SpelToken} niet gevonden.");
            }
            if (!(spel.Speler1Token == data.SpelerToken || spel.Speler2Token == data.SpelerToken)) return BadRequest("Speler zit niet in deze game");

            if (data.SpelerToken == spel.Speler1Token)
            {
                spel.Winnaar = spel.Speler2Token;
            }
            else
            {
                spel.Winnaar = spel.Speler1Token;
            }

            spel.Finished = true;

            iRepository.SaveSpellen();
            return Ok(spel);
        }


        [HttpPut("JoinSpel")]
        public bool JoinSpel(PlayerGameData joinspel)
        {
            var spel = iRepository.GetSpel(joinspel.SpelToken);

            if (spel == null)
            {
                return false;
            }
            

            if (spel.Speler1Token == null)
            {
                spel.Speler1Token = joinspel.SpelerToken;
                iRepository.SaveSpellen();
                return true;
            }

            if (spel.Speler2Token == null)
            {
                spel.Speler2Token = joinspel.SpelerToken;
                iRepository.SaveSpellen();
                return true;
            }
            return false;
        }
        [HttpDelete("DeleteSpel")]
        public ActionResult DeleteGame(PlayerGameData removeGame)
        {

            var spel = iRepository.GetSpel(removeGame.SpelToken);

            if (spel == null)
            {
                return NotFound();
            }

            if (!spel.Finished)
            {
                return BadRequest();
            }

            iRepository.DeleteSpel(spel);
            iRepository.SaveSpellen();

            return Ok();
        }

    }


}

