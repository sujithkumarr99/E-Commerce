
app.controller('priceCtrl', function ($scope, $http, $location) {

    $http.get("/api/price")
        .then(function (response) {
            $scope.priceList = response.data;
        });

    $scope.Savechanges = function () {
        $http({
            method: 'POST',
            url: "/api/price",
            data: $scope.save

        }).then(function (response) {
            $scope.ResponseText = "Saved";
            $scope.save = null;
        });
    }
    $scope.DeleteData = function (id) {
        $http.delete("/api/price/" + id).then(function (response) {
            alert('Successfully Deleted, Refresh The Page For Update');
        }, function (error) {
            alert('Already Deleted');
        });
    };
    $http({
        method: "GET",
        url: "/api/product"
    }).then(function mySuccess(response) {
        $scope.myData = response.data;
    }, function myError(response) {
        $scope.myData = response.statusText;
    });
});


