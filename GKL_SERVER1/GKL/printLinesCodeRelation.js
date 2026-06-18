/**
 * 检查番号二维码生成页面
 * 兼容 IE10+ 和 .NET Framework 4
 */

$(document).ready(function () {
    // 从 URL 参数获取 chk_no
    var urlParams = getUrlParams();
    var chk_no = urlParams.chk_no || '260430_T020670643_1';

    // 填充输入框
    $('#txtChkNo').val(chk_no);

    // 自动执行查询
    performSearch(chk_no);

    // 查询按钮点击事件
    $('#btnSearch').on('click', function () {
        var chk_no = $('#txtChkNo').val().trim();

        if (!chk_no) {
            showError('请输入检查番号');
            return;
        }

        performSearch(chk_no);
    });

    // 回车键查询
    $('#txtChkNo').on('keypress', function (e) {
        if (e.which === 13) {
            $('#btnSearch').click();
        }
    });
});

/**
 * 执行查询
 * @param {string} chk_no - 检查番号
 */
function performSearch(chk_no) {
    // 显示加载状态
    showLoading();
    hideError();
    $('#resultSection').hide();

    // 调用 AJAX 查询
    getPrintLinesCodeRelation(
        chk_no,
        function (data) {
            hideLoading();

            if (!data || data.length === 0) {
                showNoData();
                return;
            }

            // 显示结果
            $('#resultSection').show();

            // 生成二维码
            generateQRCodes(data);
            
            // 加载已打印状态
            loadPrintedStatus(chk_no);
        },
        function (error) {
            hideLoading();
            showError('查询失败: ' + error);
        }
    );
}

/**
 * 从 URL 获取参数
 * @returns {Object} 参数对象
 */
function getUrlParams() {
    var params = {};
    var queryString = window.location.search.substring(1);

    if (queryString) {
        var pairs = queryString.split('&');
        for (var i = 0; i < pairs.length; i++) {
            var pair = pairs[i].split('=');
            if (pair.length === 2) {
                params[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1] || '');
            }
        }
    }

    return params;
}

/**
 * 调用后端 API 获取数据
 * @param {string} chk_no - 检查番号
 * @param {function} successCallback - 成功回调
 * @param {function} errorCallback - 失败回调
 */
function getPrintLinesCodeRelation(chk_no, successCallback, errorCallback) {
    $.ajax({
        url: 'api.asmx/GetPrintLinesCodeRelationByChkNo',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            chk_no: chk_no
        }),
        success: function (response) {
            var result;

            // 解析返回的数据
            if (typeof response.d === 'string') {
                try {
                    result = JSON.parse(response.d);
                } catch (e) {
                    result = response.d;
                }
            } else {
                result = response.d;
            }

            if (result && result.success) {
                if (successCallback && typeof successCallback === 'function') {
                    successCallback(result.data);
                }
            } else {
                if (window.console && console.error) {
                    console.error('查询失败:', result ? result.message : '未知错误');
                }
                if (errorCallback && typeof errorCallback === 'function') {
                    errorCallback(result ? result.message : '查询失败');
                }
            }
        },
        error: function (xhr, status, error) {
            if (window.console && console.error) {
                console.error('AJAX请求错误:', error);
            }
            if (errorCallback && typeof errorCallback === 'function') {
                errorCallback('网络请求失败: ' + error);
            }
        }
    });
}

/**
 * 生成二维码卡片
 * @param {Array} data - 数据数组
 */
