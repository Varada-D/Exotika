
var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("proposalSent")) {
        loadDataTable("proposalSent");
    }
    else {
        if (url.includes("proposalApproved")) {
            loadDataTable("proposalApproved");
        }
        else {
            if (url.includes("completed")) {
                loadDataTable("completed");
            }
            else {
                loadDataTable("all");
            }
        }
    }
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/UserSpecific/Vendors/ContractList?status=" + status
        },
        "columns": [
            { "data": "requirement.resort.name", "width": "5%" },
            { "data": "requirement.requirementName", "width": "25%" },
            { "data": "status", "width": "15%" },
            { "data": "startDate", "width": "15%" },
            { "data": "endDate", "width": "15%" },
            {
                "data": "contractId",
                "render": function (data) {
                    return `
                        <div class="w-75 mb-2 btn-group" role="group">
                            <a href="/UserSpecific/Vendors/ContractDetails?contractId=${data}" class="btn1 mx-2">View Details</a>
                        </div>
                    `
                },
                "width": "5%"
            },
        ]
    });
}