 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('barangay_profile');

    angular.module('barangay_profile',['ngRoute'])
    .controller('barangay_profile_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/barangay_profile/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('barangay_profile_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/barangay_profile/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('barangay_profile_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
                $http.get('/Api/lib_approval/')
        .then(function(response){$scope.approval_id_options = response.data;});
                $http.get('/Api/lib_brgy/')
        .then(function(response){$scope.brgy_code_options = response.data;});
                $http.get('/Api/lib_city/')
        .then(function(response){$scope.city_code_options = response.data;});
                $http.get('/Api/lib_cycle/')
        .then(function(response){$scope.cycle_id_options = response.data;});
                $http.get('/Api/lib_fund_source/')
        .then(function(response){$scope.fund_source_id_options = response.data;});
                $http.get('/Api/lib_province/')
        .then(function(response){$scope.prov_code_options = response.data;});
                $http.get('/Api/lib_region/')
        .then(function(response){$scope.region_code_options = response.data;});
        
        $scope.save = function(){
            $http.post('/Api/barangay_profile/', $scope.data)
            .then(function(response){ $location.path("barangay_profile"); });
        }

    }])
    .controller('barangay_profile_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/barangay_profile/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

                $http.get('/Api/lib_approval/')
        .then(function(response){$scope.approval_id_options = response.data;});
                $http.get('/Api/lib_brgy/')
        .then(function(response){$scope.brgy_code_options = response.data;});
                $http.get('/Api/lib_city/')
        .then(function(response){$scope.city_code_options = response.data;});
                $http.get('/Api/lib_cycle/')
        .then(function(response){$scope.cycle_id_options = response.data;});
                $http.get('/Api/lib_fund_source/')
        .then(function(response){$scope.fund_source_id_options = response.data;});
                $http.get('/Api/lib_province/')
        .then(function(response){$scope.prov_code_options = response.data;});
                $http.get('/Api/lib_region/')
        .then(function(response){$scope.region_code_options = response.data;});
        
        $scope.save = function(){
            $http.put('/Api/barangay_profile/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("barangay_profile"); });
        }

    }])
    .controller('barangay_profile_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/barangay_profile/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/barangay_profile/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("barangay_profile"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/barangay_profile', {
                title: 'barangay_profile - List',
                templateUrl: '/Static/barangay_profile_List',
                controller: 'barangay_profile_list'
            })
            .when('/barangay_profile/Create', {
                title: 'barangay_profile - Create',
                templateUrl: '/Static/barangay_profile_Edit',
                controller: 'barangay_profile_create'
            })
            .when('/barangay_profile/Edit/:id', {
                title: 'barangay_profile - Edit',
                templateUrl: '/Static/barangay_profile_Edit',
                controller: 'barangay_profile_edit'
            })
            .when('/barangay_profile/Delete/:id', {
                title: 'barangay_profile - Delete',
                templateUrl: '/Static/barangay_profile_Delete',
                controller: 'barangay_profile_delete'
            })
            .when('/barangay_profile/:id', {
                title: 'barangay_profile - Details',
                templateUrl: '/Static/barangay_profile_Details',
                controller: 'barangay_profile_details'
            })
    }])
;

})();