function generateQRCodes(data) {
    var container = $('#qrGrid');
    container.empty();

    // IE10 兼容: 使用 for 循环
    for (var i = 0; i < data.length; i++) {
        var row = data[i];
        
        // 创建包装容器
        var wrapper = $('<div class="label-wrapper"></div>');
        
        // 创建标签容器
        var qrRow = $('<div class="qr-row" data-index="' + i + '"></div>');
        
        // 创建标签内容
        var labelContent = $('<div class="label-content"></div>');
        
        // 创建主表格
        var mainTable = $('<table class="label-table" border="0"></table>');
        
        // 第一行：头部信息（作番、CD、ID）
        var headerRow = $('<tr></tr>');
        var headerCell = $('<td colspan="2"></td>');
        var headerTable = $('<table></table>');
        
        headerTable.append('<tr><td class="header-info">作番:' + (row.make_no || '-') + '</td></tr>');
        headerTable.append('<tr><td class="header-info">CD:' + (row.CD || '-') + '</td></tr>');
        headerTable.append('<tr><td>ID:' + (row.chk_no+'-'+i || '-') + '</td></tr>');
        
        headerCell.append(headerTable);
        headerRow.append(headerCell);
        mainTable.append(headerRow);
        
        // 第二行：二维码和尺寸信息
        var contentRow = $('<tr></tr>');
        
        // 左侧：二维码
        var barcodeCell = $('<td class="barcode-cell"></td>');
        var qrDivId = 'qr-' + i;
        var qrContainer = $('<div class="qr-code-container" id="' + qrDivId + '"></div>');
        barcodeCell.append(qrContainer);
        contentRow.append(barcodeCell);
        
        // 右侧：尺寸表格
        var dimsCell = $('<td></td>');
        var dimsTable = $('<table class="dimensions-table"></table>');
        
        dimsTable.append('<tr><td>&nbsp;W:' + formatDecimal(row.W, '0000.0') + '</td><td>&nbsp;H:' + formatDecimal(row.H, '0000.0') + '</td></tr>');
        dimsTable.append('<tr><td>DW:' + formatDecimal(row.DW, '0000.0') + '</td><td>DH:' + formatDecimal(row.DH, '0000.0') + '</td></tr>');
        dimsTable.append('<tr><td>SW:' + formatDecimal(row.SW, '0000.0') + '</td><td>KW:' + formatDecimal(row.KW, '0000.0') + '</td></tr>');
        dimsTable.append('<tr><td colspan="2"></td></tr>');
        dimsTable.append('<tr><td colspan="2" >' + (row.J_CD) + '</td></tr>');
        
        dimsCell.append(dimsTable);
        contentRow.append(dimsCell);
        
        mainTable.append(contentRow);
        labelContent.append(mainTable);
        
        // 底部提示文字
        var footerText = $('<div class="footer-text">此生产标签生产完成后请取下！</div>');
        labelContent.append(footerText);
        
        // 将标签内容添加到标签容器
        qrRow.append(labelContent);
        
        // 添加按钮容器
        var btnContainer = $('<div style="margin-top: 8px;"></div>');
        
        // 添加打印按钮
        var printBtn = $('<button class="print-btn" data-index="' + i + '">🖨️ 打印</button>');
        btnContainer.append(printBtn);
        
        // 添加预览按钮
        var previewBtn = $('<button class="preview-btn" data-index="' + i + '">👁️ 预览</button>');
        btnContainer.append(previewBtn);
        
        // 将标签和按钮添加到包装容器
        wrapper.append(qrRow);
        wrapper.append(btnContainer);
        
        $('#qrGrid').append(wrapper);

        // 生成二维码内容 - 按照指定格式
        var qrContent = buildQRCodeContent(row);

        // 生成二维码
        setTimeout((function (divId, content) {
            return function () {
                try {
                    new QRCode(document.getElementById(divId), {
                        text: content,
                        width: 90,
                        height: 90,
                        colorDark: "#000000",
                        colorLight: "#ffffff",
                        correctLevel: QRCode.CorrectLevel.M
                    });
                } catch (e) {
                    if (window.console && console.error) {
                        console.error('二维码生成失败:', e);
                    }
                }
            };  
        })(qrDivId, qrContent), 100 * i);

        // 绑定打印按钮事件
        (function(index, rowData) {
            printBtn.on('click', function() {
                handlePrintClick(index, rowData);
            });
            
            // 绑定预览按钮事件
            previewBtn.on('click', function() {
                handlePreviewClick(index, rowData);
            });
        })(i, row);
    }
}

/**
 * 显示加载状态
 */
function showLoading() {
    $('#loading').show();
}

/**
 * 隐藏加载状态
 */
function hideLoading() {
    $('#loading').hide();
}

/**
 * 显示错误信息
 */
function showError(message) {
    $('#errorMsg').text(message).show();
}

/**
 * 隐藏错误信息
 */
function hideError() {
    $('#errorMsg').hide();
}

/**
 * 处理打印按钮点击
 * @param {number} index - 卡片索引
 * @param {Object} row - 数据行
 */
