export {
    sendMsg,
    recvMsg,
    rspParse,
    reqStringify
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

function isBigIntKey(key: string) {
    return key === "CinemaId" ||

        key === "FilmId" ||
        key === "FilmId" ||
        key === "FilmId" ||
        key === "FilmId" ||
        key === "FilmId" ||

        key === "UserId" ||

        key === "ScheduleId" ||
        key === "ScheduleFilmId" ||

        key === "OrderId: bigint" ||
        key === "OrderFilmId: bigint" ||
        key === "OrderUserId: bigint" ||
        key === "OrderCinemaId: bigint" ||
        key === "OrderScheduleId: bigint"
}

function reqStringify(req: any) {
    function replacer(key: string, value: any) {
        if (isBigIntKey(key))
            return value.toString()
        else
            return value
    }

    return JSON.stringify(req, replacer)
}

function rspParse(rsp: string) {
    function reviver(key: string, value: any) {
        if (isBigIntKey(key))
            return BigInt(value)
        else
            return value
    }

    return JSON.parse(rsp, reviver)
}