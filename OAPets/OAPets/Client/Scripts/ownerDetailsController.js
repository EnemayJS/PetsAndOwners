(function (app) {
    // OwnerDetailsController - is a controller that bind data for ownerDetails view
    // $scope - adding properties to the $scope object in the controller, the view allOwners(HTML) gets access to these properties
    // ownersService - dependency to ownersService that perform AJAX request to OwnersController.cs
    // petsService - dependency to petsService that perform AJAX request to PetsController.cs
    var OwnerDetailsController = function ($scope, $routeParams, petsService, ownersService) {

        // id of current owner
        var id = $routeParams.id;

        getDetails();
        // paging attributes
        // pagingInfo.page - current page number
        // pagingInfo.itemsPerPage - number of items per one page
        // pagingInfo.totalItems - number of all  owner's pets        
        $scope.pagingInfo = {
            page: 1,
            itemsPerPage: 3,               
            totalItems: 0
        };
        // when called by ui.bootstrap pagination widget 
        // setting pagingInfo.page to clicked page number and refresh data
        $scope.selectPage = function () {                   
            getDetails();
        };
 
        // deleting pet by id when clicking on delete button and refresh data
        $scope.deletePet = function (petId) {
            petsService.delete(petId)
            .then(function (response) {
                getDetails();
            });
        };
        // creating a new pet when clicked on Add button
        $scope.createPet = function () {
            // creating request object to POST to server
            var pet = {
                // id of current owner
                OwnerId: id,
                // name of new pet
                Name: $scope.pet.name
            };
            // request to petsService
            petsService.create(pet)
                .then(function (data) {                  
                    $scope.pet = null;
                    getDetails();
                });
        };

        // getting owners data to $scope via ownersService
        function getDetails() {
            var servCall = ownersService.getById(id, $scope.pagingInfo);

            servCall.then(function (response) {
                // $scope.pets - pets from server for current pagingInfo settings
                $scope.pets = response.data.Pets;
                // current owner name
                $scope.ownerName = response.data.OwnerName;
                // total number of all owner's pets
                $scope.pagingInfo.totalItems = response.data.Count;
            });
        };
    };

    app.controller("OwnerDetailsController", OwnerDetailsController);

} (angular.module("ownersAndPets")));