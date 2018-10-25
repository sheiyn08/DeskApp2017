

angular.module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache', 'pims-pager']).controller('AppCtrl', function ($scope, $http, $mdDialog, $mdMedia) {
    $scope.data = {};

    $scope.data.region_code = '';
    $scope.data.prov_code = '';
    $scope.data.city_code = '';
    $scope.data.brgy_code = '';
    $scope.data.fund_source_id = '';
    $scope.data.cycle_id = '';
    $scope.data.enrollment_id = '';
    $scope.data.is_male = '';
    $scope.data.is_ip = '';
    $scope.data.ip_group_id = '';
    $scope.data.occupation_id = '';
    $scope.data.is_lguofficial = '';
    $scope.data.lgu_position_id = '';
    $scope.data.education_attainment_id = '';
    $scope.data.civil_status_id = '';
    $scope.data.pageSize = '';
    $scope.data.currPage = '';
    $scope.data.push_status_id = '';
    $scope.data.approval_id = '';
    $scope.data.is_volunteer = '';
    $scope.data.volunteer_committee_position_id = '';
    $scope.data.volunteer_committee_id = '';
    $scope.data.is_worker = '';
    $scope.data.is_trained = '';
    $scope.data.training_category_id = '';
    $scope.data.community_training_id = '';
    $scope.data.grs_intake_level_id = '';
    $scope.data.grs_form_id = '';
    $scope.data.grs_filling_mode_id = '';
    $scope.data.grs_complainant_position_id = '';
    $scope.data.grs_resolution_status_id = '';
    $scope.data.grs_feedback_id = '';
    $scope.data.grs_category_id = '';
    $scope.data.grs_complaint_subject_id = '';
    $scope.data.grs_nature_id = '';
    $scope.data.spi_nature_work_id = '';

    $scope.grs_resolution_status_id;




    $http.get('/api/online/lib_region')
.then(function (response) { $scope.region_code_options = response.data; });

    $http.get('/Api/lib_fund_source/')
