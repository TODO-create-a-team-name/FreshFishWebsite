const menuBtn = document.querySelector(".menu-btn"),
    menuContainer = document.querySelector(".list-container"),
    menuModal = document.querySelector(".body-trigger");
menuBtn.checked = false;
// menu trigger
function menuAction() {
    menuContainer.classList.toggle("active");
    menuModal.classList.toggle("d-none");
    menuContainer.classList[1] !== "active" ? menuContainer.classList.add("not-active"): menuContainer.classList.remove("not-active");
}
menuBtn.addEventListener("click", () => menuAction());
menuModal.addEventListener("click", () => { menuBtn.checked = !menuBtn.checked; menuAction() });
// Product controller
const btnPrevious = document.querySelector(".btn-previous"),
    cardsProduct = document.querySelectorAll(".product-card"),
    countPage = document.querySelector(".products-container-preview-controll h3"),
    btnNext = document.querySelector(".btn-next");

function windowSize() {
    let currentValue, currentWindow;
    if (window.location.pathname == "/ShoppingCart/ShowAllProducts") {
        if (window.innerWidth <= 800) {
            currentValue = cardsProduct.length;
            currentWindow = "Psmall";
        }
        else {
            currentValue = Math.ceil(cardsProduct.length / (window.innerWidth / 280));
            currentWindow = "Pnormal"
        }
    }
    else {
        if (window.innerWidth <= 800) {
            currentValue = Math.ceil(cardsProduct.length / 2);
            currentWindow = "Msmall";
        }
        else {
            currentValue = Math.ceil(cardsProduct.length / (cardsProduct[0].parentNode.clientWidth / 340));
            currentWindow = "Mnormal"
        }
    }
    return { currentValue, currentWindow };
}
//get default values
function getCountPage(currentValue) {
    let dValue = countPage.innerHTML.split("/");
    let text = `${dValue[0]}/${currentValue}`;
    return text;
}
// check changes size
countPage.innerHTML = getCountPage(windowSize().currentValue);
setInterval(() => { countPage.innerHTML = getCountPage(windowSize().currentValue) }, 1000);
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
    moveElement(cardArray, temp, actionWindowSize(windowSize().currentWindow));
}
//get current WindowSize
function actionWindowSize(currentWindow) {
    let mValue = 0;
    var transformValue = "X";
    switch (currentWindow) {
        case "Psmall":
            mValue = window.innerWidth + 10;
            transformValue = "X";;
            break;
        case "Pnormal":
            mValue = (window.innerWidth / 100) * 96;
            transformValue = "X";;
            break;
        case "Msmall": mValue = 232;
            transformValue = "Y";;
            break;
        case "Mnormal":
            mValue = (window.innerWidth / 80) * 67;
            transformValue = "X";
            break;
        default: console.error("Action Error in actionWindowSize function");
            break;
    }
    return [mValue, transformValue];
}

function moveElement(element, temp, mValue) {
    for (let i = 0; i < element.length; i++) {
        element[i].style.transform = `translate${mValue[1]}(${-mValue[0] * temp}px)`;

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
