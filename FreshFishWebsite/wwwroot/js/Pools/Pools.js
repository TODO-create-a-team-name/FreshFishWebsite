const feedBtn  = document.querySelectorAll("#feed");
/*
poolBtn.forEach(current  => {
    current.addEventListener("click", (e) =>{
        console.info(e);
    })
})*/

function addProductToCart(id) {
    $.ajax({
        type: "POST",
        url: `/ShoppingCart/AddToCart/${id}`,
        complete: function () {
            loadShoppingCartModal();
        },
        error: function (errormessage) {
            console.log(errormessage.status);
            alert(errormessage.responseText);
        }
    })
}

feedBtn.addEventListener('click', function (event) {
    event.preventDefault();

    shoppingCartModal.classList.toggle('open');
    btnCloseModal.addEventListener('click', () => { shoppingCartModal.classList.remove('open') });

    let url = `/Pool/Index`;
    $("#poolChartDiv").load(url, function () {
        $("#poolChartModalDiv").modal("show");
    });
});