

angular.module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache', 'pims-pager']).controller('AppCtrl', function ($scope, $http, $mdDialog, $mdMedia) {
    $scope.data = {};

    $scope.udpateSPOthers = function () {
                $.blockUI({ message: '<h1><img src="@Url.Content("~/Images/kc_logo-copy.png")" /></h1> Processing...' });
                $http.post('/api/offline/v1/sub_project/update_sp_deskapp_others').success(function (data) {
                    alert("Data successfully copied to sp_deskapp_others table. You may check by refreshing the sqlite in Navicat. Thanks!");
                    setTimeout($.unblockUI, 3);
                }).error(function (data) {

                });
            };

})

/**
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that can be in foundin the LICENSE file at http://material.angularjs.org/license.
**/
