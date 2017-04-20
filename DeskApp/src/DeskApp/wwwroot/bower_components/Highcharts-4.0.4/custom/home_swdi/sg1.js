



function get_sg_one() {
    var processed_json = new Array();


    var FiltPages = $("#FiltPages").val();

    var FiltId = $('#FiltId').val();

    var FiltCases = $('#FiltCases').val();


    var FiltName = $('#FiltName').val();

    var FiltAgency = $('#FiltAgency').val();

    var FiltProgram = $('#FiltProgram').val();

    var FiltWave = $('#FiltWave').val();

    var FiltLevel = $('#FiltLevel').val();

    var FiltSet = $('#FiltSet').val();

    var FiltStatus = $('#FiltStatus').val();

    var FiltRegion = $('#FiltRegion').val();

    var FiltProvince = $('#FiltProvince').val();

    var FiltMunicipality = $('#FiltMunicipality').val();

    var FiltBarangay = $('#FiltBarangay').val();

    var FiltIndicator = $('#FiltIndicator').val();

    var FiltValue = $('#FiltValue').val();



    var parameters = "FiltAgency=" + FiltAgency +
             "&FiltProgram=" + FiltProgram +
                "&FiltCases=" + FiltCases +
                "&FiltId=" + FiltId +
           "&FiltName=" + FiltName +
           "&FiltWave=" + FiltWave +
           "&FiltLevel=" + FiltLevel +
           "&FiltIndicator=" + FiltIndicator +
           "&FiltValue=" + FiltValue +
           "&FiltSet=" + FiltSet +
           "&FiltStatus=" + FiltStatus +
           "&FiltRegion=" + FiltRegion +
           "&FiltProvince=" + FiltProvince +
           "&FiltMunicipality=" + FiltMunicipality +
           "&FiltBarangay=" + FiltBarangay +
           "&FiltPages=" + FiltPages;

   

    var url = "/Report/get_sg_one?" + parameters;


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
                text: "SG 1"
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

function get_sg_count() {
    var processed_json = new Array();



    var FiltPages = $("#FiltPages").val();

    var FiltId = $('#FiltId').val();

    var FiltCases = $('#FiltCases').val();


    var FiltName = $('#FiltName').val();

    var FiltAgency = $('#FiltAgency').val();

    var FiltProgram = $('#FiltProgram').val();

    var FiltWave = $('#FiltWave').val();

    var FiltLevel = $('#FiltLevel').val();

    var FiltSet = $('#FiltSet').val();

    var FiltStatus = $('#FiltStatus').val();

    var FiltRegion = $('#FiltRegion').val();

    var FiltProvince = $('#FiltProvince').val();

    var FiltMunicipality = $('#FiltMunicipality').val();

    var FiltBarangay = $('#FiltBarangay').val();

    var FiltIndicator = $('#FiltIndicator').val();

    var FiltValue = $('#FiltValue').val();



    var parameters = "FiltAgency=" + FiltAgency +
             "&FiltProgram=" + FiltProgram +
                "&FiltCases=" + FiltCases +
                "&FiltId=" + FiltId +
           "&FiltName=" + FiltName +
           "&FiltWave=" + FiltWave +
           "&FiltLevel=" + FiltLevel +
           "&FiltIndicator=" + FiltIndicator +
           "&FiltValue=" + FiltValue +
           "&FiltSet=" + FiltSet +
           "&FiltStatus=" + FiltStatus +
           "&FiltRegion=" + FiltRegion +
           "&FiltProvince=" + FiltProvince +
           "&FiltMunicipality=" + FiltMunicipality +
           "&FiltBarangay=" + FiltBarangay +
           "&FiltPages=" + FiltPages;



    var url = "/Report/get_sg_count?" + parameters;


    $.getJSON(url, function (datas) {
        // Populate series
        for (i = 0; i < datas.length; i++) {
            processed_json.push([datas[i].Name, datas[i].Count]);
        }

        // draw chart
        $('#get_sg_count').highcharts({
            chart: {
                type: "column"
            },
            title: {
                text: "SG Count"
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

    get_sg_one();
    get_sg_count();
    
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
