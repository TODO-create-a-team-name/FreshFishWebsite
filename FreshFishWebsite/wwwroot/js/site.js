$(document).ready( function () {
    $('.basic-table').DataTable({
        lengthChange: false,
        "info": false,
        "language": {
            "lengthMenu": "Відобразити _MENU_ елементів на сторінці",
            "zeroRecords": "Нічого не знайдено - вибачте",
            "info": "Показана сторінка _PAGE_ з _PAGES_",
            "infoEmpty": "No records available",
            searchPlaceholder: "Пошук",
            search: "_INPUT_",
        }
    });
} );
