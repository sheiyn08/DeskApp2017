 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('beneficiary_profile');

    angular.module('beneficiary_profile',['ngRoute'])
    .controller('beneficiary_profile_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/beneficiary_profile/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('beneficiary_profile_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/beneficiary_profile/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('beneficiary_profile_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
                $http.get('/Api/lib_approval/')
        .then(function(response){$scope.approval_id_options = response.data;});
                $http.get('/Api/lib_lgu_position/')
        .then(function(response){$scope.lgu_position_id_options = response.data;});
                $http.get('/Api/lib_brgy/')
        .then(function(response){$scope.brgy_code_options = response.data;});
                $http.get('/Api/lib_city/')
        .then(function(response){$scope.city_code_options = response.data;});
                $http.get('/Api/lib_civil_status/')
        .then(function(response){$scope.civil_status_id_options = response.data;});
                $http.get('/Api/lib_education_attainment/')
        .then(function(response){$scope.education_attainment_id_options = response.data;});
                $http.get('/Api/lib_ip_group/')
        .then(function(response){$scope.ip_group_id_options = response.data;});
                $http.get('/Api/lib_occupation/')
        .then(function(response){$scope.occupation_id_options = response.data;});
                $http.get('/Api/lib_province/')
        .then(function(response){$scope.prov_code_options = response.data;});
                $http.get('/Api/lib_push_status/')
        .then(function(response){$scope.push_status_id_options = response.data;});
                $http.get('/Api/lib_region/')
        .then(function(response){$scope.region_code_options = response.data;});
        
        $scope.save = function(){
            $http.post('/Api/beneficiary_profile/', $scope.data)
            .then(function(response){ $location.path("beneficiary_profile"); });
        }

    }])
    .controller('beneficiary_profile_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/beneficiary_profile/' + $routeParams.id)
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

            $http.get('/api/lib_province?id=' + response.data.region_code)
        .then(function (response) { $scope.prov_code_options = response.data; });


            $http.get('/Api/lib_city?id=' + response.data.prov_code)
        .then(function (response) { $scope.city_code_options = response.data; });

  
            $http.get('/Api/lib_brgy?id=' + response.data.city_code)
        .then(function (response) { $scope.brgy_code_options = response.data; });

            $http.get('/api/lib_civil_status')
   .then(function (response) { $scope.civil_status_id_options = response.data; });

            $http.get('/api/lib_indigenous_groups')
   .then(function (response) { $scope.ip_group_id_options = response.data; });


            $http.get('/api/lib_education_attainment')
   .then(function (response) { $scope.education_attainment_id_options = response.data; });

            $http.get('/api/lib_lgu_position')
.then(function (response) { $scope.lgu_position_id_options = response.data; });


            $http.get('/api/beneficiary_profile/get_beneficiary_trainings?beneficiary_profile_id=' + response.data.beneficiary_profile_id)
.then(function (response) { $scope.beneficiary_trainings = response.data; });

            


        });

 
        $scope.save = function(){
            $http.put('/Api/beneficiary_profile/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("beneficiary_profile"); });
        }

    }])
    .controller('beneficiary_profile_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/beneficiary_profile/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/beneficiary_profile/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("beneficiary_profile"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/beneficiary_profile', {
                title: 'beneficiary_profile - List',
                templateUrl: '/Static/beneficiary_profile_List',
                controller: 'beneficiary_profile_list'
            })
            .when('/beneficiary_profile/Create', {
                title: 'beneficiary_profile - Create',
                templateUrl: '/Static/beneficiary_profile_Edit',
                controller: 'beneficiary_profile_create'
            })
            .when('/beneficiary_profile/Edit/:id', {
                title: 'beneficiary_profile - Edit',
                templateUrl: '/Static/beneficiary_profile_Edit',
                controller: 'beneficiary_profile_edit'
            })
            .when('/beneficiary_profile/Delete/:id', {
                title: 'beneficiary_profile - Delete',
                templateUrl: '/Static/beneficiary_profile_Delete',
                controller: 'beneficiary_profile_delete'
            })
            .when('/beneficiary_profile/:id', {
                title: 'beneficiary_profile - Details',
                templateUrl: '/Static/beneficiary_profile_Details',
                controller: 'beneficiary_profile_details'
            })
    }])
;

})();
