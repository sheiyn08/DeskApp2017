
function get_lowb() {
    var processed_json = new Array();

    var FiltRegion = $('#FiltRegion').val();

    var FiltProvince = $('#FiltProvince').val();

    var FiltMunicipality = $('#FiltMunicipality').val();

    var FiltBarangay = $('#FiltBarangay').val();



    var urls = "/Report/get_lowb?FiltRegion=" + FiltRegion +
"&FiltProvince=" + FiltProvince +
"&FiltMunicipality=" + FiltMunicipality +
"&FiltBarangay=" + FiltBarangay;


    $.getJSON(urls, function (datas) {
        // Populate series
        for (i = 0; i < datas.length; i++) {
            processed_json.push([datas[i].Name, datas[i].Count]);
        }

        // draw chart
        $('#get_lowb').highcharts({
            chart: {
                type: "pie"
            },
            title: {
                text: "Level of Well Being"
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

filter();




$('#clear').click(function (e) {
    e.preventDefault();

    $('#FiltRegion').val('');

    $('#FiltProvince').val('');

    $('#FiltMunicipality').val('');

    $('#FiltBarangay').val('');


    $('#filter').click();
});
// });
