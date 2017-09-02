(function () {
    // declaring app dependencies
    // ownersAndPets -  parameter refers to an HTML element in which the application will run
    // ngRoute - module routes application to different pages without reloading the entire application
    // ui.bootstrap - create dependency for using UI.Bootstrap library https://angular-ui.github.io/bootstrap/
    var app = angular.module("ownersAndPets", ["ngRoute", "ui.bootstrap"]);

    //initializing routeProvider to configure different routes in your application
    //templateUrl - path to view file which will be loaded when navigating to different URLs like "/owners"
    //otherwise - method, which is the default route when none of the others get a match.
    var config = function ($routeProvider) {
        $routeProvider
            .when("/owners",
                 { templateUrl: "/client/views/allOwners.html" })
            .when("/owners/:id",
                 { templateUrl: "/client/views/ownerDetails.html" })
            .otherwise(
                 { redirectTo: "/owners" });
            
    };

    app.config(config);
    // configuring $locationProvider to change hashPrefix http://www.example.com/!#foo become http://www.example.com/#foo
    app.config(['$locationProvider', function ($locationProvider) {
        $locationProvider.hashPrefix("");
    }]);
    //setting constant rouning URLs
    app.constant("ownersApiUrl", "/api/owners/");
    app.constant("petsApiUrl", "/api/pets/");




}());