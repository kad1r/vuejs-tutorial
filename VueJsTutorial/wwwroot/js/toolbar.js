var toolbar_view = document.getElementById("toolbar_view");
var toolbar_new = document.getElementById("toolbar_new");
var toolbar_save = document.getElementById("toolbar_save");
var toolbar_edit = document.getElementById("toolbar_edit");
var toolbar_delete = document.getElementById("toolbar_delete");
var toolbar_export = document.getElementById("toolbar_export");

toolbar_save.addEventListener("click", submitForm);
toolbar_edit.addEventListener("click", editForm);
toolbar_delete.addEventListener("click", deleteRecord);

function submitForm(event) {
	var isValid = checkValidation();

	if (isValid) {
		form.submit();
	} else {
		event.preventDefault();
		event.stopImmediatePropagation();
		showNoty("Please fill required fields!", "warning");
	}
}

function editForm() {
}

function deleteRecord() {
}
