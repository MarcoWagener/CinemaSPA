(function (app) {
    'use strict';

    app.controller('clientsCtrl', clientsCtrl);

    clientsCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];

    function clientsCtrl($scope, $modal, apiService, notificationService) {

        $scope.pageClass = 'page-clients';
        $scope.loadingClients = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Clients = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function search(page) {
            page = page || 0;

            $scope.loadingClients = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterClients
                }
            };

            apiService.get('/api/clients/search/', config,
                            clientsLoadCompleted,
                            clientsLoadFailed);
        }

        function openEditDialog(client) {
            console.log(client);
        }

        function clientsLoadCompleted(result) {
            $scope.Clients = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingClients = false;

            if ($scope.filterClients && $scope.filterClients.length) {
                notificationService.displayInfo(result.data.Items.length + ' clients found');
            }
        }

        function clientsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterClients = '';
            search();
        }

        $scope.search();
    }
})(angular.module('solistenManager'));