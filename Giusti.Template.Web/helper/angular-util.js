app.filter('inicioPaginacao', function () {
    return function (input, start) {
        if (!angular.isArray(input)) {
            return [];
        }
        start = +start; //parse to int
        return input.slice(start);
    };
});

app.filter('Situacao', function () {
    return function (input) {
        if (input == '0') {
            return 'Ativo';
        }
        else if (input == '1') {
            return 'Inativo';
        }
    };
});

app.directive('toasterTemplateHtml', [function () {
    return {
        template: "<div ng-show='directiveData'><div ng-repeat='item in directiveData.Messages'>{{item.Message}}</div></div>"
    };
}])

app.factory('toasterAlert', function (toaster) {
    return {
        showAlert: function (alert) {
            try {
                var alert = JSON.parse(alert);
                var typeAlert = alert.Success ? "info" : "error";
                toaster.pop({
                    type: typeAlert,
                    body: 'toaster-template-html',
                    bodyOutputType: 'directive',
                    directiveData: alert
                });
            } catch (e) {
                var alert = { Sucess: "error", Messages: [{ Message: alert }] };
                toaster.pop({
                    type: "error",
                    body: 'toaster-template-html',
                    bodyOutputType: 'directive',
                    directiveData: alert
                });
            }
        }
    }
});