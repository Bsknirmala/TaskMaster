var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("all")) {
        loadDataTable("GetIssueList?status=all");
    }
    else {
        if (url.includes("techfollowup")) {
            loadDataTable("GetIssueList?status=techfollowup");
        }
        else {
            if (url.includes("closed")) {
                loadDataTable("GetIssueList?status=closed");
            }
            else {
                loadDataTable("GetIssueList?status=opsfollowup")
            }
        }
    }
});

function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Issue/" + url
        },
        "columns": [
            { "data": "carpark.prefix", "width": "9%" },
            { "data": "gantry.description", "width": "7%" },
            { "data": "hardwaretype.description", "width": "7%" },
            {
                "data": "issuedt", "width": "7%",
                'render': function (issuedt) {
                    return issuedt.substr(0,10);
                }
            },
            
            { "data": "reportedby", "width": "9%" },
            { "data": "description", "width": "24%" },
            { "data": "action", "width": "24%" },
            { "data": "serviceRptno", "width": "8%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Issue/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i> 
                                </a>
                        </div>
                        `;
                }, "width": "5%"
            }

        ]


    });
}
