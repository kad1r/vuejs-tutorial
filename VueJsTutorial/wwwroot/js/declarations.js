;

const DataStates = {
	Added: 1,
	Updated: 2,
	Deleted: 3,
	NoChange: 4
};

var selectedObj = {}, selectedRows = [], selectedSubRows = [], searchArr = [], sortArr = [], requiredFieldsByArea = [];
var isFormValid = false;
