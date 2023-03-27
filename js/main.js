
app.controller('mainCtrl', function ($scope, $http, $location) {

    $http.get("/api/product")
        .then(function (response) {
            $scope.ProductList = response.data;

        });

});


