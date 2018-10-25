

angular.module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache', 'pims-pager']).controller('AppCtrl', function ($scope, $http, $mdDialog, $mdMedia) {
    $scope.data = {};

    $scope.data.region_code = '';
    $scope.data.prov_code = '';
    $scope.data.city_code = '';
    $scope.data.brgy_code = '';
    $scope.data.fund_source_id = '';
    $scope.data.cycle_id = '';
    $scope.data.enrollment_id = '';
    $scope.data.pageSize = '';
    $scope.data.currPage = '';
    $scope.data.push_status_id = '';
    $scope.data.approval_id = '';
    $scope.data.module_id = '';

    //populate the Module dropdown:
    $scope.module_id_options = [
        { "Id": 1, "Name": "Barangay Profile" }, { "Id": 2, "Name": "Municipal Profile" },
        { "Id": 3, "Name": "Barangay Assembly" }, { "Id": 4, "Name": "Person Profile" },
        { "Id": 5, "Name": "Trainings" }, { "Id": 6, "Name": "Grievances" },
        { "Id": 7, "Name": "GRS Installation" }, { "Id": 8, "Name": "Sub Project" },
        { "Id": 9, "Name": "PTA Integration Checklist" }, { "Id": 10, "Name": "Municipal Consolidated LCC" },
        { "Id": 11, "Name": "Oversight and Coord. Committee" }, { "Id": 12, "Name": "Community Organizations" },
        { "Id": 13, "Name": "Perception Survey" }, { "Id": 14, "Name": "Municipal Talakayan Day Evaluation" }
    ];

    $scope.mov_to_push = [];

    $scope.showAlert = function () {
        alert("Synchronize functionality for this module is temporarily unavailable.");
    };

    $scope.setPushStatus = function (itemtopush) {
        $scope.mov_to_push.push(itemtopush);
    };



  $scope.delete = function (removeitem) {
      var ask = confirm("Are you sure you want to Delete attachment? This will also be deleted on the MOVs folder.");
        if (ask == true) {
            $.post('/api/delete/attached_document?id=' + removeitem.attached_document_id).success(function (value) {
                $scope.loading = false;
                var index = $scope.Items.indexOf(removeitem);
                $scope.Items.splice(index, 1);
                alert("Attachment removed!");
                $scope.search();
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

    $http.get('/api/all_lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });

    $http.get('/api/mov_list')
.then(function (response) { $scope.mov_list_id_options = response.data; });

    

 



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
        $scope.data = {};

        $scope.search();
    }


    $scope.search = function (page) {

        $scope.data.pageSize = $scope.pageSize == undefined ? '' : $scope.pageSize;
        $scope.data.currPage = page == undefined ? '' : page;

        $scope.isSearching = true;



        $scope.isSearching = true;

        $.post('/api/offline/v1/attachments/get_dto', $scope.data).success(function (value) {
                        
            $scope.loading = false;
            var base = window.location.origin;
         
             $scope.TotalUnAuthorized = value.TotalUnAuthorized;
                $scope.TotalSync = value.TotalSync;
            $scope.page = value.Page;
            $scope.pagesCount = value.TotalPages;
            $scope.totalCount = value.TotalCount;
            $scope.Items = value.Items;

            angular.forEach($scope.Items, function (item) {

                //$scope.file_url = base + "/MOVs/" + item.attached_document_id + ".pdf";
                $scope.movinfo = {};
                $scope.movinfo.attached_document_id = item.attached_document_id;
                $scope.movinfo.mov_list_id = item.attachment_id;

                //alert(JSON.stringify("Attached Document Id is: " + item.attached_document_id));

                //call function to check if GUID-only-filename exists on MOVs folder
                $http.get('/api/exists/movs/guid_only_filename?url=' + item.attached_document_id).then(function (result) {

                    if (result.data == true) {
                        //do something to change its file name to module_form_id
                        $.post('/api/offline/v1/attachment/rename_old_filename', $scope.movinfo).success(function (response) {
                            //alert("Success! Old filename renamed!");

                            //BrgyProfile
                            if (item.attachment_id == 3) {
                                item.url = base + "/MOVs/" + "BP_BrgyProfileForm_" + item.attached_document_id + ".pdf";
                            }
                            //Muni Profile
                            else if (item.attachment_id == 2) {
                                item.url = base + "/MOVs/" + "MP_MunicipalProfileForm_" + item.attached_document_id + ".pdf";
                            }
                            //Brgy Assembly
                            else if (item.attachment_id == 4) {
                                item.url = base + "/MOVs/" + "BA_BrgyActivityMinutesForm_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 5) {
                                item.url = base + "/MOVs/" + "BA_BAHouseholdParticipation_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 6) {
                                item.url = base + "/MOVs/" + "BA_BAAttendanceSheet_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 52) {
                                item.url = base + "/MOVs/" + "BA_BrgyResolution_" + item.attached_document_id + ".pdf";
                            }
                            //Person Profile
                            else if (item.attachment_id == 14) {
                                item.url = base + "/MOVs/" + "PP_CommunityVolunteersProfile_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 51) {
                                item.url = base + "/MOVs/" + "PP_CDDSPWorkerBasicProfile_" + item.attached_document_id + ".pdf";
                            }
                            //Training
                            else if (item.attachment_id == 7) {
                                item.url = base + "/MOVs/" + "TR_MunicipalActivityMinutesForm_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 8) {
                                item.url = base + "/MOVs/" + "TR_MunicipalAttendanceSheet_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 9) {
                                item.url = base + "/MOVs/" + "TR_BrgyActivityMinutesForm_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 10) {
                                item.url = base + "/MOVs/" + "TR_BrgyAttendanceSheet_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 11) {
                                item.url = base + "/MOVs/" + "TR_BrgyActionPlan_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 12) {
                                item.url = base + "/MOVs/" + "TR_BrgyResolution_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 13) {
                                item.url = base + "/MOVs/" + "TR_OtherDocuments_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 19) {
                                item.url = base + "/MOVs/" + "TR_MIBFMunicipalResolution_" + item.attached_document_id + ".pdf";
                            }
                            //Grievances
                            else if (item.attachment_id == 22) {
                                item.url = base + "/MOVs/" + "GR_GRSIntakeForm_" + item.attached_document_id + ".pdf";
                            }
                            //GRS Installation
                            else if (item.attachment_id == 20) {
                                item.url = base + "/MOVs/" + "GI_GRSInstallationChecklistMunicipal_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 21) {
                                item.url = base + "/MOVs/" + "GI_GRSInstallationChecklistBrgy_" + item.attached_document_id + ".pdf";
                            }
                            //SubProject
                            else if (item.attachment_id == 24) {
                                item.url = base + "/MOVs/" + "SP_BrgySPWorkSchedandPhysicalProgressReport_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 25) {
                                item.url = base + "/MOVs/" + "SP_SuspensionOrder_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 26) {
                                item.url = base + "/MOVs/" + "SP_ResumptionOrder_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 27) {
                                item.url = base + "/MOVs/" + "SP_VariationOrder_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 28) {
                                item.url = base + "/MOVs/" + "SP_TargetHouseholdBeneficiaries_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 29) {
                                item.url = base + "/MOVs/" + "SP_CNC_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 30) {
                                item.url = base + "/MOVs/" + "SP_CNO_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 31) {
                                item.url = base + "/MOVs/" + "SP_CP_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 32) {
                                item.url = base + "/MOVs/" + "SP_UsufructAgreement_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 33) {
                                item.url = base + "/MOVs/" + "SP_BLGUResolution_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 34) {
                                item.url = base + "/MOVs/" + "SP_DepEdCertification_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 35) {
                                item.url = base + "/MOVs/" + "SP_EmploymentRecordSheet_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 36) {
                                item.url = base + "/MOVs/" + "SP_CDDSPWorkerBasicProfile_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 37) {
                                item.url = base + "/MOVs/" + "SP_SPFundUtilizationReport_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 38) {
                                item.url = base + "/MOVs/" + "SP_CADT_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 39) {
                                item.url = base + "/MOVs/" + "SP_RequestforValidationtoNCIP_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 40) {
                                item.url = base + "/MOVs/" + "SP_Tariff_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 41) {
                                item.url = base + "/MOVs/" + "SP_SPCompletionReport_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 42) {
                                item.url = base + "/MOVs/" + "SP_FinalInspectionReport_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 43) {
                                item.url = base + "/MOVs/" + "SP_CertofCompletionandAcceptance_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 44) {
                                item.url = base + "/MOVs/" + "SP_FunctionalAuditTool_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 45) {
                                item.url = base + "/MOVs/" + "SP_ActualHouseholdBeneficiaries_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 46) {
                                item.url = base + "/MOVs/" + "SP_SustainabilityEvaluationTool_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 55) {
                                item.url = base + "/MOVs/" + "SP_ESSC_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 56) {
                                item.url = base + "/MOVs/" + "SP_ESMP_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 57) {
                                item.url = base + "/MOVs/" + "SP_ECC_" + item.attached_document_id + ".pdf";
                            }
                            //PTA
                            else if (item.attachment_id == 16) {
                                item.url = base + "/MOVs/" + "PTA_PTAIntegrationPlansChecklist_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 53) {
                                item.url = base + "/MOVs/" + "PTA_BrgyResolution_" + item.attached_document_id + ".pdf";
                            }
                            else if (item.attachment_id == 54) {
                                item.url = base + "/MOVs/" + "PTA_MunicipalResolution_" + item.attached_document_id + ".pdf";
                            }
                            //MLCC
                            else if (item.attachment_id == 23) {
                                item.url = base + "/MOVs/" + "MLCC_MunicipalConsolidatedStatusofLCC_" + item.attached_document_id + ".pdf";
                            }
                            //Oversight
                            else if (item.attachment_id == 15) {
                                item.url = base + "/MOVs/" + "OC_OversightandCoordCommitteeChecklist_" + item.attached_document_id + ".pdf";
                            }
                            //Organizations
                            else if (item.attachment_id == 50) {
                                item.url = base + "/MOVs/" + "CO_CommunityOrganizationProfileForm_" + item.attached_document_id + ".pdf";
                            }
                            //Perception
                            else if (item.attachment_id == 47) {
                                item.url = base + "/MOVs/" + "PS_PerceptionSurveyForm_" + item.attached_document_id + ".pdf";
                            }
                            //Talakayan
                            else if (item.attachment_id == 49) {
                                item.url = base + "/MOVs/" + "MT_MunicipalTalakayanEvaluationForm_" + item.attached_document_id + ".pdf";
                            }
                            else {
                                alert("Attachment not found");
                            }
                        });
                    }

                    else {
                        //BrgyProfile
                        if (item.attachment_id == 3) {
                            item.url = base + "/MOVs/" + "BP_BrgyProfileForm_" + item.attached_document_id + ".pdf";
                        }
                            //Muni Profile
                        else if (item.attachment_id == 2) {
                            item.url = base + "/MOVs/" + "MP_MunicipalProfileForm_" + item.attached_document_id + ".pdf";
                        }
                            //Brgy Assembly
                        else if (item.attachment_id == 4) {
                            item.url = base + "/MOVs/" + "BA_BrgyActivityMinutesForm_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 5) {
                            item.url = base + "/MOVs/" + "BA_BAHouseholdParticipation_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 6) {
                            item.url = base + "/MOVs/" + "BA_BAAttendanceSheet_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 52) {
                            item.url = base + "/MOVs/" + "BA_BrgyResolution_" + item.attached_document_id + ".pdf";
                        }
                            //Person Profile
                        else if (item.attachment_id == 14) {
                            item.url = base + "/MOVs/" + "PP_CommunityVolunteersProfile_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 51) {
                            item.url = base + "/MOVs/" + "PP_CDDSPWorkerBasicProfile_" + item.attached_document_id + ".pdf";
                        }
                            //Training
                        else if (item.attachment_id == 7) {
                            item.url = base + "/MOVs/" + "TR_MunicipalActivityMinutesForm_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 8) {
                            item.url = base + "/MOVs/" + "TR_MunicipalAttendanceSheet_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 9) {
                            item.url = base + "/MOVs/" + "TR_BrgyActivityMinutesForm_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 10) {
                            item.url = base + "/MOVs/" + "TR_BrgyAttendanceSheet_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 11) {
                            item.url = base + "/MOVs/" + "TR_BrgyActionPlan_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 12) {
                            item.url = base + "/MOVs/" + "TR_BrgyResolution_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 13) {
                            item.url = base + "/MOVs/" + "TR_OtherDocuments_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 19) {
                            item.url = base + "/MOVs/" + "TR_MIBFMunicipalResolution_" + item.attached_document_id + ".pdf";
                        }
                            //Grievances
                        else if (item.attachment_id == 22) {
                            item.url = base + "/MOVs/" + "GR_GRSIntakeForm_" + item.attached_document_id + ".pdf";
                        }
                            //GRS Installation
                        else if (item.attachment_id == 20) {
                            item.url = base + "/MOVs/" + "GI_GRSInstallationChecklistMunicipal_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 21) {
                            item.url = base + "/MOVs/" + "GI_GRSInstallationChecklistBrgy_" + item.attached_document_id + ".pdf";
                        }
                            //SubProject
                        else if (item.attachment_id == 24) {
                            item.url = base + "/MOVs/" + "SP_BrgySPWorkSchedandPhysicalProgressReport_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 25) {
                            item.url = base + "/MOVs/" + "SP_SuspensionOrder_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 26) {
                            item.url = base + "/MOVs/" + "SP_ResumptionOrder_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 27) {
                            item.url = base + "/MOVs/" + "SP_VariationOrder_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 28) {
                            item.url = base + "/MOVs/" + "SP_TargetHouseholdBeneficiaries_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 29) {
                            item.url = base + "/MOVs/" + "SP_CNC_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 30) {
                            item.url = base + "/MOVs/" + "SP_CNO_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 31) {
                            item.url = base + "/MOVs/" + "SP_CP_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 32) {
                            item.url = base + "/MOVs/" + "SP_UsufructAgreement_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 33) {
                            item.url = base + "/MOVs/" + "SP_BLGUResolution_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 34) {
                            item.url = base + "/MOVs/" + "SP_DepEdCertification_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 35) {
                            item.url = base + "/MOVs/" + "SP_EmploymentRecordSheet_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 36) {
                            item.url = base + "/MOVs/" + "SP_CDDSPWorkerBasicProfile_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 37) {
                            item.url = base + "/MOVs/" + "SP_SPFundUtilizationReport_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 38) {
                            item.url = base + "/MOVs/" + "SP_CADT_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 39) {
                            item.url = base + "/MOVs/" + "SP_RequestforValidationtoNCIP_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 40) {
                            item.url = base + "/MOVs/" + "SP_Tariff_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 41) {
                            item.url = base + "/MOVs/" + "SP_SPCompletionReport_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 42) {
                            item.url = base + "/MOVs/" + "SP_FinalInspectionReport_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 43) {
                            item.url = base + "/MOVs/" + "SP_CertofCompletionandAcceptance_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 44) {
                            item.url = base + "/MOVs/" + "SP_FunctionalAuditTool_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 45) {
                            item.url = base + "/MOVs/" + "SP_ActualHouseholdBeneficiaries_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 46) {
                            item.url = base + "/MOVs/" + "SP_SustainabilityEvaluationTool_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 55) {
                            item.url = base + "/MOVs/" + "SP_ESSC_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 56) {
                            item.url = base + "/MOVs/" + "SP_ESMP_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 57) {
                            item.url = base + "/MOVs/" + "SP_ECC_" + item.attached_document_id + ".pdf";
                        }
                            //PTA
                        else if (item.attachment_id == 16) {
                            item.url = base + "/MOVs/" + "PTA_PTAIntegrationPlansChecklist_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 53) {
                            item.url = base + "/MOVs/" + "PTA_BrgyResolution_" + item.attached_document_id + ".pdf";
                        }
                        else if (item.attachment_id == 54) {
                            item.url = base + "/MOVs/" + "PTA_MunicipalResolution_" + item.attached_document_id + ".pdf";
                        }
                            //MLCC
                        else if (item.attachment_id == 23) {
                            item.url = base + "/MOVs/" + "MLCC_MunicipalConsolidatedStatusofLCC_" + item.attached_document_id + ".pdf";
                        }
                            //Oversight
                        else if (item.attachment_id == 15) {
                            item.url = base + "/MOVs/" + "OC_OversightandCoordCommitteeChecklist_" + item.attached_document_id + ".pdf";
                        }
                            //Organizations
                        else if (item.attachment_id == 50) {
                            item.url = base + "/MOVs/" + "CO_CommunityOrganizationProfileForm_" + item.attached_document_id + ".pdf";
                        }
                            //Perception
                        else if (item.attachment_id == 47) {
                            item.url = base + "/MOVs/" + "PS_PerceptionSurveyForm_" + item.attached_document_id + ".pdf";
                        }
                            //Talakayan
                        else if (item.attachment_id == 49) {
                            item.url = base + "/MOVs/" + "MT_MunicipalTalakayanEvaluationForm_" + item.attached_document_id + ".pdf";
                        }
                        else {
                            alert("Attachment not found");
                        }
                    }
                });
                  

            })
            $scope.isSearching = false;

            //4.2: change records with push_status_id 5 to 2 if not uploaded
            $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });
            $http.post('/api/offline/v1/mov/clean_push_status_id').success(function (data) {
                //alert("Zero values updated!");
                setTimeout($.unblockUI, 3);
            }).error(function (data) {

            });

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });


    };


    $scope.search();


    //v4.0 Uploading of MOV to WebApp (one at a time)
    $scope.uploadMOV = function (movtoupload) {



        if ($scope.fileList.length > 1) {
            alert("Select 1 image only");
            return false;

        }
        for (var i = 0; i < $scope.fileList.length; i++) {


            if ($scope.fileList[i].uploaded != true) {

                $scope.UploadFileIndividual($scope.fileList[i].file,
                                            $scope.fileList[i].file.name,
                                            $scope.fileList[i].file.type,
                                            $scope.fileList[i].file.size,
                                            i);

            }
        }
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
            fullscreen: useFullScreen,
            locals:
                {
                    itemtopush: $scope.mov_to_push
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


function DialogController($scope, $mdDialog, $http, itemtopush) {

//injecting the array from angular.module
    $scope.mov_to_push = itemtopush;
   

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
    
    $scope.syncPost = function () {
        $scope.isSearching = true;        
        $scope.total = 0;
        $scope.finished = 0;
        var credentials;
        var username = $scope.username;
        var password = $scope.password;

        //call API that will update the push_status_id to 5:
        for (i = 0; i < $scope.mov_to_push.length; i++) {
            var url = '/api/offline/v1/attachments/post/mov_for_uploading?attached_document_id='
                    + itemtopush[i].attached_document_id + "&push_status_id=" + 5;
            $scope.isAddingItem = true;

            $http.post(url).success(function (data) {
                $scope.isAddingItem = false;
                //inner http.post -- actual sync:
                $http.post('/sync/post/attachments?username=' + username + "&password=" + password).success(function (data) {
                    $scope.false = true;
                    $scope.isSearching = false;
                    location.reload();
                    $mdDialog.cancel();
                }).error(function (data) {
                    $scope.error = "An Error has occured while Uploading Data! " + data.statusText;
                    $scope.isSearching = false;
                });
                //end 
            }).error(function (data) {
                $scope.error = "An Error has occured while Saving! " + data;
                $scope.isAddingItem = false;
            });
        }

    };

}

//.directive('demoPager', function () {
//    return {
//        scope: {
//            page: '@',
//            pagesCount: '@',
//            totalCount: '@',
//            searchFunc: '&'
//        },
//        replace: true,
//        restrict: 'E',
//        templateUrl: '../static/templates/pager-template.html',
//        controller: ['$scope', function ($scope) {
//            $scope.search = function (i) {
//                if ($scope.searchFunc) {
//                    $scope.searchFunc({ page: i });
//                }
//            };

//            $scope.range = function () {
//                if (!$scope.pagesCount) { return []; }
//                var step = 2;
//                var doubleStep = step * 2;
//                var start = Math.max(0, $scope.page - step);
//                var end = start + 1 + doubleStep;
//                if (end > $scope.pagesCount) { end = $scope.pagesCount; }

//                var ret = [];
//                for (var i = start; i != end; ++i) {
//                    ret.push(i);
//                }

//                return ret;
//            };
//        }]
//    }
//})










/**
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that can be in foundin the LICENSE file at http://material.angularjs.org/license.
**/
