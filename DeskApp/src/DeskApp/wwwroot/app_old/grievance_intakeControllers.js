 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('grievance_intake');

    angular.module('grievance_intake',['ngRoute'])
    .controller('grievance_intake_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/grievance_intake/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('grievance_intake_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/grievance_intake/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('grievance_intake_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
                $http.get('/Api/lib_approval/')
        .then(function(response){$scope.approval_id_options = response.data;});
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
                $http.get('/Api/lib_grs_category/')
        .then(function(response){$scope.grs_category_id_options = response.data;});
                $http.get('/Api/lib_grs_complaint_subject/')
        .then(function(response){$scope.grs_complaint_subject_id_options = response.data;});
                $http.get('/Api/lib_grs_feedback/')
        .then(function(response){$scope.grs_feedback_id_options = response.data;});
                $http.get('/Api/lib_grs_filling_mode/')
        .then(function(response){$scope.grs_filling_mode_id_options = response.data;});
                $http.get('/Api/lib_grs_form/')
        .then(function(response){$scope.grs_form_id_options = response.data;});
                $http.get('/Api/lib_grs_intake_level/')
        .then(function(response){$scope.grs_intake_level_id_options = response.data;});
                $http.get('/Api/lib_grs_intake_officer/')
        .then(function(response){$scope.grs_intake_officer_id_options = response.data;});
                $http.get('/Api/lib_grs_nature/')
        .then(function(response){$scope.grs_nature_id_options = response.data;});
                $http.get('/Api/lib_grs_resolution_status/')
        .then(function(response){$scope.grs_resolution_status_id_options = response.data;});
                $http.get('/Api/lib_grs_sender_designation/')
        .then(function(response){$scope.grs_sender_designation_id_options = response.data;});
                $http.get('/Api/lib_ip_group/')
        .then(function(response){$scope.ip_group_id_options = response.data;});
                $http.get('/Api/lib_province/')
        .then(function(response){$scope.prov_code_options = response.data;});
                $http.get('/Api/lib_region/')
        .then(function(response){$scope.region_code_options = response.data;});
                $http.get('/Api/lib_sex/')
        .then(function(response){$scope.sex_id_options = response.data;});
                $http.get('/Api/pincos/')
        .then(function(response){$scope.pincos_id_options = response.data;});
        
        $scope.save = function(){
            $http.post('/Api/grievance_intake/', $scope.data)
            .then(function(response){ $location.path("grievance_intake"); });
        }

    }])
    .controller('grievance_intake_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/grievance_intake/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

                $http.get('/Api/lib_approval/')
        .then(function(response){$scope.approval_id_options = response.data;});
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
                $http.get('/Api/lib_grs_category/')
        .then(function(response){$scope.grs_category_id_options = response.data;});
                $http.get('/Api/lib_grs_complaint_subject/')
        .then(function(response){$scope.grs_complaint_subject_id_options = response.data;});
                $http.get('/Api/lib_grs_feedback/')
        .then(function(response){$scope.grs_feedback_id_options = response.data;});
                $http.get('/Api/lib_grs_filling_mode/')
        .then(function(response){$scope.grs_filling_mode_id_options = response.data;});
                $http.get('/Api/lib_grs_form/')
        .then(function(response){$scope.grs_form_id_options = response.data;});
                $http.get('/Api/lib_grs_intake_level/')
        .then(function(response){$scope.grs_intake_level_id_options = response.data;});
                $http.get('/Api/lib_grs_intake_officer/')
        .then(function(response){$scope.grs_intake_officer_id_options = response.data;});
                $http.get('/Api/lib_grs_nature/')
        .then(function(response){$scope.grs_nature_id_options = response.data;});
                $http.get('/Api/lib_grs_resolution_status/')
        .then(function(response){$scope.grs_resolution_status_id_options = response.data;});
                $http.get('/Api/lib_grs_sender_designation/')
        .then(function(response){$scope.grs_sender_designation_id_options = response.data;});
                $http.get('/Api/lib_ip_group/')
        .then(function(response){$scope.ip_group_id_options = response.data;});
                $http.get('/Api/lib_province/')
        .then(function(response){$scope.prov_code_options = response.data;});
                $http.get('/Api/lib_region/')
        .then(function(response){$scope.region_code_options = response.data;});
                $http.get('/Api/lib_sex/')
        .then(function(response){$scope.sex_id_options = response.data;});
                $http.get('/Api/pincos/')
        .then(function(response){$scope.pincos_id_options = response.data;});
        
        $scope.save = function(){
            $http.put('/Api/grievance_intake/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("grievance_intake"); });
        }

    }])
    .controller('grievance_intake_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/grievance_intake/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/grievance_intake/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("grievance_intake"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/grievance_intake', {
                title: 'grievance_intake - List',
                templateUrl: '/Static/grievance_intake_List',
                controller: 'grievance_intake_list'
            })
            .when('/grievance_intake/Create', {
                title: 'grievance_intake - Create',
                templateUrl: '/Static/grievance_intake_Edit',
                controller: 'grievance_intake_create'
            })
            .when('/grievance_intake/Edit/:id', {
                title: 'grievance_intake - Edit',
                templateUrl: '/Static/grievance_intake_Edit',
                controller: 'grievance_intake_edit'
            })
            .when('/grievance_intake/Delete/:id', {
                title: 'grievance_intake - Delete',
                templateUrl: '/Static/grievance_intake_Delete',
                controller: 'grievance_intake_delete'
            })
            .when('/grievance_intake/:id', {
                title: 'grievance_intake - Details',
                templateUrl: '/Static/grievance_intake_Details',
                controller: 'grievance_intake_details'
            })
    }])
;

})();
