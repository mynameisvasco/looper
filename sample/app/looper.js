const responseListeners = new Map();

function emitRequest(path, payload) {
    window.external.sendMessage(
        JSON.stringify({
            path,
            payload
        })
    )
}

window.external.receiveMessage((data) => {
    const dataJson = JSON.parse(data);
    const path = dataJson.path;
    const callback = responseListeners.get(path);
    callback(dataJson);
})

function onResponse(path, callback) {
    responseListeners.set(path, callback)
}

const Looper = {
    emitRequest,
    onResponse
}