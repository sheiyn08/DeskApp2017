﻿

angular.module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache', 'pims-pager']).controller('AppCtrl', function ($scope, $http, $mdDialog, $mdMedia) {
    $scope.data = {};

    $scope.data.region_code = '';
    $scope.data.prov_code = '';
    $scope.data.city_code = '';
    $scope.data.cycle_id = '';
    $scope.data.pageSize = '';
    $scope.data.currPage = '';  
    $scope.data.push_status_id = '';
    $scope.data.approval_id = '';    
    $scope.data.talakayan_date_start = '';
    $scope.data.talakayan_date_end = '';
    $scope.data.talakayan_yr_id = '';

    $scope.talakayan_year_options = [{ "Id": 1, "Name": "2015" }, { "Id": 2, "Name": "2016" }, { "Id": 3, "Name": "2017" }, { "Id": 4, "Name": "2018" }, { "Id": 5, "Name": "2019" }];

    $scope.needToConfirm = false;

    $scope.list_of_selected_items = [];

    $scope.showAlert = function () {
        alert("Synchronize functionality for this module is temporarily unavailable.");
    };

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
            $.post('/api/delete/muni_financial_profile?id=' + removeitem.muni_financial_profile_id).success(function (value) {
                $scope.loading = false;

                var index = $scope.Items.indexOf(removeitem);
                $scope.Items.splice(index, 1);

                $scope.totalCount = $scope.totalCount - 1;

                alert("Record removed!")

            }).error(function (data) {

                alert(JSON.stringify(data));

                $scope.error = "An Error has occured while Deleting! " + data.statusText;
                $scope.loading = false;
            });
        }
    }

    $scope.GotoFinancialReport = function () {
        window.location.href = "/Report/municipalfinancialprofile";
    };


    $http.get('/api/online/lib_region')
.then(function (response) { $scope.region_code_options = response.data; });

    $http.get('/api/lib_cycle')
.then(function (response) { $scope.cycle_id_options = response.data; });

//    $http.get('/api/talakayan_year')
//.then(function (response) { $scope.talakayan_year_options = response.data; });

    //populating the CURRENT NCDDP Cycle field:
    $scope.$watch('data.cycle_id',
       function (newValue, oldValue) {
           var record = $scope;
           //newValue == 7; //fund-source id is 7 for KC-NCDDP
           $http({
               method: 'GET',
               url: '/api/lib_cycle?id=' + 7,

           })
                    .success(function (data, status, headers, config) {

                        $scope.cycle_id_options = data;
                    })
                    .error(function (data, status, headers, config) {

                        $scope.message = 'Unexpected Error';
                    });
       });

    //$scope.$watch('data.talakayan_yr_id',
    //  function (newValue, oldValue) {
    //      var record = $scope;
    //      $http({
    //          method: 'GET',
    //          url: '/api/talakayan_year?id=' + newValue,

    //      })
    //               .success(function (data, status, headers, config) {

    //                   $scope.talakayan_year_options = data;
    //               })
    //               .error(function (data, status, headers, config) {

    //                   $scope.message = 'Unexpected Error';
    //               });
    //  });

    $scope.$watch('data.region_code', function (newValue, oldValue) {

        var record = $scope;

        if (newValue == null) {
            record.data.prov_code = null;
            record.data.city_code = null;
            
            record.prov_code_options = [];
            record.city_code_options = [];
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

    //CLEAR function on Index:
    $scope.clear = function () {
        $scope.data = {};
        $scope.search();
        $scope.recently_edited_is_checked = false;
        $scope.recently_added_is_checked = false;
    }

    $http.get('/api/report_list?id=92')
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


    //SEARCH/FILTER function on Index
    $scope.search = function (page) {

        $scope.data.pageSize = $scope.pageSize == undefined ? '' : $scope.pageSize;
        $scope.data.currPage = page == undefined ? '' : page;

        $scope.isSearching = true;
        $scope.isSearching = true;

        $.post('/api/offline/v1/muni_financial_profile/get_dto', $scope.data).success(function (value) {

            $scope.loading = false;

            $scope.TotalUnAuthorized = value.TotalUnAuthorized;
            $scope.TotalSync = value.TotalSync;
            //  $scope.itemsToSync = value.data.itemsToSync;
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

    $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');

    $scope.showAdvanced = function (ev) {
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;

        window.onbeforeunload = null;
        //window.unbind();

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


    $scope.createNew = function () {
        window.location.href = "/MunicipalFinancialProfile/FinancialProfile";
    }

}) //end of module


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


        $http.post('/sync/get/muni_financial_profile?username=' + username + "&password=" + password + "&city_code=" + $scope.city_code + "&record_id=").success(function (data) {
            
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
                var url = '/api/offline/v1/muni_financial_profile/post/selected_items_to_sync?muni_financial_profile_id='
                        + items_selected[i].muni_financial_profile_id + "&push_status_id=" + 5;
                $scope.isAddingItem = true;

                $http.post(url).success(function (data) {
                    $scope.isAddingItem = false;
                    //inner http.post -- actual sync:
                    $http.post('/sync/post/muni_financial_profile?username=' + username + "&password=" + password + "&record_id=").success(function (data) {
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
            $http.post('/sync/post/muni_financial_profile?username=' + username + "&password=" + password + "&record_id=").success(function (data) {
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
