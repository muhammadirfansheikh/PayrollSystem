
var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope, $http) {
   
    var data = {};

    $scope.LoadTrigger = function () {
        $scope.GetGroup();
 
   
        
    };



    $scope.GetGroup = function () {
        var service = new HrmsSuiteHcmService.HcmService();
        service.getGroup($scope.onGetGroup, null, null);
        $scope.items = data;
    
    };

    $scope.onGetGroup = function (result) {
        var res = jQuery.parseJSON(result);
        data = res;
        $scope.items = data;
    };


    $scope.GetCompany = function () {
     
        var service = new HrmsSuiteHcmService.HcmService();
        service.getCompanyByGroupId(1, $scope.onGetCompany, null, null);
    };


    $scope.onGetCompany = function (result) {
        alert('');
        var res = JSON.parse(result);
        $scope.companies = res;
    };


    $scope.GetCompany = function () {

        var service = new HrmsSuiteHcmService.HcmService();
        service.getCompanyByGroupId(1, $scope.onGetCompany, null, null);
    };


    $scope.onGetCompany = function (result) {
        alert('');
        var res = JSON.parse(result);
        $scope.companies = res;
    };




    $scope.GetEmployeeA = function () {
      
        var service = new HrmsSuiteHcmService.HcmService();
        service.getEmployee('0', '0', '0', '0', '0', '0', '0', '', '', '', $scope.onGetEmployeeA, null, null);

    }

    $scope.onGetEmployeeA =  function (result) {
        var res = jQuery.parseJSON(result);
        $scope.listData = res;
    }



});