function handlePrintClick(index, row) {
    var chk_no = $('#txtChkNo').val().trim();
    
    // 从数据行中获取字段
    var line_CD = row.line_CD || '';
    var CD = row.CD || '';
    var J_CD = row.make_no || ''; // 使用 make_no 作为 J_CD
    
    // 确认打印
    //if (!confirm('确定要打印此标签吗？')) {
    //    return;
    //}
    
    // 通过 iframe 调用 ClickOnce 应用程序（kbn=1 表示打印）
    callClickOnceApp(row, chk_no, line_CD, CD, J_CD, index, 1);
}

/**
 * 通过 iframe 调用 ClickOnce 应用程序
 * @param {Object} row - 数据行
 * @param {string} chk_no - 检查番号
 * @param {string} line_CD - 生产线
 * @param {string} CD - 商品CD
 * @param {string} J_CD - 日向商品CD
 * @param {number} index - 索引
 * @param {number} kbn - 区分码（1=打印，0=预览）
 */
function callClickOnceApp(row, chk_no, line_CD, CD, J_CD, index, kbn) {
    // 构建 URL 参数
    var params = [];
    params.push('kbn=' + kbn);
    params.push('index=' + index);
    params.push('chk_no=' + encodeURIComponent(chk_no));
    params.push('line_CD=' + encodeURIComponent(line_CD));
    params.push('CD=' + encodeURIComponent(CD));
    params.push('J_CD=' + encodeURIComponent(J_CD));
    params.push('make_no=' + encodeURIComponent(row.make_no || ''));
    params.push('suu=' + encodeURIComponent(row.suu || ''));
    params.push('sapOderNo=' + encodeURIComponent(row.sapOderNo || ''));
    params.push('sapIndexNo=' + encodeURIComponent(row.sapIndexNo || ''));
    params.push('H=' + encodeURIComponent(row.H || ''));
    params.push('W=' + encodeURIComponent(row.W || ''));
    params.push('DH=' + encodeURIComponent(row.DH || ''));
    params.push('DW=' + encodeURIComponent(row.DW || ''));
    params.push('SW=' + encodeURIComponent(row.SW || ''));
    params.push('KW=' + encodeURIComponent(row.KW || ''));
    params.push('shunwei=' + encodeURIComponent(row.shunwei || ''));
    
    var queryString = params.join('&');
    //var appUrl = 'http://localhost:60669/APP/FmPrint/PrintLarGkl.application?' + queryString;
    var appUrl = 'http://10.160.192.114/GKL2020/APP/FmPrint/PrintLarGkl.application?' + queryString;
    
    // 创建隐藏的 iframe
    var iframe = document.createElement('iframe');
    iframe.id = 'clickonce-iframe-' + index;
    iframe.style.position = 'absolute';
    iframe.style.width = '0px';
    iframe.style.height = '0px';
    iframe.style.border = 'none';
    iframe.style.display = 'none';
    document.body.appendChild(iframe);
    
    // 设置 iframe 的 URL，触发 ClickOnce 应用程序
    iframe.src = appUrl;
    
    // 保存打印记录
    savePrintHistory(chk_no, line_CD, CD, J_CD, function(success, message) {
        if (success) {
            markAsPrinted(index);
        } else {
            markAsPrintedLocal(index);
        }
    });
    
    // 延迟移除 iframe
    setTimeout(function() {
        var oldIframe = document.getElementById('clickonce-iframe-' + index);
        if (oldIframe && oldIframe.parentNode) {
            oldIframe.parentNode.removeChild(oldIframe);
        }
    }, 5000);
}

/**
 * 尝试使用 ActiveX 静默打印（IE 专用）
 * @returns {boolean} 是否成功执行静默打印
 */
