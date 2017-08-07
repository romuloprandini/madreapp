import { createStore, applyMiddleware } from 'redux'
import thunk from 'redux-thunk'
import ReduxPromise from "redux-promise";

import rootReducer from '../reducers'

const configureStore = preloadedState => createStore(
  rootReducer,
  preloadedState,
  applyMiddleware(ReduxPromise, thunk)
)

export default configureStore