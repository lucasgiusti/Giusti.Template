var app = angular.module('app', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ngCookies'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/usuario', { templateUrl: '/templates/usuario/usuarios.html', controller: 'usuarioController' });
        $locationProvider.html5Mode(true);
    });