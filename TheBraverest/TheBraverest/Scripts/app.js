
(function (window, $, TheBraverest) {
    var spinnerHtml = '<div class="throbber-loader">Loading…</div>';
    var responseHtml = $('#response-modal .modal-body').html().toString();
    var thumbnailWidth = 64;
    var thumbnailHeight = 64;

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
        var template = responseHtml;
        $('#response-modal .modal-body').html(spinnerHtml);
        $.when(submitBuildRequest())
        .done(function (response) {
            //Set the template into the modal
            $('#response-modal .modal-body').html(template);

            //Insert champion data into template
            $('#label-champion-name').text(response.Champion.Name);
            $('#image-champion').attr('src', response.Champion.ImageUrl);

            //Insert items into template
            for(var i = 0; i < response.Items.length; i++){
                var item = response.Items[i];
                var id = '#list-items-1';
                if (i >= (response.Items.length / 2)) {
                    var id = '#list-items-2';
                }
                $(id).append(
                    '<li>' +
                    '   <img src="'+ item.ImageUrl +'" width="' + thumbnailWidth + '" height="' + thumbnailHeight + '" />' +
                    '   <strong>' + item.Name + '</strong>' +
                    '</li>');
            }
            //Insert summoner spells into template
            for (var i = 0; i < response.SummonerSpells.length; i++) {
                var item = response.SummonerSpells[i];
                $('#list-summoner-spells').append(
                    '<li>' +
                    '   <img src="' + item.ImageUrl + '" width="' + thumbnailWidth + '" height="' + thumbnailHeight + '" />' +
                    '   <strong>' + item.Name + '</strong>' +
                    '</li>');
            }
            
            //Insert masteries into the template
            $('#label-mastery-offense').text("" + response.MasterySummary.Offense);
            $('#label-mastery-defense').text("" + response.MasterySummary.Defense);
            $('#label-mastery-utility').text("" + response.MasterySummary.Utility);


        });
    });

})(window, jQuery, TheBraverest);