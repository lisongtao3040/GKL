<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="apexcharts_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script language="javascript" type="text/javascript" src="../js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../JidouTemp.js"></script>
    <style>
    body {
      background: #000524;
    }

    #wrapper {
      padding-top: 4px;
      background: #000524;
      border: 1px solid #000;
      box-shadow: 0 22px 35px -16px rgba(0, 0, 0, 0.71);
      max-width: 1050px;
      margin: 35px auto;
    }

    #chart-bar {
      position: relative;
      margin-top: -38px;
    }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <script src="js/apexcharts.js"></script>
        <div style="color:#fff; ">
            <table>
                <tr>
                    <td style="vertical-align:top;">
                        生产线/终了日</td>
                    <td  style="vertical-align:top;">
                        <asp:TextBox ID="tbxLineId_key" class="jq_line_id_key" runat="server" style="width:160px;background-color: #FFAA00;" list="line_id_list"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Full" Font-Names="Times New Roman" Font-Size="14pt" ForeColor="Black" Height="220px" NextPrevFormat="ShortMonth" Width="400px">
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                            <DayStyle Width="14%" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                            <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                            <TodayDayStyle BackColor="White" />
                        </asp:Calendar>
                    </td>
                </tr>
                </table>         
        </div>
        <hr />
        <div id="wrapper">
          <div id="chart-area">

          </div>
          <div id="chart-bar">

          </div>
        </div>



<script>


    <%
    Response.Write(GetGraData())
    %>

    var options1 = {
        chart: {
            id: "chart2",
            type: "area",
            height: 600,
            foreColor: "#fff",//
            toolbar: {
                autoSelected: "pan",
                show: true
            }
        },
        colors: ['#fff', '#00BAEC'],
        stroke: {
            width: 2
        },
        grid: {
            borderColor: ['red', 'blue'],
            yaxis: {
                lines: {
                    show: true
                }
            }
        },
        dataLabels: {
            enabled: true
        },
        fill: {
            gradient: {
                enabled: true,
                opacityFrom: 0.55,
                opacityTo: 0
            }
        },
        markers: {
            size: 5,
            colors: ["#000524"],
            strokeColor: "#00BAEC",
            strokeWidth: 3
        },
        series: [
          {
              name: "实施检查",
              data: data
          },
          {
              name: "检查完了",
              data: data2
          }
        ],
        tooltip: {
            theme: "dark"
        },
        xaxis: {
            type: "datetime"
        },
        yaxis: {
            min: 0,
            tickAmount: 1
        }
    };

    var chart1 = new ApexCharts(document.querySelector("#chart-area"), options1);

    chart1.render();


    function generateDayWiseTimeSeries(baseval, count, yrange) {
        var i = 0;
        var series = [];
        while (i < count) {
            var x = baseval;
            var y =
                Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;

            series.push([x, y]);
            baseval += 86400000;
            i++;
        }
        return series;
    }
</script>
    </form>
</body>
</html>
