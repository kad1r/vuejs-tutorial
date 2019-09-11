/*
 * This is for the regular declaring same kind of models.
 * By saying model for example it has id, code, heading, dateinserted, dateupdated, isactive, isdeleted fields.
 * For these kind of dtos we don't need to specify model
 * */

var vm = new Vue({
	el: "#formApp",
	name: "productactivityform",
	data: {
		result: []
	},
	methods: {
		handleSubmit: function () {
			if (!this.handleValidation()) {
				var obj = {
					ActivityType: getTextOfSelectInput("ProductActivityForJson_ActivityTypeId"),
					ActivityTypeId: this.result.productActivityForJson.ActivityTypeId,
					WareHouse: getTextOfSelectInput("ProductActivityForJson_WareHouseId"),
					WareHouseId: this.result.productActivityForJson.WareHouseId,
					InvoiceNumber: this.result.productActivityForJson.InvoiceNumber,
					ActivityDate: this.result.productActivityForJson.Date,
					Status: DataStates.Added,
					Show: true,
					RowId: getRowId(this.result.productActivities)
				};

				this.result.productActivities.push(obj);
				this.$clear(this.result.productActivityForJson);
			} else {
				showNoty("There are some validation issues. Please correct them.", "warning");
			}
		},
		handleSelection: function (activity) {
			selected = activity.RowId;
			selectedObj = activity;
			selectedRows.insert(selected);
		},
		handleEdit: function () {
			if (selectedRows.length == 1) {
			} else if (selectedRows.length > 1) {
				showNoty("Please select only 1 row for edit.", "warning");
			} else {
				showNoty("Please select a row for edit.", "warning");
			}
		},
		handleValidation: function () {
			return checkValidation(".product_activities");
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
