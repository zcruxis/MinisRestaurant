angular.module('minis.app').config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state('app', {
        url: '/',
        abstract: true,
    });
    $stateProvider.state('customerMenu', {
        url: 'menu',
        component: 'menu',
        parent: 'app'
    });
    $stateProvider.state('cashier', {
        url: 'cashier',
        component: 'cashier',
        parent: 'app'
    });
}]);