var CURRENT_USER = 0;
$('#users_grid').bootstrapTable({
    queryParams: function (params) {
        return {
            MaxResultCount: params.limit,
            PageNumber: (params.offset / params.limit),
            UserNumber: $('#search_UserNumber').val(),
            UserName: $('#search_UserName').val(),
            DepartmentName: $('#search_Department').val()
        }
    }
})

function actionFormatter(value, row, index) {
    return '\
						<a href="#" onclick=\'editViewById("' + value + '")\'  class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="修改用户">\
							<i class="la la-edit"></i>\
						</a>\
                        <a href="#" onclick=\'changeDepartmentViewById("' + value + '")\' class="m-portlet__nav-link btn m-btn m-btn--hover-primary m-btn--icon m-btn--icon-only m-btn--pill" title="调整部门">\
							            <i class="la la-object-group"></i>\
						</a>\
                        <a href="#" onclick=\'assignPermissionViewById("' + value + '")\' class="m-portlet__nav-link btn m-btn m-btn--hover-primary m-btn--icon m-btn--icon-only m-btn--pill" title="分配权限">\
							            <i class="la la-key"></i>\
						</a>\
                        <a href="#" onclick=\'assignRoleViewById("' + value + '")\' class="m-portlet__nav-link btn m-btn m-btn--hover-primary m-btn--icon m-btn--icon-only m-btn--pill" title="分配角色">\
							            <i class="la la-transgender-alt"></i>\
						</a>\
						<a href="#"  onclick=\'removeUser("' + value + '")\' class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="删除">\
							<i class="la la-trash"></i>\
						</a>\
        ';
}
$('#departmentUsers_grid').bootstrapTable()


function removeDepartmentUserFormatter(value, row, index) {
    return '\
						<a href="#" onclick=\'removeUserInDepartment("' + row.DepartmentId + '","' + row.UserId + '")\'  class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="移除">\
							<i class="la la-trash"></i>\
						</a>\
        ';
}
function removeUserRoleFormatter(value, row, index) {
    return '\
						<a href="#" onclick=\'removeRoleInUser("' + row.Id + '")\'  class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="移除">\
							<i class="la la-trash"></i>\
						</a>\
        ';
}
function editViewById(Id) {
    $.post('/User/GetUser', { Id: Id }, function (data) {
        $('#UserName').val(data.UserName)
        $('#Id').val(data.Id)
        $('#TelPhone').val(data.TelPhone)
        $("input:radio[name=Gender][value='" + data.Gender + "']").prop("checked", "checked");
        $('#Email').val(data.Email)
        $('#userModal').modal('show');
    })


}
$('#userForm').submit(function (e) {
    e.preventDefault();
    var user = {
        Id: $('#Id').val(),
        UserName: $('#UserName').val(),
        TelPhone: $('#TelPhone').val(),
        Email: $('#Email').val(),
        Gender: $("input[name='Gender']:checked").val()
    }
    abp.ajax({
        url: '/User/UpdateUser',
        data: JSON.stringify(user)
    }).done(function (data) {
        abp.notify.success(data.message);
        $('#userModal').modal('hide');
        $('#users_grid').bootstrapTable('refresh');
    })


});
$('#resetPassword_btn').click(function () {
    abp.ajax({
        url: '/User/ResetPassword',
        data: JSON.stringify({ Id: $('#Id').val() })
    }).done(function (data) {
        abp.notify.success(data.message);
        $('#userModal').modal('hide');
    })
})

function loadUserInDepartment(userId) {
    $.post('/User/DepartmentUsersGrid/' + userId, function (data) {
        $('#departmentUsers_grid').bootstrapTable("load", data);
    })

}

function removeUser(Id) {
    bootbox.confirm("确定要删除该用户吗?", function (r) {
        if (r) {
            abp.ajax({
                url: '/User/RemoveUser/',
                data: JSON.stringify({ Id: Id })
            }).done(function (data) {
                search();
                abp.notify.success(data.message);
            })

        }

    })

}
function removeUserInDepartment(departmentId, userId) {
    bootbox.confirm("确定移除该部门吗?", function (result) {
        if (result) {
            abp.ajax({
                url: '/User/RemoveDepartment',
                data: JSON.stringify({ UserId: userId, DepartmentId: departmentId })
            }).done(function (data) {
                loadUserInDepartment(userId)
                abp.notify.success(data.message);
            })
        }

    })
}
function search() {
    $('#users_grid').bootstrapTable('refresh', {
        UserNumber: $('#search_UserNumber').val(),
        UserName: $('#search_UserName').val(),
        DepartmentName: $('#search_Department').val()
    })

}
function clearSearch() {
    $('#search_UserNumber').val('')
    $('#search_UserName').val('')
    $('#search_Department').val('')

}
function IsDeleted(value, row, index) {
    if (value)
        return '<span class="m-badge m-badge--danger m-badge--wide">删除</span>'
    else
        return "正常"

}

var to2 = false;
$('#tree_search2').keyup(function () {
    if (to2) { clearTimeout(to); }
    to2 = setTimeout(function () {
        var v = $('#tree_search2').val();
        $('#depart_tree2').jstree(true).search(v);
    }, 250);
});
