angular.module('minis.app').component('app', {
    templateUrl: 'App/components/app/app.page.html',
    controller: function () {
        this.user = { name: 'world' };
    }
})