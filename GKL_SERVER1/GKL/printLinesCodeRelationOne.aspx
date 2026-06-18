<%@ Page Language="VB" AutoEventWireup="false" CodeFile="printLinesCodeRelationOne.aspx.vb" Inherits="printLinesCodeRelationOne" ResponseEncoding="utf-8" %>
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>标签打印</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Microsoft YaHei', Arial, sans-serif;
            background: white;
            padding: 0;
            margin: 0;
        }

        .label-wrapper {
            display: block;
            margin: 0 auto;
            text-align: center;
            width: 65mm;
        }

        .qr-row {
            padding: 0;
            border: 1px solid #ccc;
            background-color: #ffd800;
            width: 62mm;
            height: 48mm;
            overflow: hidden;
            position: relative;
            box-sizing: border-box;
        }

        .qr-row.printed {
            border-color: #52c41a;
            background-color: #d9f7be;
        }

        .label-content {
            padding: 1px;
            height: 100%;
            box-sizing: border-box;
        }

        .label-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }

        .label-table td {
            padding: 1px;
            vertical-align: top;
        }

        .header-info {
            font-size: 14px;
            line-height: 18px;
            font-weight: bold;
        }

        .barcode-cell {
            vertical-align: top;
            text-align: left;
            width: 100px;
        }

        .qr-code-container {
            width: 100px;
            height: 100px;
            margin: 0 auto;
            text-align: center;
        }

        .dimensions-table {
            width: 110px;
            font-size: 11px;
        }

        .dimensions-table td {
            padding: 1px 3px;
            white-space: nowrap;
        }

        .seq-info {
            font-size: 14px;
            line-height: 18px;
            font-weight: bold;
            margin-top: 5px;
        }

        .footer-text {
            position: absolute;
           
            left: 0;
            right: 0;
            text-align: center;
            font-size: 9px;
            color: #333;
        }

        @media print {
            body {
                margin: 0;
                padding: 0;
            }

            .qr-row {
                border: 1px solid #000;
            }

            .qr-row.printed {
                border-color: #000;
                background-color: #ffd800 !important;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }
        }
    </style>
</head>
<body>
    <div id="labelContainer" runat="server"></div>
</body>
</html>
