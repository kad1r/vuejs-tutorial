;

Vue.component("vue-list", {
	props: {
		data: { required: true },
		title: { type: String, required: true },
		headers: { required: true }
	},
	methods: {
		handleSelection: function (activity) {
			selected = activity.RowId;
			selectedObj = activity;
			selectedRows.insert(selected);
		},
		deleteRow: function () {
			if (selectedRows.length > 0) {
				this.$emit('deleterow', selectedRows);
			} else {
				showNoty("Please select a row for deletion.", "warning");
			}
		},
		rowEdit: function () {
			if (selectedRows.length == 1) {
				this.$emit('editrow', selectedObj);
			} else if (selectedRows.length > 1) {
				showNoty("Please select only 1 row for edit.", "warning");
			} else {
				showNoty("Please select a row for edit.", "warning");
			}
		},
		getTextOfField: function (id, value) {
			return getTextOfSelectInput(id, value);
		},
		getFilteredData: function (data) {
			if (typeof data !== "undefined" && data.length > 0) {
				return data.find(x => { return x.Show == true && x.Status != DataStates.Deleted });
			}
		}
	},
	template: `
		<div class="table-responsive" v-if="getFilteredData(data)">
			<h6>{{title}}</h6>
			<table class="table sub_list">
				<thead>
					<tr>
						<td colspan="100%">
							<a v-on:click.prevent="rowEdit" href="javascript:void(0);" class="edit-list-item"><i class="fa fa-pencil"></i> Edit</a>
							<a v-on:click.prevent="deleteRow" href="javascript:void(0);" class="delete-list-item"><i class="fa fa-trash"></i> Delete</a>
						</td>
					</tr>
					<tr>
						<td></td>
						<td v-for="head in headers">{{head.title}}</td>
					</tr>
				</thead>
				<tbody>
					<tr v-for="obj in data" v-if="obj.Show">
						<td><input type="checkbox" class="" value="obj.RowId" v-on:change="handleSelection(obj)" /></td>
						<td v-for="head in headers">
							<span v-if="typeof head.dropdownid !== 'undefined'">{{getTextOfField(head.dropdownid, obj[head.title])}}</span>
							<span v-else>{{obj[head.title]}}</span>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
	`
});
