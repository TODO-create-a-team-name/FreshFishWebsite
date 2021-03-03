const form = document.querySelector('#createRoleForm');

form.addEventListener("submit", (e) => {

    e.preventDefault();

    let roleName = document.querySelector("#roleInput").value;
    $.ajax({
        type: "POST",
        url: `Roles/Create?name=${roleName}`,
        complete: function () {
            window.location.href = "Roles/Index";
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    })
});

function changeRoles(id) {
    console.log(id);
    let url = `/Roles/Edit?userId=${id}`;
    $("#changeRoleModal").load(url, function () {
        $("#exampleAddRoleModal").modal("show");
    });
}