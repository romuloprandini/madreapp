import _ from "lodash";

import {
  LIGACAO_REQUEST,
  LIGACAO_RESPONSE,
  LIGACAO_CHECK
} from '../actions/ligacao';

const defaultState = {
  data: {},
  total: 0,
  current_page: 1,
  last_page: 1,
  news: 0
}

export default function(state = defaultState, action) {
  switch (action.type) {
    case LIGACAO_REQUEST:
      if(action.error) {
        return state;
      }

       action.ligacoes.data = _.keyBy(action.ligacoes.data, 'id');
      return {
        ...state,
        ...action.ligacoes
      };
    case LIGACAO_RESPONSE:
      if(action.error) {
        return state;
      }
      const data = {...state.data};
      data[action.ligacao.id]= action.ligacao;
    return {
      ...state,
      data: data
    }
    case LIGACAO_CHECK:
      if(action.error) {
        return state;
      }
      return {
        ...state,
        news: action.data.count
      }
  }

  return state;
}