function trySilentPrint(index, chk_no, line_CD, CD, J_CD) {
    try {
        // 检查是否是 IE 浏览器
        if (typeof ActiveXObject === 'undefined') {
            return false;
        }
        
        // 创建 WebBrowser ActiveX 对象
        var wb = new ActiveXObject('WebBrowser');
        if (!wb) {
            return false;
        }
        
        // 获取当前标签的 HTML
        var wrapper = $('.label-wrapper').eq(index);
        var qrRow = wrapper.find('.qr-row');
        var clonedContent = qrRow.clone();
        
        // 创建临时 HTML 文件
        var html = '<!DOCTYPE html>';
        html += '<html>';
        html += '<head><title>打印标签</title></head>';
        html += '<body style="margin:0;padding:0;">';
        html += clonedContent.prop('outerHTML');
        html += '</body></html>';
        
        // 保存到临时文件
        var fso = new ActiveXObject('Scripting.FileSystemObject');
        var tempFile = fso.GetSpecialFolder(2) + '\\print_label_' + index + '.html';
        var file = fso.CreateTextFile(tempFile, true);
        file.WriteLine(html);
        file.Close();
        
        // 使用 IE 打开并打印
        var ie = new ActiveXObject('InternetExplorer.Application');
        ie.Navigate(tempFile);
        
        // 等待加载完成后打印
        var checkLoaded = setInterval(function() {
            if (ie.ReadyState === 4) {
                clearInterval(checkLoaded);
                
                // 静默打印（不显示对话框）
                ie.ExecWB(6, 2); // 6=OLECMDID_PRINT, 2=OLECMDEXECOPT_DONTPROMPTUSER
                
                // 保存记录
                savePrintHistory(chk_no, line_CD, CD, J_CD, function(success, message) {
                    if (success) {
                        markAsPrinted(index);
                    } else {
                        markAsPrintedLocal(index);
                    }
                });
                
                // 延迟关闭
                setTimeout(function() {
                    ie.Quit();
                    // 删除临时文件
                    if (fso.FileExists(tempFile)) {
                        fso.DeleteFile(tempFile);
                    }
                }, 2000);
            }
        }, 100);
        
        return true;
    } catch (e) {
        if (window.console && console.log) {
            console.log('ActiveX 打印失败，切换到普通打印模式');
        }
        return false;
    }
}

/**
 * 普通打印方式（iframe）
 */
function normalPrint(index, chk_no, line_CD, CD, J_CD) {
    // 获取当前标签的 HTML
    var wrapper = $('.label-wrapper').eq(index);
    var qrRow = wrapper.find('.qr-row');
    
    // 克隆标签内容
    var clonedContent = qrRow.clone();
    
    // 创建隐藏的 iframe 用于打印
    var iframe = document.createElement('iframe');
    iframe.style.position = 'absolute';
    iframe.style.width = '0px';
    iframe.style.height = '0px';
    iframe.style.border = 'none';
    document.body.appendChild(iframe);
    
    var doc = iframe.contentWindow || iframe.contentDocument;
    if (doc.document) {
        doc = doc.document;
    }
    
    var html = '<!DOCTYPE html>';
    html += '<html>';
    html += '<head>';
    html += '<title>打印标签</title>';
    html += '</head>';
    html += '<body style="margin:0;padding:0;">';
    html += clonedContent.prop('outerHTML');
    html += '</body>';
    html += '</html>';
    
    doc.open();
    doc.write(html);
    doc.close();
    
    // 等待内容加载后打印
    setTimeout(function() {
        try {
            iframe.contentWindow.focus();
            iframe.contentWindow.print();
            
            // 打印后保存记录
            savePrintHistory(chk_no, line_CD, CD, J_CD, function(success, message) {
                if (success) {
                    markAsPrinted(index);
                } else {
                    markAsPrintedLocal(index);
                }
            });
        } catch (e) {
            if (window.console && console.error) {
                console.error('打印失败:', e);
            }
        }
        
        // 延迟移除 iframe
        setTimeout(function() {
            document.body.removeChild(iframe);
        }, 1000);
    }, 250);
}

/**
 * 处理预览按钮点击
 * @param {number} index - 卡片索引
 * @param {Object} row - 数据行
 */
function handlePreviewClick(index, row) {
    var chk_no = $('#txtChkNo').val().trim();
    
    // 从数据行中获取字段
    var line_CD = row.line_CD || '';
    var CD = row.CD || '';
    var J_CD = row.make_no || ''; // 使用 make_no 作为 J_CD
    
    // 通过 iframe 调用 ClickOnce 应用程序（kbn=0 表示预览）
    callClickOnceApp(row, chk_no, line_CD, CD, J_CD, index, 0);
}

/**
 * 标记为已打印（仅本地）
 * @param {number} index - 卡片索引
 */
function markAsPrintedLocal(index) {
    var wrapper = $('.label-wrapper').eq(index);
    var card = wrapper.find('.qr-row');
    card.addClass('printed');
    
    // 更改按钮文字，但不禁用
    wrapper.find('.print-btn').text('🖨️ 再次打印');
}

/**
 * 标记为已打印（本地+数据库）
 * @param {number} index - 卡片索引
 */
function markAsPrinted(index) {
    markAsPrintedLocal(index);
}

