
//create angularjs controller
var app = angular.module('app', []);//set and get the angular module
app.controller('customerController', ['$scope', '$http', customerController]);




//angularjs controller method
function customerController($scope, $http) {




    $http.get('/Library/GetRegions').success(function (data) {
        $scope.lib_regions = data;
        $scope.loading = false;
    })




    //Edit Customer
    $scope.getProvinces = function () {




        $scope.loading = true;
        var frien = this.customer;

        frien.city_code = null;
        frien.brgy_code = null;


        if (frien.beneficiary_profile_id != null || frie.beneficiary_profile_id != 0) {
            $http({
                method: 'GET',
                url: '/Library/GetProvinces?id=' + frien.region_code,

            }).success(function (data, status, headers, config) {

                frien.lib_provinces = data;
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error';
            });
        }
        else {

            alert("sdfsdf");
            frien.lib_provinces = null;
        }
    }


    $scope.getCities = function () {

        $scope.loading = true;
        var frien = this.customer;


        frien.brgy_code = null;

        if (frien.prov_code != null) {
            $http({
                method: 'GET',
                url: '/Library/GetCity?id=' + frien.prov_code,

            }).success(function (data, status, headers, config) {

                frien.lib_cities = data;
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error';
            });
        }
        else {
            frien.lib_cities = null;
        }
    }

    $scope.getBarangays = function () {

        $scope.loading = true;
        var frien = this.customer;

        var id = frien.city_code;

        if (id != null) {
            $http({
                method: 'GET',
                url: '/Library/GetBarangay?id=' + id,

            }).success(function (data, status, headers, config) {

                frien.lib_barangays = data;
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error';
            });
        }
        else {
            frien.lib_barangays = null;
        }
    }
    //$scope.GetStates = function () {
    //    var countryId = $scope.country;
    //    if (countryId) {
    //        $http({
    //            method: 'POST',
    //            url: '/Home/GetStates/',
    //            data: JSON.stringify({ countryId: countryId })
    //        }).success(function (data, status, headers, config) {
    //            $scope.states = data;
    //        }).error(function (data, status, headers, config) {
    //            $scope.message = 'Unexpected Error';
    //        });
    //    }
    //    else {
    //        $scope.states = null;
    //    }
    //}



    //$scope.$watch('$scope.customers.region_code', function (newVal) {
    //    //    if (newVal) $scope.lib_cities = ['Los Angeles', 'San Francisco'];

    //    alert(newVal);
    //});



    //declare variable for mainain ajax load and entry or edit mode
    $scope.loading = true;
    $scope.addMode = false;

    //get all customer information
    $http.get('/api/Volunteer/Get').success(function (data) {
        $scope.loading = true;
        $scope.customers = data;

        angular.forEach($scope.customers, function (value, key) {

            $http({
                method: 'GET',
                url: '/Library/GetProvinces?id=' + value.region_code,

            }).success(function (data, status, headers, config) {

                value.lib_provinces = data;
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error';
            });


            $http({
                method: 'GET',
                url: '/Library/GetCity?id=' + value.prov_code,

            }).success(function (data, status, headers, config) {

                value.lib_cities = data;
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error';
            });

            $http({
                method: 'GET',
                url: '/Library/GetBarangay?id=' + value.city_code,

            }).success(function (data, status, headers, config) {

                value.lib_barangays = data;
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error';
            });

        });


        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });

    //by pressing toggleEdit button ng-click in html, this method will be hit
    $scope.toggleEdit = function () {
        this.customer.editMode = !this.customer.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Inser Customer
    $scope.add = function () {
        $scope.loading = true;

        var model = this.newcustomer;
        model.lib_provinces = [];

        $http.post('/api/Customer/', model).success(function (data) {
            alert("Added Successfully!!");
            $scope.addMode = false;
            $scope.customers.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Customer! " + data;
            $scope.loading = false;
        });
    };

    //Edit Customer
    $scope.save = function () {
        alert("Edit");
        $scope.loading = true;
        var frien = this.customer;
        alert(JSON.stringify(frien));
        $http.put('/api/Volunteer/Put?id=' + frien.beneficiary_profile_id, frien).success(function (data) {
            alert("Saved Successfully!!");
            frien.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving customer! " + data;
            $scope.loading = false;
        });
    };

    //Delete Customer
    $scope.deletecustomer = function () {
        $scope.loading = true;
        var Id = this.customer.Id;
        $http.delete('/api/Customer/' + Id).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.customers, function (i) {
                if ($scope.customers[i].Id === Id) {
                    $scope.customers.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Customer! " + data;
            $scope.loading = false;
        });
    };

}


