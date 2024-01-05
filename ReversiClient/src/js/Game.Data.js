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
    if (stateMap.environment === "development") {
      return getMockData("api/Spel/Beurt");
    } else if (stateMap.environment === "production") {
      return $.get(url)
        .then((r) => {
          return r;
        })
        .catch((e) => {
          console.log(e.message);
        });
    }
  };

  const put = function (url, data) {
    if (stateMap.environment === "development") {
      return getMockData("api/Spel/Beurt");
    } else if (stateMap.environment === "production") {
      // Use $.ajax for a PUT request with data in the URL
      return fetch(url, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      })
        .then((r) => {
          return r;
        })
        .catch((e) => {
          console.log(e.message);
        });
    }
  };

  const getMockData = function (url) {
    //filter mock data, configMap.mock ... oei oei, moeilijk moeilijk :-)
    const mockData = configMap.mock.find((mock) => mock.url === url).data;

    return new Promise((resolve, reject) => {
      resolve(mockData);
    });
  };

  const Init = function (url, environment) {
    configMap.apiUrl = url;
    stateMap.environment = environment;
    if (environment != "production" && environment != "development")
      throw new Error(
        "Environment niet gelijk aan 'production' of 'development'"
      );
    console.log("Private init vanuit module Data");
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: Init,
    get: get,
    put: put,
    getMockData: getMockData,
  };
})();
