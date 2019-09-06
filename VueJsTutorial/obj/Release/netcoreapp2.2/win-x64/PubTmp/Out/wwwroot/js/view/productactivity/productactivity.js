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
				obj[activityFields[i].id] = activityFields[i].value;
				debugger;
				if (activityFields[i].hasAttribute("data-gettext") && activityFields[i].getAttribute("data-gettext") == "True") {
					var name = activityFields[i].name;
					var nameArr = name.split(".");

					name = nameArr[nameArr - 1] + "Heading";
					obj[name] = activityFields[i].text;
				}
			}

			obj["Show"] = true;
			obj["Status"] = "1";
			obj["RowId"] = details.length + 1;
			details.push(obj);
			Vue.set(this.result, "activity_list", details);
		}
	},
	filters: {
		getVisibleDetails: function () {
			return this.result.master.filter(function (elem) { return !elem.show });
		}
	},
	created() {
		this.result = data;
	}
});
