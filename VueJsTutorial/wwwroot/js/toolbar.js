;

function submitForm(event) {
	var isValid = checkFormValidation();

	if (window.isValid) {
		form.submit();
	} else {
		event.preventDefault();
		event.stopImmediatePropagation();
	}
}
