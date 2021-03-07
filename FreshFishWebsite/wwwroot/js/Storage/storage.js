function editStorage(id) {
    const url = `/Storage/Edit?id=${id}`;
    $("#editStorageModal").load(url, function () {
        $("#exampleModalEdit").modal("show");
    });
}

const createStorageButton = document.querySelector("#createStorageButton");
createStorageButton.addEventListener("click", () => {
    const url = `/Storage/Create`;
    $("#createStorageModal").load(url, function () {
        $("#exampleModalCreate").modal("show");
    });
});

