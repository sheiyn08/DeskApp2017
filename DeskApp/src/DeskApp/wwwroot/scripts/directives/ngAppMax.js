var app = angular.module('app-max', []);

app.directive('ngAppMax', function () {
    return {
        require: 'ngModel',
        scope: {
            ngAppMax: '@',
            ngModel: '='
        },
        link: function (scope, element, attrs, ctrl) {

            scope.$watch('ngAppMax', function (newVal) {
                validate(scope.ngModel, newVal, ctrl);
            });

            scope.$watch('ngModel', function (val) {
                validate(val, scope.ngAppMax);
            });

            function validate(thisVal, maxVal) {
                if (thisVal > maxVal) {
                    ctrl.$setValidity('range', false);
                } else {
                    ctrl.$setValidity('range', true);
                }
            }

        }
    }
});