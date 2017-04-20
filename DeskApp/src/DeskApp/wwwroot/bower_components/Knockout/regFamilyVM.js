
$(document).ready(function () {

	$.ajaxSetup({
		//async: true
	});

    //enables bootstrap tooltips
	$("body").tooltip({ selector: '[data-toggle=tooltip]' });

    //datepicker binding
	ko.bindingHandlers.datepicker = {
		init: function (element, valueAccessor, allBindingsAccessor) {
			var options = allBindingsAccessor().datepickerOptions || {};
			$(element).datepicker(options).on("changeDate", function (ev) {
				var observable = valueAccessor();
				observable(ev.date);
			});
		},
		update: function (element, valueAccessor) {
			var value = ko.utils.unwrapObservable(valueAccessor());
			$(element).datepicker("setValue", value);
		}
	};

	//for creating observable inside razor
	//used in getting the encoder username and tenantid as hidden input
	ko.bindingHandlers.valueWithInit = {
		init: function (element, valueAccessor, allBindingsAccessor, data, context) {
			var property = valueAccessor(),
				value = element.value;
			//create the observable, if it doesn't exist 
			if (!ko.isWriteableObservable(data[property])) {
				data[property] = ko.observable();
			}

			data[property](value);

			ko.applyBindingsToNode(element, { value: data[property] }, context);
		}
	};

	//AutoComplete Handler
	//jqAuto -- main binding (should contain additional options to pass to autocomplete)
	//jqAutoSource -- the array to populate with choices (needs to be an observableArray)
	//jqAutoQuery -- function to return choices
	//jqAutoValue -- where to write the selected value
	//jqAutoSourceLabel -- the property that should be displayed in the possible choices
	//jqAutoSourceInputValue -- the property that should be displayed in the input box
	//jqAutoSourceValue -- the property to use for the value
	ko.bindingHandlers.jqAuto = {
		init: function (element, valueAccessor, allBindingsAccessor, RegisterViewModel) {
			var options = valueAccessor() || {},
				allBindings = allBindingsAccessor(),
				unwrap = ko.utils.unwrapObservable,
				modelValue = allBindings.jqAutoValue,
				source = allBindings.jqAutoSource,
				query = allBindings.jqAutoQuery,
				valueProp = allBindings.jqAutoSourceValue,
				inputValueProp = allBindings.jqAutoSourceInputValue || valueProp,
				labelProp = allBindings.jqAutoSourceLabel || inputValueProp;

			//function that is shared by both select and change event handlers
			function writeValueToModel(valueToWrite) {
				if (ko.isWriteableObservable(modelValue)) {
					modelValue(valueToWrite);
				} else {  //write to non-observable
					if (allBindings['_ko_property_writers'] && allBindings['_ko_property_writers']['jqAutoValue'])
						allBindings['_ko_property_writers']['jqAutoValue'](valueToWrite);
				}
			}

			//on a selection write the proper value to the model
			options.select = function (event, ui) {
				writeValueToModel(ui.item ? ui.item.actualValue : null);
			};

			//on a change, make sure that it is a valid value or clear out the model value
			options.change = function (event, ui) {
				var currentValue = $(element).val();
				var matchingItem = ko.utils.arrayFirst(unwrap(source), function (item) {
					return unwrap(inputValueProp ? item[inputValueProp] : item) === currentValue;
				});

				if (!matchingItem) {
					writeValueToModel(null);
				}
			}

			//hold the autocomplete current response
			var currentResponse = null;

			//handle the choices being updated in a DO, to decouple value updates from source (options) updates
			var mappedSource = ko.computed({
				read: function () {
					mapped = ko.utils.arrayMap(unwrap(source), function (item) {
						var result = {};
						result.label = labelProp ? unwrap(item[labelProp]) : unwrap(item).toString();  //show in pop-up choices
						result.value = inputValueProp ? unwrap(item[inputValueProp]) : unwrap(item).toString();  //show in input box
						result.actualValue = valueProp ? unwrap(item[valueProp]) : item;  //store in model
						return result;
					});
					return mapped;
				},
				write: function (newValue) {
					source(newValue);  //update the source observableArray, so our mapped value (above) is correct
					if (currentResponse) {
						currentResponse(mappedSource());
					}
				},
				disposeWhenNodeIsRemoved: element
			});

			if (query) {
				options.source = function (request, response) {
					currentResponse = response;
					query.call(this, request.term, mappedSource);
				}
			} else {
				//whenever the items that make up the source are updated, make sure that autocomplete knows it
				mappedSource.subscribe(function (newValue) {
					$(element).autocomplete("option", "source", newValue);
				});

				options.source = mappedSource();
			}


			//initialize autocomplete
			$(element).autocomplete(options);
		},
		update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			//update value based on a model change
			var allBindings = allBindingsAccessor(),
				unwrap = ko.utils.unwrapObservable,
				modelValue = unwrap(allBindings.jqAutoValue) || '',
				valueProp = allBindings.jqAutoSourceValue,
				inputValueProp = allBindings.jqAutoSourceInputValue || valueProp;

			//if we are writing a different property to the input than we are writing to the model, then locate the object
			if (valueProp && inputValueProp !== valueProp) {
				var source = unwrap(allBindings.jqAutoSource) || [];
				var modelValue = ko.utils.arrayFirst(source, function (item) {
					return unwrap(item[valueProp]) === modelValue;
				}) || {};
			}

			//update the element with the value that should be shown in the input
			$(element).val(modelValue && inputValueProp !== valueProp ? unwrap(modelValue[inputValueProp]) : modelValue.toString());
		}
	};

	//setup data validation
	ko.validation.rules.pattern.message = 'Invalid.';

	ko.validation.configure({
		registerExtenders: true,
		messagesOnModified: true,
		insertMessages: true,
		parseInputAttributes: true,
		messageTemplate: null,
		errorElementClass: 'input-validation-error',
		grouping: {
			deep: true,
			observable: false //Needed so added objects AFTER the initial setup get included
		},
	});

	ko.validation.rules['mcctDate'] = {
		validator: function (val, validate) {
			return moment(val, 'YYYY/MM/DD',true).isValid();
		},
		message: 'Invalid Date'
	};

	ko.validation.registerExtenders();

	function RegisterFamilyViewModel()   {        
		var host = location.protocol + "//" + window.location.hostname;
		if ( host == location.protocol + "//202.78.95.250") { host = location.protocol + "//202.78.95.249"; }
		var self = this;

		self.submitting = ko.observable(false);

		//Family Properties
		self.address = ko.observableArray([new AddressViewModel()]);
		self.selectedFamilyRelocation = ko.observableArray([]); //vm for relocation
		self.familyrelocation = ko.computed(function () { return self.selectedFamilyRelocation().join(","); }); //family attribute
		self.selectedFamilySituation = ko.observable('');
		self.estimatedfamilyincome = ko.observable('').extend({required: true}); //family attribute
		self.isIp = ko.observable(true);
		self.ipgroup = ko.observable('').extend({ //family attribute
			required: {
				onlyIf:
				function () { return self.isIp() === true; }
			}
		});
		self.religion = ko.observable('').extend({ required: true }); //family attribute
		self.members = ko.observableArray([new MemberViewModel()]);
		self.operationcenter = ko.observable('').extend({required: true});
		self.interviewer = ko.observable('').extend({ required: true });
		self.dateinterviewed = ko.observable('').extend({ mcctDate: true });
		self.assessedby = ko.observable('').extend({ required: true });
		self.dateassessed = ko.observable('').extend({ mcctDate: true });
		self.encoder = ko.observable('');
		self.tenantid = ko.observable('');
		
		self.psgc = ko.computed(function () {
			var locid = "";
			ko.utils.arrayForEach(self.address(), function (item) {
				if (item.addresstype() == "Current") {
					locid = item.locationid();
				}
			});

			return locid;
			owner: self;
		});

		//Selections
		//self.relocationChoices = ko.observableArray(["Natural Disaster", "Economic Option", "Nomadic Lifestyle", "Informal Settler/Eviction", "Peer Pressure", "Peace and Order"]);
		//self.familySituationChoices = ko.observableArray(["Homeless Street Family", "Itinerant IPs", "IPs in GIDA"]);
		self.relocationChoices = ko.observableArray([]);
		self.familySituationChoices = ko.observableArray([]);
		self.ipgroupchoices = ko.observableArray([]);
		self.operationcenterchoices = ko.observableArray([]);

		self.addAddress = function () { self.address.push(new AddressViewModel()) };
		self.addMember = function () { self.members.push(new MemberViewModel()) };
		self.removeAddress = function (line) { self.address.remove(line) };
		self.removeMember = function (line) { self.members.remove(line) };

		self.operationcenterstr = ko.computed(function () {
			var str = "";
			ko.utils.arrayForEach(self.operationcenterchoices(), function (item) {
				if (item.Id == self.operationcenter()) {
					str = item.Name;
				}
			});

			return str;
		}).extend({ required: true });

		self.errors = ko.validation.group(self, { deep: true });

		//self.modelIsValid = ko.computed(function () {
		//    if (self.errors().length == 0)
		//        return true;
		//    else {
		//        return false;
		//    }
		//});

		self.countforeduc = ko.computed(function () {
			var count = 0;
			ko.utils.arrayForEach(self.members(), function (item) {
				if (item.foreduc() == true) {
					count++;
				}
			});

			if (count > 3) {
				$('#cfefmi').removeClass('label-primary')
				$('#cfesum').removeClass('label-info')
				$('#cfefmi').addClass('label-danger')
				$('#cfesum').addClass('label-danger')
			} else {
				$('#cfefmi').removeClass('label-danger')
				$('#cfesum').removeClass('label-danger')
				$('#cfefmi').addClass('label-primary')
				$('#cfesum').addClass('label-info')
			}

			return count;
			owner: self;
		}).extend({min: 0, max: 3});

		self.countgrantee = ko.computed(function () {
			var count = 0;
			ko.utils.arrayForEach(self.members(), function (item) {
				if (item.isgrantee() == true) {
					count++;
				}
			});

			if (count != 1) {
				$('#cfgfmi').removeClass('label-primary')
				$('#cfgsum').removeClass('label-info')
				$('#cfgfmi').addClass('label-danger')
				$('#cfgsum').addClass('label-danger')
			} else {
				$('#cfgfmi').removeClass('label-danger')
				$('#cfgsum').removeClass('label-danger')
				$('#cfgfmi').addClass('label-primary')
				$('#cfgsum').addClass('label-info')
			}

			return count;
			owner: self;
		}).extend({ min: 1, max: 1 });

		self.countcurrentaddress = ko.computed(function () {
			var count = 0;
			ko.utils.arrayForEach(self.address(), function (item) {
				if (item.addresstype() == "Current") {
					count++;
				}
			});

			return count;
			owner: self;
		}).extend({ min: 1, max: 1 });
		
		self.errors.showAllMessages(true);

		self.save = function () {
			self.submitting(true);

			var mapping = {
				'ignore': ["ipgroupchoices", "relocationChoices", "familySituationChoices", "regions", "provinces", "cityMunicipalities", "barangays",
				"addresstypes", "maritalstatuschoices", "childconditionchoices", "disabilitychoices", "notattendingschoolchoices",
				"notattendinghealthchoices", "skillschoices", "educfacilities", "healthfacilities", "errors", "modelIsValid", "occupationchoices",
				"relationchoices", "edattainmentchoices", "operationcenterchoices", "currentgylevelchoices", "eduregions", "eduprovinces",
                "edumunicipalities", "hltregions", "hltprovinces", "hltmunicipalities"]
			}

			var data = ko.mapping.toJS(self, mapping);
	
			if (self.errors().length === 0) {
			    $('#opts').block({
			        message: '<img src="/Content/Images/loading.gif" /> Saving...',
			    });

			    $.ajax({
			        type: "POST",
			        url: "/Mcct/Registration/",
			        contentType: "application/json; charset=utf-8",
			        dataType: "json",
			        data: JSON.stringify(data),
			        processData: false,
			    }).done(function (response) {
			        if (response === "Save Successful!")
			        {
			            $.blockUI({ message: $('#success'), css: { top: '25%' } });
			            //window.location.href = '/Mcct/Index'
			        } else {
			            self.submitting(false);
			            $.blockUI({
			                message: '<h1>Error Occured</h1><p>' + response + '</p>',
			                timeout: 5000
			            });
			            //$('#opts').unblock();
			        }
			    })
                .fail(function (err_reponse) {
                    //alert("Something went wrong! Check the following:\n\n" + err_response);
                    console.log("ERROR:", error);
                    //$('#opts').unblock();
                    //self.submitting(false);
                    self.submitting(false);
                    $.blockUI({
                        message: '<h1>Error Occured</h1><p>' + err_response + '</p>',
                        timeout: 5000
                    });
                });
			} else {                
				//var err = "hey there!!!\n" +
				//		  "we are sorry but it looks like the registration data contains some errors, please check all errors (red text) and try submitting again.\n\n" +
				//		  "note:\n" +
				//		  "in case there are no errors, please check the following:\n\n" +
				//		  "   • only 1 grantee is selected \n" +
				//		  "   • maximum of only 3 members selected for education monitoring \n" +
				//		  "   • for 6 years or younger children and pregnant members who are not attending health center, you should specify a reason for not attending a health center\n" +
				//          "   • for children who are monitored for education but does not have a school, you should specify a reason for not attending a school\n";
			   
			    //alert("came to error!");

			    $.blockUI({ message: $('#errors'), css: { top: '25%' } });
			    //$.blockUI({ message: err });

				self.errors.showAllMessages(true);
				//console.log(self.errors());
				//console.log(self.errors().length);
				//console.log(self);
				
				//alert(err);

				//$('#opts').unblock();
				self.submitting(false);
			}
		};

		self.selectedFamilySituation.subscribe(function () {
			//$.getJSON(host + ":8000/McctWebService/Service/Rest/api/getOperationCenters/" + self.tenantid(), function (data) {
			//	self.operationcenterchoices(data);
		    //});
		    $.getJSON("/MCCT/getOperationCenters", { tenantid: self.tenantid() }, function (data) {
		        self.operationcenterchoices(data);
		    });
		});
		
		$.getJSON("/MCCT/getMetaSupportData", { metaname: "Relocation" }, function (data) {
			self.relocationChoices(data);
		});

		//$.getJSON(host + ":8000/McctWebService/Service/Rest/api/getMetaSupportData/EnrollmentType", function (data) {
		//	self.familySituationChoices(data);
		//});        
        $.getJSON("/MCCT/getMetaSupportData", { metaname: "IndigenousGroup" }, function (data) {
			self.ipgroupchoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "EnrollmentType" }, function (data) {
	    	self.familySituationChoices(data);
		});
	}

	function AddressViewModel() {
		//var host = location.protocol + "//" + window.location.hostname;
		//if ( host == location.protocol + "//202.78.95.250") { host = location.protocol + "//202.78.95.249"; }
		var self = this;
		
		self.region = ko.observable('').extend({required: true});
		self.province = ko.observable('').extend({ required: true });
		self.cityMun = ko.observable('').extend({ required: true });
		self.barangay = ko.observable('').extend({ required: true });        
		self.purok = ko.observable('');
		self.sitio = ko.observable('');
		self.lengtOfStay = ko.observable('').extend({ required: true }).extend({number: true});
		self.addresstype = ko.observable('').extend({ required: true });

		self.locationid = ko.computed(function () {
			var barangay = self.barangay();
			if (barangay) { return barangay.Id; }
		});

		//self.barangayname = self.barangay();
		self.barangayname = ko.computed(function () {
			var barangay = self.barangay();
			if (barangay) { return barangay.Barangay; }
		}).extend({ required: true });

		self.regions = ko.observableArray([]);
		self.provinces = ko.observableArray([]);
		self.cityMunicipalities = ko.observableArray([]);
		self.barangays = ko.observableArray([]);
		self.addresstypes = ko.observableArray(["Current", "Previous", "Guardian Address"]);

		$.getJSON("/MCCT/getRegions", function (data) {
			self.regions(data);
		});

		self.region.subscribe(function (newval) {
			self.province(undefined);
			self.cityMun(undefined);
			self.barangay(undefined);

			$.getJSON("/MCCT/getProvinces", { region: newval }, function (data) {
				self.provinces(data);
			});
		});

		self.province.subscribe(function (newval) {
			var selectedRegion = self.region();
			self.barangay(undefined);
			self.cityMun(undefined);
			$.getJSON("/MCCT/getCitiesMunicipalities", { region: selectedRegion, province: newval }, function (data) {
				self.cityMunicipalities(data);
			});
		});

		self.cityMun.subscribe(function (newval) {
			var selectedRegion = self.region();
			var selectedProvince = self.province();

			self.barangay(undefined);
			$.getJSON("/MCCT/getBarangays/", { region: selectedRegion, province: selectedProvince, citymunicipality: newval }, function (data) {
				self.barangays(data);
			});
		});

		//self.barangay.subscribe(function (newval) {
		//    if (newval !== undefined) {
		//        self.barangayname = newval.Barangay;
		//    } else {
		//        self.barangayname = newval;
		//    }
		//});
	}

	function MemberViewModel() {
		var host = location.protocol + "//" + window.location.hostname;
		if ( host == location.protocol + "//202.78.95.250") { host = location.protocol + "//202.78.95.249"; }

		var self = this;
		
		self.eduregion = ko.observable('');
		self.eduprovince = ko.observable('');
		self.edumunicipality = ko.observable('');
		self.hltregion = ko.observable('');
		self.hltprovince = ko.observable('');
		self.hltmunicipality = ko.observable('');

		self.firstname = ko.observable('').extend({ required: true });
		self.middlename = ko.observable('');
		self.lastname = ko.observable('');
		self.extname = ko.observable('');
		self.birthdate = ko.observable('').extend({ mcctDate: true }).extend({ required: true });
		self.sex = ko.observable('Female');

		self.relation = ko.observable().extend({ required: true })//.extend({ number: true }).extend({ min: 1, max: 17 });
		self.maritalstatus = ko.observable('').extend({ required: true }); //member info
		self.childcondition = ko.observableArray([]);   //view model
		self.disability = ko.observableArray([]);   //view model        

		//bolean values
		self.isgrantee = ko.observable(false);
		self.foreduc = ko.observable(false);
		self.inschool = ko.observable(false);
		self.eduregi = ko.observable(false);
		self.eduprov = ko.observable(false);
		self.edumuni = ko.observable(false);
		self.hltregi = ko.observable(false);
		self.hltprov = ko.observable(false);
		self.hltmuni = ko.observable(false);
		self.attendhc = ko.observable(false);
		self.ispregnant = ko.observable(false);
		self.soloparent = ko.observable(false); //member attribute

		self.forhealth = ko.computed(function () {
			var now = moment("2014/07/01","YYYY/MM/DD"); //change this with the registration Set reference date. todo: past via viewbag in hidden input like tenantid
			var past = moment(self.birthdate(), "YYYY/MM/DD");
			var years = moment(now).diff(past, 'years');
		   
			if (years < 6) 
				return true;           
			else
				return false;
		});


		self.childconditionstr = ko.computed(function () {
			return self.childcondition().join(",");
		});  //delimited member attribute

		self.childconditiontc = ko.computed(function () {
			var str = "";

			ko.utils.arrayForEach(self.childcondition(), function (data) {
				ko.utils.arrayForEach(self.childconditionchoices(), function (item) {
					if (item.Value == data) {
						if (str == "") {
							str = str + item.TemplateCode;
						} else {
							str = str + ", " + item.TemplateCode;
						}
					}
				});
			});
			return str;
			owner: self;
		});

		self.disabilitystr = ko.computed(function () {
			return self.disability().join(",");
		}); //delimited member attribute

		self.disabilitytc = ko.computed(function () {
			var str = "";

			ko.utils.arrayForEach(self.disability(), function (data) {
				ko.utils.arrayForEach(self.disabilitychoices(), function (item) {
					if (item.Value == data) {
						if (str == "") {
							str = str + item.TemplateCode;
						} else {
							str = str + ", " + item.TemplateCode;
						}
					}
				});
			});
			return str;
			owner: self;
		});

		self.highesteducattained = ko.observable('');

		self.educationfacility = ko.observable('').extend({
			required: {
				onlyIf:
				function () { return self.inschool() === true; }
			}
		});

		self.healthfacility = ko.observable('').extend({
			required: {
				onlyIf:
					function () { return self.attendhc() === true; }
			}
		});

		self.gylevel = ko.observable('').extend({
			required: {
				onlyIf: function () { return self.inschool() === true; }
			}
		});

		self.occupation = ko.observable('').extend({ required: true })//.extend({ number: true }).extend({ min: 1, max: 11 });

		self.reasonnotattendingschool = ko.observableArray([]); //viewmodel
		self.notattendinghc = ko.observableArray([]);   //viewmodel
		self.skills = ko.observableArray([]);   //viewmodel

		self.notattendingschoolstr = ko.computed(function () {
			return self.reasonnotattendingschool().join(",");
		}).extend({
			required: { onlyIf: function () { return (self.foreduc() === true && self.inschool() === false); } }
		}); //member attribute

		self.notattendingschooltc = ko.computed(function () {
			var str = "";

			ko.utils.arrayForEach(self.reasonnotattendingschool(), function (data) {
				ko.utils.arrayForEach(self.notattendingschoolchoices(), function (item) {
					if (item.Value == data) {
						if (str == "") {
							str = str + item.TemplateCode;
						} else {
							str = str + ", " + item.TemplateCode;
						}
					}
				});
			});

			return str;
		}).extend({
		    required: { onlyIf: function () { return (self.foreduc() === true && self.inschool() === false); } }
		});

		self.notattendinghcstr = ko.computed(function () {
			return self.notattendinghc().join(",");
		}).extend({
			required: {
				onlyIf: function () {
					return (self.attendhc() === false && (self.forhealth() === true || self.ispregnant() === true));
				}
			}
		}); //member attribute

		self.notattendinghctc = ko.computed(function () {
			var str = "";

			ko.utils.arrayForEach(self.notattendinghc(), function (data) {
				ko.utils.arrayForEach(self.notattendinghealthchoices(), function (item) {
					if (item.Value == data) {                        
						if (str == "") {
							str = str + item.TemplateCode;
						} else {
							str = str + ", " + item.TemplateCode;
						}
					}
				});
			});

			return str;
		}).extend({
		    required: {
		        onlyIf: function () {
		            return (self.attendhc() === false && (self.forhealth() === true || self.ispregnant() === true));
		        }
		    }
		});

		self.skillsstr = ko.computed(function () {
			return self.skills().join(",");
		}); //member attributes              

		self.skillstc = ko.computed(function () {
			var str = "";

			ko.utils.arrayForEach(self.skills(), function (data) {
				ko.utils.arrayForEach(self.skillschoices(), function (item) {
					if (item.Value == data) {
						if (str == "") {
							str = str + item.TemplateCode;
						} else {
							str = str + ", " + item.TemplateCode;
						}
					}
				});
			});

			return str;
		});

		//Choices
		//self.barangays = ko.observableArray([]);
		self.maritalstatuschoices = ko.observableArray([]);
		self.childconditionchoices = ko.observableArray([]);
		self.disabilitychoices = ko.observableArray([]);
		self.notattendingschoolchoices = ko.observableArray([]);
		self.notattendinghealthchoices = ko.observableArray([]);
		self.skillschoices = ko.observableArray([]);
		self.educfacilities = ko.observableArray([]);
		self.healthfacilities = ko.observableArray([]);
		self.relationchoices = ko.observableArray([]);
		self.edattainmentchoices = ko.observableArray([]);
		self.occupationchoices = ko.observableArray([]);
		self.currentgylevelchoices = ko.observableArray([]);
		self.eduregions = ko.observableArray([]);
		self.eduprovinces = ko.observableArray([]);
		self.edumunicipalities = ko.observableArray([]);
		self.hltregions = ko.observableArray([]);
		self.hltprovinces = ko.observableArray([]);
		self.hltmunicipalities = ko.observableArray([]);
		
		$.getJSON("/MCCT/getRegions", function (data) {
		    self.eduregions(data);
		    self.hltregions(data);
		});

		//$.getJSON("/MCCT/getProvinces", function (data) {
		//    self.eduprovinces(data);
		//    self.hltprovinces(data);
		//});

		self.isgrantee.subscribe(function (newval) {
			//if (newval)
			//	self.relation('15');
			//else
			//	self.relation('');
		});

		self.relationstr = ko.computed(function () {
			var relation = self.relation();
			var str = null;
			ko.utils.arrayForEach(self.relationchoices(), function (item) {
				if (item.TemplateCode == relation) {
					str = item.Value;
				}
			});

			return str;
			owner: self;
		});

		self.relationtcstr = ko.computed(function () {
			var relation = self.relation();
			var str = null;
			ko.utils.arrayForEach(self.relationchoices(), function (item) {
				if (item.TemplateCode == relation) {
					str = item.TemplateCode + " - " + item.Value;
				}
			});

			return str;
			owner: self;
		}).extend({ required: true });

		self.maritalstatustc = ko.computed(function () {
			var maritalstatus = self.maritalstatus();
			var str = null;

			ko.utils.arrayForEach(self.maritalstatuschoices(), function (item) {
				if (item.Id == maritalstatus) {
					str = item.TemplateCode;
				}
			});

			return str;
			owner: self;
		}).extend({ required: true });

		self.highesteducattainedstr = ko.computed(function () {
			var highesteducattained = self.highesteducattained();
			var str = null;
			ko.utils.arrayForEach(self.edattainmentchoices(), function (item) {
				if (item.TemplateCode == highesteducattained) {
					str = item.Value;
				}
			});

			return str;
			owner: self;
		});

		self.highesteducattainedtcstr = ko.computed(function () {
			var highesteducattained = self.highesteducattained();
			var str = null;
			ko.utils.arrayForEach(self.edattainmentchoices(), function (item) {
				if (item.TemplateCode == highesteducattained) {
					str = item.TemplateCode + " - " + item.Value;
				}
			});

			return str;
			owner: self;
		});

		self.gylevelstr = ko.computed(function () {
			var gylevel = self.gylevel();
			var str = null;
			ko.utils.arrayForEach(self.currentgylevelchoices(), function (item) {
				if (item.TemplateCode == gylevel) {
					str = item.Value;
				}
			});

			return str;
			owner: self;
		});
		
		self.gyleveltcstr = ko.computed(function () {
			var gylevel = self.gylevel();
			var str = null;
			ko.utils.arrayForEach(self.currentgylevelchoices(), function (item) {
				if (item.TemplateCode == gylevel) {
					str = item.TemplateCode + " - " + item.Value;
				}
			});

			return str;
			owner: self;
		});

		self.occupationstr = ko.computed(function () {
			var occupation = self.occupation();
			var str = null;
			ko.utils.arrayForEach(self.occupationchoices(), function (item) {
				if (item.TemplateCode == occupation) {
					str = item.Value;
				}
			});

			return str;
			owner: self;
		});

		self.occupationtcstr = ko.computed(function () {
			var occupation = self.occupation();
			var str = null;
			ko.utils.arrayForEach(self.occupationchoices(), function (item) {
				if (item.TemplateCode == occupation) {
					str = item.TemplateCode + " - " + item.Value;
				}
			});

			return str;
			owner: self;
		});

		self.inschool.subscribe(function (newval) {
		    var selectedregion = $("#region").val();
		    var selectedprovince = $("#province").val();
		    var selectedcitymun = $("#cityMun").val();

		    if (newval) {
		        if (selectedregion != "" && selectedprovince != "" && selectedcitymun != "") {
		            $.getJSON("/MCCT/getFacilities", { region: selectedregion, province: selectedprovince, municipality: selectedcitymun, type: "School" }, function (data) {
		                if (data.length == 0) {
		                    alert("No School Facilities found on " + selectedregion + ", " + selectedprovince + ", " + selectedcitymun + "!");
		                    self.educationfacility('');
		                    self.educfacilities('');
		                } else {
		                    self.educfacilities(data);
		                }
		            });
		        } else {
		            var selectthese = "Please select the following on the first address entry of the family:\n\n";

		            self.educationfacility('');
		            self.educfacilities('');

		            if (selectedregion === "") { selectthese = selectthese + "Region\n"; }
		            if (selectedprovince === "") { selectthese = selectthese + "District / Province\n"; }
		            if (selectedcitymun === "") { selectthese = selectthese + "City / Municipality\n\n"; }

		            alert(selectthese);
		            self.attendhc(false);
		        }
		    } else {
		        self.eduregion('');
		        self.educationfacility('');
		        self.educfacilities('');
			}
		});

		//self.inschool.subscribe(function (newval) {            
		//    if (newval) {
		//        self.eduregion($('#region').val());
		//    } else {
		//        self.eduregion('');
		//    }
		//});

		self.eduregion.subscribe(function (newval) {

		    self.eduprovince(undefined);
            self.eduregi(false)
		    
            if (newval !== undefined) {
                $.getJSON("/MCCT/getProvinces", { region: newval }, function (data) {
                    self.eduprovinces(data);
                });
                self.eduregi(true);
            }
		});		

		self.eduprovince.subscribe(function (newval) {
		    self.edumunicipality(undefined);
		    self.eduprov(false)

		    var selectedregion = self.eduregion;

		    if (newval !== undefined) {
		        $.getJSON("/MCCT/getCitiesMunicipalities", { region: selectedregion, province: newval }, function (data) {
		            self.edumunicipalities(data);
		        });
		        self.eduprov(true);
		    }
		});

		self.edumunicipality.subscribe(function (newval) {
		    self.educationfacility('');

		    var selectedregion = self.eduregion;
		    var selectedprov = self.eduprovince;

		    if (newval !== undefined) {
		        $.getJSON("/MCCT/getFacilities", { region: selectedregion, province: selectedprov, municipality: newval, type: "School" }, function (data) {
		            if (data.length == 0) {
		                self.educfacilities('');
		                alert("No School Facilities found on " + selectedregion() + ", " + selectedprov() + ", " + newval + "!");
		            } else {
		                self.educfacilities(data);
		            }
		        });
		    } else {
		        var addressregion = $("#region").val();
		        var addressprovince = $("#province").val();
		        var addresscitymun = $("#cityMun").val();

		        if (self.inschool()) {
		            if (addressregion !== undefined && addressprovince !== undefined && addresscitymun !== undefined) {
		                $.getJSON("/MCCT/getFacilities", { region: addressregion, province: addressprovince, municipality: addresscitymun, type: "School" }, function (data) {
		                    if (data.length == 0) {
		                        self.educfacilities('');
		                        alert("No School Facilities found on " + addressregion + ", " + addressprovince + ", " + addresscitymun + "!");
		                    } else {
		                        self.educfacilities(data);
		                    }
		                });
		            } else {
		                var selectthese = "Please select the following on the first address entry of the family:\n\n";

		                self.educationfacility('');
		                self.educfacilities('');

		                if (addressregion === "") { selectthese = selectthese + "Region\n"; }
		                if (addressprovince === "") { selectthese = selectthese + "District / Province\n"; }
		                if (addresscitymun === "") { selectthese = selectthese + "City / Municipality\n\n"; }

		                alert(selectthese);
		                self.inschool(false);
		            }
		        }
		    }
		});

		self.educationfacilitystr = ko.computed(function () {
			var educationfacility = self.educationfacility();
			var str = null;
			ko.utils.arrayForEach(self.educfacilities(), function (item) {
				if (item.Id == educationfacility) {
					str = item.Name;
				}
			});

			return str;
			owner: self;
		});

		self.attendhc.subscribe(function (newval) {
		    var selectedregion = $("#region").val();
		    var selectedprovince = $("#province").val();
		    var selectedcitymun = $("#cityMun").val();

		    if (newval) {
		        if (selectedregion != "" && selectedprovince != "" && selectedcitymun != "") {
		            $.getJSON("/MCCT/getFacilities", { region: selectedregion, province: selectedprovince, municipality: selectedcitymun, type: "Health Center" }, function (data) {
		                if (data.length == 0) {
		                    alert("No Health Facilities found on " + selectedregion + ", " + selectedprovince + ", " + selectedcitymun + "!");
		                    self.healthfacility('');
		                    self.healthfacilities('');
		                } else {
		                    self.healthfacilities(data);
		                }
		            });
		        } else {
		            var selectthese = "Please select the following on the first address entry of the family:\n\n";

		            self.healthfacility('');
		            self.healthfacilities('');

		            if (selectedregion === "") { selectthese = selectthese + "Region\n"; }
		            if (selectedprovince === "") { selectthese = selectthese + "District / Province\n"; }
		            if (selectedcitymun === "") { selectthese = selectthese + "City / Municipality\n\n"; }

		            alert(selectthese);
		            self.inschool(false);
		        }
		    } else {
		        self.hltregion('');
		        self.healthfacility('');
		        self.healthfacilities('');
		    }
	    });

		self.hltregion.subscribe(function (newval) {
		    self.hltprovince(undefined);
		    self.hltregi(false)

		    if (newval !== undefined) {
		        $.getJSON("/MCCT/getProvinces", { region: newval }, function (data) {
		            self.hltprovinces(data);
		        });
		        self.hltregi(true);
		    }
		});

		self.hltprovince.subscribe(function (newval) {
		    self.hltmunicipality(undefined);
		    self.hltprov(false);

		    var selectedregion = self.hltregion;

		    if (selectedregion !== undefined && newval !== undefined) {

		        $.getJSON("/MCCT/getCitiesMunicipalities", { region: selectedregion, province: newval }, function (data) {
		            self.hltmunicipalities(data);
		        });
		        self.hltprov(true);
		    }
		});

		self.hltmunicipality.subscribe(function (newval) {
		    self.healthfacility('');

		    var selectedregion = self.hltregion;
		    var selectedprov = self.hltprovince;

		    if (newval !== undefined) {
		        $.getJSON("/MCCT/getFacilities", { region: selectedregion, province: selectedprov, municipality: newval, type: "Health Center" }, function (data) {
		            if (data.length == 0) {
		                self.healthfacilities('');
		                alert("No Health Facilities found on " + selectedregion() + ", " + selectedprov() + ", " + newval + "!");
		            } else {
		                self.healthfacilities(data);
		            }
		        });
		    } else {
		        var addressregion = $("#region").val();
		        var addressprovince = $("#province").val();
		        var addresscitymun = $("#cityMun").val();

		        if (self.attendhc()) {
		            if (addressregion !== undefined && addressprovince !== undefined && addresscitymun !== undefined) {
		                $.getJSON("/MCCT/getFacilities", { region: addressregion, province: addressprovince, municipality: addresscitymun, type: "HealthCenter" }, function (data) {
		                    if (data.length == 0) {
		                        self.healthfacilities('');
		                        alert("No Health Facilities found on " + addressregion + ", " + addressprovince + ", " + addresscitymun + "!");
		                    } else {
		                        self.healthfacilities(data);
		                    }
		                });
		            } else {
		                var selectthese = "Please select the following on the first address entry of the family:\n\n";

		                self.healthfacility('');
		                self.healthfacilities('');

		                if (addressregion === "") { selectthese = selectthese + "Region\n"; }
		                if (addressprovince === "") { selectthese = selectthese + "District / Province\n"; }
		                if (addresscitymun === "") { selectthese = selectthese + "City / Municipality\n\n"; }

		                alert(selectthese);
		                self.attendhc(false);
		            }
		        }
		    }
		});

		self.healthfacilitystr = ko.computed(function () {
			var healthfacility = self.healthfacility();
			var str = null;
			ko.utils.arrayForEach(self.healthfacilities(), function (item) {
				if (item.Id == healthfacility) {
					str = item.Name;
				}
			});

			return str;
			owner: self;
		});

		//self.birthdate.subscribe(function (newval) { alert(newval); });
		$.getJSON("/MCCT/getMetaSupportData", { metaname: "Occupation" }, function (data) {
			self.occupationchoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "EducationalAttainment" }, function (data) {
			self.edattainmentchoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "CurrentGYLevel" }, function (data) {
			self.currentgylevelchoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "Relationship" }, function (data) {
			self.relationchoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "MaritalStat" }, function (data) {
			self.maritalstatuschoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "ChildCondition" }, function (data) {
			self.childconditionchoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "Disability" }, function (data) {
			self.disabilitychoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "NoSchool" }, function (data) {
			self.notattendingschoolchoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "NoHealth" }, function (data) {
			self.notattendinghealthchoices(data);
		});

		$.getJSON("/MCCT/getMetaSupportData", { metaname: "Skills" }, function (data) {
			self.skillschoices(data);
		});

	}

	$('#reCode').click(function () {
	    window.location.href = '/Mcct/RegisterMcctFamily'
	    //$('#opts').unblock();
	    //return false;
	});

	$('#goHome').click(function () {
	    window.location.href = '/Mcct/Index'
	    //$('#opts').unblock();
	    //return false;
	});

	var registerVM = new RegisterFamilyViewModel();

	ko.applyBindings(registerVM);
});