.then(function (response) { $scope.fund_source_id_options = response.data; });

    $http.get('/api/lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });


    $http.get('/Api/lib_lgu_level/')
    .then(function (response) { $scope.lgu_level_id_options = response.data; });

 

    $scope.$watch('data.region_code', function (newValue, oldValue) {

        var record = $scope;

        if (newValue == null) {
            record.data.prov_code = null;
            record.data.city_code = null;
            record.data.brgy_code = null;


            record.prov_code_options = [];
            record.city_code_options = [];
            record.brgy_code_options = [];
        }
        else {



            $http({
                method: 'GET',
                url: '/api/online/lib_province?id=' + newValue,

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

            record.data.brgy_code = null;
            record.data.city_code = null;
            record.city_code_options = [];
            record.brgy_code_options = [];
        }

        else {
            $http({
                method: 'GET',
                url: '/api/online/lib_city?id=' + newValue,

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

            record.data.brgy_code = null;

            record.brgy_code_options = [];
        }

        else {
            $http({
                method: 'GET',
                url: '/api/online/lib_brgy?id=' + newValue,

            }).success(function (data, status, headers, config) {

                $scope.brgy_code_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });











    $scope.$watch('data.fund_source_id', function (newValue, oldValue) {

        var record = $scope;

        if (newValue == null) {

            record.data.cycle_id = null;

            record.cycle_id_options = [];
        }

        else {

            if (newValue != "") {
                $http({
                    method: 'GET',
                    url: '/api/lib_cycle?id=' + record.data.fund_source_id,

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


    $scope.clear = function () {
        window.location.href = "/View/CeacTracking";
    }


    $scope.search = function (page) {

        $scope.data.pageSize = $scope.pageSize == undefined ? '' : $scope.pageSize;
        $scope.data.currPage = page == undefined ? '' : page;

        $scope.isSearching = true;



        $scope.isSearching = true;

        $.post('/api/offline/v1/ceac_list/get_dto', $scope.data).success(function (value) {
            $scope.loading = false;

                    // alert(JSON.stringify(value));
            $scope.TotalSync = value.TotalSync;
            $scope.TotalUnAuthorized = value.TotalUnAuthorized;
            $scope.page = value.Page;
            $scope.pagesCount = value.TotalPages;
            $scope.totalCount = value.TotalCount;
            $scope.Items = value.Items;


            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });


    };

   
    $scope.search();


    $scope.exportXls = function () {


        $.blockUI({ message: '<h1><img src="@Url.Content("~/Content/Images/loading.gif")" /></h1> Processing...' });


        $.ajax({
            type: "POST",
            url: "/Export/trainings",
            data: {
                item: $scope.data,

            },
            success: function (viewHTML) {
                window.location.href = "/Export/GetExcel?id=" + viewHTML;
                setTimeout($.unblockUI, 10);

            },
            error: function (errorData) {
                onError(errorData);
                setTimeout($.unblockUI, 10);
            }

        });


    }

    $scope.exportParticipants = function (item) {

        $scope.data.record_id = item.grievance_record_id;


        $.blockUI({ message: '<h1><img src="@Url.Content("~/Content/Images/loading.gif")" /></h1> Processing...' });


        $.ajax({
            type: "POST",
            url: "/Export/training_participant",
            data: {
                item: $scope.data,

            },
            success: function (viewHTML) {
                window.location.href = "/Export/GetExcel?id=" + viewHTML;
                setTimeout($.unblockUI, 10);

            },
            error: function (errorData) {
                onError(errorData);
                setTimeout($.unblockUI, 10);
            }

        });
        $scope.data.record_id = '';


    }

    $scope.createNew = function () {

    
         window.location.href = "/Entry/Trainings";
    }

      $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');

    $scope.showAdvanced = function (ev) {
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;

        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'dialog1.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: false,
            fullscreen: useFullScreen
        })
        .then(function (answer) {
            $scope.status = 'You said the information was "' + answer + '".';
        }, function () {
            $scope.status = 'You cancelled the dialog.';
        });



        $scope.$watch(function () {
            return $mdMedia('xs') || $mdMedia('sm');
        }, function (wantsFullScreen) {
            $scope.customFullscreen = (wantsFullScreen === true);
        });

    };


})



function DialogController($scope, $mdDialog, $http) {
   
  $http.get('/api/all/cities')
.then(function (response) { $scope.all_city_code_options = response.data; });


    $scope.hide = function () {
        $mdDialog.hide();
    };

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };

    $scope.syncGet = function () {
        $scope.isSearching = true;
        $scope.total = 0;
        $scope.finished = 0;
        $scope.error = '';

        var credentials;

        var username = $scope.username;
        var password = $scope.password;


        $http.post('/sync/get/ceac?username=' + username + "&password=" + password + "&city_code=" + $scope.city_code  +"&record_id="
 
        ).success(function (data) {


            $scope.false = true;

            $mdDialog.cancel();
            location.reload();
            $scope.isSearching = false;
        }).error(function (data) {

            $scope.error = "An Error has occured while Downloading Data! ";
            $scope.isSearching = false;
        });

    };

    $scope.syncPost = function () {
        $scope.isSearching = true;
        $scope.error = '';
        $scope.total = 0;
        $scope.finished = 0;

        var credentials;

        var username = $scope.username;
        var password = $scope.password;



        $http.post('/sync/post/ceac?username=' + username + "&password=" + password + "&record_id="
        ).success(function (data) {
            $scope.false = true;
            $scope.isSearching = false;
            location.reload();
            $mdDialog.cancel();

        }).error(function (data) {



            $scope.error = "An Error has occured while Uploading Data! ";
            $scope.isSearching = false;
        });

    };

}




/**
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that can be in foundin the LICENSE file at http://material.angularjs.org/license.
**/
