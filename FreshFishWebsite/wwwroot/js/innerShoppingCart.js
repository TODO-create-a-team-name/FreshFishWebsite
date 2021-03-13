function decrement(id, productname) {
    let val = document.querySelector(`#${productname}`);
    val.stepDown();
    incrementDecrement(id, val.value);
}

function increment(id, productname) {
    let val = document.querySelector(`#${productname}`);
    val.stepUp();
    incrementDecrement(id, val.value);
}

function inputValueChanged(id, productname) {
    let val = document.querySelector(`#${productname}`);
    incrementDecrement(id, val.value);
}

function incrementDecrement(id, value) {
    $.ajax({
        type: "POST",
        url: `/ShoppingCart/IncrementOrDecrementQuantity?id=${id}&quantity=${value}`,
        complete: function () {
            let url = `/ShoppingCart/Index`;
            $("#shoppingCartContentDiv").load(url, function () {
                $("#mainShoppingCartModalDiv").modal("handleUpdate");
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    })
}
