(function (app) {
    'use strict';

    app.controller('clientEditCtrl', clientEditCtrl);

    clientEditCtrl.$inject = ['$scope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function clientEditCtrl($scope, $modalInstance, $timeout, apiService, notificationService) {
        $scope.cancelEdit = cancelEdit;
        $scope.updateClient = updateClient;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datePicker = {};

        function updateClient() {
            apiService.post('/api/clients/update/', $scope.EditedClient,
                            updateClientCompleted,
                            updateClientLoadFailed);
        }

        function updateClientCompleted() {
            notificationService.displaySuccess($scope.EditedClient.FirstName + ' ' + $scope.EditedClient.LastName + ' has been updated.');
            $scope.EditedClient = {};
            $modalInstance.dismiss();
        }

        function updateClientLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            //$scope.isEdited = {};
            $modalInstance.dismiss();
        }

        function openDatePicker() {

        }
    }
})(angular.module('solistenManager'));