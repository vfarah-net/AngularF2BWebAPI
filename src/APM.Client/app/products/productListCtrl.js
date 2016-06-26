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
        $scope.filterSearchTerm = function () {
            // used to use the web api search construct search: {$scope.search_term}
            // This uses ODATA for case insensitive search and an explicit order by price
            // NOTE: The order of the property seems to be important and the fact that it is last
            productResource.query({
                $filter: "substringof(tolower('" + $scope.search_term + "'),tolower(ProductCode)) or substringof(tolower('" + $scope.search_term + "'),tolower(ProductName))",
                $orderby: "Price desc"
            }, function (data) {
                vm.search_term = $scope.search_term;
                vm.products = data;
            });
        }
        $scope.filterSearchTerm();
    }
}());
