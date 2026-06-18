<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HuJiaoPop.aspx.vb" Inherits="HuJiaoPop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="x-ua-compatible" content="IE=11;IE=10" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./Qrcode.js"></script>
    <script language="javascript" type="text/javascript" src="./t_check_msForTpAndBg.js?version=3335"></script>
    <script language="javascript" type="text/javascript" src="./HuJiaoPop.js"></script>
    <style>
        table {
            border: 1px solid #ccc;
        }

            table td, th {
                border: 1px solid #ccc;
            }

        .qr_th {
            padding: 10px;
        }

        .qr_td {
            vertical-align: top;
            text-align: left;
            padding: 20px 20px 20px 20px;
            width: 400px;

            line-height: 30px;
            font-weight: bold;
        }

            .qr_td div {
                padding-top: 20px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <th id="" class="qr_th">站点
                    </th>

                    <th id="" class="qr_th">台车
                    </th>
                </tr>
                <tr>
                    <td id="tdStation" class="qr_td"></td>

                    <td id="tdTaiChe" class="qr_td"></td>
                </tr>
            </table>

        </div>
        <asp:HiddenField ID="hidStationNo" runat="server" />
    </form>
</body>
</html>
