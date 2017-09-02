(function (app) {
    // this is a factory to define methods for CRUD operations 
    // controllers reference to this service to perform AJAX requests to PetsController.cs 
    // petsApiUrl - constant path 
    // id - is ID of current pet
    var petsService = function ($http, petsApiUrl) {
        var getAll = function () { return $http.get(petsApiUrl); };
        var getById = function (id) { return $http.get(petsApiUrl + id); };
        var update = function (pet) { return $http.put(petsApiUrl + pet.Id, pet); };
        var create = function (pet) { return $http.post(petsApiUrl, pet); };
        var destroy = function (id) { return $http.delete(petsApiUrl + id); };
        return {
            getAll: getAll,
            getById: getById,
            update: update,
            create: create,
            delete: destroy
        };
    };
    app.factory("petsService", petsService);
}(angular.module("ownersAndPets")))