/*
 * This is for the regular declaring same kind of models.
 * By saying model for example it has id, code, heading, dateinserted, dateupdated, isactive, isdeleted fields.
 * For these kind of dtos we don't need to specify model
 * */

window.vm = new Vue({
	el: "#vue-app",
	name: "productactivityform",
	data: {
		result: []
	},
	methods: {
		handleAdd: function () {
			if (this.handleValidation()) {
				if (selected > 0) {
					showNoty("Record has beed updated.", "success");
				} else {
					var obj = {
						//ActivityType: getTextOfSelectInput("ProductActivityForJson_ActivityTypeId"),
						ActivityTypeId: this.result.productActivityForJson.ActivityTypeId,
						//WareHouse: getTextOfSelectInput("ProductActivityForJson_WareHouseId"),
						WareHouseId: this.result.productActivityForJson.WareHouseId,
						InvoiceNumber: this.result.productActivityForJson.InvoiceNumber,
						ActivityDate: this.result.productActivityForJson.ActivityDate,
						Status: DataStates.Added,
						Show: true,
						RowId: getRowId(this.result.productActivities)
					};

					this.result.productActivities.push(obj);
					showNoty("Record has beed added.", "success");
				}

				this.resetSubList();
				unCheck(".sub_list");
			} else {
				showNoty("There are some validation issues. Please correct them.", "warning");
			}
		},
		handleValidation: function () {
			return checkValidation(".product_activities");
		},
		handleEdit: function () {
			if (selectedRows.length == 1) {
				this.result.productActivityForJson = selectedObj;
				get("add").innerText = "Update Product Detail";
			} else if (selectedRows.length > 1) {
				showNoty("Please select only 1 row for edit.", "warning");
			} else {
				showNoty("Please select a row for edit.", "warning");
			}
		},
		handleChange: function () {
			console.log("Date is changed!");
		},
		handleDelete: function (ids) {
			for (var i = 0; i < ids.length; i++) {
				for (var j = 0; j < this.result.productActivities.length; j++) {
					if (ids[i] == this.result.productActivities[j].RowId) {
						this.result.productActivities[j].Show = false;
						this.result.productActivities[j].Status = DataStates.Deleted;
					}
				}
			}

			this.resetSubList();
		},
		resetSubList() {
			selected = 0;
			selectedObj = {};
			selectedRows.empty();
			this.result.productActivityForJson = Object.assign({}, {});
			get("add").innerText = "Add Product Detail";
		}
	},
	filters: {
		getVisibleDetails: function () {
			return this.result.master.filter(function (elem) { return !elem.show; });
		}
	},
	created() {
		this.result = modelData;
		fetch(modelData.appSettings.applicationUrl + "/data/productactivity.json")
			.then(res => res.json())
			.then(function (response) {
				modelData["model"] = response.model;
				modelData["headers"] = [
					{
						title: "ActivityTypeId",
						type: "string",
						dropdownid: "ProductActivityForJson_ActivityTypeId"
					}, {
						title: "WareHouseId",
						type: "string",
						dropdownid: "ProductActivityForJson_WareHouseId"
					}, {
						title: "InvoiceNumber",
						type: "string"
					}, {
						title: "ActivityDate",
						type: "date"
					}
				];
			});
	}
});