/**
 * 保存打印记录到数据库
 * @param {string} chk_no - 检查番号
 * @param {string} line_CD - 生产线
 * @param {string} CD - 商品CD
 * @param {string} J_CD - 日向商品CD
 * @param {function} callback - 回调函数
 */
function savePrintHistory(chk_no, line_CD, CD, J_CD, callback) {
    $.ajax({
        url: 'api.asmx/SavePrintHistory',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            chk_no: chk_no,
            line_CD: line_CD,
            CD: CD,
            J_CD: J_CD,
            printerName: '',
            userName: ''
        }),
        success: function (response) {
            var result;
            
            // 解析返回的数据
            if (typeof response.d === 'string') {
                try {
                    result = JSON.parse(response.d);
                } catch (e) {
                    result = response.d;
                }
            } else {
                result = response.d;
            }
            
            if (result && result.success) {
                if (window.console && console.log) {
                    console.log('打印记录保存成功');
                }
                if (callback) callback(true, result.message);
            } else {
                if (window.console && console.error) {
                    console.error('保存失败:', result ? result.message : '未知错误');
                }
                if (callback) callback(false, result ? result.message : '保存失败');
            }
        },
        error: function (xhr, status, error) {
            if (window.console && console.error) {
                console.error('保存打印记录错误:', error);
            }
            if (callback) callback(false, '网络请求失败: ' + error);
        }
    });
}

/**
 * 加载已打印状态
 * @param {string} chk_no - 检查番号
 */
function loadPrintedStatus(chk_no) {
    $.ajax({
        url: 'api.asmx/GetPrintedLabels',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            chk_no: chk_no
        }),
        success: function (response) {
            var result;
            
            // 解析返回的数据
            if (typeof response.d === 'string') {
                try {
                    result = JSON.parse(response.d);
                } catch (e) {
                    result = response.d;
                }
            } else {
                result = response.d;
            }
            
            if (result && result.success && result.data) {
                var printedCDs = result.data;
                
                // 遍历所有标签，标记已打印的
                var wrappers = $('.label-wrapper');
                for (var i = 0; i < wrappers.length; i++) {
                    var wrapper = $(wrappers[i]);
                    var qrRow = wrapper.find('.qr-row');
                    
                    // 从标签内容中提取 CD
                    var headerInfo = qrRow.find('.header-info');
                    var cdText = '';
                    for (var j = 0; j < headerInfo.length; j++) {
                        var text = $(headerInfo[j]).text();
                        if (text.indexOf('CD:') === 0) {
                            cdText = text.replace('CD:', '').trim();
                            break;
                        }
                    }
                    
                    // 检查是否在已打印列表中
                    var isPrinted = false;
                    for (var k = 0; k < printedCDs.length; k++) {
                        if (printedCDs[k] === cdText) {
                            isPrinted = true;
                            break;
                        }
                    }
                    
                    if (isPrinted) {
                        markAsPrintedLocal(i);
                    }
                }
                
                if (window.console && console.log) {
                    console.log('加载了', printedCDs.length, '条已打印记录');
                }
            }
        },
        error: function (xhr, status, error) {
            if (window.console && console.error) {
                console.error('加载已打印状态错误:', error);
            }
        }
    });
}

/**
 * 构建二维码内容字符串
 * @param {Object} row - 数据行
 * @returns {string} 格式化后的二维码内容
 */
