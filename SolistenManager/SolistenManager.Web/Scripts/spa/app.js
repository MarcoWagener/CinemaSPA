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
            .when("/client", {
                templateUrl: "scripts/spa/client/client.html",
                controller: "clientCtrl"
            })
            .when("/client/register", {
                templateUrl: "scripts/spa/client/register.html",
                controller: "clientRegCtrl"
            })
            .when("/solisten", {
                templateUrl: "scripts/spa/solisten/solisten.html",
                controller: "solistenCtrl"
            })
            .when("/solisten/add", {
                templateUrl: "scripts/spa/solisten/add.html",
                controller: "solistenAddCtrl"
            })
            .when("/solisten/:id", {
                templateUrl: "scripts/spa/solisten/details.html",
                controller: "solistenDetailsCtrl"
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