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