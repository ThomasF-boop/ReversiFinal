Game.Model = (function () {
  const _getGameState = async function (Token) {
    //aanvraag via Game.Data
    let gameData;
    await Game.Data.get(Game.configMap.apiUrl + Token)
      .then((r) => {
        gameData = r;
        console.log(gameData);
      })
      .catch((e) => {
        console.log(e.message);
      });

    /**
     * Controle of ontvangen data valide is:
     *  0: geen specifieke waarde
     *  1: wit aan zet
     *  2: zwart aan zet
     */

    if (gameData.aandeBeurt > 2 || gameData.aandeBeurt < 0)
      throw new Error("gameData valt buiten de geldige waarde");
    if (gameData.aandeBeurt === 0) {
      console.log("geen specifieke waarde");
    } else if (gameData.aandeBeurt === 1) {
      console.log("wit aan zet");
    } else if (gameData.aandeBeurt === 2) {
      console.log("zwart aan zet");
    }
    console.log("end of _getGameState, returning null...");
    return gameData;
  };

  console.log("hallo, vanuit module Model");

  let configMap = {
    apiUrl: "http://api.openweathermap.org/data/2.5/weather?q=zwolle&apikey=",
  };

  const getWeather = function () {
    return Game.Data.get(configMap.apiUrl).then((data) => {
      // Controleer of de temperatuur is meegegeven in de teruggegeven data
      if (data && data.main && data.main.temp !== undefined) {
        // Temperatuur is aanwezig, voer de rest van je code uit
        console.log(data);
      } else {
        // Temperatuur ontbreekt, gooi een fout
        throw new Error("Temperatuur ontbreekt in de teruggegeven data");
      }
    });
  };

  const privateInit = function () {
    console.log("Private init vanuit module Model");
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
    getWeather: getWeather,
    getGameState: _getGameState,
  };
})();
