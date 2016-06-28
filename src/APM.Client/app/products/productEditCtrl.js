(function () {
    "use strict";

    angular
        .module("productManagement")
        .controller("productEditCtrl",
                     productEditCtrl);

    function productEditCtrl(productResource) {
        var vm = this;
        vm.product = {};
        vm.message = '';

        productResource.get({ id: 0 },
            function (data) {
                vm.product = data;
                vm.originalProduct = angular.copy(data);
            }, function (response) {
                if (response.data.exceptionMessage) {
                    vm.message = response.data.exceptionMessage;
                }
            });

        if (vm.product && vm.product.productId) {
            vm.title = "Edit: " + vm.product.productName;
        }
        else {
            vm.title = "New Product";
        }

        vm.submit = function () {
            if (vm.product.productId) {
                vm.product.$update({ id: vm.product.productId },
                    function (data) {
                        vm.message = "... Save Complete";
                    },
                    function (response) {
                        vm.message = response.statusText + "\r\n";
                        if (response.data && response.data.exceptionMessage) {
                            vm.message += response.data.exceptionMessage;
                        }
                        if (response.data && response.data.modelState) {
                            for(var key in response.data.modelState) {
                                if (response.data.modelState.hasOwnProperty(key)) {
                                    vm.message += response.data.modelState[key] + '\r\n';
                                }
                            }
                        }
                    }
                );
            } else {
                vm.product.$save(
                    function (data) {
                        vm.originalProduct = angular.copy(data);
                        vm.message = "... Save Complete";
                    },
                    function (response) {
                        vm.message = response.statusText + "\r\n";
                        if (response.data && response.data.exceptionMessage) {
                            vm.message += response.data.exceptionMessage;
                        }
                        if (response.data && response.data.modelState) {
                            for (var key in response.data.modelState) {
                                if (response.data.modelState.hasOwnProperty(key)) {
                                    vm.message += response.data.modelState[key] + '\r\n';
                                }
                            }
                        }
                    }
                );
            }
        };

        vm.cancel = function (editForm) {
            editForm.$setPristine();
            vm.product = angular.copy(vm.originalProduct);
            vm.message = "";
        };
    }
}());
