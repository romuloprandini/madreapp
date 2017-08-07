export const LOADING = 'LOADING'
export const SHOW_MESSAGE = 'SHOW_MESSAGE'

export function loading(status = true) {
  return {
    type: LOADING,
    isLoading: status
  };
}

export function showMessage(message= '', title = '', message_type = 'default') {
  return {
    type: SHOW_MESSAGE,
    message,
    title,
    message_type
  };
}