
(function (window, $, TheBraverest) {
    
    function submitBuildRequest() {
        return $.ajax({
            url: TheBraverest.actionGetBraveChampion,
            method: 'GET',
            dataType: 'JSON',
            contentType: "application/json"
        });
    }


    $('#submit-build-request').click(function (e) {
        var test = e;
        
        $.when(submitBuildRequest())
        .done(function (response) {
            //TODO: display data from the thingy on the modal.
        });
    });

})(window, jQuery, TheBraverest);