;
Vue.component("otable", {
	props: {
		title: { type: String },
		list: {
			required: true, default() {
				return '[]'
			}
		},
		headers: { required: true },
		headerFilters: { required: true }
	},
	data: function () {
		return {
			compList: this.list
		};
	},
	methods: {
		openSearchBar: function (event) {
			var clickedElement = event.target,
				ref = this.$refs.searchDropDown,
				headFilters = document.querySelector("thead tr.table-filtering");

			if (!isNullOrUndefined(clickedElement) && ref.contains(clickedElement)) {
				var column = clickedElement.attributes["data-column"].value,
					searchInput = document.querySelector(".search-input[data-column='" + column + "']"),
					dropdown = clickedElement.closest("td").querySelector(".grid-filter-dropdown");

				if (!isNullOrUndefined(headFilters) && !isNullOrUndefined(searchInput) && !isNullOrUndefined(dropdown)) {
					if (headFilters.classList.contains("hide")) {
						headFilters.removeClass("hide");
						searchInput.focus();
					} else {
						this.closeOtherDropDowns(dropdown);

						if (dropdown.classList.contains("closed") && dropdown.classList.contains("hide")) {
							dropdown.removeClass("closed", "hide").addClass("opened");
						} else {
							dropdown.removeClass("opened").addClass("hide", "closed");
						}
					}
				}
			}
		},
		getSearchValues: function (event) {
			if (event.keyCode == 13) {
				this.generateSearchKeys(this.headers);
			}
		},
		generateSearchKeys: function (searchValues) {
			searchValues = searchValues.filter(x => { return x.value != "" });
			this.doSearch(searchValues);
		},
		handleFilter: function (event) {
			var filterRule = event.target.innerText,
				closestFilter = event.target.closest("td.position-relative").findChild("i.fa-filter"),
				dataFilter = closestFilter.attributes["data-column"].value,
				filterHeader = this.headers.filter(x => { return x.column == dataFilter });

			if (!isNullOrUndefined(filterHeader)) {
				filterHeader[0].filterRule = filterRule;
				document.body.click();
				this.generateSearchKeys(this.headers);
			}
		},
		doSearch: function (searchValues) {
			var self = this;

			if (searchValues.length > 0) {
				fetch(localUrl + "/data/tablecomponent.json")
					.then(res => res.json())
					.then(function (response) {
						debugger
						for (searchValue of searchValues) {
							response.model = response.model.filter(x => {
								switch (searchValue.filterRule) {
									case "Begins with": {
										return x[searchValue.column].startsWith(searchValue.value)
									}
									case "Ends with": {
										return x[searchValue.column].endsWith(searchValue.value)
									}
									case "Contains": {
										return x[searchValue.column].indexOf(searchValue.value) > -1
									}
									case "Doesn't contain": {
										return x[searchValue.column].indexOf(searchValue.value) === -1
									}
									case "Equals": {
										return x[searchValue.column] === searchValue.value
									}
									case "Not equal": {
										return x[searchValue.column] !== searchValue.value
									}
									default: {
										return x[searchValue.column].indexOf(searchValue.value) > -1
									}
								}
							});
						}

						if (response.model.length > 0) {
							self.compList = response.model;
						} else {
							self.compList = [];
						}
					});
			}
		},
		handleSorting: function (event) {
			var element = event.target,
				dataFilter = element.attributes["data-column"].value,
				filterHeader = this.headers.filter(x => { return x.column == dataFilter }),
				sortValues = [];

			if (!isNullOrUndefined(filterHeader)) {
				if (filterHeader[0].orderby === "" || filterHeader[0].orderby === "desc") {
					filterHeader[0].orderby = "asc";
				} else if (filterHeader[0].orderby === "asc") {
					filterHeader[0].orderby = "desc";
				}
			}

			sortValues = filterHeader.filter(x => { return x.orderby != "" });
			this.doSort(sortValues);
		},
		doSort: function (sortValues) {
			var self = this;

			if (sortValues.length > 0) {
				fetch(localUrl + "/data/tablecomponent.json")
					.then(res => res.json())
					.then(function (response) {
						var sortValue = sortValues[sortValues.length - 1];

						if (response.model.length > 0) {
							self.compList = response.model.sort(sortByColumn(sortValue.column, sortValue.orderby));
						} else {
							self.compList = [];
						}
					});
			}
		},
		getDateValue: function (event) {
			var target = event.target,
				classList = target.classList,
				value = target.value,
				column = target.attributes["data-column"].value,
				searchInput = document.querySelector(".search-input[data-column='" + column + "']"),
				searchValues = searchInput.value.split(":"),
				filterHeader = this.headers.filter(x => { return x.column == column });

			if (searchInput) {
				if (classList.contains("from")) {
					searchValues[0] = value;
				} else {
					searchValues[1] = value;
				}

				searchInput.value = searchValues[0] + (typeof searchValues[1] !== "undefined" ? " : " + searchValues[1] : "");
				debugger;
				filterHeader[0].filterRule = searchInput.value;
			}
		},
		closeOtherDropDowns: function (dropdown) {
			var specifiedElements = document.querySelectorAll(".dropdown-content");

			specifiedElements.forEach(function (e) {
				if (e.parentNode !== dropdown && e.parentNode.classList.contains("opened")) {
					e.parentNode.removeClass("opened").addClass("hide", "closed");
				}
			});
		}
	},
	template: `
		<div class="table-responsive">
			<h3 class="table-title" v-if="title.length">{{title}}</h3>
			<table class="table table-bordered table-hover table-records">
				<thead>
					<tr class="hide table-filtering">
						<td v-for="header in headers" v-if="!header.show"></td>
						<td v-else><input type="text" v-model="header.value" v-on:keyup.enter.prevent="getSearchValues" class="form-control search-input" data-orderby="asc" data-enable="true" data-search-enable="true" data-sort-enable="true" v-bind:data-inputtype="header.inputtype" v-bind:data-type="header.type" v-bind:data-column="header.column" /></td>
					</tr>
					<tr ref="searchDropDown">
						<td v-for="header in headers" v-if="!header.show"><input type="checkbox" /></td>
						<td class="position-relative" v-else>{{header.title}} 
							<i v-bind:class="{'fa fa-sort' : header.orderby == '', 'fa fa-sort-asc' : header.orderby == 'asc', 'fa fa-sort-desc' : header.orderby == 'desc'}" v-on:click.prevent="handleSorting" v-bind:data-column="header.column"></i>
							<i v-on:click.prevent="openSearchBar" v-bind:class="{'primary-color' : header.filterRule != ''}" class="fa fa-filter" v-bind:data-column="header.column" v-bind:data-type="header.type"></i>
							<div class="grid-filter-dropdown hide closed">
								<ul v-if="header.type == 'string'" class="dropdown-content">
									<li v-for="tableFilter in headerFilters.string" v-bind:class="{'primary-color' : header.filterRule == tableFilter}" v-on:click.prevent="handleFilter" v-bind:data-value="tableFilter">{{tableFilter}} <i v-if="header.filterRule == tableFilter" class="fa fa-check"></i></li>
								</ul>
								<ul v-if="header.type == 'date'" class="dropdown-content">
									<li>From: <input type="text" v-on:change="getDateValue" v-bind:data-column="header.column" class="form-control date-control for-header from"></li>
									<li>To: <input type="text" v-on:change="getDateValue" v-bind:data-column="header.column" class="form-control date-control for-header to"></li>
								</ul>
								<ul v-if="header.type == 'enum-activepassive'" class="dropdown-content">
									<li>Active</li>
									<li>Passive</li>
								</ul>
								<ul v-if="header.type == 'enum-yesno'" class="dropdown-content">
									<li>Yes</li>
									<li>No</li>
								</ul>
							</div>
						</td>
					</tr>
				</thead>
				<tbody>
					<tr v-for="column in compList" v-bind:class="{'tbl-grey-bg' : !column.IsActive}">
						<td><input type="checkbox" v-bind:id="column.Id" v-bind:value="column.Id"/></td>
						<td v-for="header in headers" v-if="header.show">{{column[header.column]}}</td>
					</tr>
				</tbody>
				<tfoot>
					<tr>
						<td colspan="100%">
							Paging
						</td>
					</tr>
				</tfoot>
			</table>
		</div>
	`
});
