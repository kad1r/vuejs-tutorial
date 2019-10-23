Vue.prototype.$clear = obj => {
	return Object.keys(obj).forEach(key => { obj[key] = null; });
};
