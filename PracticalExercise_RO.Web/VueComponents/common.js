export default {
    Constants: {
        GRIDSELECTIONLIMIT:3000,
        TIMEOUT1SEC: 1000,
        TIMEOUT2SEC: 2000,
        TIMEOUT5SEC: 5000,
        TIMEOUT: 2000,
        TIMEOUT500MS: 500,
        TIMEOUT100MS: 100,
        TIMEOUT50MS: 50
    },
    
    handleAjaxError: function (error) {
        if (error.request !== undefined) {
            if (error.request.status === 500) {
                window.location.href = '../Home/Error';
            }
            else if (error.request.status === 404) {
                window.location.href = '../Home/PageNotFound';
            }
            else if (error.request.status === 403) {
                window.location.href = '../Home/ForbiddenError';
            }
        }
    }
}
