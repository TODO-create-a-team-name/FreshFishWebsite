const card = document.querySelectorAll(".product-card"),
    cardTrigger = document.querySelectorAll(".product-card label"),
    cardRegisters = document.querySelector(".product-register");

function addClass(element, className) {
    element.classList.add(className);
}
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

