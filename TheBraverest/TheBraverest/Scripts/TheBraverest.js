var TheBraverest = TheBraverest || {}

const API_PREFIX = '/api/';

function apiPath(controller, actionName) {
    return API_PREFIX + 
        controller + '/' + 
        (actionName ? actionName : '');
}

//API Controllers
TheBraverest.BraveChampionController = 'BraveChampion';

//API Methods
TheBraverest.actionGetBraveChampion = {
    url: apiPath(TheBraverest.BraveChampionController),
    method: 'GET'
};

//Location 
TheBraverest.getLocationBuildIndex = function (version, seed) {

    return 'http://TheBraverest.com/Build?verion=' + version + '&seed=' + seed;
}

TheBraverest.getLocationDownloadJsonFile = function (version, seed) {
    return braveBuildURITemplate(version, seed, 'file');
};

TheBraverest.getLocationDownloadZipFile = function (version, seed) {
    return braveBuildURITemplate(version, seed, 'zip');
};

function braveBuildURITemplate(version, seed, format) {
    return "http://www.thebraverest.com/api/ItemSet/" +
    version + "/" + seed +
        (format ? "?format=" + format + "" : "");
}
