$(document).ready(function () {
  console.log("FeedbackWidget ready!");
  $("#ok-button").on("click", function () {
    alert("The button was clicked.");
  });
});

$("#ok-button").on("click", function () {
  alert("The button was clicked.");
});

class FeedbackWidget {
  constructor(elementId) {
    this._elementId = elementId;
    console.log("FeedbackWidget constructor");
  }

  get elementId() {
    //getter, set keyword voor setter methode
    return this._elementId;
  }

  show(message, type) {
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
      type: type,
    });
  }
  hide() {
    var x = document.getElementById(this._elementId);
    x.style.display = "none";
  }
  log(message) {
    const maxItems = 10;
    let storedMessages = localStorage.getItem("feedback_widget");

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
  removelog() {
    localStorage.clear();
  }
  history() {
    let item = JSON.parse(localStorage.getItem("feedback_widget"));
    let string = "";
    item.forEach((element) => {
      string = string + element.type + " - " + element.message + " \n ";
    });
    console.log(string);
  }
}
