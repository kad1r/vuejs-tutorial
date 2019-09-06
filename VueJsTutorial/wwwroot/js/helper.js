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

function myNoty(message, type) {
	var n = new Noty({
		text: message, type: type,
		layout: "bottomRight",
		container: ".asdasd"
	}).show();
}
