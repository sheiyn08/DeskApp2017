var modules = modules || [];
(function () {
    'use strict';
    modules.push('grievance_intake');

    angular.module('grievance_intake', ['ngRoute', 'ngMaterial', 'ngMessages', 'material.svgAssetsCache'])
    .controller('grievance_intake_list', ['$scope', '$http', function ($scope, $http) {

      

        $http.get('/api/lib_region')
.then(function (response) { $scope.region_code_options = response.data; });

        $http.get('/Api/lib_fund_source/')
    .then(function (response) { $scope.fund_source_id_options = response.data; });

        $http.get('/api/lib_enrollment')
    .then(function (response) { $scope.enrollment_id_options = response.data; });




        $http.get('/Api/lib_grs_intake_level/')
    .then(function (response) { $scope.grs_intake_level_id_options = response.data; });

        $http.get('/Api/lib_grs_form/')
    .then(function (response) { $scope.grs_form_id_options = response.data; });

        $http.get('/Api/lib_grs_filling_mode/')
    .then(function (response) { $scope.grs_filling_mode_id_options = response.data; });


        $http.get('/Api/lib_grs_resolution_status/')
    .then(function (response) { $scope.grs_resolution_status_id_options = response.data; });

        $http.get('/Api/lib_grs_feedback/')
    .then(function (response) { $scope.grs_feedback_id_options = response.data; });

        $http.get('/Api/lib_grs_category/')
    .then(function (response) { $scope.grs_category_id_options = response.data; });

        $http.get('/Api/lib_grs_complaint_subject/')
    .then(function (response) { $scope.grs_complaint_subject_id_options = response.data; });

        $http.get('/Api/lib_grs_nature/')
    .then(function (response) { $scope.grs_nature_id_options = response.data; });

        $http.get('/Api/lib_ip_group/')
    .then(function (response) { $scope.ip_group_id_options = response.data; });
        $http.get('/Api/lib_grs_intake_officer/')
    .then(function (response) { $scope.grs_intake_officer_id_options = response.data; });



        $scope.$watch('region_code', function (newValue, oldValue) {

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
                    url: '/api/lib_province?id=' + record.region_code,

                }).success(function (data, status, headers, config) {

                    $scope.prov_code_options = data;
                }).error(function (data, status, headers, config) {

                    $scope.message = 'Unexpected Error';
                });
            }

        });

        $scope.$watch('prov_code', function (newValue, oldValue) {

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
                    url: '/api/lib_city?id=' + record.prov_code,

                }).success(function (data, status, headers, config) {

                    $scope.city_code_options = data;
                }).error(function (data, status, headers, config) {

                    $scope.message = 'Unexpected Error';
                });
            }

        });

        $scope.$watch('city_code', function (newValue, oldValue) {

            var record = $scope;

            if (newValue == null) {

                record.brgy_code = '';

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

        $scope.$watch('fund_source_id', function (newValue, oldValue) {

            var record = $scope;

            if (newValue == null) {

                record.cycle_id = '';

                record.cycle_id_options = [];
            }

            else {

                if (newValue != "") {
                    $http({
                        method: 'GET',
                        url: '/api/lib_cycle?id=' + record.fund_source_id,

                    }).success(function (data, status, headers, config) {

                        $scope.cycle_id_options = data;
                    }).error(function (data, status, headers, config) {

                        $scope.message = 'Unexpected Error';
                    });
                }
                else {
                    record.cycle_id = null;

                    record.cycle_id_options = [];
                }
            }

        });


        $scope.search = function (page) {


            $scope.isSearching = true;

            var region_code = "";
            if ($scope.region_code != null) {
                region_code = $scope.region_code;
            }

            var prov_code = "";
            if ($scope.prov_code != null) {
                prov_code = $scope.prov_code;
            };

            var city_code = "";
            if ($scope.city_code != null) {
                city_code = $scope.city_code
            };

            var brgy_code = "";
            if ($scope.brgy_code != null) {
                brgy_code = $scope.brgy_code
            };

            var fund_source_id = "";
            if ($scope.fund_source_id != null) {
                fund_source_id = $scope.fund_source_id
            };

            var enrollment_id = "";
            if ($scope.enrollment_id != null) {
                enrollment_id = $scope.enrollment_id
            };


            var grs_intake_level_id = "";
            if ($scope.grs_intake_level_id != null) {
                grs_intake_level_id = $scope.grs_intake_level_id
            };

            var grs_form_id = "";
            if ($scope.grs_form_id != null) {
                grs_form_id = $scope.grs_form_id
            };

            var grs_filling_mode_id = "";
            if ($scope.grs_filling_mode_id != null) {
                grs_filling_mode_id = $scope.grs_filling_mode_id
            };

            var grs_resolution_status_id = "";
            if ($scope.grs_resolution_status_id != null) {
                grs_resolution_status_id = $scope.grs_resolution_status_id
            };


            var grs_feedback_id = "";
            if ($scope.grs_feedback_id != null) {
                grs_feedback_id = $scope.grs_feedback_id
            };

            var grs_category_id = "";
            if ($scope.grs_category_id != null) {
                grs_category_id = $scope.grs_category_id
            };

            var grs_complaint_subject_id = "";
            if ($scope.grs_complaint_subject_id != null) {
                grs_complaint_subject_id = $scope.grs_complaint_subject_id
            };

            var grs_nature_id = "";
            if ($scope.grs_nature_id != null) {
                grs_nature_id = $scope.grs_nature_id
            };

            var ip_group_id = "";
            if ($scope.ip_group_id != null) {
                ip_group_id = $scope.ip_group_id
            };



            var url = '/api/v1/grievances/get_dto?'
           + "region_code=" + region_code
           + "&prov_code=" + prov_code
           + "&city_code=" + city_code
           + "&brgy_code=" + brgy_code
           + "&enrollment_id=" + enrollment_id
           + "&fund_source_id=" + fund_source_id
            + "&currPage=" + page
            + "&pageSize=" + $scope.pageSize;


            // alert(url);


            $http.get(url)
          .then(function (value) {


              $scope.page = value.data.Page;
              $scope.pagesCount = value.data.TotalPages;
              $scope.totalCount = value.data.TotalCount;
              $scope.Items = value.data.Items;
              $scope.isSearching = false;
          });

 
        };

        $scope.search();


        $scope.getData = function () {
 $scope.loading = true;
            $http.post('/sync/get/grievances'
            ).success(function (data) {
               
               
                angular.forEach(data, function (value, key) {
                 //   alert(JSON.stringify(value));


                    $http.post('/api/v1/grievances/save', value,
                     {
                         headers: { 'Content-Type': 'application/json' }
                     }
                     ).success(function (data) {
                      //   $scope.loading = false;
                       //  $location.path("grievance_intake");
                         //   window.location.href = data.url;

                     }).error(function (data) {

                         alert(JSON.stringify(data));


                         $scope.error = "An Error has occured while Saving! " + data.statusText;
                         $scope.loading = false;
                     });



                });



            }).error(function (data) {

                alert(JSON.stringify(data));


                $scope.error = "An Error has occured while Saving! " + data.statusText;
                $scope.loading = false;
            });
        };


    }])
    .controller('grievance_intake_details', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {

        $http.get('/Api/grievance_intake/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });

    }])
    .controller('grievance_intake_create', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $scope.data = {};
        $http.get('/Api/lib_approval/')
.then(function (response) { $scope.approval_id_options = response.data; });
        $http.get('/Api/lib_brgy/')
.then(function (response) { $scope.brgy_code_options = response.data; });
        $http.get('/Api/lib_city/')
.then(function (response) { $scope.city_code_options = response.data; });
        $http.get('/Api/lib_cycle/')
.then(function (response) { $scope.cycle_id_options = response.data; });
        $http.get('/Api/lib_enrollment/')
.then(function (response) { $scope.enrollment_id_options = response.data; });
        $http.get('/Api/lib_fund_source/')
.then(function (response) { $scope.fund_source_id_options = response.data; });
        $http.get('/Api/lib_grs_category/')
.then(function (response) { $scope.grs_category_id_options = response.data; });
        $http.get('/Api/lib_grs_complaint_subject/')
.then(function (response) { $scope.grs_complaint_subject_id_options = response.data; });
        $http.get('/Api/lib_grs_feedback/')
.then(function (response) { $scope.grs_feedback_id_options = response.data; });
        $http.get('/Api/lib_grs_filling_mode/')
.then(function (response) { $scope.grs_filling_mode_id_options = response.data; });
        $http.get('/Api/lib_grs_form/')
.then(function (response) { $scope.grs_form_id_options = response.data; });
        $http.get('/Api/lib_grs_intake_level/')
.then(function (response) { $scope.grs_intake_level_id_options = response.data; });
        $http.get('/Api/lib_grs_intake_officer/')
.then(function (response) { $scope.grs_intake_officer_id_options = response.data; });
        $http.get('/Api/lib_grs_nature/')
.then(function (response) { $scope.grs_nature_id_options = response.data; });
        $http.get('/Api/lib_grs_resolution_status/')
.then(function (response) { $scope.grs_resolution_status_id_options = response.data; });
        $http.get('/Api/lib_grs_sender_designation/')
.then(function (response) { $scope.grs_sender_designation_id_options = response.data; });
        $http.get('/Api/lib_ip_group/')
.then(function (response) { $scope.ip_group_id_options = response.data; });
        $http.get('/Api/lib_province/')
.then(function (response) { $scope.prov_code_options = response.data; });
        $http.get('/Api/lib_region/')
.then(function (response) { $scope.region_code_options = response.data; });
        $http.get('/Api/lib_sex/')
.then(function (response) { $scope.sex_id_options = response.data; });
        $http.get('/Api/pincos/')
.then(function (response) { $scope.pincos_id_options = response.data; });

        $scope.save = function () {
            $http.post('/Api/grievance_intake/', $scope.data)
            .then(function (response) { $location.path("grievance_intake"); });
        }

    }])
    .controller('grievance_intake_edit', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        
        $scope.data = {};


        $http.get('/api/v1/grievances/get?id=' + $routeParams.id)
  .then(function (response) {


      $scope.loading = true;

      $scope.data = response.data;

      $scope.dt_date_intake = new Date(response.data.date_intake);
      $scope.dt_resolution_date = new Date(response.data.resolution_date);

      $http.get('/api/lib_region')
 .then(function (response) { $scope.region_code_options = response.data; });

      $http.get('/Api/lib_fund_source/')
.then(function (response) { $scope.fund_source_id_options = response.data; });

      $http.get('/api/lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });

      $http.get('/Api/lib_lgu_level/')
.then(function (response) { $scope.lgu_level_id_options = response.data; });

      $http.get('/Api/lib_grs_intake_level/')
.then(function (response) { $scope.grs_intake_level_id_options = response.data; });

      $http.get('/Api/lib_grs_form/')
.then(function (response) { $scope.grs_form_id_options = response.data; });

      $http.get('/Api/lib_grs_filling_mode/')
.then(function (response) { $scope.grs_filling_mode_id_options = response.data; });

      $http.get('/Api/lib_grs_resolution_status/')
.then(function (response) { $scope.grs_resolution_status_id_options = response.data; });

      $http.get('/Api/lib_grs_feedback/')
.then(function (response) { $scope.grs_feedback_id_options = response.data; });

      $http.get('/Api/lib_grs_category/')
.then(function (response) { $scope.grs_category_id_options = response.data; });

      $http.get('/Api/lib_grs_complaint_subject/')
.then(function (response) { $scope.grs_complaint_subject_id_options = response.data; });

      $http.get('/Api/lib_grs_nature/')
.then(function (response) { $scope.grs_nature_id_options = response.data; });

      $http.get('/Api/lib_ip_group/')
.then(function (response) { $scope.ip_group_id_options = response.data; });

      $http.get('/Api/lib_sex/')
.then(function (response) { $scope.sex_id_options = response.data; });

      $http.get('/Api/lib_grs_sender_designation/')
.then(function (response) { $scope.grs_sender_designation_id_options = response.data; });

      $http.get('/Api/lib_grs_intake_officer/')
.then(function (response) { $scope.grs_intake_officer_id_options = response.data; });



      $scope.loading = false;

      $scope.edit = function () {
          this.editMode = !this.editMode;
      };


      $scope.cancel = function () {

          $http.get('/api/v1/grievances/get?id=' + $routeParams.id)
    .then(function (response) {

        //            this.editMode = false;

        $scope.data = response.data;

        $scope.dt_intake_date = new Date(response.data.intake_date);
        $scope.dt_resolution_date = new Date(response.data.resolution_date);
    });
      }


      $scope.save = function () {
          $scope.$broadcast('show-errors-check-validity');
          $scope.loading = true;
           //      alert(JSON.stringify($scope.data));

          //'/api/v1/grievances/save'
         
        //  var model = JSON.stringify($scope.data);

          $http.post('/api/v1/grievances/save', $scope.data,
          
          
          {
              headers: {'Content-Type': 'application/json'}
          }


          ).success(function (data) {
              $scope.loading = false;
              $location.path("grievance_intake");
           //   window.location.href = data.url;

          }).error(function (data) {

              alert(JSON.stringify(data));


              $scope.error = "An Error has occured while Saving! " + data.statusText;
              $scope.loading = false;
          });
      };
  });


        $scope.$watch('data.region_code', function (newValue, oldValue) {



            var record = $scope;

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

                record.brgy_code = null;
                record.city_code = null;
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

                record.brgy_code = null;

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

      

    }])
    .controller('grievance_intake_delete', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $http.get('/Api/grievance_intake/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });
        $scope.save = function () {
            $http.delete('/Api/grievance_intake/' + $routeParams.id, $scope.data)
            .then(function (response) { $location.path("grievance_intake"); });
        }

    }])



