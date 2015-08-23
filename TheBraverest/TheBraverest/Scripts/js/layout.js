"use strict";

var TheBraverest = TheBraverest || {};

TheBraverest.MainLayout = React.createClass({
  displayName: "MainLayout",

  render: function render() {
    var contents = React.Children.map(this.props.children, (function (child) {
      return React.cloneElement(child, { test: '' });
    }).bind(this));
    return React.createElement(
      "div",
      null,
      TheBraverest.Navigation,
      React.createElement(
        "div",
        { id: "body-wrapper" },
        React.createElement(
          "div",
          { className: "container" },
          React.createElement(
            "div",
            { className: "body-bg" },
            contents
          )
        )
      ),
      TheBraverest.Footer
    );
  }
});