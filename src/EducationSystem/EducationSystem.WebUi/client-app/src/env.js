const testingSystemUrl = 'http://www.web-test.ru/';

const apiUrl = 'http://localhost:5002/api/';

const combineWithApiUrl = path => apiUrl + path;

const apiRoutes = {
	getAllUsers: combineWithApiUrl('users/all')
};
	
export {
	apiUrl,
	apiRoutes,
	testingSystemUrl
}