const Game = (function (url) {
  let configMap = {
    apiUrl: url,
  };

  console.log("hallo, vanuit een module");

  // Private function init
  const privateInit = function () {
    console.log(configMap.apiUrl);
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
  console.log("hallo, vanuit module Datra");

  let configMap = {
    apiUrl: "/api/url",
  };

  const privateInit = function () {
    console.log("Private init vanuit module Data");
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
  };
})();

Game.Model = (function () {
  console.log("hallo, vanuit module Model");

  let configMap = {
    apiUrl: "/api/url",
  };

  const privateInit = function () {
    console.log("Private init vanuit module Model");
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
  };
})();