.directive('demoPager', function () {
    return {
        scope: {
            page: '@',
            pagesCount: '@',
            totalCount: '@',
            searchFunc: '&'
        },
        replace: true,
        restrict: 'E',
        templateUrl: '/app/components/templates/pager-template.html',
        controller: ['$scope', function ($scope) {
            $scope.search = function (i) {
                if ($scope.searchFunc) {
                    $scope.searchFunc({ page: i });
                }
            };

            $scope.range = function () {
                if (!$scope.pagesCount) { return []; }
                var step = 2;
                var doubleStep = step * 2;
                var start = Math.max(0, $scope.page - step);
                var end = start + 1 + doubleStep;
                if (end > $scope.pagesCount) { end = $scope.pagesCount; }

                var ret = [];
                for (var i = start; i != end; ++i) {
                    ret.push(i);
                }

                return ret;
            };
        }]
    }
})









    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
        .when('/grievance_intake', {
            title: 'grievance_intake - List',
            templateUrl: '/app/components/grievance/index.html',
            controller: 'grievance_intake_list'
        })
        .when('/grievance_intake/Create', {
            title: 'grievance_intake - Create',
            templateUrl: '/app/components/create.html',
            controller: 'grievance_intake_create'
        })
        .when('/grievance_intake/Edit/:id', {
            title: 'grievance_intake - Edit',
            templateUrl: '/app/components/grievance/create.html',
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
