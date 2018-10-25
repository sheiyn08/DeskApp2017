

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

    $http.get('/api/all_lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });


    $http.get('/Api/lib_lgu_level/')
    .then(function (response) { $scope.lgu_level_id_options = response.data; });


    $http.get('/Api/lib_training_category?lgu_level_id=' + $scope.lgu_level_id)
.then(function (response) { $scope.training_category_id_options = response.data; });



    

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
        window.location.href = "/View/Trainings";
    }

    $http.get('/api/report_list?id=21')
   .then(function (response) {

       $scope.report_list = response.data;
   });


    $scope.exportXls = function (dlUrl, name) {

        $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });

        $.post(dlUrl, $scope.data).success(function (value) {
            $scope.loading = false;


            $scope.exported_data = value;

            setTimeout($.unblockUI, 10);

            alasql('SELECT * INTO XLSX("' + name + '.xlsx' + '",{headers:true}) FROM ?', [$scope.exported_data]);

            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });

    }

    $scope.search = function (page) {

        $scope.data.pageSize = $scope.pageSize == undefined ? '' : $scope.pageSize;
        $scope.data.currPage = page == undefined ? '' : page;

        $scope.isSearching = true;



        $scope.isSearching = true;

        $.post('/api/offline/v1/trainings/get_dto', $scope.data).success(function (value) {
            $scope.loading = false;

            // alert(JSON.stringify(value));
 
            $scope.training_items = value.Items;


            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));
            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });

        $.post('/api/export/report/ceac_tracking', $scope.data).success(function (value) {
            $scope.loading = false;

         
 
            $scope.ceac_list = value;


            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));
            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });


        $.post('/api/export/barangay_assembly/summary', $scope.data).success(function (value) {
            $scope.loading = false;

         
 
            $scope.barangay_assembly_list = value;


            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));
            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });

    };


    $scope.search();



 

})

 



/**
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that can be in foundin the LICENSE file at http://material.angularjs.org/license.
**/
