 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('task_tracking');

    angular.module('task_tracking',['ngRoute'])
    .controller('task_tracking_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/task_tracking/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('task_tracking_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/task_tracking/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('task_tracking_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
                $http.get('/Api/lib_task_priority/')
        .then(function(response){$scope.task_priority_id_options = response.data;});
                $http.get('/Api/lib_task_status/')
        .then(function(response){$scope.task_status_id_options = response.data;});
        
        $scope.save = function(){
            $http.post('/Api/task_tracking/', $scope.data)
            .then(function(response){ $location.path("task_tracking"); });
        }

    }])
    .controller('task_tracking_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/task_tracking/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

                $http.get('/Api/lib_task_priority/')
        .then(function(response){$scope.task_priority_id_options = response.data;});
                $http.get('/Api/lib_task_status/')
        .then(function(response){$scope.task_status_id_options = response.data;});
        
        $scope.save = function(){
            $http.put('/Api/task_tracking/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("task_tracking"); });
        }

    }])
    .controller('task_tracking_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/task_tracking/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/task_tracking/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("task_tracking"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/task_tracking', {
                title: 'task_tracking - List',
                templateUrl: '/Static/task_tracking_List',
                controller: 'task_tracking_list'
            })
            .when('/task_tracking/Create', {
                title: 'task_tracking - Create',
                templateUrl: '/Static/task_tracking_Edit',
                controller: 'task_tracking_create'
            })
            .when('/task_tracking/Edit/:id', {
                title: 'task_tracking - Edit',
                templateUrl: '/Static/task_tracking_Edit',
                controller: 'task_tracking_edit'
            })
            .when('/task_tracking/Delete/:id', {
                title: 'task_tracking - Delete',
                templateUrl: '/Static/task_tracking_Delete',
                controller: 'task_tracking_delete'
            })
            .when('/task_tracking/:id', {
                title: 'task_tracking - Details',
                templateUrl: '/Static/task_tracking_Details',
                controller: 'task_tracking_details'
            })
    }])
;

})();
