import {enableDebugMode} from "@/scripts/ws/meta"

export {
    sendMsg,
    recvMsg,
    rspParse,
    reqStringify,
    createWebSocket,
    utf8_to_b64,
    b64_to_utf8
}

function createWebSocket(url: string): any {
    if (enableDebugMode) {
        return null
    } else {
        return new WebSocket(url)
    }
}

async function sendMsg(ws: WebSocket, msg: string) {
    if (ws.readyState === WebSocket.OPEN)
        ws.send(msg)
    else
        setTimeout(() => {
            sendMsg(ws, msg)
        }, 8)
}

async function recvMsg(ws: WebSocket) {
    const task = new Promise<string>(resolve => {
        let handler = (ev: MessageEvent) => {
            ws
                .removeEventListener("message", handler)
            resolve(ev.data)
        }
        ws
            .addEventListener("message", handler)
    })
    return await task
}

function isDateKey(key: string) {
    return key.search("Time") !== -1
}

function isBigIntKey(key: string) {
    return key.search("Id") !== -1
}

function reqStringify(req: any) {
    function replacer(key: string, value: any) {
        if (isBigIntKey(key))
            return value.toString()
        if (isDateKey(key))
            return new Date(value).toISOString()

        return value
    }

    return JSON.stringify(req, replacer)
}

function rspParse(rsp: string) {
    function reviver(key: string, value: any) {
        if (isBigIntKey(key))
            return BigInt(value)
        if (isDateKey(key))
            return new Date(value)

        return value
    }

    return JSON.parse(rsp, reviver)
}

function utf8_to_b64(str: string) {
    return window.btoa(unescape(encodeURIComponent(str)))
}

function b64_to_utf8(str: string) {
    return decodeURIComponent(escape(window.atob(str)))
}