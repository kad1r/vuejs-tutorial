/// <reference path="helper.js" />
"use strict";

window.isValid = false;

var form = document.querySelector("form"),
	toolbar_save = get("toolbar_save"),
	requiredFields = document.getElementsByClassName("requiredField");

if (typeof toolbar_save != "undefined" && toolbar_save != null && toolbar_save.length > 0) {
	toolbar_save.addEventListener("click", submitForm);
}

for (var i = 0; i < requiredFields.length; i++) {
	requiredFields[i].addEventListener("blur", checkElementValidation);
}

function checkFormValidation() {
	for (var i = 0; i < requiredFields.length; i++) {
		checkElementValidation(requiredFields[i]);
	}
}

function checkElementValidation(element) {
	var th = this;

	if (typeof this === "undefined") {
		th = element;
	}

	var inputType = th.attributes["type"],
		inputValue = th.value,
		regexPattern = th.attributes["data-val-regex-pattern"];

	if (!isNullOrWhiteSpace(inputValue)) {
		th.classList.remove("requiredFieldError");

		var result = matchRegex(regexPattern.value, inputValue);

		if (result) {
			window.isValid = true;
			th.classList.remove("requiredFieldError");
		} else {
			th.classList.add("requiredFieldError");
		}
	} else {
		th.classList.add("requiredFieldError");
	}
}

function isNullOrWhiteSpace(value) {
	return typeof value === "" && (value != null || value != "") ? true : false;
}

function matchRegex(regex, value) {
	var _regex = new RegExp(regex, "gm"),
		result = _regex.exec(value);

	return result != null ? result.length : false;
}
