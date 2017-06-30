

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

    $scope.totalCount = 0;

    $scope.data.talakayan_date_from = '';
    $scope.data.talakayan_date_to = '';
    $scope.data.activity_name;
    $scope.data.survey_date_from = '';
    $scope.data.survey_date_to = '';
    $scope.data.talakayan_yr_id = '';
    
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

        if (ask === true) {
            $.post('/api/delete/perception_survey?id=' + removeitem.perception_survey_id).success(function (value) {
                $scope.loading = false;

                var index = $scope.Items.indexOf(removeitem);
                $scope.Items.splice(index, 1);

                $scope.totalCount = $scope.totalCount - 1;

                alert("Record removed!");

            }).error(function (data) {

                alert(JSON.stringify(data));

                $scope.error = "An Error has occured while Deleting! " + data.statusText;
                $scope.loading = false;
            });
        }
    };

    $scope.GotoPerceptionReport = function () {
        window.location.href = "/Report/talakayan";
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
               url: '/api/lib_cycle?id=' + 7
           }) //changes made here, removed comma
                    .success(function (data, status, headers, config) {

                        $scope.cycle_id_options = data;
                    })
                    .error(function (data, status, headers, config) {

                        $scope.message = 'Unexpected Error';
                    });
       });

    //$scope.$watch('data.talakayan_yr_id',
    //   function (newValue, oldValue) {
    //       var record = $scope;
    //       $http({
    //           method: 'GET',
    //           url: '/api/talakayan_year?id=' + newValue
    //       })
    //                .success(function (data, status, headers, config) {

    //                    $scope.talakayan_year_options = data;
    //                })
    //                .error(function (data, status, headers, config) {

    //                    $scope.message = 'Unexpected Error';
    //                });
    //   });

    $scope.$watch('data.region_code', function (newValue, oldValue) {

        var record = $scope;

        if (newValue === null) {
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
                url: '/api/online/lib_province?id=' + newValue

            }).success(function (data, status, headers, config) {

                $scope.prov_code_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });

    $scope.$watch('data.prov_code', function (newValue, oldValue) {

        var record = $scope;

        if (newValue === null) {

            record.data.brgy_code = null;
            record.data.city_code = null;
            record.city_code_options = [];
            record.brgy_code_options = [];
        }

        else {
            $http({
                method: 'GET',
                url: '/api/online/lib_city?id=' + newValue

            }).success(function (data, status, headers, config) {

                $scope.city_code_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });

    $scope.$watch('data.city_code', function (newValue, oldValue) {

        var record = $scope;

        if (newValue === null) {

            record.data.brgy_code = null;

            record.brgy_code_options = [];
        }

        else {
            $http({
                method: 'GET',
                url: '/api/online/lib_brgy?id=' + newValue

            }).success(function (data, status, headers, config) {

                $scope.brgy_code_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });


    $scope.clear = function () {
        $scope.data = {};
        $scope.search();
        $scope.recently_edited_is_checked = false;
        $scope.recently_added_is_checked = false;
    };

    $http.get('/api/report_list?id=89')
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

        $scope.data.pageSize = $scope.pageSize === undefined ? '' : $scope.pageSize;
        $scope.data.currPage = page === undefined ? '' : page;
        $scope.isSearching = true;
        $scope.isSearching = true;

        if (($scope.data.filter_by_recent_edit) && (!$scope.data.filter_by_recent_add)) {
            $.post('/api/offline/v1/perception_survey/get_recently_edited', $scope.data).success(function (value) {
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
            $.post('/api/offline/v1/perception_survey/get_recently_added', $scope.data).success(function (value) {
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
            $.post('/api/offline/v1/perception_survey/get_recently_edited_and_added', $scope.data).success(function (value) {
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
            $.post('/api/offline/v1/perception_survey/get_dto', $scope.data).success(function (value) {
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


    };


    $scope.search();

    $scope.report_list = ["_report1_sex_disaggreated_2015",
                                      "_report1_sex_disaggreated_2016"];

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
            //$scope.customFullscreen = (wantsFullScreen === true);
            $scope.customFullscreen = wantsFullScreen === true;
        });

    };

    //For exporting reports to Excel:

    $scope.report1_exportXls_2015 = function () {
        $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });

        $.post('/api/export/perception/report_1_2015', $scope.data).success(function (value) {
            $scope.loading = false;

            $scope.exported_data = value;
            setTimeout($.unblockUI, 10);

            alasql('SELECT * INTO XLSX("' + "report1_2015" + '.xlsx' + '",{headers:true}) FROM ?', [$scope.exported_data]);

            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    };

    $scope.report1_exportXls_2016 = function () {
        $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });

        $.post('/api/export/perception/report_1_2016', $scope.data).success(function (value) {
            $scope.loading = false;

            $scope.exported_data = value;

            setTimeout($.unblockUI, 10);

            alasql('SELECT * INTO XLSX("' + "report1_2016" + '.xlsx' + '",{headers:true}) FROM ?', [$scope.exported_data]);

            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    };


    $scope.report2_exportXls_2015 = function () {
        $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });

        $.post('/api/export/perception/report_2_2015', $scope.data).success(function (value) {
            $scope.loading = false;

            $scope.exported_data = value;

            setTimeout($.unblockUI, 10);

            alasql('SELECT * INTO XLSX("' + "report2_2015" + '.xlsx' + '",{headers:true}) FROM ?', [$scope.exported_data]);

            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    };

    $scope.report2_exportXls_2016 = function () {
        $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });

        $.post('/api/export/perception/report_2_2016', $scope.data).success(function (value) {
            $scope.loading = false;

            $scope.exported_data = value;

            setTimeout($.unblockUI, 10);

            alasql('SELECT * INTO XLSX("' + "report2_2016" + '.xlsx' + '",{headers:true}) FROM ?', [$scope.exported_data]);

            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    };

    //$scope.report3_exportXls111 = function () { 
    //    //var options = {
    //    //    headers: true,
    //    //    column: { style: { Font: { Bold: "1" } } },
    //    //    sheetid: 'Summary of Respondents'
    //    //};
    //    //alasql('SELECT COUNT(*) AS [Total Number of Respondents],' 
    //    //                + 'COUNT(case when is_male=true THEN 1 END) AS [Total Male],'
    //    //                + 'COUNT(case when is_male=false THEN 1 END) AS [Total Female], '
    //    //                + 'COUNT(case when is_pantawid=true THEN 1 END) AS [Total 4Ps], '
    //    //                + 'COUNT(case when is_slp=true THEN 1 END) AS [Total SLP], '
    //    //                + 'COUNT(case when is_ip=true THEN 1 END) AS [Total IP], '
    //    //                + 'COUNT(case when age>=60 THEN 1 END) AS [Total 60 yrs. old and above],'
    //    //                + 'COUNT(case when age>=40 && age<=59 THEN 1 END) AS [Total 40-59 yrs. old],'
    //    //                + 'COUNT(case when age>=21 && age<=39  THEN 1 END) AS [Total 21-39 yrs. old],'
    //    //                + 'COUNT(case when age<=20 THEN 1 END) AS [Total 20 yrs. old and below]'
    //    //       + 'INTO XLSX("PerceptionSurveyReport_SummaryOfRespondents.xlsx", ?) FROM ?', [options, $scope.Items]);
    //}

    $scope.report3_exportXls = function () {

        $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });

        $.post('/api/export/perception/report_3', $scope.data).success(function (value) {
            $scope.loading = false;

            $scope.exported_data = value;

            setTimeout($.unblockUI, 10);

            alasql('SELECT * INTO XLSX("' + "report3" + '.xlsx' + '",{headers:true}) FROM ?', [$scope.exported_data]);

            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });

    };


    $scope.report4_exportXls_2015 = function () {
        $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });

        $.post('/api/export/perception/report_4_2015', $scope.data).success(function (value) {
            $scope.loading = false;

            $scope.exported_data = value;

            setTimeout($.unblockUI, 10);

            alasql('SELECT * INTO XLSX("' + "report4_2015" + '.xlsx' + '",{headers:true}) FROM ?', [$scope.exported_data]);

            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));

            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    };

    $scope.report4_exportXls_2016 = function () {
        $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });

        $.post('/api/export/perception/report_4_2016', $scope.data).success(function (value) {
            $scope.loading = false;

            $scope.exported_data = value;

            setTimeout($.unblockUI, 10);

            alasql('SELECT * INTO XLSX("' + "report4_2016" + '.xlsx' + '",{headers:true}) FROM ?', [$scope.exported_data]);

            $scope.isSearching = false;

        }).error(function (data) {

            alert(JSON.stringify(data));

            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    };

    $scope.report5_exportXls_2015 = function () { //Actual Response of respondents for 2015 survey
        var options = {
            headers: true,
            column: { style: { Font: { Bold: "1" } } },
            sheetid: 'Perception Survey 2015'
        };

        alasql('SELECT CASE person_name WHEN NULL THEN "(Not stated)" ELSE person_name END AS [Full Name],'
                        + 'age AS [Age],'
                        + 'CASE is_male WHEN true THEN "Male" ELSE "Female" END AS [Sex],'
                        + 'CASE is_ip WHEN true THEN "Yes" ELSE "No" END AS [Is IP?],'
                        + 'CASE is_pantawid WHEN true THEN "Yes" ELSE "No" END AS [Is Pantawid?],'
                        + 'CASE is_slp WHEN true THEN "Yes" ELSE "No" END AS [Is SLP?],'
                        + 'trust_2 AS [The community trusts the local officials in planning, implementation, monitoring and reporting of programs and projects for the community],'
                        + 'trust_4 AS [Local officials are fair in dealing with the people],'
                        + 'trust_5 AS [Barangay officials regularly meet with the community],'
                        + 'trust_6 AS [Municipal officials regularly meet with the community],'
                        + 'trust_7 AS [I trust the local officials in planning, implementation, monitoring and reporting of programs and projects for the barangay],'
                        + 'access_1 AS [There is an adequate number of health facilities in our barangay],'
                        + 'access_3 AS [There is adequate number of educational facilities],'
                        + 'access_5 AS [There is adequate number of roads accessed by public vehicles (e.g. transportation of public goods)],'
                        + 'access_7 AS [Potable water is accessible to the community],'
                        + 'access_8 AS [There is peace and order in the community],'
                        + 'access_9 AS [It is easy to go to a health facility],'
                        + 'access_11 AS [Children in our household were able to go to school in less time],'
                        + 'access_12 AS [Our household is able to purchase basic necessities in the market/nearby store],'
                        + 'access_14 AS [I feel secure in the community],'
                        + 'access_15 AS [Our household has access to potable water],'
                        + 'participation_2 AS [Women are engaged in the implementation of programs/projects in the community],'
                        + 'participation_3 AS [(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/ projects in the community],'
                        + 'participation_4 AS [The poorest are represented in the barangay assembly],'
                        + 'participation_6 AS [Members of the community are provided with various skills training (e.g. livelihood, financial literacy among others)],'
                        + 'participation_7 AS [I am able to participate in the implementation of programs/ projects in the community],'
                        + 'participation_9 AS [I am not shy when participating in community development activities],'
                        + 'participation_11 AS [I have benefitted from the trainings/activities provided to the community],'
                        + 'participation_12 AS [(If answer to previous  is 3 or 4 only) My skills have improved because of these trainings],'
                        + 'disaster_3 AS [People in the community are aware of the dangers brought by disasters],'
                        + 'disaster_4 AS [I am aware of the dangers brought by disasters],'
                        + 'disaster_5 AS [People are aware of how the community is affected by disasters],'
                        + 'disaster_6 AS [I am aware of how the community is affected by disasters],'
                        + 'disaster_7 AS [I am aware of the different measures to reduce dangers brought by disasters],'
                        + 'disaster_8 AS [People in the community are aware of the different measures to reduce dangers brought by disasters]'
            + 'INTO XLSX("2015PerceptionSurveyReport_ActualResponsesofRespondents.xlsx", ?) FROM ? WHERE year=2015', [options, $scope.Items]);
    };

    $scope.report5_exportXls_2016 = function () { //Actual Response of respondents for 2016 survey
        var options = {
            headers: true,
            column: { style: { Font: { Bold: "1" } } },
            sheetid: 'Perception Survey 2016'
        };

        alasql('SELECT CASE person_name WHEN NULL THEN "(Not stated)" ELSE person_name END AS [Full Name],'
                        + 'age AS [Age],'
                        + 'CASE is_male WHEN true THEN "Male" ELSE "Female" END AS [Sex],'
                        + 'CASE is_ip WHEN true THEN "Yes" ELSE "No" END AS [Is IP?],'
                        + 'CASE is_pantawid WHEN true THEN "Yes" ELSE "No" END AS [Is Pantawid?],'
                        + 'CASE is_slp WHEN true THEN "Yes" ELSE "No" END AS [Is SLP?],'
                        + 'trust_1 AS [Barangay residents believe that our local officials are working for the benefit of our barangay],'
                        + 'trust_3 AS [Local officials are not fair in dealing with the people],'
                        + 'trust_5 AS [Barangay officials regularly meet with the community],'
                        + 'trust_6 AS [Municipal officials regularly meet with the community],'
                        + 'trust_8 AS [I do not believe that the local officials are working for the benefit of our barangay.],'
                        + 'access_1 AS [There is an adequate number of health facilities in our barangay],'
                        + 'access_2 AS [The number of educational facilities in our barangay is not enough.],'
                        + 'access_4 AS [Our barangay has adequate an number of roads accessed by public vehicles (e.g. transportation of public goods)],'
                        + 'access_6 AS [Potable water is not available to the community],'
                        + 'access_8 AS [There is peace and order in the community],'
                        + 'access_10 AS [School-aged children in our household are able to go to school],'
                        + 'access_12 AS [Our household is able to purchase basic necessities in the market/nearby store],'
                        + 'access_13 AS [I don’t feel secure from crimes in the community],'
                        + 'access_15 AS [Our household has access to potable water],'
                        + 'access_16 AS [If I or any of my household is sick, we can easily go to a health center],'
                        + 'participation_1 AS [Women are excluded from the implementation of programs/ projects in the community],'
                        + 'participation_3 AS [(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/ projects in the community],'
                        + 'participation_4 AS [The poorest are represented in the barangay assembly],'
                        + 'participation_5 AS [Members of the community are not given various skills training (e.g. livelihood, financial literacy, etc.)],'
                        + 'participation_7 AS [I am able to participate in the implementation of programs/ projects in the community],'
                        + 'participation_8 AS [I am shy when participating in community development activities],'
                        + 'participation_10 AS [I attend trainings/ activities (e.g. livelihood, financial literacy, etc.) provided to the community],'
                        + 'participation_12 AS [(If answer to previous  is 3 or 4 only) My skills have improved because of these trainings],'
                        + 'disaster_1 AS [People in the community are aware of the dangers brought by disasters],'
                        + 'disaster_2 AS [People in the community are not aware of the different measures to reduce dangers brought by disasters],'
                        + 'disaster_4 AS [I am aware of the dangers brought by disasters],'
                        + 'disaster_7 AS [I am aware of the different measures to reduce dangers brought by disasters],'
                        + 'disaster_9 AS [I think my barangay is not ready for any possible disaster]'
            + 'INTO XLSX("2016PerceptionSurveyReport_ActualResponsesofRespondents.xlsx", ?) FROM ? WHERE year=2016', [options, $scope.Items]);
    };

    $scope.viewReport_3 = function () {
        window.location.href = "/PerceptionSurvey/Report3";
    };

    $scope.createNew = function () {
        window.location.href = "/PerceptionSurvey/Perception";
    };

}); //end of module


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

       
        $http.post('/sync/get/perception_survey?username=' + username + "&password=" + password + "&city_code=" + $scope.city_code + "&record_id="
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
        $scope.error ='';
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
                var url = '/api/offline/v1/perception_survey/post/selected_items_to_sync?perception_survey_id='
                        + items_selected[i].perception_survey_id + "&push_status_id=" + 5;
                $scope.isAddingItem = true;

                $http.post(url).success(function (data) {
                    $scope.isAddingItem = false;
                    //inner http.post -- actual sync:
                    $http.post('/sync/post/perception_survey?username=' + username + "&password=" + password + "&record_id=").success(function (data) {
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
            $http.post('/sync/post/perception_survey?username=' + username + "&password=" + password + "&record_id=").success(function (data) {
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
