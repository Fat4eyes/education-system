export const authRoutes = {
  signIn: '/api/token/generate',
  check: '/api/token/check'
}

export const usersRoutes = {
  getInfo: '/api/users/current'
}

export const disciplineRoutes = {
  getDisciplines: `/api/disciplines`,
  getDiscipline: (id: number) => `/api/disciplines/${id}`,
  getDisciplineTests: (id: number) => `/api/disciplines/${id}/tests`,
  getDisciplineThemes: (id: number) => `/api/disciplines/${id}/themes`,
  updateDisciplineThemes: (id: number) => `/api/disciplines/${id}/themes`,
}

export const testRoutes = {
  getTests: `/api/tests`,
  getThemes: (id: number) => `/api/tests/${id}/themes`,
  add: '/api/tests'
}

export const themeRoutes = {
  add: '/api/themes',
  delete: (id: number) => `/api/themes/${id}`,
  update: (id: number) => `/api/themes/${id}`,
  getQuestions: (id: number) => `/api/themes/${id}/questions`
}

export const questionRoutes = {
  add: '/api/questions',
  update: (id: number) => `/api/questions/${id}`,
  get: (id: number) => `/api/questions/${id}`
}

export const imageRoutes = {
  add: () => `/api/images`,
  delete: (id: number) => `/api/images/${id}`,
  extensions: () => `/api/images/extensions`,
}

export const documentRoutes = {
  add: () => `/api/documents`,
  delete: (id: number) => `/api/documents/${id}`,
  extensions: () => `/api/documents/extensions`,
}

export const materialRoutes = {
  add: () => '/api/materials',
  update: (id: number) => `/api/materials/${id}`,
  get: (id: number) => `/api/materials/${id}`,
  getAll: () => `/api/materials`
}

export const studentRoutes = {
  getDisciplines: () => '/api/students/current/disciplines',
  getTestData: (testId: number) => `/api/students/current/tests/${testId}/data`,
  getTestExecution: (testId: number) => `/api/students/current/tests/${testId}/execution`,
}