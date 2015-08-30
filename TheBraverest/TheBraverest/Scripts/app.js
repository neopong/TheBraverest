
(function (window, $, TheBraverest) {
    var spinnerHtml = '<div class="throbber-loader">Loading…</div>';
    //var responseHtml = $('#response-modal .modal-body').html().toString();

    //Store the template
    var responseHtml = $('#template').html().toString();

    //Hide the template
    $('#template').html('');

    var thumbnailWidth = 64;
    var thumbnailHeight = 64;
    var summonerSpellWidth = 32;
    var summonerSpellHeight = 32;

    function submitBuildRequest() {
        var actionData = TheBraverest.actionGetBraveChampion;
        return $.ajax({
            url: actionData.url,
            method: actionData.method,
            dataType: 'JSON',
            contentType: "application/json"
        });
    }

    function changeExplanationText(text) {
        $('#modal-description').text(text);
    }

    $('#submit-build-request').click(function (e) {
        var template = responseHtml;
        $('#template').html(spinnerHtml);

        $.when(submitBuildRequest())
        .done(function (response) {
            //Set the template into the modal
            $('#template').html(template);

            //Insert champion data into template
            $('#label-champion-name').text(response.Champion.Name);
            $('#image-champion').attr('src', response.Champion.ImageUrl);

            //Insert Skill data into the template;
            $('#label-skill-name').text(response.Skill.Name);
            $('#image-skill').attr('src', response.Skill.ImageUrl);
            $('#image-skill').data('toggle', 'tooltip');
            $('#image-skill').attr('title', response.Skill.Name);

            //Insert items into template
            for(var i = 0; i < response.Items.length; i++){
                var item = response.Items[i];
                var id = '#list-items-1';
                $(id).append(
                    '<li>' +
                    '   <img src="' + item.ImageUrl +
                        '" width="' + thumbnailWidth +
                        '" height="' + thumbnailHeight +
                        '" data-toggle="tooltip" title="' + item.Name + '\nCost: ' + item.Cost +
                        '" class="list-item-close" '+ '/>' +
                    '</li>');
            }

            //Insert summoner spells into template
            for (var i = 0; i < response.SummonerSpells.length; i++) {
                var item = response.SummonerSpells[i];
                $('#list-summoner-spells').append(
                    '<li>' +
                    '   <img src="' + item.ImageUrl +
                        '" width="' + summonerSpellWidth +
                        '" height="' + summonerSpellHeight +
                        '" data-toggle="tooltip" title="' + item.Name +
                        '" class="list-item-close" ' + '/>' +
                    '</li>');
            }
            
            //Insert masteries into the template
            $('#label-mastery-offense').text("" + response.MasterySummary.Offense);
            $('#label-mastery-defense').text("" + response.MasterySummary.Defense);
            $('#label-mastery-utility').text("" + response.MasterySummary.Utility);
            
            //Make sure the links have the correct href.
            $('#link-download-text').attr('href', TheBraverest.getLocationDownloadJsonFile(response.Version, response.Seed));
            $('#link-download-zip').attr('href', TheBraverest.getLocationDownloadZipFile(response.Version, response.Seed));
            $('#text-share-build').val(TheBraverest.getLocationBuildIndex(response.Version, response.Seed));
        });
    });

    //button-show-modal
    /*
    link-download-text"
    link-download-zip" 
    link-download-text"
    */
    $('#link-download-text').click(function () {

    });
    $('#link-download-zip').click(function () {

    });
    $('#link-download-file').click(function () {

    });

})(window, jQuery, TheBraverest);