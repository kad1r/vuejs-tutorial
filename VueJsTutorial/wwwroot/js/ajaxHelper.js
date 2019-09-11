;
"use strict"

function $get(ref, url) {
	axios.get(url)
		.then(response => {
			//ref.result = response.data.result
			ref.result = response.data
		})
		.catch(error => console.log(error));
}

function ajaxcall(url, method) {
	var request = new httpRequest();

	request.method = method;
	request.url = url;

	request.success = function (response) {
		return JSON.parse(JSON.stringify(response))
	};

	request.fail = function (error) {
		console.log(error);
	};

	request.send();
};
