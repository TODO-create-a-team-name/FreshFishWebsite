const menuBtn = document.querySelector(".menu-btn"),
        menuContainer = document.querySelector(".list-container");

menuBtn.checked = false;
menuBtn.addEventListener("click", function(){
    if (menuBtn.checked == true) {
        menuContainer.classList.add("active");
    }
    else{
        menuContainer.classList.remove("active");
    }
});