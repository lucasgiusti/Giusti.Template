var app = angular.module('app', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ngCookies', 'toaster'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/perfil', { templateUrl: '/app/templates/perfil/perfis.html', controller: 'perfilController' });
        $routeProvider.when('/perfil/add', { templateUrl: '/app/templates/perfil/perfil-add.html', controller: 'perfilController' });
        $routeProvider.when('/perfil/:id/edit', { templateUrl: '/app/templates/perfil/perfil-edit.html', controller: 'perfilController' });
        $routeProvider.when('/perfil/:id', { templateUrl: '/app/templates/perfil/perfil-view.html', controller: 'perfilController' });
        $routeProvider.when('/usuario', { templateUrl: '/app/templates/usuario/usuarios.html', controller: 'usuarioController' });
        $routeProvider.when('/usuario/add', { templateUrl: '/app/templates/usuario/usuario-add.html', controller: 'usuarioController' });
        $routeProvider.when('/usuario/:id/edit', { templateUrl: '/app/templates/usuario/usuario-edit.html', controller: 'usuarioController' });
        $routeProvider.when('/usuario/:id', { templateUrl: '/app/templates/usuario/usuario-view.html', controller: 'usuarioController' });
        $locationProvider.html5Mode(true);
    });