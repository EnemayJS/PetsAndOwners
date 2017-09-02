(function (app) {
    // this is a factory to define methods for CRUD operations 
    // controllers reference to this service to perform AJAX requests to OwnersController.cs 
    var ownersService = function ($http, ownersApiUrl) {
        // getting all owners 
        // ownersApiUrl - constant path; { params: pagingInfo } - pagination attributes
        var getAll = function (pagingInfo) { return $http.get(ownersApiUrl, { params: pagingInfo }); };
        // id - is ID of current owner; { params: pagingInfo } - pagination attributes
        var getById = function (id, pagingInfo) { return $http.get(ownersApiUrl + id, { params: pagingInfo }); };
        var update = function (owner) { return $http.put(ownersApiUrl + owner.Id, owner); };
        var create = function (owner) { return $http.post(ownersApiUrl, owner); };
        var destroy = function (id) { return $http.delete(ownersApiUrl + id); };
        return {
            getAll: getAll,
            getById: getById,
            update: update,
            create: create,
            delete: destroy
        };
    };
    app.factory("ownersService", ownersService);
}(angular.module("ownersAndPets")))