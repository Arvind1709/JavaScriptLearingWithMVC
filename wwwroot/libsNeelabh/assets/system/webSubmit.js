var Protocol = {
    Commands: {
        Row: "[$RW]",
        Col: "[$CL]",
        Gap: "[$GP]",
        Break: "[$BR]",
        NoRow: "[$NR]",
        MoreData: "[$MD]",
        DataCompleted: "[$DC]",
        Success: "[$SUCCESS]",
        Error: "[$ERROR]",
        Information: "[$INFO]",
        Warning: "[$WARNING]"
    },

    Methods: {
        Get: "GET",
        Post: "POST"
    }
};

// used to prevent double click --
var ajaxIsAlreadyInProcess = false; 

(function ($) {

    $.fn.webSubmit = function (options) {
        if (ajaxIsAlreadyInProcess && options.multipleRequests !== true) return;
        ajaxIsAlreadyInProcess = true;

        //$.fn.startPageLoading({animate: false});  // in support file
        $('.btn').addClass('disabled');

        function postXHR() {
            var defaults = {
                location: '',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                processData: true,
                action: options.action,
                data: "",
                method: Protocol.Methods.Post,
                displayID: null,
                successMsg: null,
                showForm: null,
                errDisplayID: null,
                resetCall: null,
                callback: null,
                debug : false
            };

            var settings = $.extend({}, defaults, options);
            if (settings.debug) alert("Request Being Send...\nProcess=" + settings.action + "\nYou are sending below Data\n" + settings.data);

            function end_PostXHR(xhr, status) {
                var IsJSON = "";        // check whether response in Json Format 
                var HasError = "";      // check there is any error after execution
                var IsFormError = "";   // used to show errors in form and not popup it

                $('.btn').removeClass('disabled');
                ajaxIsAlreadyInProcess = false;
                //$.fn.stopPageLoading();

                if (status == "success") {

                    IsJSON = xhr.getResponseHeader('IsJSON') == "True" ? true : false;
                    HasError = xhr.getResponseHeader('HasError') === "True" ? true : false;
                    IsFormError = xhr.getResponseHeader('IsFormError') === "True" ? true : false;

                    if (IsJSON) xhr.responseText = JSON.parse(xhr.responseText);

                    if (HasError && !IsFormError) {
                        $.fn.displayMsg("Process Failed!!", xhr.responseText.message, "error");
                        if (settings.callback != null) settings.callback(HasError, xhr.responseText);
                        return;
                    }

                    if (!IsJSON && !HasError) {
                        if (settings.displayID !== null) $('#' + settings.displayID).html(xhr.responseText);
                    }
                    else if (!IsJSON && HasError) {
                        if (settings.errDisplayID !== null) $('#' + settings.errDisplayID).html(xhr.responseText);
                        else $('#' + settings.displayID).html(xhr.responseText);
                    }

                    if (!HasError) {
                        if (settings.showForm != null) settings.showForm.showForm();
                        if (settings.successMsg != null) $.fn.displayMsg("Information!", settings.successMsg, "success");
                        if (settings.resetCall != null) settings.resetCall();
                    }

                    //if (!IsJSON && settings.callback != null) settings.callback(HasError, xhr.responseText);
                    if (settings.callback != null) settings.callback(HasError, xhr.responseText);
                    if (!IsJSON) $.fn.initDefaultTerms();
                }
                else {
                    if (xhr.status == 400)
                        $.fn.displayMsg("ERROR 400 | Bad Request!", "The server could not understand the request.", "error");
                    else if (xhr.status == 401) 
                        $.fn.displayMsg("ERROR 401 | Unauthorized!", "You are not authorized to access the resource.", "error");
                    else if (xhr.status == 403)
                        $.fn.displayMsg("ERROR 403 | Forbidden!", "You are not allowed to perform the operation.", "error");
                    else if (xhr.status == 404)
                        $.fn.displayMsg("ERROR 404 | Not Found!", "Resource is unavailable or expired.", "error");
                    else if (xhr.status == 408)
                        $.fn.displayMsg("ERROR 408 | Request Timeout!", "The request took longer than the server was prepared to wait.", "error");
                    else if (xhr.status == 414)
                        $.fn.displayMsg("ERROR 414 | Request Url Too Long!", "The server will not accept the request, because the url is too long.", "error");
                    else if (xhr.status == 415) 
                        $.fn.displayMsg("ERROR 415 | Unsupported Media!", "The server will not accept the request, due to the mediatype is not supported.", "error");
                    else if (xhr.status == 500)
                        $.fn.displayMsg("ERROR 500 | Internal Server Error!", "The request was not completed. The server met an unexpected condition.", "error");
                    else
                        $.fn.displayMsg("Unexpected Server Error!", "An error occurred during the process. Please try again or contact to your vendor.", "error");
                }
            }

            $.ajax({
                url: settings.action,
                contentType: settings.contentType,
                processData: settings.processData,
                data: settings.data,
                type: settings.method,
                complete: end_PostXHR,
                cache: false
            });

            //==================================================
        }

        postXHR();
    }
})(jQuery);