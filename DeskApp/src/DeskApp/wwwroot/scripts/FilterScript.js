

var dataarray;

 

var ViewModel = function (data) {
    var self = this;
    dataarray =  @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model));



    ko.mapping.fromJS(dataarray, {}, self);
 
    self.filter = function () {
        var data = ko.mapping.toJS(self);            
    }

    ko.bindingHandlers.selectPicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            if ($(element).is('select')) {
                if (ko.isObservable(valueAccessor())) {
                    if ($(element).prop('multiple') && $.isArray(ko.utils.unwrapObservable(valueAccessor()))) {
                        // in the case of a multiple select where the valueAccessor() is an observableArray, call the default Knockout selectedOptions binding
                        ko.bindingHandlers.selectedOptions.init(element, valueAccessor, allBindingsAccessor);
                    } else {
                        // regular select and observable so call the default value binding
                        ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);
                    }
                }
                $(element).addClass('selectpicker').selectpicker();
            }
        },
        update: function (element, valueAccessor, allBindingsAccessor) {
            if ($(element).is('select')) {
                var selectPickerOptions = allBindingsAccessor().selectPickerOptions;
                if (typeof selectPickerOptions !== 'undefined' && selectPickerOptions !== null) {
                    var options = selectPickerOptions.optionsArray,
                        optionsText = selectPickerOptions.optionsText,
                        optionsValue = selectPickerOptions.optionsValue,
                        optionsCaption = selectPickerOptions.optionsCaption,
                        isDisabled = selectPickerOptions.disabledCondition || false,
                        resetOnDisabled = selectPickerOptions.resetOnDisabled || false;
                    if (ko.utils.unwrapObservable(options).length > 0) {
                        // call the default Knockout options binding
                        ko.bindingHandlers.options.update(element, options, allBindingsAccessor);
                    }
                    if (isDisabled && resetOnDisabled) {
                        // the dropdown is disabled and we need to reset it to its first option
                        $(element).selectpicker('val', $(element).children('option:first').val());
                    }
                    $(element).prop('disabled', isDisabled);
                }
                if (ko.isObservable(valueAccessor())) {
                    if ($(element).prop('multiple') && $.isArray(ko.utils.unwrapObservable(valueAccessor()))) {
                        // in the case of a multiple select where the valueAccessor() is an observableArray, call the default Knockout selectedOptions binding
                        ko.bindingHandlers.selectedOptions.update(element, valueAccessor);
                    } else {
                        // call the default Knockout value binding
                        ko.bindingHandlers.value.update(element, valueAccessor);
                    }
                }

                $(element).selectpicker('refresh');
            }
        }
    };

    //arrays
    self.ItemsPerPage = ko.observableArray([10,20,30,40,50,100]);
    self.lib_regions = ko.observableArray([]);
    self.lib_provinces = ko.observableArray([]);
    self.lib_cities = ko.observableArray([]);
    self.lib_brgy = ko.observableArray([]);
    self.lib_fund_sources = ko.observableArray([]);
    self.lib_cycles = ko.observableArray([]);
    self.lib_barangay_assembly_purposes = ko.observableArray([]);
    self.lib_enrollments = ko.observableArray([]);
    self.selected_regions = ko.observableArray([]);
    self.selected_fund_sources = ko.observableArray([]);



        

    self.lib_regions(dataarray.lib_regions);
    self.lib_fund_sources(dataarray.lib_fund_sources);
    self.lib_barangay_assembly_purposes(dataarray.lib_barangay_assembly_purposes);
    self.lib_enrollments(dataarray.lib_enrollments);


    //observables
    self.region_code = ko.observable('');
    self.prov_code = ko.observable('');
    self.city_code = ko.observable('');
    self.brgy_code = ko.observable('');
    self.cycle_id = ko.observable('');
    self.enrollment_id = ko.observable('');
    self.fund_source_id = ko.observable('');
    self.barangay_assembly_purpose_id = ko.observable('');
 

    //subscribe
    self.fund_source_id.subscribe(function (newval) {

         
        if (self.fund_source_id() == '') {
            self.lib_cycles(undefined);
        }
        else {
                 
               
            $.post("/Library/GetCycle", { id: self.fund_source_id() }, function (states) {
                self.lib_cycles(states);
            });

            
        }
    })

    self.region_code.subscribe(function (newval) {
            
             
        if (self.region_code() == '') {
            self.lib_provinces(undefined);
            self.lib_cities(undefined);
            self.lib_brgy(undefined);
        }
        else {
 

            $.post("/Library/GetProvinces", { id: self.region_code() }, function (states) {
                self.lib_provinces(states);
            });

                
        }
    })

    self.prov_code.subscribe(function (newval) {
        $.post("/Library/GetCity", { id: self.prov_code() }, function (states) {
            self.lib_cities(states);
        })

    })

    self.city_code.subscribe(function (newval) {
        $.post("/Library/GetBarangay", { id: self.city_code() }, function (states) {
            self.lib_brgy(states);
        })
    })
 

     

    self.filter =  function () {


          
        $.blockUI({ message: '<h1><img src="@Url.Content("~/Content/Images/loading.gif")" /></h1> Processing...' });


        var mapping = {
            'ignore': ["lib_regions","lib_provinces", "lib_cities", "lib_brgy",
                "lib_fund_sources","lib_cycles", "lib_barangay_assembly_purposes","lib_enrollments"
            ]
        }
        var data = JSON.stringify(ko.mapping.toJS(self,mapping));
          
        
        var url = "/View/_ba?item=" + data;

        $.ajax({
            type: "GET",
            url: url,
            //  data: someArguments,
            success: function (viewHTML) {
                $("#result").html(viewHTML);
                setTimeout($.unblockUI, 100);

            },
            error: function (errorData) { onError(errorData); }
        });
    }
}

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
        
    $("#filter").trigger('click');
});

