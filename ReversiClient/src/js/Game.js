const Game = (function (url) {
  let configMap = {
    apiUrl: "https://localhost:7020/api/Spel/",
    playerToken: "",
    Token: "",
    Kleur: "",
  };

  let stateMap = {
    gameState: null,
  };
  console.log("hallo, vanuit Game.js");

  // Game.init("https://localhost:7020/api/Spel/","test","83f344d1-3d83-4675-82fa-48b78ea2b0dc")

  let pollrate;

  // Private function init
  const privateInit = function (url, playerToken, Token) {
    configMap.apiUrl = url;
    configMap.playerToken = playerToken;
    configMap.Token = Token;
    Game.Data.init(url, "production");
    console.log(configMap.apiUrl);
    pollrate = setInterval(_getCurrentGameState, 2000);
  };

  let initialize = false;

  const _getCurrentGameState = function () {
    stateMap.gameState = Game.Model.getGameState(configMap.Token).then(
      (data) => {
        if (initialize === false) {
          initialize = true;
          Game.Reversie.CreateBord(data.bord);
          Game.configMap.Kleur = data.aandeBeurt;
        } else {
          Game.Reversie.updateBord(data.bord);
        }
      }
    );
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
    configMap: configMap,
  };
})("/api/url");
