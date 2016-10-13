(function (app) {
    'use strict';

    app.controller('solistenCtrl', solistenCtrl);

    solistenCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function solistenCtrl($scope, apiService, notificationService) {

        $scope.pageClass = 'page-solistens';
        $scope.loadingSolistens = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Solistens = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        
        function search(page) {
            page = page || 0;

            $scope.loadingSolistens = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterSolistens
                }
            };

            apiService.get('/api/solistens/', config,
                            solistensLoadCompleted,
                            solistensLoadFailed);
        }

        function solistensLoadCompleted(result) {
            $scope.Solistens = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingSolistens = false;

            if ($scope.filterSolistens && $scope.filterSolistens.length) {
                notificationService.displayInfo(result.data.Items.length + ' solistens found');
            }
        }

        function solistensLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterSolistens = '';
            search();
        }

        $scope.search();
    }

})(angular.module('solistenManager'));