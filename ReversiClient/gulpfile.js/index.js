const config = require("./config.js");

const js = require("./tasks/js.js").javascript(
  config.files.js,
  config.fileOrder.js,
  config.localServerProjectPath
);

const sass = require("./tasks/sass").sass(
  config.localServerProjectPath,
  config.files.sass
);
sass.displayName = "sass";

const hello = function (done) {
  console.log(`Groeten van ${config.voornaam}!`);
  done();
};

exports.default = hello;
exports.js = js;
exports.sass = sass;
