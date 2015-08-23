var TheBraverest = TheBraverest || {}

const API_PREFIX = 'api/';

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

