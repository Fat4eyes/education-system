const testingSystemUrl = 'http://www.web-test.ru/';

const apiUrl = 'api/';

const combineWithApiUrl = path => apiUrl + path;

const apiRoutes = {
  getAllUsers: combineWithApiUrl('users/all')
};

export {
  apiUrl,
  apiRoutes,
  testingSystemUrl
}