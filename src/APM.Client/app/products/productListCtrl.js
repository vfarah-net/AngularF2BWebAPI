(function () {
    "use strict";
    angular
        .module("productManagement")
        .controller("productListCtrl",
                    ["$scope", "$location", "productResource", productListCtrl]);

    function productListCtrl($scope, $location, productResource) {        
        var vm = this;
        //example if this was used http://localhost:53689/index.html#?searchTerm=0035 to extract the searchTerm from the url
        var searchTerm = $location.search().searchTerm;
        $scope.search_term = searchTerm;
        $scope.productResource = productResource;
        $scope.filterSearchTerm = function() {
            productResource.query({ search: $scope.search_term }, function (data) {
                vm.search_term = $scope.search_term;
                vm.products = data;
            });
        }
        $scope.filterSearchTerm();
    }
}());
