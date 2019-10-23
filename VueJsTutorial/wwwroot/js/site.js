window.isValid = false;

var form = document.querySelector("form"),
	toolbarSave = get("toolbar_save"),
	requiredFields = document.getElementsByClassName("requiredField"),
	appStorage = new AppStorage();

if (typeof toolbarSave !== "undefined" && toolbarSave !== null && toolbarSave.length > 0) {
	toolbarSave.addEventListener("click", submitForm);
}

function pageInit() {
	selectedRows.length = 0, selectedSubRows.length = 0, selected = 0;
	selectedObj = {};

	// Adding validation check when input blur
	if (typeof requiredFields !== "undefined") {
		for (var i = 0; i < requiredFields.length; i++) {
			requiredFields[i].addEventListener("blur", checkElementValidation);
		}
	}

	appStorage.init();

	// Date selector
	flatpickr(".date-control");
}
