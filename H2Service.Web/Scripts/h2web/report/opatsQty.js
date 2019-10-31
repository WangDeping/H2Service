var currentDate = moment().subtract(1, 'days').format('YYYY-MM-DD')
$('#patQtyStart_datepicker,#patQtyEnd_datepicker').datepicker({
    todayHighlight: true,
    format: 'yyyy-mm-dd',
    autoclose: true,
    orientation: 'bottom'
});
$('#patQtyStart_datepicker,#patQtyEnd_datepicker').datepicker('setDate', currentDate)
$('#patQty_grid').bootstrapTable({
    queryParams: function (params) {
        return {
            MaxResultCount: params.limit,
            PageNumber: (params.offset / params.limit),
            Start: moment($('#patQtyStart_datepicker').datepicker('getDate')).format('YYYY-MM-DD'),
            End: moment($('#patQtyEnd_datepicker').datepicker('getDate')).format('YYYY-MM-DD')
        }
    }
})
function search() {
     
    $('#patQty_grid').bootstrapTable('refresh',
        {
            Start: moment($('#patQtyStart_datepicker').datepicker('getDate')).format('YYYY-MM-DD'),
            End: moment($('#patQtyEnd_datepicker').datepicker('getDate')).format('YYYY-MM-DD')

        })
}
