
      var dataarray;
var SkillsAndExperienceChoice;

$.ajax({
    url: '/SWDI/get_household_to_encode?id=012801001-4323-00021',
    async: false,
    dataType: 'json',
    success: function (data) {
        dataarray = data;

    }
});


var metaUrl = "/SWDI/get_metaanswers";




var viewModel = {};

//var ProfileCollection = ko.mapping.fromJS(dataarray);



//ko.applyBindings(ProfileCollection);

function success() {
    var self = this;

    var metaUrl = "/SWDI/get_metaanswers";

    self.ID1Choices = ko.observableArray([]);
    self.IIA1AChoices = ko.observableArray([]);
    self.IIA1BChoices = ko.observableArray([]);

    self.IIA2AChoices = ko.observableArray([]);

    self.IIA3AChoices = ko.observableArray([]);
    self.IIA3BChoices = ko.observableArray([]);
    self.IIA3CChoices = ko.observableArray([]);

    self.IIB1AChoices = ko.observableArray([]);
    self.IIB1BChoices = ko.observableArray([]);



    self.IIB2Choices = ko.observableArray([]);
    self.IIB3Choices = ko.observableArray([]);

    //ROLE PERFORMANCE OPTIONS
    self.IID1Choices = ko.observableArray([]);
    self.IID2Choices = ko.observableArray([]);
    self.IID3Choices = ko.observableArray([]);


    //FAMILY AWARENESS OPTIONS
    self.IIE1Choices = ko.observableArray([]);
    self.IIE2Choices = ko.observableArray([]);
    self.IIE3Choices = ko.observableArray([]);



    //INDIVIDUAL
    self.SkillsAndExperienceChoices = ko.observableArray([]);


    $.getJSON(metaUrl, {}, function (json) {


        $.each(json, function (key, data) {

            switch (data.filter) {

                case "IA1":
                    self.SkillsAndExperienceChoices.push(data);
                    break;

                case "ID1":
                    self.ID1Choices.push(data)
                    break;

                case "IIA1A":
                    self.IIA1AChoices.push(data)
                    break;

                case "IIA1B":
                    self.IIA1BChoices.push(data)
                    break;

                case "IIA2A":
                    self.IIA2AChoices.push(data)
                    break;

                case "IIA3A":
                    self.IIA3AChoices.push(data)
                    break;

                case "IIA3B":
                    self.IIA3BChoices.push(data)
                    break;

                case "IIA3C":
                    self.IIA3CChoices.push(data)
                    break;

                case "IIB1A":
                    self.IIB1AChoices.push(data)
                    break;

                case "IIB1B":
                    self.IIB1BChoices.push(data)
                    break;

                case "IIB2":
                    self.IIB2Choices.push(data)
                    break;

                case "IIB3":
                    self.IIB3Choices.push(data)
                    break;

                case "IID1":
                    self.IID1Choices.push(data)
                    break;

                case "IID2":
                    self.IID2Choices.push(data)
                    break;

                case "IID3":
                    self.IID3Choices.push(data)
                    break;

                case "IIE1":
                    self.IIE1Choices.push(data)
                    break;

                case "IIE2":
                    self.IIE2Choices.push(data)
                    break;

                case "IIE3":
                    self.IIE3Choices.push(data)
                    break;
            }
        });

    });




    self.saveProfile = function () {
        alert("Saved!");
    }


    viewModel = ko.mapping.fromJS(dataarray);


    //function ViewModel() {
    //    self = this;
    //    self.items = ko.observableArray();
    //}

    //var myViewModel = new ViewModel();
    //ko.applyBindings(myViewModel);

    //$.getJSON(url, function (data) {
    //    myViewModel.items.push(ko.mapping.fromJSON(data));
    //});


    ko.applyBindings(viewModel);
}
success();

