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
