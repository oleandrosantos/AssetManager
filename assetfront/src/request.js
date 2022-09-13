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
  const response = await fetch(baseURL + url, options);
  console.log("resposata", response);
  console.log("em js", response.json());
  return response.json();
}

export { request as default, request, getHeaders }