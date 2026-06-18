/**
 * 生产线权限管理 - AJAX版本
 * 使用ES5 + jQuery
 */

// 全局变量
var selectedLineId = '';
var linesKengenServiceUrl = 'lines_kengen.asmx';

// 页面加载完成后初始化
$(document).ready(function() {
    // 加载数据
    loadData();
    
    // 绑定按钮事件
    $('#btnSearch').click(searchData);
    $('#btnClearSearch').click(clearSearch);
    $('#btnInsert').click(insertLine);
    $('#btnUpdate').click(updateLine);
    $('#btnDelete').click(deleteLine);
    
    // 绑定输入框事件
    $('#txtSearchLineId').keypress(function(e) {
        if (e.which === 13) { // Enter键
            searchData();
        }
    });
});

// 显示状态消息
function showStatus(message, type) {
    var statusDiv = $('#statusMessage');
    statusDiv.removeClass('success error');
    statusDiv.addClass(type);
    statusDiv.text(message);
    statusDiv.show();
    
    // 5秒后自动隐藏
    setTimeout(function() {
        statusDiv.hide();
    }, 5000);
}

// 加载数据
function loadData(searchText) {
    showLoading(true);
    
    $.ajax({
        url: linesKengenServiceUrl + '/GetAllLines',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ searchText: searchText || '' }),
        success: function(response) {
            showLoading(false);
            if (response.d && response.d.success) {
                renderTable(response.d.data);
            } else {
                showStatus('加载数据失败: ' + (response.d ? response.d.message : '未知错误'), 'error');
                renderTable([]);
            }
        },
        error: function(xhr, status, error) {
            showLoading(false);
            showStatus('加载数据失败: ' + error, 'error');
            renderTable([]);
        }
    });
}

// 搜索数据
function searchData() {
    var searchText = $('#txtSearchLineId').val().trim();
    loadData(searchText);
}

// 清空搜索
function clearSearch() {
    $('#txtSearchLineId').val('');
    loadData();
}

// 新增生产线
function insertLine() {
    var lineId = $('#txtLineId').val().trim();
    
    if (!lineId) {
        showStatus('请输入生产线ID', 'error');
        return;
    }
    
    if (lineId.length > 10) {
        showStatus('生产线ID不能超过10个字符', 'error');
        return;
    }
    
    $.ajax({
        url: linesKengenServiceUrl + '/InsertLine',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ lineId: lineId }),
        success: function(response) {
            if (response.d && response.d.success) {
                showStatus('新增成功', 'success');
                $('#txtLineId').val('');
                selectedLineId = '';
                loadData();
            } else {
                showStatus('新增失败: ' + (response.d ? response.d.message : '未知错误'), 'error');
            }
        },
        error: function(xhr, status, error) {
            showStatus('新增失败: ' + error, 'error');
        }
    });
}

// 更新生产线
function updateLine() {
    if (!selectedLineId) {
        showStatus('请先选择要更新的生产线', 'error');
        return;
    }
    
    var newLineId = $('#txtLineId').val().trim();
    
    if (!newLineId) {
        showStatus('请输入新的生产线ID', 'error');
        return;
    }
    
    if (newLineId.length > 10) {
        showStatus('生产线ID不能超过10个字符', 'error');
        return;
    }
    
    $.ajax({
        url: linesKengenServiceUrl + '/UpdateLine',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ oldLineId: selectedLineId, newLineId: newLineId }),
        success: function(response) {
            if (response.d && response.d.success) {
                showStatus('更新成功', 'success');
                $('#txtLineId').val('');
                selectedLineId = '';
                loadData();
            } else {
                showStatus('更新失败: ' + (response.d ? response.d.message : '未知错误'), 'error');
            }
        },
        error: function(xhr, status, error) {
            showStatus('更新失败: ' + error, 'error');
        }
    });
}

// 删除生产线
function deleteLine() {
    if (!selectedLineId) {
        showStatus('请先选择要删除的生产线', 'error');
        return;
    }
    
    if (!confirm('确定要删除生产线 "' + selectedLineId + '" 吗？')) {
        return;
    }
    
    $.ajax({
        url: linesKengenServiceUrl + '/DeleteLine',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ lineId: selectedLineId }),
        success: function(response) {
            if (response.d && response.d.success) {
                showStatus('删除成功', 'success');
                $('#txtLineId').val('');
                selectedLineId = '';
                loadData();
            } else {
                showStatus('删除失败: ' + (response.d ? response.d.message : '未知错误'), 'error');
            }
        },
        error: function(xhr, status, error) {
            showStatus('删除失败: ' + error, 'error');
        }
    });
}

// 选择生产线
function selectLine(lineId) {
    selectedLineId = lineId;
    $('#txtLineId').val(lineId);
    
    // 更新单选按钮状态
    $('input[name="lineRadio"]').prop('checked', false);
    $('input[value="' + lineId + '"]').prop('checked', true);
}

// 编辑生产线
function editLine(lineId) {
    selectLine(lineId);
}

// 渲染表格
function renderTable(data) {
    var tableBody = $('#tableBody');
    tableBody.empty();
    
    if (!data || data.length === 0) {
        tableBody.append('<tr><td colspan="3" class="loading">没有找到数据</td></tr>');
        return;
    }
    
    for (var i = 0; i < data.length; i++) {
        var line = data[i];
        var row = $('<tr></tr>');
        
        // 选择列
        var radioCell = $('<td></td>');
        var radio = $('<input type="radio" name="lineRadio" value="' + line.line_id_gen + '">');
        radio.click(function() {
            selectLine($(this).val());
        });
        radioCell.append(radio);
        row.append(radioCell);
        
        // 生产线ID列
        var idCell = $('<td></td>');
        idCell.text(line.line_id_gen);
        idCell.css('font-weight', 'bold');
        row.append(idCell);
        
        // 操作列
        var actionCell = $('<td></td>');
        var editBtn = $('<button type="button" class="btn btn-primary">编辑</button>');
        editBtn.click(function() {
            var lineId = $(this).closest('tr').find('input[type="radio"]').val();
            editLine(lineId);
        });
        actionCell.append(editBtn);
        row.append(actionCell);
        
        tableBody.append(row);
    }
    
    // 设置表格行点击效果
    $('#dataTable tbody tr').click(function(e) {
        if (!$(e.target).is('input') && !$(e.target).is('button')) {
            var radio = $(this).find('input[type="radio"]');
            if (radio.length) {
                radio.prop('checked', true);
                selectLine(radio.val());
            }
        }
    });
}

// 显示/隐藏加载状态
function showLoading(show) {
    if (show) {
        $('#tableBody').html('<tr><td colspan="3" class="loading">加载中...</td></tr>');
    }
}

// 工具函数：格式化日期
function formatDate(date) {
    if (!date) return '';
    var d = new Date(date);
    return d.getFullYear() + '-' + 
           ('0' + (d.getMonth() + 1)).slice(-2) + '-' + 
           ('0' + d.getDate()).slice(-2) + ' ' + 
           ('0' + d.getHours()).slice(-2) + ':' + 
           ('0' + d.getMinutes()).slice(-2) + ':' + 
           ('0' + d.getSeconds()).slice(-2);
}