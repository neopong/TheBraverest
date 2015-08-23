var TheBraverest = TheBraverest || {}﻿

TheBraverest.MainLayout = React.createClass({
  render: function(){
    var contents = React.Children.map(this.props.children,
      function(child){
        return React.cloneElement(child, {test: ''});
      }.bind(this)
    );
    return(
      <div>
        {TheBraverest.Navigation}
        <div id="body-wrapper">
          <div className="container">
            <div className="body-bg">
              {contents}
            </div>
          </div>
        </div>
        {TheBraverest.Footer}
      </div>
    );
  }
});
