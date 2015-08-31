
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

    function submitBuildRequest(version, seed) {
        var actionData = TheBraverest.actionGetBraveChampion;

        var url = actionData.url;
        if (version && seed) {
            url = url + '/' + version + '/' + seed;
        }
        return $.ajax({
            url: url,
            method: actionData.method,
            dataType: 'JSON',
            contentType: "application/json"
        });
    }

    function changeExplanationText(text) {
        $('#modal-description').html(text);
    }

    function openModal() {
        $('#button-show-modal').click();
    }

    function getAndDisplayBuild(version, seed) {
        var template = responseHtml;
        $('#template').html(spinnerHtml);

        $.when(submitBuildRequest(version, seed))
        .done(function (response) {
            //Set the template into the modal
            $('#template').html(template);

            $('#build-details').css('visibility', 'visible');

            //Insert champion data into template
            $('#label-champion-name').text(response.Champion.Name);
            $('#image-champion').css('background', 'url(' + response.Champion.ImageUrl + ') no-repeat');

            //Insert Skill data into the template;
            $("#skill-letter").text(response.Skill.Letter);
            $("#skill-letter").attr('title', 'Skill letter: ' + response.Skill.Letter);
            $('#image-skill').attr('src', response.Skill.ImageUrl);
            $('#image-skill').attr('title', 'Max ' + response.Skill.Name + ' first (' + response.Skill.Letter + ')');

            //Insert items into template
            for (var i = 0; i < response.Items.length; i++) {
                var item = response.Items[i];
                var id = '#list-items-1';
                $(id).append(
                    '<li style="padding: 0px;">' +
                    '   <img src="' + item.ImageUrl +
                        '" width="' + thumbnailWidth +
                        '" height="' + thumbnailHeight +
                        '" data-toggle="tooltip' +
                        '" data_placement = "top" ' +
                        'title="' + item.Name + '\nCost: ' + item.Cost +
                        '" class="list-item-close" ' + '/>' +
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
                        '" data-toggle="tooltip' +
                        '" data-placement="right' +
                        '" title="' + item.Name +
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
            $('#link-download-plain').attr('href', TheBraverest.getLocationDownloadPlainText(response.Version, response.Seed));
            $('#text-share-build').val(TheBraverest.getLocationBuildIndex(response.Version, response.Seed));

            $('#link-download-text').on("click", function () {
                changeExplanationText('Copy the file that just downloaded into the following directory:  {Riot Games Directory - default C:\\Riot Games}\\League of Legends\\Config\\Champions\\'
                    +
                    response.Key
                    +
                    '\\Recommended<br/><br/>'
                    +
                    'If you ever want to get rid of your super brave item set just delete the file you just copied.<br /><br />' +
                    '<b>GO FORTH AND BE THE BRAVEREST!</b>');
                openModal();

            });
            $('#link-download-zip').on("click", function () {
                changeExplanationText('Please note this is a totally optional feature.  It will make things easier for you, however it will run a .bat file on your computer.<br/>' +
                    '<b>If you don\'t trust us please don\'t choose this option!</b><br /><br />' +
                    'If you are brave enough to continue, extract all of the files out of the zip file and remember where you put it.<br /><br />' +
                    'Once that is done just run the CopyItemSet.bat file and you should be good to go.<br /><br />' +
                    'If you ever want to get rid of your super brave item set just run the DeleteItemSet.bat file.<br /><br />' +
                    '<b>GO FORTH AND BE THE BRAVEREST!</b>');
                openModal();
            });
            $('#link-download-plain').on("click", function () {
                changeExplanationText('Create a .json file and copy/paste the text that opened in a new window into it and save it. <br /><br />' +
                    'Once you have done that copy the file into the following directory:  {Riot Games Directory - default C:\\Riot Games}\\League of Legends\\Config\\Champions\\'
                    +
                    response.Key
                    +
                    '\\Recommended<br/><br/>'
                    +
                    'If you ever want to get rid of your super brave item set just delete the file you created.<br /><br />' + 
                    '<b>GO FORTH AND BE THE BRAVEREST!</b>');
                openModal();
            });

            $('[data-toggle="tooltip"]').tooltip();
        });
    }

    $('#submit-build-request').click(function (e) {
        getAndDisplayBuild();
    });

    //button-show-modal
    /*
    link-download-text"
    link-download-zip" 
    link-download-text"
    */

})(window, jQuery, TheBraverest);