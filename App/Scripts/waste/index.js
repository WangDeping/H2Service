$('#waste_container').on('click', '.append-btn', function () {
    var btnAdd = $(this);
    var target = btnAdd.parent().prev().children('input');
    var title = btnAdd.parents('.weui-cells_form').prev('.weui-cells__title').html()
    var dialog = $.prompt2({
        title: title + '追加',      
        empty: false, // 是否允许为空        
        onOK: function (input,input2) {        
            if (input <= 0) {
                $.toptip('输入正确称重', 'error');
                $(".weui-dialog,.weui-mask").remove();
                return;
            }
            if (input > 5) {
                $.toptip('重量超过5Kg给与提示', 2500,'warning');
            }
            $.ajax({
                url: APPEND_WASTE_URL,
                data: { FlowId: btnAdd.attr('flowId'), Kind: btnAdd.attr('kind'), Weight: input,Code:input2 },
                success: function (data) {
                    $.toast("操作成功");
                    $('#waste_container').html(data);                  
                },
                error: function (data) {
                    $.toast("出错了", "cancel");
                }
            })
            $(".weui-dialog,.weui-mask").remove();
        },
        onCancel: function () {
            $(".weui-dialog,.weui-mask").remove();
        }
    });
    $('#weui-prompt-input').attr('type', 'number')

})
function scanWasteCode(codeInput) {
    wx.scanQRCode({
        desc: '医疗废物条码扫描',
        needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
        scanType: ["barCode"], // 可以指定扫二维码还是一维码，默认二者都有
        success: function (res) {
            var code = res.resultStr.replace('CODE_128,','')
         
            $.post(GET_ISEXISTSWASTE_CODE_URL, { code: code }, function (data) {
                if (data=="True") {
                    $.toast("条码号重复", "forbidden");
                }
                else if(data=="False")
                    $('#weui-prompt-input2').val(code);
            })
        },
        error: function (res) {
            if (res.errMsg.indexOf('function_not_exist') > 0) {
                $.alert('版本过低请升级')
            }
        }
    });

}
function scan() {
    wx.scanQRCode({
        desc: 'scanQRCode desc',
        needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
        scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
        success: function (res) {
            var code = res.resultStr  
            $.post(GET_EXTERNAL_USER_URL, { code: code }, function (data) {
                if (data.Id>0) {
                    $('#inputMan').val(data.UserName)
                    matchUser(data)
                }
                else
                    $.toast("非法用户", "forbidden");
            })          
        },
        error: function (res) {
            if (res.errMsg.indexOf('function_not_exist') > 0) {
                $.alert('版本过低请升级')
            }
        }
    });
}
function choosePic() {
    var imgs_len = $('#uploaderFiles li img').length

    if (imgs_len >= 6) {
        $.toast("最多6张", "forbidden");
        return;
    }
    wx.chooseImage({
        count: 1,
        sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
        sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
        success: function (res) {
            var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片

            $('#uploaderFiles').append('<li class="weui-uploader__file"><img width="77" height="77" src="' + localIds + '" /></li>');
            wx.uploadImage({
                localId: localIds[0], // 需要上传的图片的本地ID，由chooseImage接口获得
                isShowProgressTips: 1,// 默认为1，显示进度提示
                success: function (ures) {
                    var serverId = ures.serverId; // 返回图片的服务器端ID

                    $.post(APPEND_IMAGE_URL, { FlowId: FLOW_ID, ServerId: serverId }, function (data) {
                        $('.weui-uploader__info').val($('#uploaderFiles   li img').size());
                        if (data.code < 0)
                            $.toast("上传失败", "cancel");
                    })

                }
            });
        }
    });

}
function matchUser(user) {
    $('#UserName').html(user.UserName)  
    //$('#ExternalCompanyName').html(user.ExternalCompanyName)
    $('#Gender').html(user.Gender)
    $('#TelPhone').html(user.TelPhone)  
    $('#userAvatar').attr('src', user.AvatarUrl)
    var url = PREVIEWFLOW_URL + "?FlowId=" + FLOW_ID + "&CollectUserId=" + user.Id
    $('#showTooltips').attr('href',url);
    $("#collectUserInfo").popup();
}
