

var app = angular.module('psgc-codes',[])

.controller('AppCtrl', function ($scope, $mdDialog, $http) {


    $scope.$watch('data.region_code', function (newValue, oldValue) {



        var record = $scope;

        if (newValue == null) {
            record.prov_code = '';
            record.city_code = '';
            record.brgy_code = '';


            record.prov_code_options = [];
            record.city_code_options = [];
            record.brgy_code_options = [];
        }
        else {

            $http({
                method: 'GET',
                url: '/api/lib_province?id=' + newValue,

            }).success(function (data, status, headers, config) {

                $scope.prov_code_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });

    $scope.$watch('data.prov_code', function (newValue, oldValue) {

        var record = $scope;

        if (newValue == null) {

            record.brgy_code = '';
            record.city_code = '';
            record.city_code_options = [];
            record.brgy_code_options = [];
        }

        else {
            $http({
                method: 'GET',
                url: '/api/lib_city?id=' + newValue,

            }).success(function (data, status, headers, config) {

                $scope.city_code_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });

    $scope.$watch('data.city_code', function (newValue, oldValue) {

        var record = $scope;

        if (newValue == null) {

            record.brgy_code = '';

            record.brgy_code_options = [];
        }

        else {
            $http({
                method: 'GET',
                url: '/api/lib_brgy?id=' + newValue,

            }).success(function (data, status, headers, config) {

                $scope.brgy_code_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });

})