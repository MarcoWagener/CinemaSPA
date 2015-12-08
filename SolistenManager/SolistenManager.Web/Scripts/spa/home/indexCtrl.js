(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function indexCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-home';
        $scope.loadingSolistens = true;
        $scope.isReadOnly = true; //Used for the rating directive.

        $scope.solistenList = [];
        $scope.loadData = loadData;

        function loadData() {
            apiService.get('/api/solistens/all', null,
                            solistenLoadCompleted,
                            solistenLoadFailed);
        }

        function solistenLoadCompleted(result) {
            $scope.solistenList = result.data;
            $scope.loadingSolistens = false;
        }

        function solistenLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        loadData();
    }

})(angular.module('solistenManager'));

//#region Some_Cool_Grid_Code

//function genresLoadCompleted(result) {
//    var genres = result.data;
//    Morris.Bar({
//        element: "genres-bar",
//        data: genres,
//        xkey: "Name",
//        ykeys: ["NumberOfMovies"],
//        labels: ["Number Of Movies"],
//        barRatio: 0.4,
//        xLabelAngle: 55,
//        hideHover: "auto",
//        resize: 'true'
//    });

//    $scope.loadingGenres = false;
//}

//#endregion