import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllFilmOnlineTime}
export type {
    GetAllFilmOnlineTimeReq,
    GetAllFilmOnlineTimeRsp
}

type GetAllFilmOnlineTimeReq =
    {}

type GetAllFilmOnlineTimeRsp =
    {
        Collection: Date[]
    }

async function getAllFilmOnlineTime(req: GetAllFilmOnlineTimeReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_all_film_online_time`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_all_film_online_time req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_all_film_online_time rsp:' + msg)

    return <GetAllFilmOnlineTimeRsp>rspParse(msg)
}
