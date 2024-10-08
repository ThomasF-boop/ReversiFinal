"use strict";

function _createForOfIteratorHelper(o, allowArrayLike) { var it = typeof Symbol !== "undefined" && o[Symbol.iterator] || o["@@iterator"]; if (!it) { if (Array.isArray(o) || (it = _unsupportedIterableToArray(o)) || allowArrayLike && o && typeof o.length === "number") { if (it) o = it; var i = 0; var F = function F() {}; return { s: F, n: function n() { if (i >= o.length) return { done: true }; return { done: false, value: o[i++] }; }, e: function e(_e) { throw _e; }, f: F }; } throw new TypeError("Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); } var normalCompletion = true, didErr = false, err; return { s: function s() { it = it.call(o); }, n: function n() { var step = it.next(); normalCompletion = step.done; return step; }, e: function e(_e2) { didErr = true; err = _e2; }, f: function f() { try { if (!normalCompletion && it["return"] != null) it["return"](); } finally { if (didErr) throw err; } } }; }
function _unsupportedIterableToArray(o, minLen) { if (!o) return; if (typeof o === "string") return _arrayLikeToArray(o, minLen); var n = Object.prototype.toString.call(o).slice(8, -1); if (n === "Object" && o.constructor) n = o.constructor.name; if (n === "Map" || n === "Set") return Array.from(o); if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return _arrayLikeToArray(o, minLen); }
function _arrayLikeToArray(arr, len) { if (len == null || len > arr.length) len = arr.length; for (var i = 0, arr2 = new Array(len); i < len; i++) arr2[i] = arr[i]; return arr2; }
function _regeneratorRuntime() { "use strict"; /*! regenerator-runtime -- Copyright (c) 2014-present, Facebook, Inc. -- license (MIT): https://github.com/facebook/regenerator/blob/main/LICENSE */ _regeneratorRuntime = function _regeneratorRuntime() { return e; }; var t, e = {}, r = Object.prototype, n = r.hasOwnProperty, o = Object.defineProperty || function (t, e, r) { t[e] = r.value; }, i = "function" == typeof Symbol ? Symbol : {}, a = i.iterator || "@@iterator", c = i.asyncIterator || "@@asyncIterator", u = i.toStringTag || "@@toStringTag"; function define(t, e, r) { return Object.defineProperty(t, e, { value: r, enumerable: !0, configurable: !0, writable: !0 }), t[e]; } try { define({}, ""); } catch (t) { define = function define(t, e, r) { return t[e] = r; }; } function wrap(t, e, r, n) { var i = e && e.prototype instanceof Generator ? e : Generator, a = Object.create(i.prototype), c = new Context(n || []); return o(a, "_invoke", { value: makeInvokeMethod(t, r, c) }), a; } function tryCatch(t, e, r) { try { return { type: "normal", arg: t.call(e, r) }; } catch (t) { return { type: "throw", arg: t }; } } e.wrap = wrap; var h = "suspendedStart", l = "suspendedYield", f = "executing", s = "completed", y = {}; function Generator() {} function GeneratorFunction() {} function GeneratorFunctionPrototype() {} var p = {}; define(p, a, function () { return this; }); var d = Object.getPrototypeOf, v = d && d(d(values([]))); v && v !== r && n.call(v, a) && (p = v); var g = GeneratorFunctionPrototype.prototype = Generator.prototype = Object.create(p); function defineIteratorMethods(t) { ["next", "throw", "return"].forEach(function (e) { define(t, e, function (t) { return this._invoke(e, t); }); }); } function AsyncIterator(t, e) { function invoke(r, o, i, a) { var c = tryCatch(t[r], t, o); if ("throw" !== c.type) { var u = c.arg, h = u.value; return h && "object" == _typeof(h) && n.call(h, "__await") ? e.resolve(h.__await).then(function (t) { invoke("next", t, i, a); }, function (t) { invoke("throw", t, i, a); }) : e.resolve(h).then(function (t) { u.value = t, i(u); }, function (t) { return invoke("throw", t, i, a); }); } a(c.arg); } var r; o(this, "_invoke", { value: function value(t, n) { function callInvokeWithMethodAndArg() { return new e(function (e, r) { invoke(t, n, e, r); }); } return r = r ? r.then(callInvokeWithMethodAndArg, callInvokeWithMethodAndArg) : callInvokeWithMethodAndArg(); } }); } function makeInvokeMethod(e, r, n) { var o = h; return function (i, a) { if (o === f) throw new Error("Generator is already running"); if (o === s) { if ("throw" === i) throw a; return { value: t, done: !0 }; } for (n.method = i, n.arg = a;;) { var c = n.delegate; if (c) { var u = maybeInvokeDelegate(c, n); if (u) { if (u === y) continue; return u; } } if ("next" === n.method) n.sent = n._sent = n.arg;else if ("throw" === n.method) { if (o === h) throw o = s, n.arg; n.dispatchException(n.arg); } else "return" === n.method && n.abrupt("return", n.arg); o = f; var p = tryCatch(e, r, n); if ("normal" === p.type) { if (o = n.done ? s : l, p.arg === y) continue; return { value: p.arg, done: n.done }; } "throw" === p.type && (o = s, n.method = "throw", n.arg = p.arg); } }; } function maybeInvokeDelegate(e, r) { var n = r.method, o = e.iterator[n]; if (o === t) return r.delegate = null, "throw" === n && e.iterator["return"] && (r.method = "return", r.arg = t, maybeInvokeDelegate(e, r), "throw" === r.method) || "return" !== n && (r.method = "throw", r.arg = new TypeError("The iterator does not provide a '" + n + "' method")), y; var i = tryCatch(o, e.iterator, r.arg); if ("throw" === i.type) return r.method = "throw", r.arg = i.arg, r.delegate = null, y; var a = i.arg; return a ? a.done ? (r[e.resultName] = a.value, r.next = e.nextLoc, "return" !== r.method && (r.method = "next", r.arg = t), r.delegate = null, y) : a : (r.method = "throw", r.arg = new TypeError("iterator result is not an object"), r.delegate = null, y); } function pushTryEntry(t) { var e = { tryLoc: t[0] }; 1 in t && (e.catchLoc = t[1]), 2 in t && (e.finallyLoc = t[2], e.afterLoc = t[3]), this.tryEntries.push(e); } function resetTryEntry(t) { var e = t.completion || {}; e.type = "normal", delete e.arg, t.completion = e; } function Context(t) { this.tryEntries = [{ tryLoc: "root" }], t.forEach(pushTryEntry, this), this.reset(!0); } function values(e) { if (e || "" === e) { var r = e[a]; if (r) return r.call(e); if ("function" == typeof e.next) return e; if (!isNaN(e.length)) { var o = -1, i = function next() { for (; ++o < e.length;) if (n.call(e, o)) return next.value = e[o], next.done = !1, next; return next.value = t, next.done = !0, next; }; return i.next = i; } } throw new TypeError(_typeof(e) + " is not iterable"); } return GeneratorFunction.prototype = GeneratorFunctionPrototype, o(g, "constructor", { value: GeneratorFunctionPrototype, configurable: !0 }), o(GeneratorFunctionPrototype, "constructor", { value: GeneratorFunction, configurable: !0 }), GeneratorFunction.displayName = define(GeneratorFunctionPrototype, u, "GeneratorFunction"), e.isGeneratorFunction = function (t) { var e = "function" == typeof t && t.constructor; return !!e && (e === GeneratorFunction || "GeneratorFunction" === (e.displayName || e.name)); }, e.mark = function (t) { return Object.setPrototypeOf ? Object.setPrototypeOf(t, GeneratorFunctionPrototype) : (t.__proto__ = GeneratorFunctionPrototype, define(t, u, "GeneratorFunction")), t.prototype = Object.create(g), t; }, e.awrap = function (t) { return { __await: t }; }, defineIteratorMethods(AsyncIterator.prototype), define(AsyncIterator.prototype, c, function () { return this; }), e.AsyncIterator = AsyncIterator, e.async = function (t, r, n, o, i) { void 0 === i && (i = Promise); var a = new AsyncIterator(wrap(t, r, n, o), i); return e.isGeneratorFunction(r) ? a : a.next().then(function (t) { return t.done ? t.value : a.next(); }); }, defineIteratorMethods(g), define(g, u, "Generator"), define(g, a, function () { return this; }), define(g, "toString", function () { return "[object Generator]"; }), e.keys = function (t) { var e = Object(t), r = []; for (var n in e) r.push(n); return r.reverse(), function next() { for (; r.length;) { var t = r.pop(); if (t in e) return next.value = t, next.done = !1, next; } return next.done = !0, next; }; }, e.values = values, Context.prototype = { constructor: Context, reset: function reset(e) { if (this.prev = 0, this.next = 0, this.sent = this._sent = t, this.done = !1, this.delegate = null, this.method = "next", this.arg = t, this.tryEntries.forEach(resetTryEntry), !e) for (var r in this) "t" === r.charAt(0) && n.call(this, r) && !isNaN(+r.slice(1)) && (this[r] = t); }, stop: function stop() { this.done = !0; var t = this.tryEntries[0].completion; if ("throw" === t.type) throw t.arg; return this.rval; }, dispatchException: function dispatchException(e) { if (this.done) throw e; var r = this; function handle(n, o) { return a.type = "throw", a.arg = e, r.next = n, o && (r.method = "next", r.arg = t), !!o; } for (var o = this.tryEntries.length - 1; o >= 0; --o) { var i = this.tryEntries[o], a = i.completion; if ("root" === i.tryLoc) return handle("end"); if (i.tryLoc <= this.prev) { var c = n.call(i, "catchLoc"), u = n.call(i, "finallyLoc"); if (c && u) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } else if (c) { if (this.prev < i.catchLoc) return handle(i.catchLoc, !0); } else { if (!u) throw new Error("try statement without catch or finally"); if (this.prev < i.finallyLoc) return handle(i.finallyLoc); } } } }, abrupt: function abrupt(t, e) { for (var r = this.tryEntries.length - 1; r >= 0; --r) { var o = this.tryEntries[r]; if (o.tryLoc <= this.prev && n.call(o, "finallyLoc") && this.prev < o.finallyLoc) { var i = o; break; } } i && ("break" === t || "continue" === t) && i.tryLoc <= e && e <= i.finallyLoc && (i = null); var a = i ? i.completion : {}; return a.type = t, a.arg = e, i ? (this.method = "next", this.next = i.finallyLoc, y) : this.complete(a); }, complete: function complete(t, e) { if ("throw" === t.type) throw t.arg; return "break" === t.type || "continue" === t.type ? this.next = t.arg : "return" === t.type ? (this.rval = this.arg = t.arg, this.method = "return", this.next = "end") : "normal" === t.type && e && (this.next = e), y; }, finish: function finish(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.finallyLoc === t) return this.complete(r.completion, r.afterLoc), resetTryEntry(r), y; } }, "catch": function _catch(t) { for (var e = this.tryEntries.length - 1; e >= 0; --e) { var r = this.tryEntries[e]; if (r.tryLoc === t) { var n = r.completion; if ("throw" === n.type) { var o = n.arg; resetTryEntry(r); } return o; } } throw new Error("illegal catch attempt"); }, delegateYield: function delegateYield(e, r, n) { return this.delegate = { iterator: values(e), resultName: r, nextLoc: n }, "next" === this.method && (this.arg = t), y; } }, e; }
function asyncGeneratorStep(gen, resolve, reject, _next, _throw, key, arg) { try { var info = gen[key](arg); var value = info.value; } catch (error) { reject(error); return; } if (info.done) { resolve(value); } else { Promise.resolve(value).then(_next, _throw); } }
function _asyncToGenerator(fn) { return function () { var self = this, args = arguments; return new Promise(function (resolve, reject) { var gen = fn.apply(self, args); function _next(value) { asyncGeneratorStep(gen, resolve, reject, _next, _throw, "next", value); } function _throw(err) { asyncGeneratorStep(gen, resolve, reject, _next, _throw, "throw", err); } _next(undefined); }); }; }
function _typeof(o) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && "function" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? "symbol" : typeof o; }, _typeof(o); }
function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, _toPropertyKey(descriptor.key), descriptor); } }
function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); Object.defineProperty(Constructor, "prototype", { writable: false }); return Constructor; }
function _toPropertyKey(arg) { var key = _toPrimitive(arg, "string"); return _typeof(key) === "symbol" ? key : String(key); }
function _toPrimitive(input, hint) { if (_typeof(input) !== "object" || input === null) return input; var prim = input[Symbol.toPrimitive]; if (prim !== undefined) { var res = prim.call(input, hint || "default"); if (_typeof(res) !== "object") return res; throw new TypeError("@@toPrimitive must return a primitive value."); } return (hint === "string" ? String : Number)(input); }
var Game = function (url) {
  var configMap = {
    apiUrl: "https://localhost:7020/api/Spel/",
    playerToken: "",
    Token: "",
    Kleur: ""
  };
  var stateMap = {
    gameState: null
  };
  console.log("hallo, vanuit Game.js");

  // Game.init("https://localhost:7020/api/Spel/","test","83f344d1-3d83-4675-82fa-48b78ea2b0dc")

  var pollrate;

  // Private function init
  var privateInit = function privateInit(url, playerToken, Token) {
    configMap.apiUrl = url;
    configMap.playerToken = playerToken;
    configMap.Token = Token;
    Game.Data.init(url, "production");
    Game.Template.init();
    console.log(configMap.apiUrl);
    pollrate = setInterval(_getCurrentGameState, 2000);
  };
  var initialize = false;
  var _getCurrentGameState = function _getCurrentGameState() {
    stateMap.gameState = Game.Model.getGameState(configMap.Token).then(function (data) {
      if (initialize === false) {
        initialize = true;
        Game.Reversie.CreateBord(data.bord);
        Game.configMap.Kleur = data.aandeBeurt;
      } else if (data.finished == true) {
        clearInterval(pollrate);
        Game.Reversie.setWinner(data.winnaar);
        var currentUrl = window.location;
        var redirectUrl = "".concat(currentUrl.protocol, "//").concat(currentUrl.host, "/Spel/Result?Token=").concat(configMap.Token);
        window.location.href = redirectUrl;
        console.log("Game is finished");
      } else {
        Game.Reversie.updateBord(data);
      }
    });
  };
  function fillGameBoard() {
    var boardData = [[1, 0, 1, 0, 0, 2, 0, 0], [2, 1, 1, 1, 1, 1, 1, 0], [0, 0, 1, 1, 1, 2, 0, 0], [2, 2, 2, 1, 2, 2, 0, 0], [0, 0, 1, 2, 2, 0, 0, 0], [0, 0, 2, 2, 2, 0, 0, 0], [0, 0, 0, 2, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0]];
    var templateFunction = spa_templates.templates.gameboard.body;
    var renderedHTML = templateFunction({
      board: boardData
    });
    document.getElementById("game-board").innerHTML = renderedHTML;
  }

  //  "${currentUrl.protocol}//${currentUrl.host}/Spel/Result?Token=" +configMap.Token;
  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
    fillGameBoard: fillGameBoard,
    configMap: configMap
  };
}("/api/url");
$(document).ready(function () {
  console.log("FeedbackWidget ready!");
  $("#ok-button").on("click", function () {
    alert("The button was clicked.");
  });
});
$("#ok-button").on("click", function () {
  alert("The button was clicked.");
});
var FeedbackWidget = /*#__PURE__*/function () {
  function FeedbackWidget(elementId) {
    _classCallCheck(this, FeedbackWidget);
    this._elementId = elementId;
    console.log("FeedbackWidget constructor");
  }
  _createClass(FeedbackWidget, [{
    key: "elementId",
    get: function get() {
      //getter, set keyword voor setter methode
      return this._elementId;
    }
  }, {
    key: "show",
    value: function show(message, type) {
      var x = document.getElementById(this._elementId);
      $("#feedback-success").text(message);
      if (type == "success") {
        $("#feedback-success").removeClass("alert-danger");
        $("#feedback-success").addClass("alert-success");
      } else {
        $("#feedback-success").removeClass("alert-success");
        $("#feedback-success").addClass("alert-danger");
      }
      this.log({
        message: message,
        type: type
      });
    }
  }, {
    key: "hide",
    value: function hide() {
      var x = document.getElementById(this._elementId);
      x.style.display = "none";
    }
  }, {
    key: "log",
    value: function log(message) {
      var maxItems = 10;
      var storedMessages = localStorage.getItem("feedback_widget");
      if (storedMessages == null) {
        storedMessages = [];
      } else {
        storedMessages = JSON.parse(storedMessages);
      }
      storedMessages.push(message);

      // Keep only the last 10 items
      if (storedMessages.length > maxItems) {
        storedMessages = storedMessages.slice(-maxItems);
      }
      localStorage.setItem("feedback_widget", JSON.stringify(storedMessages));
    }
  }, {
    key: "removelog",
    value: function removelog() {
      localStorage.clear();
    }
  }, {
    key: "history",
    value: function history() {
      var item = JSON.parse(localStorage.getItem("feedback_widget"));
      var string = "";
      item.forEach(function (element) {
        string = string + element.type + " - " + element.message + " \n ";
      });
      console.log(string);
    }
  }]);
  return FeedbackWidget;
}();
Game.Data = function () {
  console.log("hallo, vanuit module Data");
  var configMap = {
    apiUrl: "/api/url",
    apikey: "387f76accb140d8e11247c2ac81ae2a3",
    mock: [{
      url: "api/Spel/Beurt",
      data: 0
    }]
  };
  var stateMap = {
    environment: "development"
  };
  var get = function get(url) {
    if (stateMap.environment === "development") {
      return getMockData("api/Spel/Beurt");
    } else if (stateMap.environment === "production") {
      return $.get(url).then(function (r) {
        return r;
      })["catch"](function (e) {
        console.log(e.message);
      });
    }
  };
  var put = function put(url, data) {
    if (stateMap.environment === "development") {
      return getMockData("api/Spel/Beurt");
    } else if (stateMap.environment === "production") {
      // Use $.ajax for a PUT request with data in the URL
      return fetch(url, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
      }).then(function (r) {
        return r;
      })["catch"](function (e) {
        console.log(e.message);
      });
    }
  };
  var getMockData = function getMockData(url) {
    //filter mock data, configMap.mock ... oei oei, moeilijk moeilijk :-)
    var mockData = configMap.mock.find(function (mock) {
      return mock.url === url;
    }).data;
    return new Promise(function (resolve, reject) {
      resolve(mockData);
    });
  };
  var Init = function Init(url, environment) {
    configMap.apiUrl = url;
    stateMap.environment = environment;
    if (environment != "production" && environment != "development") throw new Error("Environment niet gelijk aan 'production' of 'development'");
    console.log("Private init vanuit module Data");
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: Init,
    get: get,
    put: put,
    getMockData: getMockData
  };
}();
Game.Model = function () {
  var _getGameState = /*#__PURE__*/function () {
    var _ref = _asyncToGenerator( /*#__PURE__*/_regeneratorRuntime().mark(function _callee(Token) {
      var gameData;
      return _regeneratorRuntime().wrap(function _callee$(_context) {
        while (1) switch (_context.prev = _context.next) {
          case 0:
            _context.next = 2;
            return Game.Data.get(Game.configMap.apiUrl + Token).then(function (r) {
              gameData = r;
              console.log(gameData);
            })["catch"](function (e) {
              console.log(e.message);
            });
          case 2:
            if (!(gameData.aandeBeurt > 2 || gameData.aandeBeurt < 0)) {
              _context.next = 4;
              break;
            }
            throw new Error("gameData valt buiten de geldige waarde");
          case 4:
            if (gameData.aandeBeurt === 0) {
              console.log("geen specifieke waarde");
            } else if (gameData.aandeBeurt === 1) {
              console.log("wit aan zet");
            } else if (gameData.aandeBeurt === 2) {
              console.log("zwart aan zet");
            }
            console.log("end of _getGameState, returning null...");
            return _context.abrupt("return", gameData);
          case 7:
          case "end":
            return _context.stop();
        }
      }, _callee);
    }));
    return function _getGameState(_x) {
      return _ref.apply(this, arguments);
    };
  }();
  console.log("hallo, vanuit module Model");
  var configMap = {
    apiUrl: "http://api.openweathermap.org/data/2.5/weather?q=zwolle&apikey="
  };
  var getWeather = function getWeather() {
    return Game.Data.get(configMap.apiUrl).then(function (data) {
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
  var privateInit = function privateInit() {
    console.log("Private init vanuit module Model");
  };

  // Waarde/object geretourneerd aan de outer scope
  return {
    init: privateInit,
    getWeather: getWeather,
    getGameState: _getGameState
  };
}();
Game.Reversie = function () {
  console.log("hallo, vanuit module Reversi");
  var configMap = {
    apiUrl: "/api/url"
  };
  var privateInit = function privateInit(gameboard) {};
  var _initBord = function _initBord(bordData) {};

  // Waarde/object geretourneerd aan de outer scope

  var showPiece = function showPiece(row, col, player) {
    var move = {
      speltoken: Game.configMap.Token,
      spelertoken: Game.configMap.playerToken,
      rij: row,
      colom: col
    };
    Game.Data.put(Game.configMap.apiUrl + "Zet", move);
    var cell = document.getElementById("cell-".concat(row, "-").concat(col));
    var pieceClass = player === 1 ? "white" : "black";

    // Verwijder alle bestaande klassen en voeg de nieuwe klasse toe
    cell.className = "cell " + pieceClass;
  };
  var SkipTurn = function SkipTurn() {
    console.log("Skkip");
    var skip = {
      speltoken: Game.configMap.Token,
      spelertoken: Game.configMap.playerToken
    };
    Game.Data.put(Game.configMap.apiUrl + "pas", skip);
  };
  var GiveUp = function GiveUp() {
    console.log("Give uop");
    var opgeven = {
      speltoken: Game.configMap.Token,
      spelertoken: Game.configMap.playerToken
    };
    Game.Data.put(Game.configMap.apiUrl + "opgeven", opgeven);
  };
  var CreateBord = function CreateBord(data) {
    var gameBoard = document.getElementById("game-board");
    var templateFunction = spa_templates.templates.gameboard.body;
    var renderedHTML = templateFunction({
      board: data
    });
    document.getElementById("game-board").innerHTML = renderedHTML;
  };
  function updateBord(data) {
    var gameBoard = document.getElementById("game-board");
    var templateFunction = spa_templates.templates.gameboard.body;
    var renderedHTML = templateFunction({
      board: data.bord
    });
    document.getElementById("game-board").innerHTML = renderedHTML;

    // Update player turn div based on data.aandeBeurt
    var playerTurnDiv = document.getElementById("player-turn");
    if (data.aandeBeurt === 1) {
      playerTurnDiv.textContent = "Black's turn"; // Replace with actual text or logic as needed
    } else if (data.aandeBeurt === 2) {
      playerTurnDiv.textContent = "White's turn"; // Replace with actual text or logic as needed
    } else {
      playerTurnDiv.textContent = "Unknown turn"; // Handle unexpected values if needed
    }
    var playerColorDiv = document.getElementById("player-color");
    console.log(Game.configMap.playerToken);
    console.log(data.speler1Token);
    if (Game.configMap.playerToken === data.speler1Token) {
      playerColorDiv.innerHTML = "Your playing as Black";
    } else {
      playerColorDiv.innerHTML = "Your playing as White";
    }
    var popup = document.getElementById("popup");
    if (!data.speler2Token) {
      popup.style.display = "block"; // Show the popup
    } else {
      popup.style.display = "none"; // Hide the popup
    }
  }
  function setWinner(winnaar) {
    var gameBoard = document.getElementById("endState");
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
    setWinner: setWinner
  };
}();
Game.Template = function () {
  function _getTemplate(templateName) {
    if (typeof templateName !== "string") {
      console.error("Template name is not a string:", templateName);
      return null; // or handle the error appropriately
    }
    var template = spa_templates.templates;
    var parts = templateName.split(".");
    var _iterator = _createForOfIteratorHelper(parts),
      _step;
    try {
      for (_iterator.s(); !(_step = _iterator.n()).done;) {
        var part = _step.value;
        template = template[part];
        if (!template) {
          console.error("Template not found:", templateName);
          return null; // or handle the error appropriately
        }
      }
    } catch (err) {
      _iterator.e(err);
    } finally {
      _iterator.f();
    }
    return template;
  }
  var renderTemplate = function renderTemplate(template, data, targetElementId) {
    if (typeof template !== "function") {
      console.error("Template is not a function:", template);
      return;
    }
    var html = template(data);
    document.getElementById(targetElementId).innerHTML = html;
  };
  var init = function init() {
    Handlebars.registerHelper("isBlack", function (player) {
      return player === 1; // Assuming 1 represents black
    });
    Handlebars.registerHelper("isWhite", function (player) {
      return player === 2; // Assuming 2 represents white
    });
  };
  return {
    _getTemplate: _getTemplate,
    renderTemplate: renderTemplate,
    init: init
  };
}();