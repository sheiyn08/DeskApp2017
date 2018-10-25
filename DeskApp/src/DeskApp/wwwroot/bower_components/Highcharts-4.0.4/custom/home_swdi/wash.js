



function get_toilet() {
    var processed_json = new Array();


    var FiltRegion = $('#FiltRegion').val();

    var FiltProvince = $('#FiltProvince').val();

    var FiltMunicipality = $('#FiltMunicipality').val();

    var FiltBarangay = $('#FiltBarangay').val();

    var FiltStatus = $('#FiltStatus').val();

    var FiltSet = $('#FiltSet').val();

    var FiltIndicator = $('#FiltIndicator').val();

    var FiltValue = $('#FiltValue').val();
    var FiltLevel = $('#FiltLevel').val();

    var parameters = "FiltRegion=" + FiltRegion +
    "&FiltProvince=" + FiltProvince +
    "&FiltMunicipality=" + FiltMunicipality +
    "&FiltBarangay=" + FiltBarangay +
    "&FiltStatus=" + FiltStatus +
     "&FiltSet=" + FiltSet +
    "&FiltIndicator=" + FiltIndicator +
    "&FiltValue=" + FiltValue +
    "&FiltLevel=" + FiltLevel +
    "&FiltDisplayIndicator=" + "IIA3B";



    var url = "/Report/get_toilet?" + parameters;


    $.getJSON(url, function (datas) {
        // Populate series
        for (i = 0; i < datas.length; i++) {
            processed_json.push([datas[i].Name, datas[i].Count]);
        }

        // draw chart
        $('#get_toilet').highcharts({
            chart: {
                type: "pie"
            },
            title: {
                text: "Toilet Type"
            },
            xAxis: {
                type: 'category',
                allowDecimals: false,
                title: {
                    text: ""
                }
            },
            yAxis: {
                title: {
                    text: "No. of Households"
                }
            },
            series: [{
                name: 'Households',
                data: processed_json
            }]
        });
    });
}

function get_toilet_breakdown() {
    var processed_json = new Array();


    var FiltRegion = $('#FiltRegion').val();

    var FiltProvince = $('#FiltProvince').val();

    var FiltMunicipality = $('#FiltMunicipality').val();

    var FiltBarangay = $('#FiltBarangay').val();

    var FiltStatus = $('#FiltStatus').val();

    var FiltSet = $('#FiltSet').val();

    var FiltIndicator = $('#FiltIndicator').val();

    var FiltValue = $('#FiltValue').val();
    var FiltLevel = $('#FiltLevel').val();

    var parameters = "FiltRegion=" + FiltRegion +
    "&FiltProvince=" + FiltProvince +
    "&FiltMunicipality=" + FiltMunicipality +
    "&FiltBarangay=" + FiltBarangay +
    "&FiltStatus=" + FiltStatus +
     "&FiltSet=" + FiltSet +
    "&FiltIndicator=" + FiltIndicator +
    "&FiltValue=" + FiltValue +
    "&FiltLevel=" + FiltLevel +
    "&FiltDisplayIndicator=" + "IIA3B";



    var url = "/Report/get_toilet?" + parameters;


    $.getJSON(url, function (datas) {
        // Populate series
        for (i = 0; i < datas.length; i++) {
            processed_json.push([datas[i].Name, datas[i].Count]);
        }

        // draw chart
        $('#get_toilet_breakdown').highcharts({
            chart: {
                type: "column"
            },
            title: {
                text: "Toilet Type"
            },
            xAxis: {
                type: 'category',
                allowDecimals: false,
                title: {
                    text: ""
                }
            },
            yAxis: {
                title: {
                    text: "No. of Households"
                }
            },
            series: [{
                name: 'Households',
                data: processed_json
            }]
        });
    });
}
function filter() {

    get_toilet();
    get_toilet_breakdown();
}
filter();




$('#clear').click(function (e) {
    e.preventDefault();

    $('#FiltRegion').val('');

    $('#FiltProvince').val('');

    $('#FiltMunicipality').val('');

    $('#FiltBarangay').val('');


    $('#FiltLevel').val('');

    $('#FiltSet').val('');

    $('#FiltStatus').val('');


    $('#FiltIndicator').val('');

    $('#FiltValue').val('');
    $('#filter').click();
});
// });
