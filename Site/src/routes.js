import React from 'react';
import { Route, IndexRoute, Redirect } from 'react-router';

import App from './containers/App';
import Dashboard from './views/Dashboard';
import LigacaoPage from './views/LigacaoPage';
import LoginPage from './views/LoginPage';
import NotFoundPage from './views/NotFoundPage';

export default
    <div>
    //Aqui vem as rotas especificas (login, registrar, etc)
    <Route path="/login" name="Login" component={LoginPage} />
    // Aqui é a rota principal e ela redireciona para o dashboard por padrão
    <Redirect from='/' to='/dashboard' />
    <Route path="/" name="Principal" component={App}>
      // Aqui bem todas rotas normais
      <Route path="/dashboard" name="Dashboard" component={Dashboard} />
      <Route path="/ligacao" name="Ligações" component={LigacaoPage} />
    </Route>
    //Redireciona todas as rotas inválidas para a página de página não encontrada
    <Route path='*' component={NotFoundPage} />
</div>
