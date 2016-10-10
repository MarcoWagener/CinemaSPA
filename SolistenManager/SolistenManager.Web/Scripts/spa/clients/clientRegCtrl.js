(function (app) {
    'use-strict';

    app.controller('clientRegCtrl', clientRegCtrl);

    clientRegCtrl.$inject = ['$scope', '$location', '$rootScope', 'apiService', 'notificationService'];

    function clientRegCtrl($scope, $location, $rootScope, apiService, notificationService) {
        $scope.newClient = {};
        $scope.Register = Register;

        function Register() {
            apiService.post('/api/clients/register', $scope.newClient,
                registerClientSucceded,
                registerClientFailed);
        }

        function registerClientSucceded(response) {
            notificationService.displaySuccess($scope.newClient.LastName + ' has been successfully registered.');
            $scope.newClient = {};
        }

        function registerClientFailed(response) {
            if (response.status == '400')
                notificationService.displayError(response.data);
            else
                notificationService.displayError(response.statusText);
        }
    }
})(angular.module('solistenManager'));