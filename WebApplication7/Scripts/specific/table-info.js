var currentDat;

function colorStatus() {
    $(".status").each(function(index) {
        console.log($(this).html());
        console.log($(this));

        if ($(this).html() == 0) {
            $(this).css("color", "blue");
        } else if ($(this).html() == 4) {
            $(this).css("color", "red");
        } else {
            $(this).css("color", "grey");
        }
    });
};

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
        function(data, status) {
            modalLogs(data, datId);
        });
};

function resendClick(datId) {
    console.log(datId);


    var data = new Object();
    data.Id = datId;

    $.post("http://localhost:55698/api/resend", data);
    console.log("Sent booles!");
};

function gatherData(data, datId) {
    $.each(data, function (index, value) {
        if (value.Id == datId) {
            value.Id=$("#selectId").html;
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

function postToResend(postDat) {
    postDat = JSON.stringify(postDat);
    $.post("http://localhost:55698/api/Logs", postDat);
};

$(document).ready(function() {
    $("td").click(function () {
        currentDat = $(this).parent().attr("accesskey");
        console.log(currentDat);
        getLogs(currentDat);

        $("#modalData").modal("toggle");
    });

    $("#resend").click(function() {
        currentDat = $("#selectId").html();
        resendClick(currentDat);
    });

    colorStatus();
});