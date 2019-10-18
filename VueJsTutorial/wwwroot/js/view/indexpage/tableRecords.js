;

window.vm = new Vue({
	el: "#vue-app",
	name: "tablerecords",
	data: {
		result: []
	},
	created() {
		var self = this;

		fetch(localUrl + "/data/tablecomponent.json")
			.then(res => res.json())
			.then(function (response) {
				response["tableFilters"] = tableFilters;
				self.result = response;
			});
	}
});
