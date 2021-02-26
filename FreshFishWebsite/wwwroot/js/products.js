const card = document.querySelectorAll(".product-card-content"),
    cardTrigger = document.querySelectorAll(".product-card label"),
    cardRegisters = document.querySelector(".product-register");

function addClass(element, className) {
    element.classList.add(className);
}

fetch('GetProductsData')
    .then(response => response.json())
    .then(data => console.log(data));

// get ajax request data
/*function ajaxGetData(url, callback, id) {
    var http = new XMLHttpRequest();
    http.onreadystatechange = function(){
        if (http.readyState == 4 && http.status == 200){
            try {
                var data = function getData(http, id) {
                    var getCurrentData = JSON.parse(http.responseText);
                    for (let i = 0; i < getCurrentData.length; i++) {
                        if (getCurrentData[i].id == id) {
                            return getCurrentData[i];
                        }
                        else{
                            console.error(`Sorry but this ${id} data not found`);
                        }
                    }
                }
            }
            catch (err){
                console.error(`${http.readyState}:  ${err.message} in ${http.responseText}`);
                return;
            }
            console.log(data);
            callback(data);

        }
    }
}
*/

//cardTrigger 
cardTrigger.forEach(currentCardTrigger => {
    currentCardTrigger.addEventListener('click', {
        handleEvent(){
            card.forEach(cardAction => {
                cardAction.classList.remove('active');
            });
            addClass(currentCardTrigger.parentElement, 'active');
            
        }
    });
});




const btnShoppingCart = document.querySelector('.shopping-card'),
    btnCloseModal = document.querySelector('.close'),
    shoppingCartModal= document.querySelector('.modal');


btnShoppingCart.addEventListener('click', function (event) {
    event.preventDefault();
    shoppingCartModal.classList.toggle('open');
    btnCloseModal.addEventListener('click',  ()=> { shoppingCartModal.classList.remove('open')});
    
});