app.controller('homeController', function ($scope, $window, $location, UserService) {
    UserService.verificaLogin();

    $scope.heading = "Giusti Template";
    $scope.message = "Sistema template desenvolvido em ASP.Net WebAPI, Angularjs e Bootstrap";
});