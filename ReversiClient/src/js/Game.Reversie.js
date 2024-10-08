Game.Reversie = (function () {
  console.log("hallo, vanuit module Reversi");

  let configMap = {
    apiUrl: "/api/url",
  };

  const privateInit = function (gameboard) {};

  const _initBord = function (bordData) {};

  // Waarde/object geretourneerd aan de outer scope

  const showPiece = function (row, col, player) {
    let move = {
      speltoken: Game.configMap.Token,
      spelertoken: Game.configMap.playerToken,
      rij: row,
      colom: col,
    };
    Game.Data.put(Game.configMap.apiUrl + "Zet", move);

    const cell = document.getElementById(`cell-${row}-${col}`);
    const pieceClass = player === 1 ? "white" : "black";

    // Verwijder alle bestaande klassen en voeg de nieuwe klasse toe
    cell.className = "cell " + pieceClass;
  };

  const SkipTurn = function () {
    console.log("Skkip");
    let skip = {
      speltoken: Game.configMap.Token,
      spelertoken: Game.configMap.playerToken,
    };
    Game.Data.put(Game.configMap.apiUrl + "pas", skip);
  };

  const GiveUp = function () {
    console.log("Give uop");
    let opgeven = {
      speltoken: Game.configMap.Token,
      spelertoken: Game.configMap.playerToken,
    };
    Game.Data.put(Game.configMap.apiUrl + "opgeven", opgeven);
  };

  const CreateBord = function (data) {
    const gameBoard = document.getElementById("game-board");

    const templateFunction = spa_templates.templates.gameboard.body;
    const renderedHTML = templateFunction({ board: data });
    document.getElementById("game-board").innerHTML = renderedHTML;
  };

  function updateBord(data) {
    const gameBoard = document.getElementById("game-board");

    const templateFunction = spa_templates.templates.gameboard.body;
    const renderedHTML = templateFunction({ board: data.bord });
    document.getElementById("game-board").innerHTML = renderedHTML;

    // Update player turn div based on data.aandeBeurt
    const playerTurnDiv = document.getElementById("player-turn");
    if (data.aandeBeurt === 1) {
      playerTurnDiv.textContent = "Black's turn"; // Replace with actual text or logic as needed
    } else if (data.aandeBeurt === 2) {
      playerTurnDiv.textContent = "White's turn"; // Replace with actual text or logic as needed
    } else {
      playerTurnDiv.textContent = "Unknown turn"; // Handle unexpected values if needed
    }
    const playerColorDiv = document.getElementById("player-color");
    console.log(Game.configMap.playerToken);
    console.log(data.speler1Token);
    if (Game.configMap.playerToken === data.speler1Token) {
      playerColorDiv.innerHTML = "Your playing as Black";
    } else {
      playerColorDiv.innerHTML = "Your playing as White";
    }

    const popup = document.getElementById("popup");
    if (!data.speler2Token) {
      popup.style.display = "block"; // Show the popup
    } else {
      popup.style.display = "none"; // Hide the popup
    }
  }

  function setWinner(winnaar) {
    const gameBoard = document.getElementById("endState");
    if (winnaar == null) {
      gameBoard.innerText = "This game ended in a draw";
    } else {
      gameBoard.innerText = "The winner of this game is " + winnaar;
    }
  }

  return {
    init: privateInit,
    initBord: _initBord,
    showPiece: showPiece,
    SkipTurn: SkipTurn,
    GiveUp: GiveUp,
    CreateBord: CreateBord,
    updateBord: updateBord,
    setWinner: setWinner,
  };
})();
