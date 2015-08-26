
(function (window, $, TheBraverest) {
    var spinnerHtml = '<div class="throbber-loader">Loading…</div>';
    var responseHtml = $('#response-modal .modal-body').html().toString();
    

    function submitBuildRequest() {
        var actionData = TheBraverest.actionGetBraveChampion;
        return $.ajax({
            url: actionData.url,
            method: actionData.method,
            dataType: 'JSON',
            contentType: "application/json"
        });
    }


    $('#submit-build-request').click(function (e) {
        var replacement = responseHtml;
        $('#response-modal .modal-body').html(spinnerHtml);
        $.when(submitBuildRequest())
        .done(function (response) {
            //TODO: display data from the thingy on the modal.
            var test = response;

        });
    });

})(window, jQuery, TheBraverest);