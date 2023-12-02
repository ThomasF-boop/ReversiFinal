const Game = (function (url) {
  let configMap = {
    apiUrl: url,
  };

  let stateMap = {
    gameState: null,
  };
  console.log("hallo, vanuit Game");

  // Private function init
  const privateInit = function (callback) {
    console.log(configMap.apiUrl);
    _getCurrentGameState();
    callback();
  };

  const _getCurrentGameState = function () {
    setInterval(function () {
      stateMap.gameState = Game.Model.getGameState();
    }, 2000);
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
  };
})("/api/url");

Game.Reversie = (function () {
  console.log("hallo, vanuit module Reversi");

  let configMap = {
    apiUrl: "/api/url",
  };

  const privateInit = function () {
    console.log("Private init vanuit module Reversi");
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
  };
})();

Game.Data = (function () {
  console.log("hallo, vanuit module Data");

  let configMap = {
    apiUrl: "/api/url",
    apikey: "387f76accb140d8e11247c2ac81ae2a3",
    mock: [
      {
        url: "api/Spel/Beurt",
        data: 0,
      },
    ],
  };

  let stateMap = {
    environment: "development",
  };

  const get = function (url) {
    // return $.get(url + configMap.apikey)
    //   .then((r) => {
    //     return r;
    //   })
    //   .catch((e) => {
    //     console.log(e.message);
    //   });

    if (stateMap.environment === "development") {
      return getMockData("api/Spel/Beurt");
    }
  };

  const getMockData = function (url) {
    //filter mock data, configMap.mock ... oei oei, moeilijk moeilijk :-)
    const mockData = configMap.mock.find((mock) => mock.url === url).data;

    return new Promise((resolve, reject) => {
      resolve(mockData);
    });
  };

  const privateInit = function (enviroment) {
    stateMap.environment = environment;
    if (environment !== "production" || environment !== "development")
      throw new Error(
        "Environment niet gelijk aan 'production' of 'development'"
      );
    if (environment === "production") {
      //request aan server
    }
    if (environment === "development") {
      return getMockData("api/Spel/Beurt");
    }
    console.log("Private init vanuit module Data");
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
    get: get,
    getMockData: getMockData,
  };
})();

Game.Model = (function () {
  const _getGameState = async function () {
    //aanvraag via Game.Data
    let gameData;
    await Game.Data.get("/api/Spel/Beurt/token")
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
    if (gameData > 2 || gameData < 0)
      throw new Error("gameData valt buiten de geldige waarde");
    if (gameData === 0) {
      console.log("geen specifieke waarde");
      return "geen specifieke waarde";
    } else if (gameData === 1) {
      console.log("wit aan zet");
      return "wit aan zet";
    } else if (gameData === 2) {
      console.log("zwart aan zet");
      return "zwart aan zet";
    }
    console.log("end of _getGameState, returning null...");
    return null;
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
