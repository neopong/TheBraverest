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
      React.createElement(
        "div",
        { className: "navbar navbar-inverse navbar-fixed-top" },
        React.createElement(
          "div",
          { className: "container" },
          React.createElement(
            "div",
            { className: "navbar-header" },
            React.createElement(
              "button",
              { type: "button", className: "navbar-toggle", "data-toggle": "collapse", "data-target": ".navbar-collapse" },
              React.createElement("span", { className: "icon-bar" }),
              React.createElement("span", { className: "icon-bar" }),
              React.createElement("span", { className: "icon-bar" })
            ),
            React.createElement(
              "a",
              { className: "navbar-brand", href: "/" },
              React.createElement("img", { alt: "TheBraverest.com logo", src: "/Images/Brand.png", style: { height: '30px', width: '30px' } }),
              "The Braverest!"
            )
          )
        )
      ),
      React.createElement(
        "div",
        { className: "container" },
        React.createElement(
          "div",
          { className: "row" },
          React.createElement(
            "div",
            { className: "col-md-12" },
            contents
          )
        )
      ),
      React.createElement(
        "div",
        { className: "footer" },
        React.createElement(
          "div",
          { className: "container" },
          React.createElement(
            "div",
            { className: "row" },
            React.createElement("div", { className: "col-md-12" })
          )
        )
      )
    );
  }
});