const card = document.querySelectorAll(".product-card-content"),
    cardTrigger = document.querySelectorAll(".product-card label"),
    cardRegisters = document.querySelector(".product-register");

function addClass(element, className) {
    element.classList.add(className);
}

// get ajax request data
function ajaxGetData(id) {
    fetch('GetProductsData')
        .then(response => response.json())
        .then(data => setProductData(getData(data, id)))
        .catch(err => console.error(err));
}

function setProductData(data) {
    document.querySelector(".products-container-view-info h2").innerHTML = data.productName;
    document.querySelector(".products-container-view-info p").innerHTML = data.description;
    document.querySelector(".products-details p[name = calories]").innerHTML = `${data.calories} ккал`
    document.querySelector(".products-details p[name = pricePerKg]").innerHTML = `${data.pricePerKg} грн/кг`;
    document.querySelector(".fish-img").src = `../images/productsImages/${data.image}`;
}

function getData(data, id) {
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
            ajaxGetData(currentCardTrigger.dataset.id)
        }
    });
});




const btnShoppingCart = document.querySelector('.shopping-card'),
    btnCloseModal = document.querySelector('.close'),
    shoppingCartModal = document.querySelector('.modal');


btnShoppingCart.addEventListener('click', function (event) {
    event.preventDefault();
    shoppingCartModal.classList.toggle('open');
    btnCloseModal.addEventListener('click', () => { shoppingCartModal.classList.remove('open') });

});