(function (app) {
    'use strict';

    app.directive('availableSolisten', availableSolisten);

    function availableSolisten() {
        return {
            restrict: 'E',
            templateUrl: "/Scripts/spa/directives/availableSolisten.html",
            link: function ($scope, $element, $attrs) {
                $scope.getAvailableClass = function () {
                    if ($attrs.isAvailable === 'true')
                        return 'label label-success'
                    else
                        return 'label label-danger'
                };

                $scope.getAvailability = function () {
                    if ($attrs.isAvailable === 'true')
                        return 'Available!'
                    else
                        return 'Not Available'
                };
            }
        }
    }

})(angular.module('common.ui'));