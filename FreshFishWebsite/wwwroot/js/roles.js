﻿const form = document.querySelector('#createRoleForm');

form.addEventListener("submit", (e) => {

    e.preventDefault();

    let roleName = document.querySelector("#roleInput").value;
    $.ajax({
        type: "POST",
        url: `Roles/Create?name=${roleName}`,
        complete: function () {
            window.location.reload();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    })
});

function changeRoles(id) {
    const url = `/Roles/Edit?userId=${id}`;
    $("#changeRoleModal").load(url, function () {
        $("#exampleAddRoleModal").modal("show");
    });
}

