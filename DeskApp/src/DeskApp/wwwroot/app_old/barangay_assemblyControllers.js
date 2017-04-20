 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('barangay_assembly');

    angular.module('barangay_assembly',['ngRoute'])
    .controller('barangay_assembly_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/barangay_assembly/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('barangay_assembly_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/barangay_assembly/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('barangay_assembly_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
                $http.get('/Api/lib_approval/')
        .then(function(response){$scope.approval_id_options = response.data;});
                $http.get('/Api/lib_barangay_assembly_purpose/')
        .then(function(response){$scope.barangay_assembly_purpose_id_options = response.data;});
                $http.get('/Api/lib_brgy/')
        .then(function(response){$scope.brgy_code_options = response.data;});
                $http.get('/Api/lib_city/')
        .then(function(response){$scope.city_code_options = response.data;});
                $http.get('/Api/lib_cycle/')
        .then(function(response){$scope.cycle_id_options = response.data;});
                $http.get('/Api/lib_enrollment/')
        .then(function(response){$scope.enrollment_id_options = response.data;});
                $http.get('/Api/lib_fund_source/')
        .then(function(response){$scope.fund_source_id_options = response.data;});
                $http.get('/Api/lib_province/')
        .then(function(response){$scope.prov_code_options = response.data;});
                $http.get('/Api/lib_push_status/')
        .then(function(response){$scope.push_status_id_options = response.data;});
                $http.get('/Api/lib_region/')
        .then(function(response){$scope.region_code_options = response.data;});
        
        $scope.save = function(){
            $http.post('/Api/barangay_assembly/', $scope.data)
            .then(function(response){ $location.path("barangay_assembly"); });
        }

    }])
    .controller('barangay_assembly_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/barangay_assembly/' + $routeParams.id)
        .then(function (response) {
            $scope.data = response.data;


            $scope.$watch('data.region_code', function (newValue, oldValue) {

                var record = $scope.data;

                if (newValue == null) {
                    record.prov_code = null;
                    record.city_code = null;
                    record.brgy_code = null;


                    record.prov_code_options = [];
                    record.city_code_options = [];
                    record.brgy_code_options = [];
                }
                else {

                    $http({
                        method: 'GET',
                        url: '/api/lib_province?id=' + record.region_code,

                    }).success(function (data, status, headers, config) {

                        $scope.prov_code_options = data;
                    }).error(function (data, status, headers, config) {

                        $scope.message = 'Unexpected Error';
                    });
                }

            });

            $scope.$watch('data.prov_code', function (newValue, oldValue) {

                var record = $scope.data;

                if (newValue == null) {

                    record.brgy_cod = null;
                    record.city_code = null;
                    record.city_code_options = [];
                    record.brgy_code_options = [];
                }

                else {
                    $http({
                        method: 'GET',
                        url: '/api/lib_city?id=' + record.prov_code,

                    }).success(function (data, status, headers, config) {

                        $scope.city_code_options = data;
                    }).error(function (data, status, headers, config) {

                        $scope.message = 'Unexpected Error';
                    });
                }

            });

            $scope.$watch('data.city_code', function (newValue, oldValue) {

                var record = $scope.data;

                if (newValue == null) {

                    record.brgy_code = null;

                    record.brgy_code_options = [];
                }

                else {
                    $http({
                        method: 'GET',
                        url: '/api/lib_brgy?id=' + record.city_code,

                    }).success(function (data, status, headers, config) {

                        $scope.brgy_code_options = data;
                    }).error(function (data, status, headers, config) {

                        $scope.message = 'Unexpected Error';
                    });
                }

            });

            $scope.$watch('data.fund_source_id', function (newValue, oldValue) {

                var record = $scope.data;

                if (newValue == null) {

                    record.cycle_id = null;

                    record.cycle_id_options = [];
                }

                else {
                    $http({
                        method: 'GET',
                        url: '/api/lib_cycle?id=' + record.fund_source_id,

                    }).success(function (data, status, headers, config) {

                        $scope.cycle_id_options = data;
                    }).error(function (data, status, headers, config) {

                        $scope.message = 'Unexpected Error';
                    });
                }

            });


            $http.get('/api/lib_region')
        .then(function (response) { $scope.region_code_options = response.data; });


            $http.get('/api/lib_barangay_assembly_purpose')
 .then(function (response) { $scope.barangay_assembly_purpose_id_options = response.data; });

            



            $http.get('/api/lib_province?id=' + response.data.region_code)
        .then(function (response) { $scope.prov_code_options = response.data; });


            $http.get('/Api/lib_city?id=' + response.data.prov_code)
        .then(function (response) { $scope.city_code_options = response.data; });

            $http.get('/Api/lib_brgy?id=' + response.data.city_code)
        .then(function (response) { $scope.brgy_code_options = response.data; });

            $http.get('/Api/lib_fund_source/')
    .then(function (response) { $scope.fund_source_id_options = response.data; });

            $http.get('/Api/lib_cycle?id=' + response.data.fund_source_id)
    .then(function (response) { $scope.cycle_id_options = response.data; });


        });

               
        $scope.save = function(){
            $http.put('/Api/barangay_assembly/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("barangay_assembly"); });
        }

    }])
    .controller('barangay_assembly_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/barangay_assembly/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/barangay_assembly/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("barangay_assembly"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/barangay_assembly', {
                title: 'barangay_assembly - List',
                templateUrl: '/Static/barangay_assembly_List',
                controller: 'barangay_assembly_list'
            })
            .when('/barangay_assembly/Create', {
                title: 'barangay_assembly - Create',
                templateUrl: '/Static/barangay_assembly_Edit',
                controller: 'barangay_assembly_create'
            })
            .when('/barangay_assembly/Edit/:id', {
                title: 'barangay_assembly - Edit',
                templateUrl: '/Static/barangay_assembly_Edit',
                controller: 'barangay_assembly_edit'
            })
            .when('/barangay_assembly/Delete/:id', {
                title: 'barangay_assembly - Delete',
                templateUrl: '/Static/barangay_assembly_Delete',
                controller: 'barangay_assembly_delete'
            })
            .when('/barangay_assembly/:id', {
                title: 'barangay_assembly - Details',
                templateUrl: '/Static/barangay_assembly_Details',
                controller: 'barangay_assembly_details'
            })
    }])
;

})();
