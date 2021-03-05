const card = document.querySelectorAll(".product-card-content"),
    cardTrigger = document.querySelectorAll(".product-card label"),
    cardRegisters = document.querySelector(".product-register");

    var selectedId;

function addClass(element, className) {
    element.classList.add(className);
}
fetchGetData();
// get ajax request data
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

const shoppingCartButton = document.querySelector("#addToShoppingCartSelectedProductButton");
shoppingCartButton.addEventListener("click", () => {

    $.ajax({
        type: "POST",
        url: `/ShoppingCart/AddToCart/${selectedId}`,
        complete: function (res) {
            //if (res.status === 401) window.location.href = "/Account/Login";
            window.location.href = "/ShoppingCart/ShowAllProducts";
        },
        error: function (errormessage) {
            console.log(errormessage.status);
            alert(errormessage.responseText);
        }
    })
})

