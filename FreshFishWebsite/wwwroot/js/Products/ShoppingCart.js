const btnCloseModal = document.querySelector('.close'),
     shoppingCartModal = document.querySelector('.modal');

const shoppingCartButton = document.querySelector("#shoppingCartButton");

shoppingCartButton.addEventListener('click', function (event) {
    event.preventDefault();

    shoppingCartModal.classList.toggle('open');
    btnCloseModal.addEventListener('click', () => { shoppingCartModal.classList.remove('open') });

    let url = `/ShoppingCart/Index`;
    $("#shoppingCartContentDiv").load(url, function () {
        $("#mainShoppingCartModalDiv").modal("show");
    });
});
