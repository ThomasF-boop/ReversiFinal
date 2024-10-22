Game.Stats = (function () {
  console.log("Hallo, vanuit module Stats");

  // Start met een leeg bord (8x8)
  let board = Array.from({ length: 8 }, () => Array(8).fill(0));
  let stoneChart; // Voor de bar grafiek
  let stoneDistributionChart; // Voor de cirkeldiagram

  // Functie om het aantal stenen te tellen
  const countStones = function () {
    let blackCount = 0; // Aantal zwarte stenen
    let whiteCount = 0; // Aantal witte stenen
    let emptyCount = 0; // Aantal lege cellen

    // Loop door het bord om het aantal stenen te tellen
    board.forEach((row) => {
      row.forEach((cell) => {
        if (cell === 1) {
          blackCount++; // Zwarte steen
        } else if (cell === 2) {
          whiteCount++; // Witte steen
        } else {
          emptyCount++; // Lege cel
        }
      });
    });

    return { black: blackCount, white: whiteCount, empty: emptyCount };
  };

  // Functie om de grafiek weer te geven
  const displayChart = function (blackCount, whiteCount) {
    const ctx = document.getElementById("stoneChart").getContext("2d");

    // Als de grafiek nog niet bestaat, maak deze aan
    if (!stoneChart) {
      stoneChart = new Chart(ctx, {
        type: "bar", // Type grafiek (bijv. bar, line, pie, etc.)
        data: {
          labels: ["Zwarte Stenen", "Witte Stenen"],
          datasets: [
            {
              label: "Aantal Stenen",
              data: [blackCount, whiteCount],
              backgroundColor: [
                "rgba(0, 0, 0, 0.5)", // Zwart voor zwarte stenen
                "rgba(255, 255, 255, 0.5)", // Wit voor witte stenen
              ],
              borderColor: [
                "rgba(0, 0, 0, 1)", // Zwart voor zwarte stenen
                "rgba(0, 0, 0, 1)", // Zwart voor de omlijn van de witte stenen
              ],
              borderWidth: 2, // Dikte van de omlijn
            },
          ],
        },
        options: {
          scales: {
            y: {
              beginAtZero: true,
            },
          },
        },
      });
    } else {
      // Als de grafiek al bestaat, werk de data bij
      stoneChart.data.datasets[0].data[0] = blackCount; // Bijwerken van zwarte stenen
      stoneChart.data.datasets[0].data[1] = whiteCount; // Bijwerken van witte stenen
      stoneChart.update(); // Update de grafiek
    }
  };

  // Functie om de verdeling van stenen grafiek weer te geven
  const displayStoneDistributionChart = function () {
    const { black, white, empty } = countStones();
    const ctx = document
      .getElementById("stoneDistributionChart")
      .getContext("2d");

    // Als de grafiek nog niet bestaat, maak deze aan
    if (!stoneDistributionChart) {
      stoneDistributionChart = new Chart(ctx, {
        type: "doughnut", // Gebruik doughnut grafiek
        data: {
          labels: ["Zwarte Stenen", "Witte Stenen", "Lege Cellen"],
          datasets: [
            {
              label: "Verdeling van Stenen",
              data: [black, white, empty],
              backgroundColor: [
                "rgba(0, 0, 0, 0.5)", // Zwart voor zwarte stenen
                "rgba(255, 255, 255, 0.5)", // Wit voor witte stenen
                "rgba(200, 200, 200, 0.5)", // Grijs voor lege cellen
              ],
              borderColor: [
                "rgba(0, 0, 0, 1)", // Zwart voor de omlijn van de zwarte stenen
                "rgba(0, 0, 0, 1)", // Zwart voor de omlijn van de witte stenen
                "rgba(150, 150, 150, 1)", // Zwart voor de omlijn van de lege cellen
              ],
              borderWidth: 2, // Dikte van de omlijn
            },
          ],
        },
        options: {
          responsive: true, // Maak de grafiek responsief
          plugins: {
            legend: {
              position: "top", // Plaats de legenda boven de grafiek
            },
            tooltip: {
              callbacks: {
                label: function (tooltipItem) {
                  const label = tooltipItem.label || "";
                  const value = tooltipItem.raw || 0;
                  const total = black + white + empty; // Totaal aantal cellen
                  const percentage = ((value / total) * 100).toFixed(2); // Percentage
                  return `${label}: ${value} (${percentage}%)`; // Weergave van het aantal en percentage
                },
              },
            },
          },
        },
      });
    } else {
      // Als de grafiek al bestaat, werk de data bij
      const { black, white, empty } = countStones();
      stoneDistributionChart.data.datasets[0].data[0] = black; // Bijwerken van zwarte stenen
      stoneDistributionChart.data.datasets[0].data[1] = white; // Bijwerken van witte stenen
      stoneDistributionChart.data.datasets[0].data[2] = empty; // Bijwerken van lege cellen
      stoneDistributionChart.update(); // Update de grafiek
    }
  };

  // Functie om de statistieken weer te geven
  const displayStats = function () {
    const statsContainer = document.getElementById("statsContainer");
    if (!statsContainer) {
      console.error("Stats container not found.");
      return;
    }

    // Leeg de container
    statsContainer.innerHTML = "";

    // Tel de stenen
    const { black, white } = countStones();

    // Maak een lijst voor de statistieken
    const blackStats = document.createElement("div");
    blackStats.textContent = `Aantal zwarte stenen: ${black}`;
    statsContainer.appendChild(blackStats);

    const whiteStats = document.createElement("div");
    whiteStats.textContent = `Aantal witte stenen: ${white}`;
    statsContainer.appendChild(whiteStats);

    // Toon de grafiek
    displayChart(black, white);
    // Toon de verdeling van stenen grafiek
    displayStoneDistributionChart();
  };

  // Functie om het bord bij te werken
  const updateBoard = function (data) {
    // Bijwerken van de board-variabele met de nieuwe data
    board = data;
    displayStats(); // Geef de statistieken weer
  };

  // Init functie voor Stats module
  const init = function () {
    console.log("Stats module geinitialiseerd."); // Log de initialisatie
    displayStats(); // Toon de statistieken bij init
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: init,
    updateBoard: updateBoard,
  };
})();
