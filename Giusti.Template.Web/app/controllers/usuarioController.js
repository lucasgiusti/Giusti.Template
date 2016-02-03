app.controller('usuarioController', function ($scope, $http, $window, $location, $uibModal, $routeParams) {

    var mensagemExcluir = 'Deseja realmente excluir o usuario [NOMEUSUARIO] ?';
    var mensagemSalvo = 'Usuário salvo com sucesso.';
    $scope.heading = 'Usuários';
    $scope.alerts = [];
    $scope.usuarios = [];

    //GET API
    var url = 'api/usuario';
    $scope.getUsuarios = function () {

        $http.get(url).success(function (data) {
            $scope.usuarios = data;
            $scope.total = $scope.usuarios.length;
        }).error(function (jqxhr, textStatus) {
            $scope.showAlert(jqxhr.message);
        })
    };

    // GET API
    $scope.getUsuario = function () {
        if (!angular.isUndefined($routeParams.id)) {
            $scope.id = $routeParams.id;
        }

        $http.get(url + '/' + $scope.id).success(function (data) {
            $scope.usuario = data;
        }).error(function (jqxhr, textStatus) {
            $scope.showAlert(jqxhr.message);
        });
    };

    //POST API
    $scope.postUsuario = function () {

        $http.post(url, JSON.stringify($scope.usuario)).success(function (id) {
            $scope.id = id;
            $scope.getUsuario();
            $scope.showAlert(JSON.stringify([{ type: "info", msgs: [{ msg: mensagemSalvo }] }]));
        }).error(function (jqxhr, textStatus) {
            $scope.showAlert(jqxhr.message);
        });
    };

    //PUT API
    $scope.putUsuario = function () {

        $http.put(url + '/' + $scope.id, JSON.stringify($scope.usuario)).success(function (data) {
            $scope.usuario = data;
            $scope.showAlert(JSON.stringify([{ type: "info", msgs: [{ msg: mensagemSalvo }] }]));
        }).error(function (jqxhr, textStatus) {
            $scope.showAlert(jqxhr.message);
        });
    };

    //DELETE API
    $scope.deleteUsuario = function () {

        $http.delete(url + '/' + $scope.usuario.codigo).success(function (result) {
            $scope.showAlert(result);
            $scope.usuarios.splice($scope.usuarios.indexOf($scope.usuario), 1);
        }).error(function (result) {
            $scope.showAlert(result);
        });
    };

    //ADD USUARIO
    $scope.addUsuario = function () {
        $scope.usuario = { codigo: null, perfils: [], ativo: true };
    };

    //MODAL DELETE
    $scope.openModalDelete = function (usuario) {
        $scope.usuario = usuario;
        $scope.dadosModalConfirm = { 'titulo': 'Excluir', 'mensagem': mensagemExcluir.replace('[NOMEUSUARIO]', $scope.usuario.nome) };

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/templates/modalConfirm.html',
            controller: 'modalConfirmInstanceController',
            resolve: {
                dadosModalConfirm: function () {
                    return $scope.dadosModalConfirm;
                }
            }
        });
        modalInstance.result.then(function () {
            $scope.deleteUsuario();
        });
    };

    //ALERT
    $scope.closeAlert = function () {
        $scope.alerts = [];
    };
    $scope.showAlert = function (alerts) {
        try {
            $scope.alerts = JSON.parse(alerts);
        } catch (e) {
            $scope.alerts = [{ type: "danger", msgs: [{ msg: alerts }] }];
        }
    };

    //PAGINATION
    $scope.total = 0;
    $scope.currentPage = 1;
    $scope.itemPerPage = 5;
    $scope.start = 0;
    $scope.maxSize = 5;
    $scope.pageChanged = function () {
        $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage;
    };
});
