;

"use strict";

window.isValid = false;

var form = document.querySelector("form"),
	toolbar_save = get("toolbar_save"),
	requiredFields = document.getElementsByClassName("requiredField");

if (typeof toolbar_save != "undefined" && toolbar_save != null && toolbar_save.length > 0) {
	toolbar_save.addEventListener("click", submitForm);
}

function pageInit() {
	selectedRows.length = 0;
	selectedSubRows.length = 0;
	selected = 0;
	selectedObj = {};
	flatpickr(".date-control");

	for (var i = 0; i < requiredFields.length; i++) {
		requiredFields[i].addEventListener("blur", checkElementValidation);
	}
}