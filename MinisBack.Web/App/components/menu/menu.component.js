angular.module('minis.app').component('menu', {
    templateUrl: 'App/components/menu/menu.page.html',
    controller: ['$scope', '$http', function ($scope, $http) {
       var $ctrl = this;
       $ctrl.orderItems = {};

       $http.get('api/sandwich/list').then(function(response) {
          $ctrl.sandwiches = response.data;
       });

       var createOrderItem = function (orderItem) {
         var orderSandwich = orderItem.sandwich;
         return {
            sandwichId: orderSandwich.id,
            sandwichPrice: orderSandwich.price,
            quantiy: orderItem.quantity,
            totalPrice: orderItem.total
         };
       }

       var createOrder = function() {
         return {
           price: $ctrl.calculateTotalOrder(),
             orderItems: $.map($ctrl.orderItems, createOrderItem)
         };
       }

       $ctrl.addSandwich = function(sandwichType) {
           if (!$ctrl.orderItems[sandwichType.id]) {
               $ctrl.orderItems[sandwichType.id] = {
                   sandwich: sandwichType,
                   quantity: 0,
                   total: 0,
                   values: []
               };
           }

           $ctrl.orderItems[sandwichType.id].quantity += 1;
           $ctrl.orderItems[sandwichType.id].total += sandwichType.price;
           $ctrl.orderItems[sandwichType.id].values.push(sandwichType);
       }

       $ctrl.lessSandwich = function(sandwichType) {
           if ($ctrl.orderItems[sandwichType.id].values && $ctrl.orderItems[sandwichType.id].values.length > 1) {
               $ctrl.orderItems[sandwichType.id].quantity -= 1;
               $ctrl.orderItems[sandwichType.id].total -= sandwichType.price;
               $ctrl.orderItems[sandwichType.id].values.pop();
           } else if ($ctrl.orderItems[sandwichType.id].values && $ctrl.orderItems[sandwichType.id].values.length === 1) {
               $ctrl.removeOrder($ctrl.orderItems[sandwichType.id]);
           }
       }

       $ctrl.removeOrder = function(orderItem) {
          delete $ctrl.orderItems[orderItem.sandwich.id];
       }

       $ctrl.calculateTotalOrder = function() {
           var sum = 0;
           angular.forEach($ctrl.orderItems, function (value, key) {
               sum = sum + parseInt(value.total, 10);
           });
           return sum;
       }

       $ctrl.placeOrder = function() {
          $http.post('api/order/new', createOrder()).then(function(response) {
            console.log(response.data);
          });
       }
    }]
})