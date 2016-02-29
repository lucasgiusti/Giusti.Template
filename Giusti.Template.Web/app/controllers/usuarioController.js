app.controller('usuarioController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams) {

    var mensagemExcluir = 'Deseja realmente excluir o usuário [NOMEUSUARIO] ?';
    var mensagemSalvo = JSON.stringify({ Success: "info", Messages: [{ Message: 'Usuário salvo com sucesso.' }] });
    $scope.heading = 'Usuários';
    $scope.usuarios = [];
    $scope.usuario = null;

    //GET API
    var url = 'api/usuario';
    $scope.getUsuarios = function () {

        $http.get(url).success(function (data) {
            $scope.usuarios = data;
            $scope.total = $scope.usuarios.length;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    };

    // GET API
    $scope.getUsuario = function () {
        if (!angular.isUndefined($routeParams.id)) {
            $scope.id = $routeParams.id;
        }

        $http.get(url + '/' + $scope.id).success(function (data) {
            $scope.usuario = data;
            $scope.getPerfis();
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    //POST API
    $scope.postUsuario = function () {
        $scope.preenchePerfisUsuario();

        $http.post(url, JSON.stringify($scope.usuario)).success(function (id) {
            $scope.id = id;
            $scope.getUsuario();
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    //PUT API
    $scope.putUsuario = function () {
        $scope.preenchePerfisUsuario();

        $http.put(url + '/' + $scope.id, JSON.stringify($scope.usuario)).success(function (data) {
            $scope.usuario = data;
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    //DELETE API
    $scope.deleteUsuario = function () {

        $http.delete(url + '/' + $scope.usuario.id).success(function (result) {
            toasterAlert.showAlert(result);
            $scope.usuarios.splice($scope.usuarios.indexOf($scope.usuario), 1);
        }).error(function (result) {
            toasterAlert.showAlert(result);
        });
    };

    //MODAL DELETE
    $scope.openModalDelete = function (usuario) {
        $scope.usuario = usuario;
        $scope.dadosModalConfirm = { 'titulo': 'Excluir', 'mensagem': mensagemExcluir.replace('[NOMEUSUARIO]', $scope.usuario.nome) };

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'app/templates/modalConfirm.html',
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

    //ADD USUARIO
    $scope.addUsuario = function () {
        $scope.usuario = { ativo: 1 };
        $scope.getPerfis();
    };


    $scope.preenchePerfisUsuario = function () {
        $scope.usuario.perfis = [];
        angular.forEach($scope.perfisDisponiveis, function (perfil, key) {
            if (perfil.usuarioPossui) {
                $scope.usuario.perfis.push({ perfilId: perfil.id });
            }
        });
    }

    var urlPerfil = 'api/perfil';
    $scope.getPerfis = function () {

        $http.get(urlPerfil).success(function (data) {
            $scope.perfisDisponiveis = data;

            angular.forEach(data, function (perfil, key) {

                perfil.usuarioPossui = false;
                angular.forEach($scope.usuario.perfis, function (perfilUsuario, key) {
                    if (perfilUsuario.perfilId == perfil.id) {
                        perfil.usuarioPossui = true;
                    }
                });
            });



        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    }

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
