"use strict";

const DataStates = { Added: 1, Updated: 2, Deleted: 3, NoChange: 4 };
const tableFilters = {
	"string": ["Begins with", "Ends with", "Contains", "Doesn't contain", "Equals", "Not equal"],
	"date": ["From", "To"]
};
var selectedObj = {};
var selectedRows = [], selectedSubRows = [], searchArr = [], sortArr = [], requiredFieldsByArea = [];
var isFormValid = false;
var selected = 0;
