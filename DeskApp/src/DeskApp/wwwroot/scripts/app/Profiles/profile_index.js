
angular.module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache', 'pims-pager']).controller('AppCtrl', function ($scope, $http, $mdDialog, $mdMedia) {


    $scope.data = {};

    $scope.data.filter_by_recent_edit = '';
    $scope.data.filter_by_recent_add = '';
    
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
    // $scope.data.is_trained = '';
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

    $scope.needToConfirm = false;

    $scope.list_of_selected_items = [];

    window.onbeforeunload = function () {
        if ($scope.list_of_selected_items.length > 0 || $scope.needToConfirm == true) { //if value is true
            return "You have checked items for uploading. Click the Synchronize button to proceed with uploading or leave this page and changes made will not be saved."
        }
    };

    $scope.itemSelectedforSync = function (selected_item) {
        var idx = $scope.list_of_selected_items.indexOf(selected_item);

        // is currently selected
        if (idx > -1) {
            $scope.list_of_selected_items.splice(idx, 1);
            //splice explanation: at position <idx>, remove 1
        }
            // is newly selected
        else {
            $scope.list_of_selected_items.push(selected_item);
        }
    };

    $scope.checkAll = function () {

        if ($scope.data.check_all) {
            //alert("Size of array: " + $scope.Items.length);
            for (var i = 0; i < $scope.Items.length; i++) {
                $scope.list_of_selected_items.push($scope.Items[i]);
            }
        } else {
            $scope.list_of_selected_items = [];
            //alert("Array emptied.");
        }
        angular.forEach($scope.Items, function (item) {
            item.is_item_selected = $scope.data.check_all; //ito yung nag checheck sa bawat item; sineset as true yung kada item if naka check yung select all checkbox        
        });

    };

    $scope.delete = function (removeitem) {



        var ask = confirm("Are you sure you want to Delete this?");


        if (ask == true) {
            $.post('/api/delete/person_profile?id=' + removeitem.person_profile_id).success(function (value) {
                $scope.loading = false;


                var index = $scope.Items.indexOf(removeitem);
                $scope.Items.splice(index, 1);


                alert("Record removed!")

            }).error(function (data) {

                alert(JSON.stringify(data));


                $scope.error = "An Error has occured while Deleting! " + data.statusText;
                $scope.loading = false;
            });

        }

    }

    $http.get('/api/online/lib_region')
.then(function (response) { $scope.region_code_options = response.data; });

    $http.get('/Api/lib_fund_source/')
