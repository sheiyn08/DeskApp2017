var modules = modules || [];
(function () {
    'use strict';
    modules.push('employee');

    angular.module('employee', ['ngRoute'])
    .controller('employee_list', ['$scope', '$http', function ($scope, $http) {
        $scope.isSearching = false;

        $scope.data = {};

        $http.get('/Api/employee/')
        .then(function (response) { $scope.data = response.data; });


        $http.get('/Api/lib_hiring_status/')
        .then(function (response) {

            // alert(JSON.stringify(response));

            $scope.hiring_status_id_options = response.data;
        });


        $http.get('/api/lib_region')
    .then(function (response) { $scope.region_code_options = response.data; });

        $http.get('/Api/lib_fund_source/')
.then(function (response) { $scope.fund_source_id_options = response.data; });

    }])



    .controller('employee_details', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {

        $http.get('/Api/employee/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });

    }])


    .controller('employee_create', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $scope.data = {};
        $http.get('/Api/lib_hiring_status/')
        .then(function (response) {

            // alert(JSON.stringify(response));

            $scope.hiring_status_id_options = response.data;
        });


        $http.get('/Api/lib_positions/')
       .then(function (response) {

           // alert(JSON.stringify(response));

           $scope.position_id_options = response.data;
       });

        $http.get('/Api/lib_regions/')
  .then(function (response) {

      // alert(JSON.stringify(response));

      $scope.region_code_options = response.data;
  });

        $scope.save = function () {
            $http.post('/Api/employee/', $scope.data)
            .then(function (response) { $location.path("employee"); });
        }

    }])
    .controller('employee_edit', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $http.get('/Api/employee/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });

        $http.get('/Api/lib_hiring_status/')
.then(function (response) { $scope.hiring_status_id_options = response.data; });

        $scope.save = function () {
            $http.put('/Api/employee/' + $routeParams.id, $scope.data)
            .then(function (response) { $location.path("employee"); });
        }

    }])
    .controller('employee_delete', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $http.get('/Api/employee/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });
        $scope.save = function () {
            $http.delete('/Api/employee/' + $routeParams.id, $scope.data)
            .then(function (response) { $location.path("employee"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
        .when('/employee', {
            title: 'employee - List',
            templateUrl: '/Static/employee_List',
            controller: 'employee_list'
        })
        .when('/employee/Create', {
            title: 'employee - Create',
            templateUrl: '/Static/employee_Edit',
            controller: 'employee_create'
        })
        .when('/employee/Edit/:id', {
            title: 'employee - Edit',
            templateUrl: '/Static/employee_Edit',
            controller: 'employee_edit'
        })
        .when('/employee/Delete/:id', {
            title: 'employee - Delete',
            templateUrl: '/Static/employee_Delete',
            controller: 'employee_delete'
        })
        .when('/employee/:id', {
            title: 'employee - Details',
            templateUrl: '/Static/employee_Details',
            controller: 'employee_details'
        })
    }])
    ;

})();
