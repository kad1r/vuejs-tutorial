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
