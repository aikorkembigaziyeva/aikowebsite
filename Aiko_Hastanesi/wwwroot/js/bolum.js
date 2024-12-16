var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/bolum/getall',
        },
        "columns": [
            {
                data: 'name',
                "width": "20%"
            },
            {
                data: 'numberOfBeds',
                "width": "7.5%"
            },
            {
                data: 'currentCapacity',
                "width": "7.5%"
            },
            {
                data: 'doctor.fullName',
                "width": "10%"
            },
            {
                data: 'id',
                "render": function (data, type, row) {
                    return `
                        <div class="btn-group" role="group">
                            <a href="/bolum/edit/${data}" class="btn btn-primary mx-2">
                                Edit
                            </a>
                            <button class="btn btn-danger delete-btn" data-id="${data}">
                                Delete
                            </button>
                        </div>
                    `;
                },
                "width": "25%"
            }
        ]
    });

    // Event delegation for delete button
    $('#tblData').on('click', '.delete-btn', function () {
        var id = $(this).data('id');
        Delete(`/bolum/delete/${id}`);
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                },
                error: function (xhr, status, error) {
                    toastr.error("An error occurred while deleting the department.");
                }
            });
        }
    });
}