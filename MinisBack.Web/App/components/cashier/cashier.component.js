angular.module('minis.app').component('cashier', {
    templateUrl: 'App/components/cashier/cashier.page.html',
    controller: ['$http', function($http) {
        var $ctrl = this;

        $http.get('api/order/search').then(function(response) {
           $ctrl.orders = response.data;
        });


        $ctrl.seeDetail = function(order) {
           $ctrl.currentOrder = order;
        }
    }]
})