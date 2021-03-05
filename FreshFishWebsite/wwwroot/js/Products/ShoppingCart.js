const btnShoppingCart = document.querySelector('.shopping-card'),
    btnCloseModal = document.querySelector('.close'),
    shoppingCartModal = document.querySelector('.modal');


btnShoppingCart.addEventListener('click', function (event) {
    event.preventDefault();
    shoppingCartModal.classList.toggle('open');
    btnCloseModal.addEventListener('click', () => { shoppingCartModal.classList.remove('open') });

});