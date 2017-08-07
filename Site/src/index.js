import React from 'react'
import { render } from 'react-dom'
import { browserHistory } from 'react-router'
//import { hashHistory } from 'react-router'
import { syncHistoryWithStore } from 'react-router-redux'

// Containers
import Root from './containers/Root'
import configureStore from './store/configureStore'

const store = configureStore()
const history = syncHistoryWithStore(browserHistory, store)
//const history = syncHistoryWithStore(hashHistory, store)

render(
  <Root store={store} history={history} />,
  document.getElementById('root'))
