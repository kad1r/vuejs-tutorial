/// <reference path="ajaxhelper.js" />
/// <reference path="helper.js" />

function submitForm(event) {
	var isValid = checkFormValidation();

	if (window.isValid) {
		form.submit();
	} else {
		event.preventDefault();
		event.stopImmediatePropagation();
	}
}
