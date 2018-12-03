
$('#period_grid').bootstrapTable({

    queryParams: function (params) {
        return {
            MaxResultCount: params.limit,
            PageNumber: (params.offset / params.limit)
        }
    }

})