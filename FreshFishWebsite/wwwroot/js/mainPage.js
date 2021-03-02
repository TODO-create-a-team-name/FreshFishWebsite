const menuBtn = document.querySelector(".menu-btn"),
    menuContainer = document.querySelector(".list-container"),
    menuModal = document.querySelector(".body-trigger");
menuBtn.checked = false;
// menu trigger
function menuAction() {
    menuContainer.classList.toggle("active");
    menuModal.classList.toggle("d-none");
    menuContainer.classList[1] !== "active" ? menuContainer.classList.add("not-active") : menuContainer.classList.remove("not-active");
}
menuBtn.addEventListener("click", () => menuAction());
menuModal.addEventListener("click", () => { menuBtn.checked = !menuBtn.checked; menuAction() });

