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
        <div className="navbar navbar-inverse navbar-fixed-top">
          <div className="container">
              <div className="navbar-header">
                  <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                      <span className="icon-bar"></span>
                      <span className="icon-bar"></span>
                      <span className="icon-bar"></span>
                  </button>
                  <a className="navbar-brand" href="/">
                      <img alt="TheBraverest.com logo" src="/Images/Brand.png" style={{height: '30px', width: '30px'}} />
                      The Braverest!
                  </a>
              </div>
          </div>
      </div>
      <div className="container">
        <div className="row">
          <div className="col-md-12">
            {contents}
          </div>
        </div>
      </div>
      <div className="footer">
        <div className="container">
          <div className="row">
            <div className="col-md-12">
            </div>
          </div>
        </div>
      </div>
    </div>
    );
  }
});
