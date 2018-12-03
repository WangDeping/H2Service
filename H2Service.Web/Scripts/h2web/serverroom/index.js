

$('#serverRoom_grid').bootstrapTable({

    queryParams: function (params) {
        return {
            MaxResultCount: params.limit,
            PageNumber: (params.offset / params.limit)
        }
    }, onCheck: function (row, arg) {



    }

})
//提交表单
$('#serverRoomForm').submit(function (e) {
    e.preventDefault();
    if ($('#departmentsSelect2').select2('val') <= 0) {
        abp.notify.error("请选择机房管理科室");
        return;
    }
    var serverRoom = {
        "Id": $('#Id').val(),
        "RoomName": $('#ServerName').val(),
        "Location": $('#Location').val(),
        "DepartmentId": $('#departmentsSelect2').select2('val'),
        "Description": $('#Description').val()
    }
    abp.ajax({
        url: '/ServerRoom/AddorUpdateServerRoom',
        data: JSON.stringify(serverRoom)
    }).done(function (data) {
        abp.notify.success(data.message);
        $('#serverRoomModal').modal('hide');
        $('#serverRoom_grid').bootstrapTable('refresh');
        }) 

})

$('#newBtn').click(function () {
    $('#Id').val(0);
    $('#departmentsSelect2').val(0).select2()
    $('#serverRoomForm').resetForm();
    $('#serverRoomModal').modal('show');

})

function actionFormatter(value, row, index) {
    var result = "<a href='#' onclick=\"ShowQrcode('" + value + "')\" title='二维码'><i class='la la-qrcode'></i></a>";
    result += "<a href='#'   onclick=\"UpdateServerRoom('" + value + "')\" title='修改'><i class='la la-edit'></i></a>";
    return result
}
function ShowQrcode(roomId) {
    var qrUrl = "/ServerRoom/ServerRoomQrcode/" + roomId
    $('#qrCodeImg').attr('src', qrUrl)
    $('#qrModal').modal('show');
}
function UpdateServerRoom(rooId) {
    $('#serverRoomForm').resetForm();
    $.post('/ServerRoom/GetServerRoom/' + rooId, function (data) {

        $('#Id').val(data.Id)
        $('#ServerName').val(data.RoomName)
        $('#Location').val(data.Location);
        $('#departmentsSelect2').val(data.DepartmentId).select2()
        $('#Description').val(data.Description)
        $('#serverRoomModal').modal('show');
    })

}