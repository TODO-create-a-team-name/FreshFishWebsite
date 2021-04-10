import loadShoppingCartModal  from './Products/ShoppingCart.js'

const card = document.querySelectorAll(".product-card-content"),
    cardTrigger = document.querySelectorAll(".product-card label"),
    cardRegisters = document.querySelector(".product-register");

var selectedId;

function addClass(element, className) {
    element.classList.add(className);
}
fetchGetData();

function fetchGetData(id) {
    fetch('GetProductsData')
        .then(response => response.json())
        .then(data => setProductData(getData(data, id)))
        .catch(err => console.error(err));
}

function setProductData(data) {
    document.querySelector(".products-container-view-info h2").innerHTML = data.productName;
    document.querySelector(".products-container-view-info p").innerHTML = data.description;
    document.querySelector(".products-details p[name = calories]").innerHTML = `${data.calories} ккал.`
    document.querySelector(".products-details p[name = pricePerKg]").innerHTML = `${data.pricePerKg} грн/кг`;
    document.querySelector(".fish-img").src = `../images/productsImages/${data.image}`;
}

function getData(data, id = data[0].id) {
    selectedId = id;
    let requestedData = data.find(d => d.id == id);
    return requestedData == undefined ? console.error(`Sorry but this ${id} data not found`) : requestedData;
}
//cardTrigger 
cardTrigger.forEach(currentCardTrigger => {
    currentCardTrigger.addEventListener('click', {
        handleEvent() {
            card.forEach(cardAction => {
                cardAction.classList.remove('active');
            });
            addClass(currentCardTrigger.parentElement, 'active');
            fetchGetData(currentCardTrigger.dataset.id)
        }
    });
});

const addToShoppingCartButton = document.querySelector("#addToShoppingCartSelectedProductButton");
addToShoppingCartButton.addEventListener("click", () => {
    addProductToCart( );
});

const addToShoppingCartButtonFromScroll = document.querySelectorAll(".addToCart");
addToShoppingCartButtonFromScroll.forEach((b) => {
b.addEventListener("click", () => {
    addProductToCart(b.dataset.id);
    });
})

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
