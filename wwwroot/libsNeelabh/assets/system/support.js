// Jquery extention support library methods --
(function ($) {

    // Form Related --
    $.fn.showForm = function () {

        $('.app_form').addClass('visually-hidden');
        $(this).removeClass('visually-hidden');
    };

    $.fn.validateForm = function () {
        $.validator.unobtrusive.parse(this)
        return $(this).valid();
    };

    $.fn.redirectRequest = function (action, params, method = 'POST') {

        const form = document.createElement('form');
        form.action = action;
        form.method = method;

        for (const key in params) {
            if (params.hasOwnProperty(key)) {
                const hiddenField = document.createElement('input');
                hiddenField.type = 'hidden';
                hiddenField.name = key;
                hiddenField.value = params[key];

                form.appendChild(hiddenField);
            }
        }

        document.body.appendChild(form);
        form.submit();
    };
    // Data Table --
    $.fn.initDataTable = function () {
        var tableObj = $(this).DataTable({
            "displayLength": 15,
            "destroy": true
        });

        $('.dataTables_length').remove();
        $('.dataTables_filter').remove();
        $('.dataTables_info').remove();
    };

    $.fn.initDefaultTerms = function () {
        // Disabled Autocomple text on text box
        $('form').attr('autocomplete', 'off');
        $('input').attr('list', 'autocompleteOff');
        $('input').attr('autocomplete', 'off');

        // Prevent submit form when press on enter key in input elements
        $('input').keypress(function (e) {
            if (e.which == 13) {
                e.preventDefault(); //stops default action: submitting form
            }
        });

        if ($('.initdatatable:visible').length > 0) $('.initdatatable:visible').initDataTable();

        // New ==
        // When user press enter key the call its submit button --
        // Call Form Submit Button On Enter Key Press Event --
        $('form input').keypress(function (e) {
            if (e.which == 13) {
                var ctrl_id = $(this).attr('id');
                var form_id = $(this).closest("form").attr('id');
                $('#' + form_id + " .callonenter:visible:enabled").click();
            }
        });

        //// Month and Year Select Picker --
        //$('[data-toggle="datepicker"]').datepicker({
        //    autoHide: true,
        //    pick: function (e) {
        //        e.preventDefault(); //prvent any default action..
        //        var pickedDate = e.date; //get date
        //        var date = e.date.getDate()
        //        var month = $(this).datepicker('getMonthName')
        //        var year = e.date.getFullYear()
        //        var new_date = date + "-" + month + "-" + year
        //        //set date
        //        // $(this).val(`${date} ${month} ${year}`)
        //        $(this).val(new_date)
        //    }
        //});

        // date picker
        /*flatpickr(".flatpickr");*/

        //.$("select").select2("destroy").select2();
        //$('.form-select').each(function (index, elem) {
        //    var attr = $(this).attr('data-choices');
        //    if (typeof attr !== 'undefined' && attr !== false) {
        //        new Choices(elem);
        //    }
        //});


        // Required Filed Sign --
        $('label[for!=""]').each(function (idx, ele) {
            // return for checkbox --
            if ($('#' + $(ele).attr('for')).is(":checkbox")) return;
            if ($(ele).hasClass('skip-star')) return;   // don't show star sign 

            var attr = $('#' + $(ele).attr('for')).attr('data-val-required');
            if (typeof attr !== typeof undefined && attr !== "false" && attr !== false) {
                var text = $(ele).text().replace(' *', '');
                $(ele).html(text + " <span class='text-danger'>*</span>");
            }
        });

    };

    $.fn.initDefaultTerms();

    //This code solves the problem, at least in IE and Firefox (haven't tested any other, but I give it a reasonable chance of working if the problem even exists in other browsers).
    // Prevent the backspace key from navigating back.
    $(document).unbind('keydown').bind('keydown', function (event) {
        var doPrevent = false;
        if (event.keyCode === 8) {
            var d = event.srcElement || event.target;
            if ((d.tagName.toUpperCase() === 'INPUT' &&
                (
                    d.type.toUpperCase() === 'TEXT' ||
                    d.type.toUpperCase() === 'PASSWORD' ||
                    d.type.toUpperCase() === 'FILE' ||
                    d.type.toUpperCase() === 'SEARCH' ||
                    d.type.toUpperCase() === 'EMAIL' ||
                    d.type.toUpperCase() === 'NUMBER' ||
                    d.type.toUpperCase() === 'DATE')
            ) ||
                d.tagName.toUpperCase() === 'TEXTAREA') {
                doPrevent = d.readOnly || d.disabled;
            }
            else {
                doPrevent = true;
            }
        }

        if (doPrevent) {
            event.preventDefault();
        }
    });

    $.fn.deleteAlert = function (executeFun, id) {
        Swal.fire({
            title: 'Are you sure?',
            text: "Once deleted you will not be able to get it back.",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            confirmButtonClass: 'btn btn-primary',
            cancelButtonClass: 'btn btn-danger ms-2',
            buttonsStyling: false,
        }).then(function (result) {
            if (result.value) {
                executeFun(id);
            }
        });
    };

    $.fn.displayMsg = function (title, message, msgType) {

        //alert(title + "\n" + msgType + ":" + message);

        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-center"
        };

        if (msgType.toLowerCase() == "success") {
            toastr.success(message, title);
        }
        else if (msgType.toLowerCase() == "warning") {
            toastr.warning(message, title);
        }
        else if (msgType.toLowerCase() == "error") {
            message = "<span class='text-warning mb-3'>Review the below error(s):</span> <br/><i class='fa fa-caret-right'></i> " + message;
            toastr.error(message, title);
        }
    };

    $.fn.confirmAlert = function (title, message) {
        return confirm(title + "\n" + message);
    };


    // Page Loader --
    $.fn.startPageLoading = function (options) {
        if (options && options.animate) {
            $('.page-spinner-bar').remove();
            $('body').append('<div class="page-spinner-bar"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>');
        } else {
            $('.page-loading').remove();
            $('body').append('<div class="page-loading"><img src="' + $.fn.getRootWebSitePath() + '/assets/images/loading-spinner-blue.gif"/>&nbsp;<span>' + (options && options.message ? options.message : 'Please Wait...') + '</span></div>');
        }
    }

    $.fn.stopPageLoading = function () {
        $('.page-loading, .page-spinner-bar').remove();
    };

    // Page Scroll
    $.fn.scrollHere = function () {
        if ($(this) == undefined) return;
        var pos = $(this).offset().top - 200;
        $('html, body').animate({ 'scrollTop': pos }, 10);
        return false;
    }

    // validate number --
    $.fn.IsValidNumber = function (value) {
        var intRegex = /^\d+$/;
        var floatRegex = /^((\d+(\.\d *)?)|((\d*\.)?\d+))$/;

        if (intRegex.test(value) || floatRegex.test(value))
            return true;
        else
            return false;
    }

})(jQuery);
