(function (app) {
    // AllOwnersController - is a controller that bind data for allOwners view
    // $scope - adding properties to the $scope object in the controller, the view allOwners(HTML) gets access to these properties
    // ownersService - dependency to ownersService that perform AJAX request to OwnersController.cs
    var AllOwnersController = function ($scope, ownersService) {


        getOwners();
        // paging attributes
        // pagingInfo.page - current page number
        // pagingInfo.itemsPerPage - number of items per one page
        // pagingInfo.totalItems - number of all owners
        $scope.pagingInfo = {
            page: 1,
            itemsPerPage: 3,                       
            totalItems: 0
        };
        // when called by ui.bootstrap pagination widget 
        // setting pagingInfo.page to clicked page number and refresh data
        $scope.selectPage = function () {                   
            getOwners();            
        };
 
        

        // deleting owner by id when clicking on delete button and refresh data
       
        $scope.deleteOwner = function (ownerId) {
            ownersService.delete(ownerId)
            .then(function (response) {
                getOwners();
            });
        };

        // creating a new owner when clicked on Add button
        $scope.createOwner = function () {
            // creating request object to POST to server
            var owner = {
                // $scope.owner.name - data from form input 
                Name: $scope.owner.name
            };         
   
            ownersService.create(owner)
                .then(function (data) {                    
                    $scope.owner = null;
                    getOwners();
                });
        };

        
        // getting owners data to $scope via ownersService
        
        function getOwners() {
                      
            var servCall = ownersService.getAll($scope.pagingInfo);           
            servCall.then(function (response) {
                // $scope.owners - owners from server for current pagingInfo settings
                $scope.owners = response.data.owners;
                // $scope.pagingInfo.totalItems total number of owners
                      $scope.pagingInfo.totalItems = response.data.count;
                       });
        };           
    };
    

    app.controller("AllOwnersController", AllOwnersController);

}(angular.module("ownersAndPets")));
