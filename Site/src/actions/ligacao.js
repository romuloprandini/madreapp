import _ from 'lodash'
import axios from 'axios'

import { loading, showMessage } from './index'

export const LIGACAO_REQUEST = 'LIGACAO_REQUEST'
export const LIGACAO_RESPONSE = 'LIGACAO_RESPONSE'
export const LIGACAO_CHECK = 'LIGACAO_CHECK'

const API_URL = 'http://104.154.65.154/api';

export function fetchLigacoes(search = null, page = 1) {
  const request = axios.get(`${API_URL}/ligacao?search=${search}&page=${page}`);
  return dispatch => {
    dispatch(loading(true))
    return request
      .then(json => {
        dispatch({
        type: LIGACAO_REQUEST,
        ligacoes: json.data
      })
      dispatch(loading(false))
      })
      .catch(() => {
        dispatch(showMessage('Não foi possível listar as ligações', 'Erro', 'error'));
        dispatch(loading(false))
      });
  }
}

export function getLigacao(id) {
  const request = axios.get(`${API_URL}/ligacao/${id}`);
  return {
    type: LIGACAO_REQUEST,
    payload: request
  };
}

export function responderLigacao(id, retorno) {
    const request = axios.put(`${API_URL}/ligacao/${id}`, {retorno});
    return dispatch => {
    return request
      .then(json => {
        dispatch({
          type: LIGACAO_RESPONSE,
          ligacao: json.data
        })
      })
      .catch(() => {
        dispatch(showMessage('Não foi possível salvar o retorno', 'Erro', 'error'));
        dispatch({
          type: LIGACAO_RESPONSE,
          error: true
        })
      });
  }
}

export function checkNewLigacoes(last_time) {
  const formated_date = last_time.getTime();
  const request = axios.get(`${API_URL}/ligacao/refresh?last_time=${formated_date}`);
  return dispatch => {
    return request
      .then(json => {
        dispatch({
        type: LIGACAO_CHECK,
        data: json.data
      })
      })
      .catch(() => {
        dispatch(showMessage('Não foi possível checar novas ligações', 'Erro', 'error'));
      });
  }
}

export function cleanNewLigacoes() {
  return {
        type: LIGACAO_CHECK,
        data: { count: 0 }
      }
}