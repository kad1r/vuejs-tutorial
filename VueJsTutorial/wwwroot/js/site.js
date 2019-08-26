"use strict";

var requiredFields = document.getElementsByClassName("requiredField");

for (var i = 0; i < requiredFields.length; i++) {
	requiredFields[i].addEventListener("blur", checkValidation, true);
}

function checkValidation() {
	window.isValid = false;

	var inputType = this.attributes["type"];
	var inputValue = this.value;
	var regexPattern = this.attributes["data-val-regex-pattern"];

	if (!isNullOrWhiteSpace(inputValue)) {
		var result = matchRegex(regexPattern.value, inputValue);
	}
}

function isNullOrWhiteSpace(value) {
	return typeof value === "" && (value != null || value != "") ? true : false;
}

function matchRegex(regex, value) {
	debugger;
	var regex = new RegExp(regex, "g"),
		reg = new RegExp(/[0-9]/, "g"),
		res = reg.exec(value),
		result = regex.exec(value);

	return result != null ? result.length : false;
}
