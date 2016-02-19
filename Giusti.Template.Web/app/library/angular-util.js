app.filter('inicioPaginacao', function () {
    return function (input, start) {
        if (!angular.isArray(input)) {
            return [];
        }
        start = +start; //parse to int
        return input.slice(start);
    };
});

app.filter('SituacaoString', function () {
    return function (input) {
        if (input == '1') {
            return 'Ativo';
        }
        else if (input == '0') {
            return 'Inativo';
        }
    };
});

app.filter('SituacaoBool', function () {
    return function (input) {
        if (input == '0') {
            return false;
        }
        else if (input == '1') {
            return true;
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
                this.show(alert);
            } catch (e) {
                var alert = { Sucess: "error", Messages: [{ Message: alert }] };
                this.show(alert);
            }
        },
        show: function(alert){
        toaster.pop({
            type: alert.Success ? "info" : "error",
            body: 'toaster-template-html',
            bodyOutputType: 'directive',
            directiveData: alert
            });
        }
    }
});