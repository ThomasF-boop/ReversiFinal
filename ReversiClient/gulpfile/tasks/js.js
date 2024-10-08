const { src, dest } = require("gulp");
const babel = require("gulp-babel");
const concat = require("gulp-concat");
const order = require("gulp-order");
const uglify = require("gulp-uglify");

const fn = function (backendPath) {
  return function () {
    //console.log(`Taak js is uitgevoerd, ${voornaam}!`);
    //return Promise.resolve("Klaar");

    return src("js/*.js").pipe(dest(backendPath));
  };
};

const javascript = function (filesJs, filesJsOrder, backendPath) {
  return function () {
    return (
      src(filesJs)
        .pipe(order(filesJsOrder, { base: "./" }))
        .pipe(concat("app.js"))
        .pipe(
          babel({
            presets: ["@babel/preset-env"],
          })
        )
        //  .pipe(uglify({ compress: true }))
        .pipe(dest("./dist/js"))
        .pipe(dest(backendPath + "/js"))
    );
  };
};

exports.js = fn;
exports.javascript = javascript;
