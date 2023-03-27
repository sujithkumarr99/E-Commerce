
app.controller('myCtrl', function ($scope, $http, $location) {
   
    $http.get("/api/category")
        .then(function (response) {
            $scope.CategoryList = response.data;
        });
    $scope.Savechanges = function () {
        $http({
            method: 'POST',
            url: "/api/category",
            data: $scope.save

        }).then(function (response) {
            $scope.ResponseText = "Saved";
            $scope.save = null;
        });

    }
    $scope.DeleteData = function (id) {
        $http.delete("/api/category/" + id).then(function (response) {
            alert('Successfully Deleted, Refresh The Page For Update');
        }, function (error) {
                alert('Already Deleted');
        });
    };
});


