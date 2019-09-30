;

function get(elementId) {
	return document.getElementById(elementId);
}

Array.prototype.insert = function (value) {
	var index = this.indexOf(value);

	if (index > -1) {
		this.splice(index, 1);
	} else {
		this.push(value);
	}
}

Array.prototype.empty = function () {
	this.length = 0;
}

Array.prototype.findbyid = function (id) {
	return this.find(x => x.RowId == id);
}

function unCheck(area) {
	var checkboxes = document.querySelectorAll(area + " input[type=checkbox]");

	for (var i = 0; i < checkboxes.length; i++) {
		checkboxes[i].checked = false;
	}
}

// #region noty

function showNoty(message, type) {
	var n = new Noty({
		text: message, type: type,
		layout: "bottomRight",
		theme: "light",
		timeout: 2500,
		progressBar: true
	}).show();
}

// #endregion

function getRowId(arr) {
	var id = 1;

	if (arr.length > 0) {
		var result = arr.filter(x => x.Status != DataStates.Deleted);
		id = result.length + 1;
	}

	return id;
}

function getTextOfSelectInput(id, value) {
	var field = document.getElementById(id);

	if (typeof value !== "undefined") {
		for (var i = 0; i < field.options.length; i++) {
			if (value == field.options[i].value) {
				return field.options[i].text;
			}
		}
	} else {
		return field.options[field.selectedIndex].text;
	}
}

function isNullOrWhiteSpace(value) {
	return typeof value === "undefined" || typeof value === "" || (value == null || value == "");

	//var isUndefined = typeof value === "undefined";
	//var isValueEmpty = typeof value === "";

	//if (isUndefined || isValueEmpty) {
	//	return true;
	//} else if (value == null || value == "") {
	//	return true;
	//} else {
	//	return false;
	//}
}

function matchRegex(regex, value) {
	var _regex = new RegExp(regex, "gm"),
		result = _regex.exec(value);

	return result != null ? result.length : false;
}

function checkValidation(area) {
	var fields = document.querySelectorAll(area + " .requiredField");

	requiredFieldsByArea.empty();

	for (var i = 0; i < fields.length; i++) {
		checkElementValidation(fields[i]);
	}

	if (requiredFieldsByArea.find(x => x == true)) {
		return false;
	} else {
		return true;
	}
}

function checkElementValidation(element) {
	if (typeof element.target !== "undefined") {
		element = element.target;
	}

	var inputType = element.attributes["type"],
		inputValue = element.value,
		regexPattern = element.attributes["data-val-regex-pattern"];

	if (!isNullOrWhiteSpace(inputValue)) {
		element.classList.remove("requiredFieldError");

		if (!isNullOrWhiteSpace(regexPattern)) {
			var result = matchRegex(regexPattern.value, inputValue);

			if (result) {
				window.isValid = true;
				element.classList.remove("requiredFieldError");
			} else {
				element.classList.add("requiredFieldError");
				requiredFieldsByArea.push(true);
			}
		}
	} else {
		element.classList.add("requiredFieldError");
		requiredFieldsByArea.push(true);
	}
}

function checkSelectedRows(rows) {
	if (rows.length == 1) {
		console.log(selectedRows);
	} else if (rows.length > 1) {
		showNoty("Please select only 1 row for edit.", "warning");
	} else {
		showNoty("Please select a row for edit.", "warning");
	}
}
