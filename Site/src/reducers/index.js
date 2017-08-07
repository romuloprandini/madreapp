import merge from 'lodash/merge'
import { routerReducer as routing } from 'react-router-redux'
import { combineReducers } from 'redux'

import ligacaoReducer from './ligacao'
import { LOADING, SHOW_MESSAGE } from '../actions'


function loadingReducer(state = false, action) {
  switch(action.type)
  {
    case LOADING:
      return action.isLoading
    default:
      return state
  }
}

function messageReducer(state = {}, action) {
  switch(action.type)
  {
    case SHOW_MESSAGE:
      return { title: action.title, message: action.message, type: action.message_type };
    default:
      return state
  }
}

const rootReducer = combineReducers({
  ligacoes: ligacaoReducer,
  isLoading: loadingReducer,
  message: messageReducer,
  routing,
})

export default rootReducer