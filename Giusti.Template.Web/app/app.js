﻿var app = angular.module('app', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ngCookies', 'toaster'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', { templateUrl: '/app/templates/home.html', controller: 'homeController' });
        $routeProvider.when('/signin', { templateUrl: '/app/templates/signin.html', controller: 'signinController' });
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

app.factory('UserService', function ($http, $window, $cookies, $location, toasterAlert) {
    return {
        getUser: function () {
            var user = $cookies.get('user');
            if (user) {
                return JSON.parse(user);
            }
        },
        setUser: function (newUser) {
            if (newUser) {
                var urlFuncionalidade = 'api/funcionalidade/GetForMenu';

                var headerAuth = { headers: { 'Authorization': 'Basic ' + newUser.token } };

                $http.get(urlFuncionalidade, headerAuth).success(function (data) {
                    newUser.menus = data;
                    $cookies.put('user', JSON.stringify(newUser));
                    $location.path('');
                }).error(function (jqxhr, textStatus) {
                    toasterAlert.showAlert(jqxhr.message);
                });
            }
            else {
                $cookies.put('user', null);
            }
        },
        getMenus: function () {
            var user = this.getUser();
            if (user) {
                return user.menus;
            }
            else {
                return [];
            }
        },
        verificaLogin: function () {
            var user = this.getUser();
            if (!user) {
               $location.path('\signin');
            }
        }
    };
});