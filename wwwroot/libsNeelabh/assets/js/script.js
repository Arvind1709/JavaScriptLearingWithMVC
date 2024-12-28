
// script for radio button to open and close text area

$(document).ready(function () {
    $(".medicine_radio").change(function () {
        if ($(this).val() == 'yes') {
            $('#medicine_box').show();
        } else {
            $('#medicine_box').hide();
        }
    });

    $(".checkup_radio").change(function () {
        if ($(this).val() == 'yes') {
            $('#checkup_box').show();
        } else {
            $('#checkup_box').hide();
        }
    });

    $(".alergy_radio").change(function () {
        if ($(this).val() == 'yes') {
            $('#alergy_box').show();
        } else {
            $('#alergy_box').hide();
        }
    });

    $(".mentalhealth_radio").change(function () {
        if ($(this).val() == 'yes') {
            $('#mental_healthbox').show();
        } else if ($(this).val() == 'no') {
            $('#mental_healthbox').hide();
        }
    });

    $("#land_radio").change(function () {
        if ($(this).val() == 'yes') {
            $('#landbox').show();
        } else if ($(this).val() == 'no') {
            $('#landbox').hide();
        }
    });

    $("#home_radio").change(function () {
        if ($(this).val() == 'yes') {
            $('#homebox').show();
        } else if ($(this).val() == 'no') {
            $('#homebox').hide();
        }
    });

    $(".education_radio").change(function () {
        if ($(this).val() == 'yes') {
            $('#select_education').show();
        } else if ($(this).val() == 'no') {
            $('#select_education').hide();
        }
    });
});