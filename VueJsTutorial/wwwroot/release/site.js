;

HTMLElement.prototype.findParent = function fn(selector) {
	var parent = this.parentNode;

	if (parent && parent.tagName.toLowerCase() != selector) {
		return parent.findParent(selector);
	} else {
		return parent;
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


;

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

;

"use strict";

(function () {
	this.AppStorage = function () {
		this.storage = window.localStorage;
		this.clicks = [];
	}

	AppStorage.prototype.init = function () {
		console.log("localstorage initializing...");

		if (typeof this.storage !== "undefined") {
			console.log("localstorage initialized.");
		} else {
			console.log("localstorage is undefined!");
		}

		initializeEvents();
	}

	AppStorage.prototype.get = function (item) {
		return this.storage.getItem(item);
	}

	AppStorage.prototype.set = function (item, obj) {
		return this.storage.setItem(item, obj);
	}

	AppStorage.prototype.remove = function (item) {
		return this.storage.removeItem(item);
	}

	AppStorage.prototype.clear = function () {
		return this.storage.clear();
	}

	AppStorage.prototype.setClick = function (item, obj) {
		clicks = appStorage.get(item);

		if (typeof clicks !== "undefined" && clicks != null) {
			var _clicks = JSON.parse(clicks);
			_clicks.push(obj);
			appStorage.remove(item);
			appStorage.set(item, JSON.stringify(_clicks));
		} else {
			clicks = [];
			clicks.push(obj);
			appStorage.set(item, JSON.stringify(clicks));
		}

		//TODO: send informations to mongodb
	}

	function initializeEvents() {
		document.body.addEventListener("click", getClicksForHeatMap);
	}

	function getClicksForHeatMap(e) {
		var clickInfo = {
			url: location.href,
			xCoor: e.clientX,
			yCoor: e.clientY,
			date: new Date()
		};

		appStorage.setClick("clicks", clickInfo);
	}
}());

/*
// initialize
var appStorage = {
	self: this,
	storage: window.localStorage,
	init: function () {
		console.log("localstorage initializing");
		if (typeof storage !== "undefined") {
			console.log("localstorage initialized.");
		} else {
			console.log("localstorage is undefined!");
		}
	},
	getItem: function (item) {
		return appStorage.storage.getItem(item);
	},
	setItem: function (item, obj) {
		return appStorage.storage.setItem(item, obj);
	},
	setObj: function (item, obj) {
		var storage = appStorage.getItem(item);
	}
};

*/

;

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

;

"use strict";

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

;

"use strict";

(function () {
	this.Heatmap = function () {

	};

	Heatmap.prototype.initialize = function () {

	};


}());