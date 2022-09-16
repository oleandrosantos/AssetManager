const baseURL = 'https://localhost:7108/api/';

function getHeaders() {
  const token = localStorage.getItem('token');
  return {
    'Content-Type': 'application/json',
    'accept': '*/*',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept',
    ...token && {
      'Authorization': `Bearer ${token}`
    }
  };
}

async function request(method, url, body) {
  const options = {
    method,
    headers: getHeaders(),
    ...(method !== 'GET') && {
      body: JSON.stringify(body)
    }
  };
  const result = await fetch(baseURL + url, options)
    .then((response) => {
      if (!response.ok) {
        return response.json().then(text => { throw new Error(text.message) })
      }
      return response.json();
    });

  return result;
}

export { request as default, request, getHeaders }