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