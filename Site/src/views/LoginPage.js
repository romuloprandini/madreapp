import React, { Component } from 'react';

class LoginPage extends Component {
  render() {
    return (
      <div className="app flex-row align-items-center">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-md-8 col-lg-5">
              <div className="card-group mb-0">
                <div className="card card-inverse card-primary">
                  <div className="card-block">
                    <h1>Login</h1>
                    <p className="text-muted">Entre com os dados abaixo para acessar o MadreApp</p>
                    <div className="input-group mb-3">
                      <span className="input-group-addon"><i className="icon-user"></i></span>
                      <input type="text" className="form-control" placeholder="Email"/>
                    </div>
                    <div className="input-group mb-4">
                      <span className="input-group-addon"><i className="icon-lock"></i></span>
                      <input type="password" className="form-control" placeholder="Senha"/>
                    </div>
                    <div className="row">
                      <div className="col-6">
                        <button type="button" className="btn btn-primary active px-4">Entrar</button>
                      </div>
                      <div className="col-6 text-right">
                        <button type="button" className="btn btn-link text-muted px-0">Esqueceu sua senha?</button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default LoginPage;
