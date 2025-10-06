// contacts.js - Complete functionality for Contact Manager

$(document).ready(function () {
    initializeTableFunctions();
});

function initializeTableFunctions() {
    // Initialize all fields as disabled
    $('.editable-field').prop('disabled', true);

    // 🔍 Search functionality
    $('#searchInput').on('input', function () {
        const filter = $(this).val().toLowerCase();
        const rows = $('#contactsTable tbody tr');

        rows.each(function () {
            const row = $(this);
            const rowText = row.find('.editable-field').map(function () {
                return $(this).val().toLowerCase();
            }).get().join(' ');

            // Show/hide row based on search filter
            row.toggle(rowText.includes(filter));
        });
    });

    // 🔼🔽 Sort functionality
    $('th[data-column]').on('click', function () {
        const column = $(this).data('column');
        const table = $('#contactsTable');
        const rows = table.find('tbody tr').get();
        const isAsc = !$(this).hasClass('asc');

        // Remove sort classes from all headers
        $('th[data-column]').removeClass('asc desc');

        // Add sort class to current header
        $(this).addClass(isAsc ? 'asc' : 'desc');

        // Sort rows
        rows.sort((a, b) => {
            const aRow = $(a);
            const bRow = $(b);

            const aValue = aRow.find(`[data-field="${column}"]`).val();
            const bValue = bRow.find(`[data-field="${column}"]`).val();

            // Special handling for different data types
            if (column === 'salary') {
                const aNum = parseFloat(aValue) || 0;
                const bNum = parseFloat(bValue) || 0;
                return isAsc ? aNum - bNum : bNum - aNum;
            } else if (column === 'dateOfBirth') {
                const aDate = new Date(aValue);
                const bDate = new Date(bValue);
                return isAsc ? aDate - bDate : bDate - aDate;
            } else if (column === 'married') {
                // Convert boolean-like values for sorting
                const aBool = aValue === 'True' ? 1 : 0;
                const bBool = bValue === 'True' ? 1 : 0;
                return isAsc ? aBool - bBool : bBool - aBool;
            } else {
                // String comparison
                return isAsc ?
                    aValue.localeCompare(bValue) :
                    bValue.localeCompare(aValue);
            }
        });

        // Re-append sorted rows
        const tbody = table.find('tbody');
        tbody.empty().append(rows);
    });

    // ✏️ Edit/Save/Cancel/Delete functionality
    initializeEditHandlers();
}

function initializeEditHandlers() {
    // Edit button click
    $(document).off('click', '.edit-btn').on('click', '.edit-btn', function () {
        var contactId = $(this).data('id');
        var row = $('#contact-' + contactId);

        // Show save/cancel, hide edit/delete
        row.find('.save-btn, .cancel-btn').show();
        row.find('.edit-btn, .delete-btn').hide();

        // Enable editing
        row.find('.editable-field').prop('disabled', false);

        // Store original values
        row.find('.editable-field').each(function () {
            var originalValue = $(this).val();
            $(this).data('original-value', originalValue);
        });
    });

    // Cancel button click
    $(document).off('click', '.cancel-btn').on('click', '.cancel-btn', function () {
        var contactId = $(this).data('id');
        resetRow(contactId);
    });

    // Save button click
    $(document).off('click', '.save-btn').on('click', '.save-btn', function () {
        var contactId = $(this).data('id');
        saveContact(contactId);
    });

    // Delete button click
    $(document).off('click', '.delete-btn').on('click', '.delete-btn', function () {
        var contactId = $(this).data('id');
        deleteContact(contactId);
    });

    // Enter key support in editable fields
    $(document).off('keypress', '.editable-field').on('keypress', '.editable-field', function (e) {
        if (e.which === 13) { // Enter key
            var contactId = $(this).closest('tr').data('id');
            saveContact(contactId);
            return false; // Prevent form submission
        }
    });
}

function resetRow(contactId) {
    var row = $('#contact-' + contactId);

    // Restore original values
    row.find('.editable-field').each(function () {
        var originalValue = $(this).data('original-value');
        $(this).val(originalValue);
    });

    // Reset buttons
    row.find('.save-btn, .cancel-btn').hide();
    row.find('.edit-btn, .delete-btn').show();

    // Disable fields
    row.find('.editable-field').prop('disabled', true);
}

function saveContact(contactId) {
    var row = $('#contact-' + contactId);

    // Validate required fields
    const name = row.find('[data-field="name"]').val();
    if (!name || name.trim() === '') {
        showMessage('Name is required', 'error');
        return;
    }

    // Create form data
    var formData = new FormData();
    formData.append('Id', contactId);
    formData.append('Name', name.trim());
    formData.append('DateOfBirth', row.find('[data-field="dateOfBirth"]').val());
    formData.append('Married', row.find('[data-field="married"]').val());
    formData.append('Phone', row.find('[data-field="phone"]').val());
    formData.append('Salary', row.find('[data-field="salary"]').val());

    // Show loading state
    const saveBtn = row.find('.save-btn');
    saveBtn.html('<span class="spinner-border spinner-border-sm" role="status"></span> Saving...');
    saveBtn.prop('disabled', true);

    // AJAX call to Edit action
    $.ajax({
        url: '/Contact/Edit',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            window.location.href = '/Contact/Index';
        },
        error: function (xhr, status, error) {
            const errorMessage = xhr.responseJSON?.error || xhr.statusText || error;
            showMessage('Error updating contact: ' + errorMessage, 'error');
            saveBtn.html('Save');
            saveBtn.prop('disabled', false);
        }
    });
}

function deleteContact(contactId) {
    if (!confirm('Are you sure you want to delete this contact?')) {
        return;
    }

    var row = $('#contact-' + contactId);

    // AJAX call to Delete action
    $.ajax({
        url: '/Contact/Delete',
        type: 'POST',
        data: { id: contactId },
        success: function (response) {
            showMessage('Contact deleted successfully!', 'success');
            // Remove the row from table with animation
            row.fadeOut(300, function () {
                $(this).remove();
            });
        },
        error: function (xhr, status, error) {
            const errorMessage = xhr.responseJSON?.error || xhr.statusText || error;
            showMessage('Error deleting contact: ' + errorMessage, 'error');
        }
    });
}

function showMessage(message, type) {
    // Remove existing messages
    $('.alert-message').remove();

    // Create message element
    var alertClass = type === 'success' ? 'alert-success' : 'alert-danger';
    var messageHtml = `
        <div class="alert ${alertClass} alert-dismissible fade show alert-message" role="alert">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `;

    // Add message to page
    $('h2').after(messageHtml);

    // Auto-hide after 5 seconds
    setTimeout(function () {
        $('.alert-message').alert('close');
    }, 5000);
}

// Re-initialize when partial view is reloaded (for CSV upload)
$(document).ajaxComplete(function () {
    // Check if contacts table exists and re-initialize
    if ($('#contactsTable').length) {
        setTimeout(initializeTableFunctions, 100);
    }
});