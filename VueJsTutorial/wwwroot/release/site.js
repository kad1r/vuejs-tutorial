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

	//Date selector
	flatpickr(".date-control");

	//Adding validation check when input blur
	if (typeof requiredFields !== "undefined") {
		for (var i = 0; i < requiredFields.length; i++) {
			requiredFields[i].addEventListener("blur", checkElementValidation);
		}
	}

	appStorage.init();
}

;

"use strict";

(function () {
	this.Heatmap = function () {

	};

	Heatmap.prototype.initialize = function () {

	};


}());