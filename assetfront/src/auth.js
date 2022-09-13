import decode from 'jwt-decode';
import request from './request';

export async function signIn(email, password) {
  console.log('signin!');
  const { token } = await request('POST', 'User/Login', {
    email,
    password
  });
  console.log(token);
  //localStorage.set('token', token);
}

export function signOut() {
  localStorage.removeItem('token');
}

export function isSignedIn() {
  const token = localStorage.getItem('token');

  if (!token)     // Se não existe o token no LocalStorage
    return false; // significa que o usuário não está assinado.

  try {
    const { exp: expiration } = decode(token);
    const isExpired = !!expiration && Date.now() > expiration * 1000;

    if (isExpired)  // Se o token tem uma data de expiração e
      return false; // essa data é menor que a atual o usuário
    // não está mais assinado.
    return true;
  } catch (_) {   // O "jwt-decode" lança erros pra tokens inválidos.
    return false; // Com um token inválido o usuário não está assinado.
  }
}