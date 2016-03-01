app.controller('signinController', function ($scope, $http, toasterAlert, UserService, $location) {

    var url = 'api/signin';

    $scope.heading = 'Login';
    $scope.navbarCollapsed = true;

    $scope.menus = UserService.getMenus();
    var user = UserService.getUser();
    if (user != null) {
        $scope.usuarioLogado = user.nome;
    }

    $scope.CookieDestroy = function () {
        UserService.setUser(null);
    }

    //APIs
    $scope.postLogin = function () {

        $http.post(url, $scope.usuario).success(function (data) {
            UserService.setUser(data);
            $scope.menus = UserService.getMenus();
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };
});