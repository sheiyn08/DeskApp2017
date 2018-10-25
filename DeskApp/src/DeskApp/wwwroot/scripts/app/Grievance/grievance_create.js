 
    angular.module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache'])

        .controller('AppCtrl', function ($scope, $http) {

            $scope.data;

          
            $http.get('/api/v1/grievances/get?id=')
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

              $http.get('/api/v1/grievances/get?id=' + @ViewBag.id)
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
              
              

              $.post('/api/v1/grievances/save', $scope.data).success(function (data) {
                  $scope.loading = false;

                  window.location.href = data.url;

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


            

        })

    .directive('showErrors', function ($timeout, showErrorsConfig) {
        var getShowSuccess, linkFn;
        getShowSuccess = function (options) {
            var showSuccess;
            showSuccess = showErrorsConfig.showSuccess;
            if (options && options.showSuccess != null) {
                showSuccess = options.showSuccess;
            }
            return showSuccess;
        };
        linkFn = function (scope, el, attrs, formCtrl) {
            var blurred, inputEl, inputName, inputNgEl, options, showSuccess, toggleClasses;
            blurred = false;
            options = scope.$eval(attrs.showErrors);
            showSuccess = getShowSuccess(options);
            inputEl = el[0].querySelector('[name]');
            inputNgEl = angular.element(inputEl);
            inputName = inputNgEl.attr('name');
            if (!inputName) {
                throw 'show-errors element has no child input elements with a \'name\' attribute';
            }
            inputNgEl.bind('blur', function () {
                blurred = true;
                return toggleClasses(formCtrl[inputName].$invalid);
            });
            scope.$watch(function () {
                return formCtrl[inputName] && formCtrl[inputName].$invalid;
            }, function (invalid) {
                if (!blurred) {
                    return;
                }
                return toggleClasses(invalid);
            });
            scope.$on('show-errors-check-validity', function () {
                return toggleClasses(formCtrl[inputName].$invalid);
            });
            scope.$on('show-errors-reset', function () {
                return $timeout(function () {
                    el.removeClass('has-error');
                    el.removeClass('has-success');
                    return blurred = false;
                }, 0, false);
            });
            return toggleClasses = function (invalid) {
                el.toggleClass('has-error', invalid);
                if (showSuccess) {
                    return el.toggleClass('has-success', !invalid);
                }
            };
        };
        return {
            restrict: 'A',
            require: '^form',
            compile: function (elem, attrs) {
                if (!elem.hasClass('form-group')) {
                    throw 'show-errors element does not have the \'form-group\' class';
                }
                return linkFn;
            }
        };
    }
  )

    .provider('showErrorsConfig', function () {
        var _showSuccess;
        _showSuccess = false;
        this.showSuccess = function (showSuccess) {
            return _showSuccess = showSuccess;
        };
        this.$get = function () {
            return { showSuccess: _showSuccess };
        };
    });



/**
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that can be in foundin the LICENSE file at http://material.angularjs.org/license.
**/
 