
am4core.ready(function () {

    // Themes begin
    am4core.useTheme(am4themes_animated);
    // Themes end

    diesel()
    petrol()


}); // end am4core.ready()


function diesel() {

    // Create chart instance
    var chart = am4core.create("chartdiv", am4charts.XYChart3D);

    chart.titles.create().text = "Diesel";

    // Add data
    chart.data = [{
        "category": "2018 Q1",
        "value1": 30,
        "value2": 70
    }, {
        "category": "2018 Q2",
        "value1": 15,
        "value2": 85
    }, {
        "category": "2018 Q3",
        "value1": 40,
        "value2": 60
    }, {
        "category": "2018 Q4",
        "value1": 55,
        "value2": 45
    }, {
        "category": "2018 Q5",
        "value1": 55,
        "value2": 45
    }
    , {
        "category": "2018 Q6",
        "value1": 55,
        "value2": 45
    }
    , {
        "category": "2018 Q7",
        "value1": 55,
        "value2": 45
    }];

    // Create axes
    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis.dataFields.category = "category";
    categoryAxis.renderer.grid.template.location = 0;
    categoryAxis.renderer.grid.template.strokeOpacity = 0;

    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
    valueAxis.renderer.grid.template.strokeOpacity = 0;
    valueAxis.min = -10;
    valueAxis.max = 110;
    valueAxis.strictMinMax = true;
    valueAxis.renderer.baseGrid.disabled = true;
    valueAxis.renderer.labels.template.adapter.add("text", function (text) {
        if ((text > 100) || (text < 0)) {
            return "";
        }
        else {
            return text + "%";
        }
    })

    // Create series
    var series1 = chart.series.push(new am4charts.ConeSeries());
    series1.dataFields.valueY = "value1";
    series1.dataFields.categoryX = "category";
    series1.columns.template.width = am4core.percent(80);
    series1.columns.template.fillOpacity = 0.9;
    series1.columns.template.strokeOpacity = 1;
    series1.columns.template.strokeWidth = 2;

    var series2 = chart.series.push(new am4charts.ConeSeries());
    series2.dataFields.valueY = "value2";
    series2.dataFields.categoryX = "category";
    series2.stacked = true;
    series2.columns.template.width = am4core.percent(80);
    series2.columns.template.fill = am4core.color("#000");
    series2.columns.template.fillOpacity = 0.1;
    series2.columns.template.stroke = am4core.color("#000");
    series2.columns.template.strokeOpacity = 0.2;
    series2.columns.template.strokeWidth = 2;
}

function petrol() {

    // Create chart instance
    var chart = am4core.create("petroldiv", am4charts.XYChart3D);

    chart.titles.create().text = "Petrol";

    // Add data
    chart.data = [{
        "category": "2018 Q1",
        "value1": 30,
        "value2": 70
    }, {
        "category": "2018 Q2",
        "value1": 15,
        "value2": 85
    }, {
        "category": "2018 Q3",
        "value1": 40,
        "value2": 60
    }, {
        "category": "2018 Q4",
        "value1": 55,
        "value2": 45
    }, {
        "category": "2018 Q5",
        "value1": 55,
        "value2": 45
    }
    , {
        "category": "2018 Q6",
        "value1": 55,
        "value2": 45
    }
    , {
        "category": "2018 Q7",
        "value1": 55,
        "value2": 45
    }];

    // Create axes
    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis.dataFields.category = "category";
    categoryAxis.renderer.grid.template.location = 0;
    categoryAxis.renderer.grid.template.strokeOpacity = 0;

    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
    valueAxis.renderer.grid.template.strokeOpacity = 0;
    valueAxis.min = -10;
    valueAxis.max = 110;
    valueAxis.strictMinMax = true;
    valueAxis.renderer.baseGrid.disabled = true;
    valueAxis.renderer.labels.template.adapter.add("text", function (text) {
        if ((text > 100) || (text < 0)) {
            return "";
        }
        else {
            return text + "%";
        }
    })

    // Create series
    var series1 = chart.series.push(new am4charts.ConeSeries());
    series1.dataFields.valueY = "value1";
    series1.dataFields.categoryX = "category";
    series1.columns.template.width = am4core.percent(80);
    series1.columns.template.fillOpacity = 0.9;
    series1.columns.template.strokeOpacity = 1;
    series1.columns.template.strokeWidth = 2;

    var series2 = chart.series.push(new am4charts.ConeSeries());
    series2.dataFields.valueY = "value2";
    series2.dataFields.categoryX = "category";
    series2.stacked = true;
    series2.columns.template.width = am4core.percent(80);
    series2.columns.template.fill = am4core.color("#000");
    series2.columns.template.fillOpacity = 0.1;
    series2.columns.template.stroke = am4core.color("#000");
    series2.columns.template.strokeOpacity = 0.2;
    series2.columns.template.strokeWidth = 2;
}

function highOctrane() {
    
}