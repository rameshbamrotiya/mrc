// (function ($) {
//   $.fn.typetext = function (options) {
//     var selector = $(this).selector;
//     var settings = $.extend({
//       newline: true,
//       newlinechar: '<',
//       speed: 40
//     }, options);
//     var txt = $(selector).text();
//     var timeOut;
//     var txtLen = txt.length;
//     var char = 0;
//     $(selector).text('');
//     (function typeIt() {
//       var humanize = settings.speed;
//       timeOut = setTimeout(function () {
//         char++;
//         var type = txt.substring(char - 1, char);

//         $(selector).append(type);

//         if (settings.newline && txt.substring(char - 1, char) == settings.newlinechar) {
//           $(selector).append('<br/>');
//         }

//         typeIt();

//         if (char == txtLen) {
//           clearTimeout(timeOut);
//         }

//       }, humanize);
//     }());
//   }
// })(jQuery);


$.fn.typeText = function () {
  var keystrokeDelay = 40,
    self = this,
    str = self.html(),
    i = 0,
    isTag,
    text;

  self.html("");

  (function type() {
    text = str.slice(0, ++i);
    if (text == str) {
      console.log("completed typing");
      return;
    }
    self.html(text);

    var char = text.slice(-1);
    if (char == "<") {
      isTag = true;
    }
    if (char == ">") {
      isTag = false;
    }

    if (isTag) {
      return type();
    }
    setTimeout(type, keystrokeDelay);
  }());
};

$(".text").typeText();
