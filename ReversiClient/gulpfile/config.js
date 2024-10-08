module.exports = {
  localServerProjectPath:
    "F:\\school\\Reversi Final\\ReversiFinal\\ReversiMvcApp\\ReversiMvcApp\\wwwroot",
  files: {
    js: ["src/js/**/*.js", "src/js/*.js"],
    sass: ["src/css/*.scss"],
    html: ["index.html"],
    vendor: ["vendor/*.js"],
    templates: ["templates/**/[^_]*.hbs"],
    partials: ["templates/**/_*.hbs"],
  },
  fileOrder: {
    js: ["src/js/Game.js", "src/js/*.js"],
    css: ["src/css/app.scss"],
  },
  voornaam: "Thomas",
};
