const authRoutes = {
  signIn: '/api/token/generate',
  check: '/api/token/check'
};

const usersRoutes = {
  getInfo: '/api/users/current'
};

const accountRoutes = {
  getInfo: id => `/api/account/getinfo/${id}`
};


const disciplineRoutes = {
  getDisciplines: `/api/disciplines`,
  getDiscipline: id => `/api/disciplines/${id}`,
  getDisciplineTests: id => `/api/disciplines/${id}/tests`,
  getDisciplineThemes: id => `/api/disciplines/${id}/themes`
};

const testRoutes = {
  getTests: `/api/tests`,
  getThemes: id => `/api/tests/${id}/themes`,
  add: '/api/tests'
};

export {
  authRoutes,
  usersRoutes,
  accountRoutes,
  disciplineRoutes,
  testRoutes
}