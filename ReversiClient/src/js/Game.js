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
    Game.Template.init();
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
        } else if (data.finished == true) {
          clearInterval(pollrate);
          Game.Reversie.setWinner(data.winnaar);
          const currentUrl = window.location;
          const redirectUrl = `${currentUrl.protocol}//${currentUrl.host}/Spel/Result?Token=${configMap.Token}`;
          window.location.href = redirectUrl;
          console.log("Game is finished");
        } else {
          Game.Reversie.updateBord(data);
        }
      }
    );
  };

  function fillGameBoard() {
    const boardData = [
      [1, 0, 1, 0, 0, 2, 0, 0],
      [2, 1, 1, 1, 1, 1, 1, 0],
      [0, 0, 1, 1, 1, 2, 0, 0],
      [2, 2, 2, 1, 2, 2, 0, 0],
      [0, 0, 1, 2, 2, 0, 0, 0],
      [0, 0, 2, 2, 2, 0, 0, 0],
      [0, 0, 0, 2, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0],
    ];

    const templateFunction = spa_templates.templates.gameboard.body;
    const renderedHTML = templateFunction({ board: boardData });
    document.getElementById("game-board").innerHTML = renderedHTML;
  }

  //  "${currentUrl.protocol}//${currentUrl.host}/Spel/Result?Token=" +configMap.Token;
  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
    fillGameBoard: fillGameBoard,
    configMap: configMap,
  };
})("/api/url");
