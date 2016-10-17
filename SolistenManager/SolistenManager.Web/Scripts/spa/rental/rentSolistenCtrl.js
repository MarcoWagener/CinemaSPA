(function (app) {
    'use strict';


    app.controller('rentSolistenCtrl', rentSolistenCtrl);

    rentSolistenCtrl.$inject = ['$scope', '$modalInstance', '$location', 'apiService', 'notificationService'];

    function rentSolistenCtrl($scope, $modalInstance, $location, apiService, notificationService) {
        $scope.SerialNumber = $scope.solisten.SerialNumber;
        $scope.loadStockItems = loadStockItems;
        $scope.selectClient = selectClient;
        $scope.selectionChanged = selectionChanged;
        $scope.rentSolisten = rentSolisten;
        $scope.cancelRental = cancelRental;
        $scope.stockItems = [];
        $scope.selectedClient = -1;
        $scope.isEnabled = false;

        function loadStockItems() {
            notificationService.displayInfo('Loading available stock items for ' + $scope.solisten.SerialNumber);

            apiService.get('/api/stocks/solisten/' + $scope.solisten.ID, null,
                stockItemsLoadCompleted,
                stockItemsLoadFailed);
        }

        function stockItemsLoadCompleted(response) {
            $scope.stockItems = response.data;
            $scope.selectedStockItem = $scope.stockItems[0].ID;
        }

        function stockItemsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function rentSolisten() {
            apiService.post('/api/rentals/rent/' + $scope.selectedClient + '/' + $scope.selectedStockItem, null,
                rentSolistenSucceeded,
                rentSolistenFailed);
        }

        function rentSolistenSucceeded(response) {
            notificationService.displaySuccess('Rental completed successfully');
            $modalInstance.close();
        }

        function rentSolistenFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        function cancelRental() {
            $scope.stockItems = [];
            $scope.selectedClient = -1;
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

        function selectClient($item) {
            if ($item) {
                $scope.selectedClient = $item.originalObject.ID;
                $scope.isEnabled = true;
            }
            else {
                $scope.selectedClient = -1;
                $scope.isEnabled = false;
            }
        }

        function selectionChanged($item) {

        }

        loadStockItems();
    }
})(angular.module('solistenManager'));