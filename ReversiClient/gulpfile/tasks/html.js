const htmlmin = require("gulp-htmlmin");
const rename = require("gulp-rename");
const { src, dest } = require("gulp");

const html = function (htmlfiles, backendPath) {
  return function () {
    return src(htmlfiles)
      .pipe(
        htmlmin({
          collapseWhitespace: true,
          minifyJS: true,
          minifyCSS: true,
          removeComments: true,
        })
      )
      .pipe(
        rename(function (path) {
          path.dirname += "/";
          path.basename = "index";
          path.extname = ".html";
        })
      )
      .pipe(dest("./dist"))
      .pipe(dest(backendPath));
  };
};

exports.html = html;
