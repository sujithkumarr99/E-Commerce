
app.controller('productCtrl', function ($scope, $http, $location) {

    $http.get("/api/product")
        .then(function (response) {
            $scope.CategoryList = response.data;
           
        });


    $scope.Savechanges = function () {
        $http({
            method: 'POST',
            url: "/api/product",
            data: $scope.save

        }).then(function (response) {

            $scope.ResponseText = "Saved";
            $scope.save = null;

        });

    }
    $scope.DeleteData = function (id) {
        $http.delete("/api/product/" + id).then(function (response) {
            alert('Successfully Deleted, Refresh The Page For Update');
        }, function (error) {
            alert('Already Deleted');
        });

    };
    $http({
        method: "GET",
        url: "/api/category"
    }).then(function mySuccess(response) {
        $scope.myData = response.data;
    }, function myError(response) {
        $scope.myData = response.statusText;
    });

});


//$scope.update = function () {
//    if ($scope.Product.Name != "" &&
//        $scope.Product.Price != "" && $scope.Product.Category != "") {
//        $http({
//            method: 'PUT',
//            url: 'api/Product/PutProduct/' + $scope.Product.Id,
//            data: $scope.Product
//        }).then(function successCallback(response) {
//            $scope.productsData = response.data;
//            $scope.clear();
//            alert("Product Updated Successfully !!!");
//        }, function errorCallback(response) {
//            alert("Error : " + response.data.ExceptionMessage);
//        });
//    }
//    else {
//        alert('Please Enter All the Values !!');
//    }
//};