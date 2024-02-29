$(() => { 
    let rowCount = 1;

    $('#add-row').on('click', () => {
        $('#input-rows').append(getRow(rowCount));
        rowCount++;
    })

    $('form').on('click', '.delete', function () {
        const deleted = $(this).data('id')
        //console.log(`deleted: ${deleted}`);
        $(this).closest('.person-row').remove();
        updateRows(deleted)
    })

    const updateRows = (deletedRow) => {
        for (let i = deletedRow + 1; i < rowCount; i++) {

            const values = {
                firstName: $(`#${i} .first-name`).val(),
                lastName: $(`#${i} .last-name`).val(),
                age: $(`#${i} .age`).val()
            };
            $('#input-rows').append(getRow(i - 1, values))
            $(`#${i} .first-name`).closest('.person-row').remove();
        }
        rowCount--;
    }

    const getRow = (rowNumber, person) =>
        `<div id=${rowNumber} class="row person-row" style="margin-bottom: 10px;">
            <div class="col-md-4">
                <input class="form-control first-name" value="${(person != null ? person.firstName : "")}" type="text" name="people[${rowNumber}].firstname" placeholder="First Name" />
            </div>
            <div class="col-md-4">
                <input class="form-control last-name" value="${(person != null ? person.lastName : "")}" type="text" name="people[${rowNumber}].lastname" placeholder="Last Name" />
            </div>
            <div class="col-md-2">
                <input class="form-control age" value="${(person != null ? person.age : "")}" type="text" name="people[${rowNumber}].age" placeholder="Age" />
            </div>
             <div class="col-md-2">
                <button data-id="${rowNumber}" type="button" class="btn btn-danger delete">Delete</button>
            </div>
        </div>`
});
