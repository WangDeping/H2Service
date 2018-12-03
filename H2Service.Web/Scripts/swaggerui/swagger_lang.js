///    <summary>
/// ����ת��
///    </summary>
var SwaggerTranslator = (function () {
    //��ʱִ�м���Ƿ�ת��������,���ִ��500��  ��500*50/1000=25s
    var iexcute = 0,
        //�������԰�
        _words = {
            "Warning: Deprecated": "���棺�ѹ�ʱ",
            "Implementation Notes": "ʵ�ֱ�ע",
            "Response Class": "��Ӧ��",
            "Status": "״̬",
            "Parameters": "����",
            "Parameter": "����",
            "Value": "ֵ",
            "Description": "����",
            "Parameter Type": "��������",
            "Data Type": "��������",
            "Response Messages": "��Ӧ��Ϣ",
            "HTTP Status Code": "HTTP״̬��",
            "Reason": "ԭ��",
            "Response Model": "��Ӧģ��",
            "Request URL": "����URL",
            "Response Body": "��Ӧ��",
            "Response Code": "��Ӧ��",
            "Response Headers": "��Ӧͷ",
            "Hide Response": "������Ӧ",
            "Headers": "ͷ",
            "Try it out!": "��һ�£�",
            "Show/Hide": "��ʾ/����",
            "List Operations": "��ʾ����",
            "Expand Operations": "չ������",
            "Raw": "ԭʼ",
            "can't parse JSON.  Raw result": "�޷�����JSON. ԭʼ���",
            "Model Schema": "ģ�ͼܹ�",
            "Model": "ģ��",
            "apply": "Ӧ��",
            "Username": "�û���",
            "Password": "����",
            "Terms of service": "��������",
            "Created by": "������",
            "See more at": "�鿴���ࣺ",
            "Contact the developer": "��ϵ������",
            "api version": "api�汾",
            "Response Content Type": "��ӦContent Type",
            "fetching resource": "���ڻ�ȡ��Դ",
            "fetching resource list": "���ڻ�ȡ��Դ�б�",
            "Explore": "���",
            "Show Swagger Petstore Example Apis": "��ʾ Swagger Petstore ʾ�� Apis",
            "Can't read from server.  It may not have the appropriate access-control-origin settings.": "�޷��ӷ�������ȡ������û����ȷ����access-control-origin��",
            "Please specify the protocol for": "��ָ��Э�飺",
            "Can't read swagger JSON from": "�޷���ȡswagger JSON��",
            "Finished Loading Resource Information. Rendering Swagger UI": "�Ѽ�����Դ��Ϣ��������ȾSwagger UI",
            "Unable to read api": "�޷���ȡapi",
            "from path": "��·��",
            "Click to set as parameter value": "������ò���",
            "server returned": "����������"
        },

        //��ʱִ��ת��
        _translator2Cn = function () {
            if ($("#resources_container .resource").length > 0) {
                _tryTranslate();
            }

            if ($("#explore").text() === "Explore" && iexcute < 500) {
                iexcute++;
                setTimeout(_translator2Cn, 50);
            }
        },

        //���ÿ�����ע��
        _setControllerSummary = function () {
            $.ajax({
                type: "get",
                async: true,
                url: $("#input_baseUrl").val(),
                dataType: "json",
                success: function (data) {
                    var summaryDict = data.ControllerDesc;
                    var id, controllerName, strSummary;
                    $("#resources_container .resource").each(function (i, item) {
                        id = $(item).attr("id");
                        if (id) {
                            controllerName = id.substring(9);
                            strSummary = summaryDict[controllerName];
                            if (strSummary) {
                                $(item).children(".heading").children(".options").prepend('<li class="controller-summary" title="' + strSummary + '">' + strSummary + '</li>');
                            }
                        }
                    });
                }
            });
        },

        //���Խ�Ӣ��ת��������
        _tryTranslate = function () {
            $('[data-sw-translate]').each(function () {
                $(this).html(_getLangDesc($(this).html()));
                $(this).val(_getLangDesc($(this).val()));
                $(this).attr('title', _getLangDesc($(this).attr('title')));
            });
        },
        _getLangDesc = function (word) {
            return _words[$.trim(word)] !== undefined ? _words[$.trim(word)] : word;
        };

    return {
        Translator: function () {
            document.title = "API�����ĵ�";
            $('body').append('<style type="text/css">.controller-summary{color:#10a54a !important;word-break:keep-all;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;max-width:250px;text-align:right;cursor:default;} </style>');
            $("#logo").html("�ӿ�����").attr("href", "/Home/Index");
            //���ÿ���������
            _setControllerSummary();
            _translator2Cn();
        }
    }
})();
//ִ��ת��
SwaggerTranslator.Translator();

function setCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) +
        ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
}

function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}

var token = getCookie("xToken");
if (token) {
    $('[name="authorization"]').val(token);
}


var XSRFToken = getCookie("XSRF-TOKEN");
if (XSRFToken) {
    $('[name="X-XSRF-TOKEN"]').val(XSRFToken);
}

var setToken = function () {
    var json = $("#resource_Account .response_body.json").text()
    if (json) {
        var obj = eval('(' + json + ')');
        if (obj.result) {
            $('[name="authorization"]').val("Bearer " + obj.result);
            setCookie("xToken", "Bearer " + obj.result);
        }
    }
}

setInterval(setToken, 5000)