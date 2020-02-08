function eventCheckStyle() {
    $(".eventChoice").change(function() {
        $(this).toggleClass("selectedEvent", this.checked);
    });
}

function radioDisplayStyle() {
    $(".displayChoice").change(function () {
        $(this).toggleClass("displaySelected", this.checked);
        $(this).siblings().removeClass("displaySelected", this.checked);
    });
}

function timeRange() {
    $('#timeRange').dateRangePicker();
}

$(document).ready(function () {
    radioDisplayStyle();

    eventCheckStyle();
    timeRange();
});

