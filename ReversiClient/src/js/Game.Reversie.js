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

  const CreateBord = function (data) {
    const gameBoard = document.getElementById("game-board");

    // Leeg het huidige bord voordat je het opnieuw opbouwt
    gameBoard.innerHTML = "";

    // Bouw het spelbord op basis van de data array
    for (let row = 0; row < 8; row++) {
      for (let col = 0; col < 8; col++) {
        const cell = document.createElement("div");
        cell.id = `cell-${row}-${col}`;
        cell.className = "cell";

        // Voeg een extra klasse toe op basis van de waarde in de data array
        switch (data[row][col]) {
          case 1:
            cell.classList.add("white-piece");
            break;
          case 2:
            cell.classList.add("black-piece");
            break;
          // Voeg hier extra cases toe voor andere waarden indien nodig
        }

        // Voeg een event listener toe als de cel leeg is (geen fiche)
        if (data[row][col] === 0) {
          cell.addEventListener("click", () => {
            // Roep de showPiece-functie aan met de juiste coördinaten en speler (1 voor zwart, 2 voor wit)
            Game.Reversie.showPiece(row, col, 1);
          });
        }

        gameBoard.appendChild(cell);
      }
    }
  };

  function updateBord(data) {
    const gameBoard = document.getElementById("game-board");

    // Loop door elke cel en werk de klasse bij op basis van de waarde in de data array
    for (let row = 0; row < 8; row++) {
      for (let col = 0; col < 8; col++) {
        const cell = document.getElementById(`cell-${row}-${col}`);

        // Verwijder alle bestaande klassen
        cell.className = "cell";

        // Voeg een extra klasse toe op basis van de waarde in de data array
        switch (data[row][col]) {
          case 1:
            cell.classList.add("white-piece");
            break;
          case 2:
            cell.classList.add("black-piece");
            break;
          // Voeg hier extra cases toe voor andere waarden indien nodig
        }
      }
    }
  }

  return {
    init: privateInit,
    initBord: _initBord,
    showPiece: showPiece,
    CreateBord: CreateBord,
    updateBord: updateBord,
  };
})();
