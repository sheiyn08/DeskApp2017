var urlContact = "/contact";
var AddressTypeData;
var PhoneTypeData;
var url = window.location.pathname;
var profileId = url.substring(url.lastIndexOf('/') + 1);


$.getJSON("/SWDI/get_staged_members?id=012801001-4323-00021", {}, function (data) {
    AddressTypeData.push(data);
});

$.getJSON("/SWDI/get_staged_members?id=012801001-4323-00021", {}, function (data) {
    PhoneTypeData.push(data);
});


//$.ajax({
//    url: urlContact + '/InitializePageData',
//    async: false,
//    dataType: 'json',
//    success: function (json) {
//        AddressTypeData = json.lstAddressTypeDTO;
//        PhoneTypeData = json.lstPhoneTypeDTO;
//    }
//});

$(function () {


    


    var HouseholdLine = function (household) {
        var self = this;
        self.HouseholdId = ko.observable(household ? household.HouseholdId : '').extend({ required: true, maxLength: 100 });
        self.Grantee = ko.observable(household ? household.Grantee : '').extend({ required: true, maxLength: 100 });
        self.region_name = ko.observable(household ? household.region_name : '').extend({ required: true, maxLength: 100 });
        self.prov_name = ko.observable(household ? household.prov_name : '').extend({ required: true, maxLength: 100 });
        self.city_name = ko.observable(household ? household.city_name : '').extend({ required: true, maxLength: 100 });
        self.brgy_name = ko.observable(household ? household.brgy_name : '').extend({ required: true, maxLength: 100 });
        self.HHSet = ko.observable(household ? household.HHSet : '').extend({ required: true, maxLength: 100 });
    }

    var SWDIEnhancedLine = function (swdienhanced) {
        var self = this;
        self.Id = ko.observable(swdienhanced ? swdienhanced.Id : '').extend({ required: true, maxLength: 100 });
        self.HouseholdId = ko.observable(swdienhanced ? swdienhanced.HouseholdId : '').extend({ required: true, maxLength: 100 });
        self.Year = ko.observable(swdienhanced ? swdienhanced.Year : '');
        self.IA1 = ko.observable(swdienhanced ? swdienhanced.IA1 : '');
        self.IB1 = ko.observable(swdienhanced ? swdienhanced.IB1 : '');
        self.IC1 = ko.observable(swdienhanced ? swdienhanced.IC1 : '');
        self.IC2 = ko.observable(swdienhanced ? swdienhanced.IC2 : '');
        self.IC3 = ko.observable(swdienhanced ? swdienhanced.IC3 : '');
        self.IC4 = ko.observable(swdienhanced ? swdienhanced.IC4 : '');
        self.RawIncome = ko.observable(swdienhanced ? swdienhanced.RawIncome : '');
        self.PerCapita = ko.observable(swdienhanced ? swdienhanced.PerCapita : '');
        self.ICCapitaResult = ko.observable(swdienhanced ? swdienhanced.ICCapitaResult : '');
        self.Remarks = ko.observable(swdienhanced ? swdienhanced.Remarks : '');
        self.ID1 = ko.observable(swdienhanced ? swdienhanced.ID1 : '').extend({ required: true, maxLength: 100 });
        self.EconomicSufficiency = ko.observable(swdienhanced ? swdienhanced.EconomicSufficiency : '');
        self.IIA1A = ko.observable(swdienhanced ? swdienhanced.IIA1A : '').extend({ required: true, maxLength: 100 });
        self.IIA1B = ko.observable(swdienhanced ? swdienhanced.IIA1B : '').extend({ required: true, maxLength: 100 });
        self.HealthCondition = ko.observable(swdienhanced ? swdienhanced.HealthCondition : '');
        self.IIA2A = ko.observable(swdienhanced ? swdienhanced.IIA2A : '').extend({ required: true, maxLength: 100 });
        self.IIA2B = ko.observable(swdienhanced ? swdienhanced.IIA2B : '');
        self.NutritionCondition = ko.observable(swdienhanced ? swdienhanced.NutritionCondition : '');
        self.IIA3A = ko.observable(swdienhanced ? swdienhanced.IIA3A : '').extend({ required: true, maxLength: 100 });
        self.IIA3B = ko.observable(swdienhanced ? swdienhanced.IIA3B : '').extend({ required: true, maxLength: 100 });
        self.IIA3C = ko.observable(swdienhanced ? swdienhanced.IIA3C : '').extend({ required: true, maxLength: 100 });
        self.WaterAndSanitation = ko.observable(swdienhanced ? swdienhanced.WaterAndSanitation : '');
        self.SA1_HealthComponent = ko.observable(swdienhanced ? swdienhanced.SA1_HealthComponent : '');
        self.IIB1A = ko.observable(swdienhanced ? swdienhanced.IIB1A : '').extend({ required: true, maxLength: 100 });
        self.IIB1B = ko.observable(swdienhanced ? swdienhanced.IIB1B : '').extend({ required: true, maxLength: 100 });
        self.IIB2 = ko.observable(swdienhanced ? swdienhanced.IIB2 : '').extend({ required: true, maxLength: 100 });
        self.IIB3 = ko.observable(swdienhanced ? swdienhanced.IIB3 : '').extend({ required: true, maxLength: 100 });
        self.SA2_HousingComponent = ko.observable(swdienhanced ? swdienhanced.SA2_HousingComponent : '');
        self.IIC1 = ko.observable(swdienhanced ? swdienhanced.IIC1 : '');
        self.IIC2 = ko.observable(swdienhanced ? swdienhanced.IIC2 : '');
        self.SA3_EducationComponent = ko.observable(swdienhanced ? swdienhanced.SA3_EducationComponent : '');
        self.IID1 = ko.observable(swdienhanced ? swdienhanced.IID1 : '').extend({ required: true, maxLength: 100 });
        self.IID2 = ko.observable(swdienhanced ? swdienhanced.IID2 : '').extend({ required: true, maxLength: 100 });
        self.IID3 = ko.observable(swdienhanced ? swdienhanced.IID3 : '').extend({ required: true, maxLength: 100 });


        self.SA4_RolePerformanceComponent = ko.observable(swdienhanced ? swdienhanced.SA4_RolePerformanceComponent : '');
        self.IIE1 = ko.observable(swdienhanced ? swdienhanced.IIE1 : '').extend({ required: true, maxLength: 100 });
        self.IIE2 = ko.observable(swdienhanced ? swdienhanced.IIE2 : '').extend({ required: true, maxLength: 100 });
        self.IIE3 = ko.observable(swdienhanced ? swdienhanced.IIE3 : '').extend({ required: true, maxLength: 100 });

        self.SA5_FamilyAwarenessComponent = ko.observable(swdienhanced ? swdienhanced.SA5_FamilyAwarenessComponent : '');
        self.SocialAdequacy = ko.observable(swdienhanced ? swdienhanced.SocialAdequacy : '');
        self.LowBRaw = ko.observable(swdienhanced ? swdienhanced.LowBRaw : '');
        self.Respondent = ko.observable(swdienhanced ? swdienhanced.Respondent : '').extend({ required: true, maxLength: 100 });

        self.Interviewer = ko.observable(swdienhanced ? swdienhanced.Interviewer : '').extend({ required: true, maxLength: 100 });

        self.DateInterviewed = ko.observable(swdienhanced ? swdienhanced.DateInterviewed : '').extend({ required: true, maxLength: 100 });

        self.Religion = ko.observable(swdienhanced ? swdienhanced.Religion : '').extend({ required: true, maxLength: 100 });
        self.EthnicGroup = ko.observable(swdienhanced ? swdienhanced.EthnicGroup : '').extend({ required: true, maxLength: 100 });
        self.IsGrantee = ko.observable(swdienhanced ? swdienhanced.IsGrantee : '').extend({ required: true, maxLength: 100 });

        self.Cellphone = ko.observable(swdienhanced ? swdienhanced.Cellphone : '');

        //arrays
        self.swdifamilyincomes = ko.observableArray([]);
        self.swdikcriders = ko.observableArray([]);
        self.swdimembers = ko.observableArray([new SWDIMemberLine]);
    }

    var SWDIMemberLine = function (swdimembers) {
        var self = this;
        self.FirstName = ko.observable(swdimembers ? swdimembers.FirstName : '').extend({ required: true, maxLength: 100 });
        self.LastName = ko.observable(swdimembers ? swdimembers.LastName : '');
        self.EntryId = ko.observable(swdimembers ? swdimembers.EntryId : '').extend({ required: true, maxLength: 100 });
        self.Id = ko.observable(swdimembers ? swdimembers.Id : '').extend({ required: true, maxLength: 100 });
        self.SWDIEnhancedId = ko.observable(swdimembers ? swdimembers.SWDIEnhancedId : '').extend({ required: true, maxLength: 100 });
        self.Year = ko.observable(swdimembers ? swdimembers.Year : '').extend({ required: true, maxLength: 100 });
        self.HouseholdId = ko.observable(swdimembers ? swdimembers.HouseholdId : '').extend({ required: true, maxLength: 100 });
        self.MiddleName = ko.observable(swdimembers ? swdimembers.MiddleName : '');
        self.ExtName = ko.observable(swdimembers ? swdimembers.ExtName : '').extend({ required: true, maxLength: 100 });
        self.Sex = ko.observable(swdimembers ? swdimembers.Sex : '').extend({ required: true, maxLength: 100 });
        self.BirthDate = ko.observable(swdimembers ? swdimembers.BirthDate : '').extend({ required: true, maxLength: 100 });
        self.Age = ko.observable(swdimembers ? swdimembers.Age : '').extend({ required: true, maxLength: 100 });
        self.MaritalStatus = ko.observable(swdimembers ? swdimembers.MaritalStatus : '').extend({ required: true, maxLength: 100 });
        self.HighestEducationalAttainment = ko.observable(swdimembers ? swdimembers.HighestEducationalAttainment : '').extend({ required: true, maxLength: 100 });
        self.PrimaryOccupation = ko.observable(swdimembers ? swdimembers.PrimaryOccupation : '').extend({ required: true, maxLength: 100 });
        self.ClassOfWorker = ko.observable(swdimembers ? swdimembers.ClassOfWorker : '').extend({ required: true, maxLength: 100 });
        self.Remarks = ko.observable(swdimembers ? swdimembers.Remarks : '');
        self.SkillsAndExperience = ko.observable(swdimembers ? swdimembers.SkillsAndExperience : '');
        self.Employment = ko.observable(swdimembers ? swdimembers.Employment : '').extend({ required: true, maxLength: 100 });
        self.BasicCompensation = ko.observable(swdimembers ? swdimembers.BasicCompensation : '').extend({ required: true, maxLength: 100 });
        self.CashCommision = ko.observable(swdimembers ? swdimembers.CashCommision : '').extend({ required: true, maxLength: 100 });
        self.CashAllowance = ko.observable(swdimembers ? swdimembers.CashAllowance : '').extend({ required: true, maxLength: 100 });
        self.SalaryAndWage = ko.observable(swdimembers ? swdimembers.SalaryAndWage : '').extend({ required: true, maxLength: 100 });
        self.Weight = ko.observable(swdimembers ? swdimembers.Weight : '');
        self.Literacy = ko.observable(swdimembers ? swdimembers.Literacy : '');
        self.Enrollment = ko.observable(swdimembers ? swdimembers.Enrollment : '');
        self.RelationshipToTheGrantee = ko.observable(swdimembers ? swdimembers.RelationshipToTheGrantee : '').extend({ required: true, maxLength: 100 });
        self.UnderOrOverWeight = ko.observable(swdimembers ? swdimembers.UnderOrOverWeight : '');
        self.IsFamilyMember = ko.observable(swdimembers ? swdimembers.IsFamilyMember : '').extend({ required: true, maxLength: 100 });



       
        //choices
        self.swdislprider = ko.observableArray([new SkillSet]);
        //    self.DesiredSkill = ko.observableArray([new SkillSet]);


    }

    var SWDIIncome = function (swdiincome) {
        var self = this;

      
    }


    var SWDIKCRiderLine = function (rider) {
        var self = this;


    }


    var MemberLine = function (members) {
        var self = this;
        self.SWDIMemberStagingId = ko.observable(members ? members.SWDIMemberStagingId : '').extend({ required: true, maxLength: 100 });
        self.FirstName = ko.observable(members ? members.FirstName : '').extend({ required: true, maxLength: 100 });
        self.LastName = ko.observable(members ? members.LastName : '');
        self.EntryId = ko.observable(members ? members.EntryId : '').extend({ required: true, maxLength: 100 });
        self.Id = ko.observable(members ? members.Id : '').extend({ required: true, maxLength: 100 });
        self.SWDIEnhancedId = ko.observable(members ? members.SWDIEnhancedId : '').extend({ required: true, maxLength: 100 });
        self.Year = ko.observable(members ? members.Year : '').extend({ required: true, maxLength: 100 });
        self.HouseholdId = ko.observable(members ? members.HouseholdId : '').extend({ required: true, maxLength: 100 });
        self.MiddleName = ko.observable(members ? members.MiddleName : '');
        self.ExtName = ko.observable(members ? members.ExtName : '').extend({ required: true, maxLength: 100 });
        self.Sex = ko.observable(members ? members.Sex : '').extend({ required: true, maxLength: 100 });
        self.BirthDate = ko.observable(members ? members.BirthDate : '').extend({ required: true, maxLength: 100 });
        self.Age = ko.observable(members ? members.Age : '').extend({ required: true, maxLength: 100 });
        self.MaritalStatus = ko.observable(members ? members.MaritalStatus : '').extend({ required: true, maxLength: 100 });
        self.HighestEducationalAttainment = ko.observable(members ? members.HighestEducationalAttainment : '').extend({ required: true, maxLength: 100 });
        self.PrimaryOccupation = ko.observable(members ? members.PrimaryOccupation : '').extend({ required: true, maxLength: 100 });
        self.ClassOfWorker = ko.observable(members ? members.ClassOfWorker : '').extend({ required: true, maxLength: 100 });
        self.Remarks = ko.observable(members ? members.Remarks : '');
        self.SkillsAndExperience = ko.observable(members ? members.SkillsAndExperience : '');
        self.Employment = ko.observable(members ? members.Employment : '').extend({ required: true, maxLength: 100 });
        self.BasicCompensation = ko.observable(members ? members.BasicCompensation : '').extend({ required: true, maxLength: 100 });
        self.CashCommision = ko.observable(members ? members.CashCommision : '').extend({ required: true, maxLength: 100 });
        self.CashAllowance = ko.observable(members ? members.CashAllowance : '').extend({ required: true, maxLength: 100 });
        self.SalaryAndWage = ko.observable(members ? members.SalaryAndWage : '').extend({ required: true, maxLength: 100 });
        self.Weight = ko.observable(members ? members.Weight : '');
        self.Literacy = ko.observable(members ? members.Literacy : '');
        self.Enrollment = ko.observable(members ? members.Enrollment : '');
        self.RelationshipToTheGrantee = ko.observable(members ? members.RelationshipToTheGrantee : '').extend({ required: true, maxLength: 100 });
        self.UnderOrOverWeight = ko.observable(members ? members.UnderOrOverWeight : '');
        self.IsFamilyMember = ko.observable(members ? members.IsFamilyMember : '').extend({ required: true, maxLength: 100 });



        //choices
        self.swdislprider = ko.observableArray([new SkillSet]);
        //    self.DesiredSkill = ko.observableArray([new SkillSet]);


    }


    var SkillSet = function (skill) {
        var self = this;
        self.MetaRiderId = ko.observable(skill ? skill.MetaRiderId : '').extend({ required: true, maxLength: 100 });
          

        self.HasSkill = ko.observable(skill ? skill.HasSkill : '').extend({ required: true, maxLength: 100 });
        self.SWDIMemberStagingId = ko.observable(skill ? skill.SWDIMemberStagingId : '').extend({ required: true, maxLength: 100 });
        self.Id = ko.observable(skill ? skill.Id : '').extend({ required: true, maxLength: 100 });
        self.EntryId = ko.observable(skill ? skill.EntryId : '').extend({ required: true, maxLength: 100 });

        self.Year = ko.observable(skill ? skill.Year : '').extend({ required: true, maxLength: 100 });
        self.HouseholdId = ko.observable(skill ? skill.HouseholdId : '').extend({ required: true, maxLength: 100 });



    }

    var ProfileCollection = function () {
        var self = this;


        

            self.skillset = ko.observableArray([]);
            $.getJSON("/SWDI/get_slp_skills?id=slpskill", {}, function (json) {
                self.skillset(json);
            });


            var metaUrl = "/SWDI/get_metaanswers" ;

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

      
      

        //if ProfileId is 0, It means Create new Profile
        //   if (profileId == 0) {
     

     //   self.tenantid = ko.observable('');
        //            self.skill = ko.observableArray([new SkillSet]);


        self.swdienhanced = ko.observable(new SWDIEnhancedLine());

        self.household = ko.observable(new HouseholdLine());

        self.members = ko.observableArray([new MemberLine]);

        self.swdimembers = ko.observableArray([new SWDIMemberLine]);


        $.getJSON("/SWDI/get_household_to_encode?id=012801001-4323-00021", {}, function (data) {
            alert(data.household.region_name);
           // self.swidienhanced = ko.observable(data.swdienhanced);

       //  <input type="checkbox" data-bind="checked: isDone" />    self.household = ko.observable(new HouseholdLine(data.household));
        //  self.household.push(new HouseholdLine(data.household));

          self.swdienhanced(data.swdienhanced);



          //$.each(json, function (key, data) {
          //    $.getJSON("/SWDI/get_single_member?id=" + data.swdienhanced.swdimembers.EntryId, {}, function (json) {

          //        self.swdimembers.push(new SWDIMemberLine(json))
          //    });

          //});



            //self.swdienhanced = ko.observableArray(ko.utils.arrayMap(data.swdienhanced, function (phone) {
            //                  return phone;
            //                }));


            //$.each(data.swdimembers, function (key, item) {
            //    self.members.push(item.swdimembers);
            //});


          //  self.members = ko.observable(new MemberLine(data.swdimembers));
           // self.swdienhanced = ko.observable(new SWDIEnhancedLine(data.swdimembers));
            //self.household(data);
        });


        $.ajax({
            url: '/SWDI/get_household?id=012801001-4323-00021',
            async: false,
            dataType: 'json',
            success: function (data) {
                //   self.household(data)

                self.household = ko.observable(new HouseholdLine(data));

            }
        });


  

        //$.getJSON("/SWDI/get_staged_members?id=012801001-4323-00021", {}, function (json) {
        //    $.each(json, function (key, data) {
        //        self.members.push(data);
        //    });
          
        //});

        //$.getJSON("/SWDI/get_encoded_members?id=012801001-4323-00021", {}, function (json) {
        //    self.swdimembers(json);
        //});


        


        //$.getJSON("/SWDI/get_slp_skills?id=slpskill", {}, function (data) {
        //    self.skill(data);

        //});
        //}
        //else {
        //    $.ajax({
        //        url: urlContact + '/GetProfileById/' + profileId,
        //        async: false,
        //        dataType: 'json',
        //        success: function (json) {
        //            self.profile = ko.observable(new Profile(json));
        //            self.phoneNumbers = ko.observableArray(ko.utils.arrayMap(json.PhoneDTO, function (phone) {
        //                return phone;
        //            }));
        //            self.addresses = ko.observableArray(ko.utils.arrayMap(json.AddressDTO, function (address) {
        //                return address;
        //            }));
        //        }
        //    });
        //}

        self.addFamilyMember = function (members) {
            $.getJSON("/SWDI/get_single_member?id=" + members.EntryId, {}, function (json) {
                self.members.remove(members);
                self.swdimembers.push(new SWDIMemberLine(json))
            });
        };


        self.notFamilyMember = function (members) {
         
            $.getJSON("/SWDI/get_single_member?id=" + members.EntryId, {}, function (json) {
                alert(json);
                self.members.remove(members);
                self.swdimembers.push(new SWDIMemberLine(json))
            });
        };

        //self.addPhone = function () { self.phoneNumbers.push(new PhoneLine()) };

        //self.removePhone = function (phone) { self.phoneNumbers.remove(phone) };

        //self.addAddress = function () { self.addresses.push(new AddressLine()) };

        //self.removeAddress = function (address) { self.addresses.remove(address) };

        //self.backToProfileList = function () { window.location.href = '/contact'; };

        //self.profileErrors = ko.validation.group(self.profile());
        //self.phoneErrors = ko.validation.group(self.phoneNumbers(), { deep: true });
        //self.addressErrors = ko.validation.group(self.addresses(), { deep: true });

        self.saveProfile = function () {
            alert("Saved!");
        }

        //    var isValid = true;

        //    if (self.profileErrors().length != 0) {
        //        self.profileErrors.showAllMessages();
        //        isValid = false;
        //    }

        //    if (self.phoneErrors().length != 0) {
        //        self.phoneErrors.showAllMessages();
        //        isValid = false;
        //    }

        //    if (self.addressErrors().length != 0) {
        //        self.addressErrors.showAllMessages();
        //        isValid = false;
        //    }

        //    if (isValid) {
        //        self.profile().AddressDTO = self.addresses;
        //        self.profile().PhoneDTO = self.phoneNumbers;

        //        $.ajax({
        //            type: (self.profile().ProfileId > 0 ? 'PUT' : 'POST'),
        //            cache: false,
        //            dataType: 'json',
        //            url: urlContact + (self.profile().ProfileId > 0 ? '/UpdateProfileInformation?id=' + self.profile().ProfileId : '/SaveProfileInformation'),
        //            data: JSON.stringify(ko.toJS(self.profile())),
        //            contentType: 'application/json; charset=utf-8',
        //            async: false,
        //            success: function (data) {
        //                window.location.href = '/contact';
        //            },
        //            error: function (err) {
        //                var err = JSON.parse(err.responseText);
        //                var errors = "";
        //                for (var key in err) {
        //                    if (err.hasOwnProperty(key)) {
        //                        errors += key.replace("profile.", "") + " : " + err[key];
        //                    }
        //                }
        //                $("<div></div>").html(errors).dialog({ modal: true, title: JSON.parse(err.responseText).Message, buttons: { "Ok": function () { $(this).dialog("close"); } } }).show();
        //            },
        //            complete: function () {
        //            }
        //        });
        //    }
       // };
    };

    ko.applyBindings(new ProfileCollection());
});