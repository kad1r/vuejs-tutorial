"use strict";

const DataStates = { Added: 1, Updated: 2, Deleted: 3, NoChange: 4 };
const tableFilters = {
	"string": ["Begins with", "Ends with", "Contains", "Doesn't contain", "Equals", "Not equal"],
	"date": ["From", "To"]
};
var selectedObj = {};
var selectedRows = [], selectedSubRows = [], searchArr = [], sortArr = [], requiredFieldsByArea = [];
var isFormValid = false;
var selected = 0;

HTMLElement.prototype.findParent = function fn(selector) {
	var parent = this.parentNode;

	if (parent && parent.tagName.toLowerCase() != selector) {
		return parent.findParent(selector);
	} else {
		return parent;
	}
}

HTMLElement.prototype.findChild = function (selector) {
	// check selector if actual element or just a selector
	var element = this.querySelectorAll(selector);

	if (element.length > 0 && element.length == 1) {
		return element[0];
	} else {
		return this.querySelectorAll(selector);
	}
}

HTMLElement.prototype.addClass = function () {
	for (var arg of arguments) {
		if (!this.classList.contains(arg)) {
			this.classList.add(arg);
		}
	}

	return this;
}

HTMLElement.prototype.removeClass = function () {
	for (var arg of arguments) {
		if (this.classList.contains(arg)) {
			this.classList.remove(arg);
		}
	}

	return this;
}

String.prototype.turkishToUpper = function () {
	var string = this;
	var letters = { "i": "İ", "ş": "Ş", "ğ": "Ğ", "ü": "Ü", "ö": "Ö", "ç": "Ç", "ı": "I" };
	string = string.replace(/(([iışğüçö]))/g, function (letter) { return letters[letter]; })
	return string.toUpperCase();
}

String.prototype.turkishToLower = function () {
	var string = this;
	var letters = { "İ": "i", "I": "ı", "Ş": "ş", "Ğ": "ğ", "Ü": "ü", "Ö": "ö", "Ç": "ç" };
	string = string.replace(/(([İIŞĞÜÇÖ]))/g, function (letter) { return letters[letter]; })
	return string.toLowerCase();
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

function get(elementId) {
	return document.getElementById(elementId);
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

function isNullOrUndefined(element) {
	return !(typeof element !== "undefined" && element != null);
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
	var fields;

	requiredFieldsByArea.empty();

	if (typeof area !== "undefined") {
		fields = document.querySelectorAll(area + " .requiredField");
	} else {
		fields = document.querySelectorAll(".requiredField");
	}

	for (var i = 0; i < fields.length; i++) {
		checkElementValidation(fields[i]);
	}

	if (requiredFieldsByArea.find(x => x == true)) {
		return false;
	} else {
		isFormValid = true;
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

function sortByColumn(property, sortway) {
	var sortOrder = 1;

	if (sortway === "desc") {
		sortOrder = -1;
	}

	return function (a, b) {
		var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;

		return result * sortOrder;
	}
}

function $get(url, ref) {
	if (typeof ref !== "undefined") {
		axios.get(url)
			.then(response => {
				ref.result = response.data;
			}).catch(error => console.log(error));
	} else {
		axios.get(url)
			.then(response => {
				return response.data;
			}).catch(error => console.log(error));
	}
}

function $call(url, method, async, responseType, callbackFuncName) {
	var xhr;

	if (window.XMLHttpRequest) {
		xhr = new XMLHttpRequest();
	} else {
		xhr = new ActiveXObject("Microsoft.XMLHTTP");
	}

	xhr.async = async;
	xhr.responseType = responseType;
	xhr.open(method, url);
	xhr.send();

	xhr.onload = function () {
		if (xhr.status != 200) {
			console.log(xhr.status + " > " + xhr.statusText);
		} else {
			return xhr.response;
		}
	};

	xhr.onreadystatechange = function () {
		/*
		UNSENT = 0; // initial state
		OPENED = 1; // open called
		HEADERS_RECEIVED = 2; // response headers received
		LOADING = 3; // response is loading (a data packed is received)
		DONE = 4; // request complete
		*/

		if (xhr.readyState == 4 && xhr.status == 200) {
			window[callbackFuncName](xhr.response);
		}
	};

	xhr.onerror = function () {
		console.log("ajax call error!");
	};

	xhr.onprogress = function (event) {
		if (event.lengthComputable) {
			console.log("ajax call progress: " + event.loaded + " of " + event.total);
		} else {
			console.log("ajax call progress load: " + event.loaded);
		}
	};
}

//async function $fetch(url) {
//	fetch(url)
//		.then(response => { return response.json(); })
//		.catch((e) => { });
//}

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

/*
var validationMixin = window.vuelidate.validationMixin;
Vue.use(window.vuelidate.default);
//Vue.use(validationMixin);
*/

window.onload = function () {
	window.tableGridFilters = document.querySelectorAll(".dropdown-content");

	pageInit();
	
	if (!this.isNullOrUndefined(tableGridFilters)) {
		document.addEventListener("click", closeTableGridFilters);
	}
}

function closeTableGridFilters(event) {
	// hide dropdown from grid tables if click to outside of the unrelated element
	var expectedElements = document.querySelectorAll("table .fa-filter"),
		isInSpecifiedElements = false, isInExpectedElements = false;

	tableGridFilters.forEach(function (e) {
		if (e.contains(event.target)) {
			isInSpecifiedElements = true;
		}
	});

	expectedElements.forEach(function (e) {
		if (e.contains(event.target)) {
			isInExpectedElements = true;
		}
	});

	if (!isInSpecifiedElements && !isInExpectedElements) {
		tableGridFilters.forEach(function (e) {
			if (e.parentNode.classList.contains("opened")) {
				e.parentNode.removeClass("opened").addClass("hide", "closed");
			}
		});
	}
}

(function () {
	this.Heatmap = function () {
	};

	Heatmap.prototype.initialize = function () {
	};
}());