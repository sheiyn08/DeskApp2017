var modules = modules || [];
(function () {
    'use strict';
    modules.push('training');

    angular.module('training', ['ngRoute', 'ngMaterial', 'ngMessages', 'material.svgAssetsCache'])

    //.factory('Product', ['$resource',
    //  function ($resource) {
    //      return $resource('', {}, {
    //          query: {
    //              method: 'GET',
    //              url: '/api/training'
    //          }
    //      });
    //  }])


    .controller('training_list', ['$scope', '$http', function ($scope, $http) {



        $scope.isSearching = false;

        $scope.region_code = "";
        $scope.prov_code = "";
        $scope.city_code = "";
        $scope.brgy_code = "";

        $scope.fund_source_id = "";
        $scope.cycle_id = "";

        $http.get('/api/lib_region')
    .then(function (response) { $scope.region_code_options = response.data; });

        $http.get('/Api/lib_fund_source/')
.then(function (response) { $scope.fund_source_id_options = response.data; });

        $http.get('/api/lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });


        $http.get('/Api/lib_lgu_level/')
.then(function (response) { $scope.lgu_level_id_options = response.data; });

        $http.get('/Api/lib_training_category/')
.then(function (response) { $scope.training_category_id_options = response.data; });


        $scope.$watch('region_code', function (newValue, oldValue) {

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

                record.brgy_code = null;
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

        $scope.$watch('city_code', function (newValue, oldValue) {

            var record = $scope;

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

        $scope.$watch('fund_source_id', function (newValue, oldValue) {

            var record = $scope;

            if (newValue == null) {

                record.cycle_id = null;

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

        //  $scope.page = 0;
        //  $scope.pagesCount = 0;




        $scope.formatDate = function (date) {
            var dateOut = new Date(date);
            return dateOut;
        };


        $scope.search = function (page) {

            $scope.isSearching = true;


            var title = "";
            if ($scope.training_title != null) {
                title = $scope.training_title;
            }
            var region_code = "";
            if ($scope.region_code != null) {
                region_code = $scope.region_code;
            }

            var prov_code = "";
            if ($scope.prov_code != null) {
                prov_code = $scope.prov_code;
            }
            var city_code = "";
            if ($scope.city_code != null) {
                city_code = $scope.city_code;
            }
            var brgy_code = "";
            if ($scope.brgy_code != null) {
                brgy_code = $scope.brgy_code;
            }

            var pageSize = 10;
            if ($scope.pageSize != null) {
                pageSize = $scope.pageSize;
            }

            var currPage = 0;
            if (page != null) {
                currPage = page;
            }


            $http.get('/Api/training?'
            + "pageSize=" + pageSize
            + "&currPage=" + currPage
            + "&region_code=" + region_code
            + "&prov_code=" + prov_code
            + "&city_code=" + city_code
            + "&brgy_code=" + brgy_code
            + "&fund_source_id=" + $scope.fund_source_id
            + "&cycle_id=" + $scope.cycle_id
               // + "&title=" + $scope.title
                )
          .then(function (value) {

              $scope.page = value.data.Page;
              $scope.pagesCount = value.data.TotalPages;
              $scope.totalCount = value.data.TotalCount;
              $scope.data = value.data;
              $scope.isSearching = false;
          });


            //   alert("searching");
            //page = page || 0;

            //var _onSuccess = function (value) {
            //    $scope.page = value.Page;
            //    $scope.pagesCount = value.TotalPages;
            //    $scope.totalCount = value.TotalCount;
            //    $scope.Data = value;
            //    $scope.isSearching = false;
            //};
            //var _onError = function () {
            //    $scope.isSearching = false;
            //};

            ////$scope.isSearching = true;

            //Product.query({ page: page, pageSize: 8 },
            //    _onSuccess,
            //    _onError);
        };

        $scope.search();


    }])
    .controller('training_details', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {

        $http.get('/Api/training/' + $routeParams.id)
        .then(function (response) {

            $scope.data = response.data;




        });



    }])
    .controller('training_create', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $scope.data = {};

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

                record.brgy_code = null;
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

        $http.get('/Api/lib_enrollment/')
.then(function (response) { $scope.enrollment_id_options = response.data; });
        $http.get('/Api/lib_fund_source/')
.then(function (response) { $scope.fund_source_id_options = response.data; });
        $http.get('/Api/lib_lgu_level/')
.then(function (response) { $scope.lgu_level_id_options = response.data; });

        $http.get('/Api/lib_training_category/')
.then(function (response) { $scope.training_category_id_options = response.data; });

        $scope.save = function () {
            $http.post('/Api/training/', $scope.data)
            .then(function (response) {
                alert(JSON.stringify(response));

                $location.path("training");
            });
        }

    }])
    .controller('training_edit', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $scope.isSearching = true;







        $http.get('/Api/training/' + $routeParams.id)
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



        $scope.dt_start_date = new Date(response.data.start_date);


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


        $http.get('/Api/beneficiary_profile?city_code=' + $scope.data.city_code + "&pageSize=10000")
  .then(function (result) {

      $scope.members = result.data;

      angular.forEach($scope.members, function (todo) {


          $http.post('/Api/training/CheckParticipation?beneficiary_profile_id=' + todo.beneficiary_profile_id + "&training_id=" + $routeParams.id)
          .then(function (response) {

              if (response.data == true) {
                  todo.is_participant = true;
              }
              else {
                  todo.is_participant = false;
              }

          }).then(function () {
              $scope.isSearching = false;
          });



      });

  });

    });




        $http.get('/api/lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });


        $http.get('/Api/lib_lgu_level/')
.then(function (response) { $scope.lgu_level_id_options = response.data; });

        $http.get('/Api/lib_training_category/')
.then(function (response) { $scope.training_category_id_options = response.data; });

        $scope.ConfirmParticipation = function (member) {
            var frien = member

            var url = '/Api/beneficiary_profile/SaveBeneficiaryTraining?beneficiary_profile_id='
            + member.beneficiary_profile_id + "&training_id=" + $scope.data.training_id +
            "&is_participant=" + !member.is_participant;



            $scope.isSearching = true;

            $http.post(url).success(function (data) {


                $scope.isSearching = false;

            }).error(function (data) {
                $scope.error = "An Error has occured while Saving! " + data;
                $scope.isSearching = false;
            });


        }

        $scope.save = function () {

            $scope.loading = true;

            $http.put('/Api/training/' + $routeParams.id, $scope.data).success(function (data) {
                //frien.editMode = false;
                //$scope.loading = false;

                alert(JSON.stringify(data));


                $location.path("training");

            }).error(function (data) {
                $scope.error = "An Error has occured while Saving customer! " + data;
                $scope.loading = false;
            });
        };

        //by pressing toggleEdit button ng-click in html, this method will be hit
        $scope.toggleEdit = function () {
            this.customer.editMode = !this.customer.editMode;
        };

    }])
    .controller('training_delete', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $http.get('/Api/training/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });
        $scope.save = function () {
            $http.delete('/Api/training/' + $routeParams.id, $scope.data)
            .then(function (response) { $location.path("training"); });
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
            templateUrl: '/scripts/templates/pager-template.html',
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
        .when('/training', {
            title: 'training - List',
            templateUrl: '/Static/training_List',
            controller: 'training_list'
        })
        .when('/training/Create', {
            title: 'training - Create',
            templateUrl: '/Static/training_Edit',
            controller: 'training_create'
        })
        .when('/training/Edit/:id', {
            title: 'training - Edit',
            templateUrl: '/Static/training_Edit',
            controller: 'training_edit'
        })
        .when('/training/Delete/:id', {
            title: 'training - Delete',
            templateUrl: '/Static/training_Delete',
            controller: 'training_delete'
        })
        .when('/training/:id', {
            title: 'training - Details',
            templateUrl: '/Static/training_Details',
            controller: 'training_details'
        })
    }])
    ;

})();
