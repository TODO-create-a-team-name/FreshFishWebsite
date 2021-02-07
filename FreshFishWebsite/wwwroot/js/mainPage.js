const menuBtn = document.querySelector(".menu-btn"),
        menuContainer = document.querySelector(".list-container");

menuBtn.checked = false;
menuBtn.addEventListener("click", function(){
    if (menuBtn.checked == true) {
        menuContainer.classList.add("active");
        menuContainer.classList.remove("not-active");     
    }
    else{
        menuContainer.classList.remove("active");
        menuContainer.classList.add("not-active");
    }
});