const menuBtn = document.querySelector(".menu-btn"),
    menuContainer = document.querySelector(".list-container");

menuBtn.checked = false;
menuBtn.addEventListener("click", function () {
    if (menuBtn.checked == true) {
        menuContainer.classList.add("active");
        menuContainer.classList.remove("not-active");
    }
    else {
        menuContainer.classList.remove("active");
        menuContainer.classList.add("not-active");
    }
});
// Product controller
const btnPrevious = document.querySelector(".btn-previous"),
    cardsProduct = document.querySelectorAll(".product-card"),
    countPage = document.querySelector(".products-container-preview-controll h3"),
    btnNext = document.querySelector(".btn-next");
//get default values
function getCountPage() {
    let dValue = countPage.innerHTML.split("/");
    let currentValue;
    if (window.innerWidth <= 800) {
        currentValue = Math.ceil(cardsProduct.length / 2);
    }
    else {
        currentValue = Math.ceil(cardsProduct.length / 4);
    }
    let text = `${dValue[0]}/${currentValue}`;
    return text;
}
countPage.innerHTML = getCountPage();
setInterval(()=>{countPage.innerHTML = getCountPage()},1000);
//set new values
function setCountPage(count, action) { 
    let value = count.innerHTML.split("/");
    let e = action.target.dataset.trigger;
    switch (e) {
        case "next": next();
            break;
        case "previous": prev();
            break;
        default: console.error(`Action.error ${e}`);
            break;
    }
    function next() {
        if (value[0] !== value[1]) {
            value[0] = parseInt(value[0]) + 1;
        }
    }
    function prev() {
        if (parseInt(value[0]) !== 1) {
            value[0] = parseInt(value[0]) - 1;
        }
    }
    text = `${value[0]}/${value[1]}`;
    count.innerHTML = text;

    return text;
}
//move products card
function moveCard(cardArray, position) {
    let countVal = position.split("/");
    let temp = countVal[0] - 1;
    moveElement(cardArray, temp, getWindowSize());
}

function getWindowSize() {
    let mValue = 0;
    var transformValue = "";
    if (window.innerWidth <= 800) {
        mValue = 232;
        transformValue = "Y";
    }
    else {
        mValue = 540;
        transformValue = "X";
    }
    return [mValue, transformValue];
}

function moveElement(element, temp, mValue) {
    for (let i = 0; i < element.length; i++) {
        element[i].style.transform = `translate${mValue[1]}(${-mValue[0] * temp}%)`;
    }
}
//events trigger
function eventHandler(event) {
    let position = setCountPage(countPage, event);
    moveCard(cardsProduct, position);
}
//events definition
btnPrevious.addEventListener("click", (e) => eventHandler(e));
btnNext.addEventListener("click", (e) => eventHandler(e));
