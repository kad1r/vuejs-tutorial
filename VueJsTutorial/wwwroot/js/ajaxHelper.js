function $get(url, ref) {
	if (typeof ref !== "undefined") {
		axios.get(url)
			.then(response => {
				ref.result = response.data;
			}).catch(error => console.log(error));
	} else {
		axios.get(url)
			.then(response => {
				return response.data;
			}).catch(error => console.log(error));
	}
}

function $call(url, method, async, responseType, callbackFuncName) {
	var xhr;

	if (window.XMLHttpRequest) {
		xhr = new XMLHttpRequest();
	} else {
		xhr = new ActiveXObject("Microsoft.XMLHTTP");
	}

	xhr.async = async;
	xhr.responseType = responseType;
	xhr.open(method, url);
	xhr.send();

	xhr.onload = function () {
		if (xhr.status != 200) {
			console.log(xhr.status + " > " + xhr.statusText);
		} else {
			return xhr.response;
		}
	};

	xhr.onreadystatechange = function () {
		/*
		UNSENT = 0; // initial state
		OPENED = 1; // open called
		HEADERS_RECEIVED = 2; // response headers received
		LOADING = 3; // response is loading (a data packed is received)
		DONE = 4; // request complete
		*/

		if (xhr.readyState == 4 && xhr.status == 200) {
			window[callbackFuncName](xhr.response);
		}
	};

	xhr.onerror = function () {
		console.log("ajax call error!");
	};

	xhr.onprogress = function (event) {
		if (event.lengthComputable) {
			console.log("ajax call progress: " + event.loaded + " of " + event.total);
		} else {
			console.log("ajax call progress load: " + event.loaded);
		}
	};
}

//async function $fetch(url) {
//	fetch(url)
//		.then(response => { return response.json(); })
//		.catch((e) => { });
//}
