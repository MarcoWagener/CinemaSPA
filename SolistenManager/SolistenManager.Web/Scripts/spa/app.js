(function () {
    'use strict';

    angular.module('solistenManager', ['common.core', 'common.ui'])
        .config(config);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                controller: "indexCtrl"
            })
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            })
            .when("/clients", {
                templateUrl: "scripts/spa/clients/clients.html",
                controller: "clientsCtrl"
            })
            .when("/clients/register", {
                templateUrl: "scripts/spa/clients/register.html",
                controller: "clientRegCtrl"
            })
            .when("/solisten", {
                templateUrl: "scripts/spa/solistens/solistens.html",
                controller: "solistenCtrl"
            })
            .when("/solisten/add", {
                templateUrl: "scripts/spa/solisten/add.html",
                controller: "solistenAddCtrl"
            })
            .when("/solisten/:id", {
                templateUrl: "scripts/spa/solistens/details.html",
                controller: "solistenDetailCtrl"
            })
            .when("/solisten/edit/:id", {
                templateUrl: "scripts/spa/solisten/edit.html",
                controller: "solistenEditCtrl"
            })
            .when("/rental", {
                templateUrl: "scripts/spa/rental/rental.html",
                controller: "rentStatsCtrl"
            }).otherwise({ redirectTo: "/" });
    }
})();