import React, { Component } from 'react';
import { Link } from 'react-router';

class NotFoundPage extends Component {

  render() {
    return (
      <div className="app flex-row align-items-center">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-md-10 col-lg-8">
              <div className="card-group mb-0">
                <div className="card card-inverse card-primary">
                  <div className="card-block text-center">
                    <h1>Página não encontrada</h1>
                    <p className="text-muted text-big">A página que você quer acessar não existe ou alguém removeu ela</p>
                    <p className="text-muted text-big"><Link to="/" className="text-white">Clique aqui</Link> para ir para a página principal</p>
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

export default NotFoundPage;
