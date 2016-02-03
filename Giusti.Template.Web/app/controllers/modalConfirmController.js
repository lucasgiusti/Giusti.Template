app.controller('modalConfirmInstanceController', function ($scope, $modalInstance, dadosModalConfirm) {
    $scope.dadosModalConfirm = dadosModalConfirm;
    $scope.confirmar = function () {
        $modalInstance.close();
    };

    $scope.cancelar = function () {
        $modalInstance.dismiss('cancel');
    };
});