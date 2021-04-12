//statisticsChart
let poolId = $("#poolIdInput").val();
console.log(poolId);
var data;
var nitrogenArray;
var temperatureArray;
var waterLevelArray;
$.ajax({
    type: "GET",
    url: `Pool/GetDataForStatistics?poolId=${poolId}`,
    contentType: "application/json",
    dataType: "json",
    success: function (result) {

        data = result;
        console.log(data);
        createChart(data);
    },
    error: function (xhr, status, error) {
        var errorMessage = xhr.status + ': ' + xhr.statusText
        alert('Error - ' + errorMessage);
    }
});
function createChart(data) {
    var datesAdded = [];
    data.forEach(x => datesAdded.push(x.dataAdded));
    var nitrogenArray = [];
    data.forEach(x => nitrogenArray.push(x.nitrogen));
    var temperatureArray = [];
    data.forEach(x => temperatureArray.push(x.temperature));
    var waterLevelArray = [];
    data.forEach(x => waterLevelArray.push(x.waterLevel));

    Highcharts.chart('statisticsChart', {

        title: {
            text: 'Статистика стану басейну'
        },

        yAxis: {
            title: {
                text: 'Показник'
            }
        },

        xAxis: {
            accessibility: {
                rangeDescription: 'Дата'
            }
        },

        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle'
        },



        series: [{
            name: 'Азот',
            data: nitrogenArray
        }, {
            name: 'Температура',
            data: temperatureArray
        }, {
            name: 'Рівень води',
            data: waterLevelArray
        }],

        responsive: {
            rules: [{
                condition: {
                    maxWidth: 500
                },
                chartOptions: {
                    legend: {
                        layout: 'horizontal',
                        align: 'center',
                        verticalAlign: 'bottom'
                    }
                }
            }]
        }

    });
}