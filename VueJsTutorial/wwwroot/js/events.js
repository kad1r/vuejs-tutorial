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
