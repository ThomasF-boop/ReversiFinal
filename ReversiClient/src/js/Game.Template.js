Game.Template = (function () {
  function _getTemplate(templateName) {
    if (typeof templateName !== "string") {
      console.error("Template name is not a string:", templateName);
      return null; // or handle the error appropriately
    }

    let template = spa_templates.templates;
    const parts = templateName.split(".");
    for (let part of parts) {
      template = template[part];
      if (!template) {
        console.error("Template not found:", templateName);
        return null; // or handle the error appropriately
      }
    }
    return template;
  }

  const renderTemplate = function (template, data, targetElementId) {
    if (typeof template !== "function") {
      console.error("Template is not a function:", template);
      return;
    }
    const html = template(data);
    document.getElementById(targetElementId).innerHTML = html;
  };

  const init = function () {
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
    init: init,
  };
})();
