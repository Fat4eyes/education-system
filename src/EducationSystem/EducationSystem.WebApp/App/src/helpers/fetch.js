async function fetchHandler(url, options, errorHandler) {
  try {
    console.log(url);
    return fetch(url, options);
  } catch (e) {
    if (errorHandler) {
      errorHandler(e);
    } else {
      console.log(e);
    }
  }
}

export default fetchHandler