function buildQRCodeContent(row) {
    var qrFM = '';
    
    // 工单号 make_no（10位）
    var makeNo = (row.make_no || '').toString().trim();
    qrFM += padRight(makeNo, 10, ' ') + '/';
    
    // code CD（20位）
    var cd = (row.CD || '').toString().trim();
    qrFM += padRight(cd, 20, ' ') + '/';
    
    // 数量 suu（4位）
    var suu = (row.suu || '0').toString().trim();
    qrFM += padLeft(suu, 4, '0') + '/';
    
    // SAP订单号 sapOderNo（15位）
    var sapOderNo = (row.sapOderNo || '').toString().trim();
    qrFM += padRight(sapOderNo.substring(0, 15), 15, ' ') + '/';
    
    // SAP订单序号 sapIndexNo（10位）- 取后2位
    var sapIndexNo = (row.sapIndexNo || '').toString().trim();
    var last2Chars = sapIndexNo.length >= 2 ? sapIndexNo.substring(sapIndexNo.length - 2) : sapIndexNo;
    qrFM += padRight(last2Chars, 10, ' ') + '/';
    
    // 打印时间（17位）
    var now = new Date();
    var timeStr = formatDate(now, 'yyyyMMddHHmmssfff');
    qrFM += padRight(timeStr, 17, ' ') + '/';
    
    // H（6位，格式 0000.0）
    var h = formatDecimal(row.H, '0000.0');
    qrFM += h + '/';
    
    // W（6位，格式 0000.0）
    var w = formatDecimal(row.W, '0000.0');
    qrFM += w + '/';
    
    // DH（6位，格式 0000.0）
    var dh = formatDecimal(row.DH, '0000.0');
    qrFM += dh + '/';
    
    // DW（6位，格式 0000.0）
    var dw = formatDecimal(row.DW, '0000.0');
    qrFM += dw + '/';
    
    // SW（6位，格式 0000.0）
    var sw = formatDecimal(row.SW, '0000.0');
    qrFM += sw + '/';
    
    // KW（6位，格式 0000.0）
    var kw = formatDecimal(row.KW, '0000.0');
    qrFM += kw + '/';
    
    // lot_no（8位）
    var lotNo = (row.lot_no || '').toString().trim();
    qrFM += padRight(lotNo, 8, ' ') + '/';
    
    // 末尾补充空格
    qrFM += '       ';
    
    return qrFM;
}

/**
 * 左填充字符串
 * @param {string} str - 原始字符串
 * @param {number} len - 目标长度
 * @param {string} padChar - 填充字符
 * @returns {string} 填充后的字符串
 */
function padLeft(str, len, padChar) {
    str = str.toString();
    while (str.length < len) {
        str = padChar + str;
    }
    return str;
}

/**
 * 右填充字符串
 * @param {string} str - 原始字符串
 * @param {number} len - 目标长度
 * @param {string} padChar - 填充字符
 * @returns {string} 填充后的字符串
 */
function padRight(str, len, padChar) {
    str = str.toString();
    while (str.length < len) {
        str = str + padChar;
    }
    return str;
}

/**
 * 格式化数字为指定格式
 * @param {*} value - 数值
 * @param {string} format - 格式（如 '0000.0'）
 * @returns {string} 格式化后的字符串
 */
function formatDecimal(value, format) {
    if (value === null || value === undefined || value === '' || value === 'null') {
        return format.replace(/0/g, '0').replace('.', '.');
    }
    
    var num = parseFloat(value.toString());
    if (isNaN(num)) {
        return format.replace(/0/g, '0').replace('.', '.');
    }
    
    // 根据格式确定小数位数
    var decimalPlaces = 0;
    var dotIndex = format.indexOf('.');
    if (dotIndex !== -1) {
        decimalPlaces = format.length - dotIndex - 1;
    }
    
    // 格式化数字
    var formatted = num.toFixed(decimalPlaces);
    
    // 确保总长度符合格式要求（不包括小数点）
    var totalLength = format.length - 1; // 减去小数点
    var parts = formatted.split('.');
    var intPart = parts[0];
    var decPart = parts.length > 1 ? parts[1] : '';
    
    // 左填充整数部分
    var intLength = format.indexOf('.') !== -1 ? format.indexOf('.') : format.length;
    intPart = padLeft(intPart, intLength, '0');
    
    return intPart + (decPart ? '.' + decPart : '');
}

/**
 * 格式化日期
 * @param {Date} date - 日期对象
 * @param {string} format - 格式字符串
 * @returns {string} 格式化后的日期字符串
 */
function formatDate(date, format) {
    var year = date.getFullYear();
    var month = padLeft(date.getMonth() + 1, 2, '0');
    var day = padLeft(date.getDate(), 2, '0');
    var hours = padLeft(date.getHours(), 2, '0');
    var minutes = padLeft(date.getMinutes(), 2, '0');
    var seconds = padLeft(date.getSeconds(), 2, '0');
    var milliseconds = padLeft(date.getMilliseconds(), 3, '0');
    
    return format
        .replace('yyyy', year)
        .replace('MM', month)
        .replace('dd', day)
        .replace('HH', hours)
        .replace('mm', minutes)
        .replace('ss', seconds)
        .replace('fff', milliseconds);
}

/**
 * 显示无数据提示
 */
function showNoData() {
    $('#resultSection').show();
    $('#qrGrid').html('<div class="no-data">没有找到匹配的记录</div>');
}
