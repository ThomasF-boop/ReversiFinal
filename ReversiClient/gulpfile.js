const { series } = require("gulp");
const config = require("./gulpfile/config.js");

const js = require("./gulpfile/tasks/js.js").javascript(
  config.files.js,
  config.fileOrder.js,
  config.localServerProjectPath
);

const sass = require("./gulpfile/tasks/sass.js").sass(
  config.localServerProjectPath,
  config.files.sass
);
sass.displayName = "sass";

const html = require("./gulpfile/tasks/html.js").html(
  config.files.html,
  config.localServerProjectPath
);

const vendor = require("./gulpfile/tasks/vendor.js").vendor(
  config.files.vendor,
  config.localServerProjectPath
);

const template = require("./gulpfile/tasks/templates.js").templates(
  config.files.templates,
  config.localServerProjectPath,
  config.files.partials
);

const hello = function (done) {
  console.log(`Groeten van ${config.voornaam}!`);
  done();
};

exports.default = hello;
exports.js = js;
exports.sass = sass;
exports.html = html;
exports.vendor = vendor;
exports.template = template;

exports.all = series(js, sass, html, vendor, template);
