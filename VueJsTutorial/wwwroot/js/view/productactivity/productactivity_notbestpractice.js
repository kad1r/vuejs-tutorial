/*
 * This js file is getting model using by attributes.
 * Best practice is using model for each dto, not to use attributes.
 * */

var vm = new Vue({
	el: "#formApp",
	name: "productactivityform",
	data: {
		result: []
	},
	methods: {
		submit: function () {
			var obj = {},
				activityFields = document.querySelectorAll(".product_activities input, select, checkbox, radio, textarea");

			for (var i = 0; i < activityFields.length; i++) {
				var field = activityFields[i],
					id = field.id,
					idArr = id.split("_");

				if (idArr.length > 0) {
					id = idArr[idArr.length - 1];
				}

				obj[id] = field.value;

				if (field.hasAttribute("data-gettext") && field.getAttribute("data-gettext") == "True") {
					var name = field.name;
					var nameArr = name.split(".");

					name = nameArr[nameArr.length - 1] + "Heading";
					obj[name] = field.options[field.selectedIndex].text;
				}
			}

			obj["Show"] = true;
			obj["Status"] = 1;
			obj["RowId"] = this.result.productActivities.length + 1;
			this.result.productActivities.push(obj);
			this.$clear(this.result.productActivityForJson);
		},
		handleSelection: function (activity) {
			selected = activity.RowId;
			selectedObj = activity;
			selectedRows.insert(selected);
		},
		handleEdit: function() {
			if (selectedRows.length == 1) {

			} else if (selectedRows.length > 1) {
				showNoty("Please select only 1 row for edit.", "warning");
			} else {
				showNoty("Please select a row for edit.", "warning");
			}
		}
	},
	filters: {
		getVisibleDetails: function () {
			return this.result.master.filter(function (elem) { return !elem.show; });
		}
	},
	created() {
		this.result = modelData;
	}
});
