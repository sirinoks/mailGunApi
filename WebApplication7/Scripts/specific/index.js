var currentDat;

function modalLogs(data, datId) {
    $.each(data, function (index, value) {
        if (value.Id == datId) {
            $("#selectId").html(value.Id);
            $("#selectDate").html(value.Date);
            $("#selectStatus").html(value.Status);
            $("#selectFrom").html(value.From);
            $("#selectTo").html(value.To);
            $("#selectTitle").html(value.Title);
            $("#selectBody").html(value.Body);
        }
    });
};

function getLogs(datId) {
    $.get("http://localhost:55698/api/Logs",
        function (data, status) {
            modalLogs(data, datId);
        });
};

function resendClick(datId) {
    console.log(datId);


    var data = new Object();
    data.Id = datId;

    $.post("http://localhost:55698/api/resend", data);
    console.log("Sent booles!");
}

function postToResend(postDat) {
    postDat = JSON.stringify(postDat);
    $.post("http://localhost:55698/api/Logs", postDat); 
}

$(document).ready(function () {
    addTags();

    $(".item").click(function () {
        currentDat = $(this).attr("accesskey");
        console.log(currentDat);
        getLogs(currentDat);

        $("#modalData").modal("toggle");
    });

    $("#resend").click(function () {
        currentDat = $("#selectId").html();
        resendClick(currentDat);
    });
});

