var data;
var maxQuantity = 1;
const poolId = $("#poolIdInput").val();
const storageId = $("#storageIdInput").val();
const maxQuantityInput = $("#maxQuantityInput");

$('input[type=radio][name="ProductId"]').change(changeMaxInput);

$.ajax({
    type: "GET",
    url: `GetProductsForPoolData?storageId=${storageId}&poolId=${poolId}`,
    contentType: "application/json",
    dataType: "json",
    success: function (result) {
        maxQuantity = result.maxQuantity;
        data = result.products;
    },
    complete: function () {

        setMaxQuantity(maxQuantity);
    },
    error: function (xhr, status, error) {
        var errorMessage = xhr.status + ': ' + xhr.statusText
        alert('Error - ' + errorMessage);
    }
});

function changeMaxInput() {
    let productId = $('input[name="ProductId"]:checked').val();
    let product = data.find(d => d.id == productId);
    if (product.remainingQuantityKg < maxQuantity) {
        setMaxQuantity(product.remainingQuantityKg)
    } else {
        setMaxQuantity(maxQuantity)
    }
}

function setMaxQuantity(quantity) {
    maxQuantityInput.attr({
        "max": quantity,
    });
}