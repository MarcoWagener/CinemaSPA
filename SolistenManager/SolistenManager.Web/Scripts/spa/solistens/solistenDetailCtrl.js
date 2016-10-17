(function (app) {
    'use strict';

    app.controller('solistenDetailCtrl', solistenDetailCtrl);

    solistenDetailCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function solistenDetailCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-solistens';
        $scope.solisten = {};
        $scope.loadingSolisten = true;
        $scope.loadingRentals = true;
        $scope.isReadOnly = true;
        $scope.openRentDialog = openRentDialog;
        $scope.returnSolisten = returnSolisten;
        $scope.rentalHistory = [];
        $scope.getStatusColor = getStatusColor;
        $scope.clearSearch = clearSearch;
        $scope.isBorrowed = isBorrowed;

        function loadSolisten() {
            $scope.loadingSolisten = true;

            apiService.get('/api/solistens/details/' + $routeParams.id, null,
                solistenLoadCompleted,
                solistenLoadFailed);
        }

        function loadRentalHistory() {
            $scope.loadingRentals = true;

            apiService.get('/api/rentals/' + $routeParams.id + '/rentalhistory', null,
                rentalHistoryLoadCompleted,
                rentalHistoryLoadFailed);
        }

        function loadSolistenDetails() {
            loadSolisten();
            loadRentalHistory();
        }

        function returnSolisten(rentalID) {
            apiService.post('/api/rentals/return/' + rentalID, null,
                returnSolistenSucceeded,
                returnSolistenFailed);
        }

        function isBorrowed(rental) {
            return rental.Status == 'Borrowed';
        }

        function getStatusColor(status) {
            if (status == 'Borrowed')
                return 'red'
            else
                return 'green';
        }

        function clearSearch() {
            $scope.filterRentals = '';
        }

        function solistenLoadCompleted(result) {
            $scope.solisten = result.data;
            $scope.loadingSolisten = false;
        }

        function solistenLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function rentalHistoryLoadCompleted(result) {
            $scope.rentalHistory = result.data;
            $scope.loadingRentals = false;
        }

        function rentalHistoryLoadFailed(response) {
            notificationService.displayError(response);
        }

        function returnSolistenSucceeded(response) {
            notificationService.displaySuccess('Solisten returned successfully');
            loadSolistenDetails();
        }

        function returnSolistenFailed(response) {
            notificationService.displayError(response.data);
        }

        function openRentDialog() {
            $modal.open({
                templateUrl: 'scripts/spa/rental/rentSolistenModal.html',
                controller: 'rentSolistenCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                loadSolistenDetails();
            }, function() {
            });
        }

        loadSolistenDetails();
    }

})(angular.module('solistenManager'));