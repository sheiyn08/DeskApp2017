
function getUrlVars() {
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi,
    function (m, key, value) {
        vars[key] = value;
    });
    return vars == 'undefined' ? "" : vars;
}

var record_id = getUrlVars()["id"];

angular.module('MyApp', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache', 'pims-pager'])



.controller('AppCtrl', function ($scope, $mdDialog, $http) {





    $scope.data;

    $scope.problems = [];


    $http.get('/api/offline/v1/trainings/get?id=@ViewBag.id')
.then(function (response) {


    $scope.loading = true;

    $scope.data = response.data;





    $scope.search = function (page) {

        $http.get('/api/offline/v1/profiles/get_dto?city_code=' + $scope.data.city_code + "&pageSize=10&currPage=" + page)
         .then(function (value) {

             $scope.page = value.data.Page;
             $scope.pagesCount = value.data.TotalPages;
             $scope.totalCount = value.data.TotalCount;
             $scope.members = value.data.Items;
             $scope.isSearching = false;
         });

    }



    $scope.dt_start_date = new Date(response.data.start_date);
    $scope.dt_end_date = new Date(response.data.end_date);


    $scope.today = new Date();




    $scope.maxDate = new Date($scope.dt_start_date);

    $scope.$watch('dt_start_date', function (newValue, oldValue) {


        $scope.maxDate = new Date(newValue);


    });


    $scope.showAttributes = false;

    $scope.listParticipants = function () {

        $scope.showPSA = false;


        $http.get('/api/training_record_exists?id=@ViewBag.id').then(function (result) {


            $scope.message = "";


            if (result.data == true) {

                $scope.search();

                $scope.showParticipants = true;
            }
            else {

                $scope.message = "Record not yet saved in the database. Please save if before proceeding in adding other attributes.";
                $scope.showParticipants = false;
                alert($scope.message);
            }
        });

    }


    $scope.listPSA = function () {

        $scope.showParticipants = false;

        $http.get('/api/training_record_exists?id=@ViewBag.id').then(function (result) {


            $scope.message = "";

            if (result.data == true) {
                $http.get('/api/offline/v1/psa_problem/get_mapped?community_training_id='+ record_id).then(function (red) {

                    $scope.problems = red.data;


                })

                $scope.showPSA = true;
            }
            else {

                $scope.message = "Record not yet saved in the database. Please save if before proceeding in adding other attributes.";
                $scope.showPSA = false;
                alert($scope.message);
            }
        });
    }




    $scope.ConfirmParticipation = function (member) {
        var frien = member

        var url = '/api/offline/v1/trainings/SaveBeneficiaryTraining?person_profile_id='
        + member.person_profile_id + "&community_training_id=" + $scope.data.community_training_id +
        "&is_participant=" + !member.is_participant;



        $scope.isAddingPax = true;

        $http.post(url).success(function (data) {


            $scope.isAddingPax = false;

        }).error(function (data) {
            $scope.error = "An Error has occured while Saving! " + data;
            $scope.isAddingPax = false;
        });


    }


    $http.get('/api/lib_psa_solution_category')
.then(function (response) { $scope.psa_solution_category_id_options = response.data; });

    $http.get('/api/lib_psa_problem_category')
.then(function (response) { $scope.psa_problem_category_id_options = response.data; });



    $http.get('/api/lib_region')
.then(function (response) { $scope.region_code_options = response.data; });

    $http.get('/Api/lib_fund_source/')
.then(function (response) { $scope.fund_source_id_options = response.data; });

    $http.get('/api/lib_enrollment')
.then(function (response) { $scope.enrollment_id_options = response.data; });

    $http.get('/Api/lib_lgu_level/')
.then(function (response) { $scope.lgu_level_id_options = response.data; });


    $http.get('/Api/lib_training_category?lgu_level_id=' + $scope.data.lgu_level_id)
.then(function (response) { $scope.training_category_id_options = response.data; });


    $scope.$watch('data.fund_source_id', function (newValue, oldValue) {

        var record = $scope;

        if (newValue == null) {

            record.cycle_id = null;

            record.cycle_id_options = [];
        }

        else {
            $http({
                method: 'GET',
                url: '/api/lib_cycle?id=' + newValue,

            }).success(function (data, status, headers, config) {

                $scope.cycle_id_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });

    $scope.loading = false;

    $scope.edit = function () {
        this.editMode = !this.editMode;
    };


    $scope.cancel = function () {

   window.location.href ="/View/Trainings/"
    }


    $scope.addProblem = function () {
        var obj = {
            rank: $scope.rank,
            problem: $scope.problem,
            psa_problem_category_id: $scope.psa_problem_category_id,
            community_training_id: record_id
        };


        $.post('/api/offline/v1/psa_problem/save', obj).success(function (data) {
            $scope.loading = false;


            $('#addProblem').modal('toggle');

            $scope.problems.push(data);



        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    }

    $scope.openSolutionForm = function (item) {

        $scope.psa_problem_id = item.psa_problem_id;

        $('#addSolution').modal('toggle');

    }

    $scope.addSolution = function () {
        var obj = {

            solution: $scope.solution,
            psa_solution_category_id: $scope.psa_solution_category_id,
            psa_problem_id: $scope.psa_problem_id,
        };


        $.post('/api/offline/v1/psa_solution/save', obj).success(function (data) {
            $scope.loading = false;


            $('#addSolution').modal('toggle');

            //   $scope.problems.push(data);
            $scope.listPSA();


        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    }


    $scope.save = function () {
        $scope.$broadcast('show-errors-check-validity');
        $scope.loading = true;
        //       alert(JSON.stringify($scope.data));

        //'/api/v1/grievances/save'


        $.post('/api/offline/v1/trainings/save', $scope.data).success(function (data) {
            $scope.loading = false;

            window.location.href = data.url;

        }).error(function (data) {

            alert(JSON.stringify(data));


            $scope.error = "An Error has occured while Saving! " + data.statusText;
            $scope.loading = false;
        });
    };





    $scope.$watch('data.region_code', function (newValue, oldValue) {



        var record = $scope;

        if (newValue == null) {
            record.prov_code = '';
            record.city_code = '';
            record.brgy_code = '';


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

            record.brgy_code = '';
            record.city_code = '';
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

            record.brgy_code = '';

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


    $scope.$watch('data.lgu_level_id', function (newValue, oldValue) {

        var record = $scope;

        if (newValue == null) {

            record.training_category_id = '';
            record.training_category_id_options = [];
        }

        else {
            $http({
                method: 'GET',
                url: '/api/lib_training_category?lgu_level_id=' + newValue,

            }).success(function (data, status, headers, config) {

                $scope.training_category_id_options = data;
            }).error(function (data, status, headers, config) {

                $scope.message = 'Unexpected Error';
            });
        }

    });





    //$scope.search = function() {

    //    alert("gelow");
    //}
    //         $http.get('/api/offline/v1/profiles/get_dto?city_code=' + $scope.data.city_code + "&pageSize=10")
    //.then(function (value) {


    //    $scope.page = value.data.Page;
    //    $scope.pagesCount = value.data.TotalPages;
    //    $scope.totalCount = value.data.TotalCount;
    //    $scope.members = value.data.Items;
    //    $scope.isSearching = false;


    //angular.forEach($scope.members, function (todo) {


    //    $http.post('/Api/offline/v1/trainings/CheckParticipation?person_profile_id=' + todo.person_profile_id + "&community_training_id=" + $scope.data.community_training_id)
    //    .then(function (response) {

    //        if (response.data == true) {
    //            todo.is_participant = true;
    //        }
    //        else {
    //            todo.is_participant = false;
    //        }

    //    }).then(function () {
    //        $scope.isSearching = false;
    //    });



    //});


    //});





    //}






});





})
