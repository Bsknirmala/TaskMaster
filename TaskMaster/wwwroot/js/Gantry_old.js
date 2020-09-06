var dataTable;

$(document).ready(function () {
    $('#CarparkID').change(function () {
        var carparkid = $(this).val();
        $.ajax({
            type: "Post",
            url: "/Admin/Issue/GetGantryList?CarparkID=" + carparkid,
            contentType: "html",
            success: function (response) {
                $("#GantryID").empty();
                $("#GantryID").append(response);
            }
        });
    });
});