const authRoutes = {
  signIn: '/api/token/generate',
  check: '/api/token/check'
};

const usersReutes = {
  getInfo: '/api/users/current'
};

const accountRoutes = {
  getInfo: id => `/api/account/getinfo/${id}`
};


export {
  authRoutes,
  usersReutes,
  accountRoutes
}