.then(function (response) { $scope.fund_source_id_options = response.data; });

    $http.get('/api/lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });

    $http.get('/api/lib_volunteer_committee')
.then(function (response) { $scope.volunteer_committee_id_options = response.data; });

    $http.get('/api/lib_volunteer_committee_position')
.then(function (response) { $scope.volunteer_committee_position_id_options = response.data; });

    $http.get('/api/lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });

    $http.get('/api/lib_training_category')
.then(function (response) { $scope.training_category_id_options = response.data; });

    $http.get('/Api/lib_occupation/')
.then(function (response) { $scope.occupation_id_options = response.data; });


    $http.get('/Api/lib_education_attainment/')
.then(function (response) { $scope.educational_attainment_id_options = response.data; });

    $http.get('/Api/lib_ip_group/')
    .then(function (response) { $scope.ip_group_id_options = response.data; });


    $scope.$watch('data.is_volunteer', function (newValue, oldValue) {
        if (newValue != true) {
            $scope.data.volunteer_committee_id = '';
            $scope.data.volunteer_committee_position_id = '';

            $scope.data.fund_source_id = '';
            $scope.data.cycle_id = '';

        }
    })


    $scope.$watch('data.is_trained', function (newValue, oldValue) {
        if (newValue == null || newValue == undefined) {
            $scope.data.training_category_id = '';


        }
    })


    $scope.$watch('data.is_ip', function (newValue, oldValue) {
        if (newValue != '') {
            $scope.data.ip_group_id = '';


        }
    })


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

            record.brgy_code = null;

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
                    url: '/api/lib_cycle?id=' + newValue,

                }).success(function (data, status, headers, config) {

                    $scope.cycle_id_options = data;
                }).error(function (data, status, headers, config) {

                    $scope.message = 'Unexpected Error';
                });
            }
            else {
                record.data.cycle_id = null;

                record.cycle_id_options = [];
            }
        }

    });




    $http.get('/api/report_list?id=18')
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


    $scope.clear = function () {
        $scope.data = {};
        $scope.search();
    }


    $scope.search = function (page) {

        $scope.data.pageSize = $scope.pageSize == undefined ? '' : $scope.pageSize;
        $scope.data.currPage = page == undefined ? '' : page;
        
        $scope.isSearching = true;

        if (($scope.data.filter_by_recent_edit) && (!$scope.data.filter_by_recent_add)) {

            $.post('/api/offline/v1/profiles/get_recently_edited', $scope.data).success(function (value) {
                $scope.loading = false;

                $scope.TotalUnAuthorized = value.TotalUnAuthorized;
                $scope.TotalSync = value.TotalSync;
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
        }

        else if ((!$scope.data.filter_by_recent_edit) && ($scope.data.filter_by_recent_add)) {

            $.post('/api/offline/v1/profiles/get_recently_added', $scope.data).success(function (value) {
                $scope.loading = false;

                $scope.TotalUnAuthorized = value.TotalUnAuthorized;
                $scope.TotalSync = value.TotalSync;
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
        }

        else if (($scope.data.filter_by_recent_edit) && ($scope.data.filter_by_recent_add)) {

            $.post('/api/offline/v1/profiles/get_recently_edited_and_added', $scope.data).success(function (value) {
                $scope.loading = false;

                $scope.TotalUnAuthorized = value.TotalUnAuthorized;
                $scope.TotalSync = value.TotalSync;
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
        }

        else {
            $.post('/api/offline/v1/profiles/get_dto', $scope.data).success(function (value) {
                $scope.loading = false;

                // alert(JSON.stringify(value));
                $scope.TotalUnAuthorized = value.TotalUnAuthorized;
                $scope.TotalSync = value.TotalSync;
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
        }


        
    };

    $scope.search();


    


    $scope.open_merge = function () {
        $scope.mergeList = [];

        $scope.isMergeMode = true;
    }


    $scope.AddMergeList = function (item) {

        $scope.mergeList.push(item);

    }



    $scope.createNew = function () {


        window.location.href = "/Entry/Profiles";
    }

    $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');




    $scope.showAdvanced = function (ev) {
        
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
        window.onbeforeunload = null;

        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'dialog1.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: false,
            fullscreen: useFullScreen,
            locals:
                {
                    items_selected: $scope.list_of_selected_items
                }
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



function DialogController($scope, $mdDialog, $http, items_selected) {

    //injecting the array from angular.module
    $scope.list_of_selected_items = items_selected;

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


        $http.post('/sync/get/profiles?username=' + username + "&password=" + password + "&city_code=" + $scope.city_code + "&record_id="
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

        if ($scope.list_of_selected_items.length > 0) {
            alert("Only selected item/s will be uploaded as you wish.");
            //alert("Array size: " + $scope.list_of_selected_items.length);

            //call API that will update the push_status_id

            for (i = 0; i < $scope.list_of_selected_items.length; i++) {
                var url = '/api/offline/v1/person_profile/post/selected_items_to_sync?person_profile_id='
                        + items_selected[i].person_profile_id + "&push_status_id=" + 5;
                $scope.isAddingItem = true;

                $http.post(url).success(function (data) {
                    $scope.isAddingItem = false;
                    //inner http.post -- actual sync:
                    $http.post('/sync/post/profiles?username=' + username + "&password=" + password + "&record_id=").success(function (data) {
                        $scope.needToConfirm = false;
                        $scope.false = true;
                        $scope.isSearching = false;
                        location.reload();
                        $mdDialog.cancel();
                    }).error(function (data) {
                        $scope.error = "An Error has occured while Uploading Data! ";
                        $scope.isSearching = false;
                    });
                    //end 
                }).error(function (data) {
                    $scope.error = "An Error has occured while Saving! " + data;
                    $scope.isAddingItem = false;
                });
            }
        }

        else {
            alert("You did not select any item to upload. Hence, system will upload items that are newly created and/or edited.");
            //inner http.post -- actual sync:
            $http.post('/sync/post/profiles?username=' + username + "&password=" + password + "&record_id=").success(function (data) {
                $scope.needToConfirm = false;
                $scope.false = true;
                $scope.isSearching = false;
                location.reload();
                $mdDialog.cancel();
            }).error(function (data) {
                $scope.error = "An Error has occured while Uploading Data! ";
                $scope.isSearching = false;
            });
            //end 
        }
                
    };
    

}


/**
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that can be in foundin the LICENSE file at http://material.angularjs.org/license.
**/
