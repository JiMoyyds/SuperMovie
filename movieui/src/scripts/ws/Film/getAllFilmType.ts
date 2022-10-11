import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllFilmType}
export type {
    GetAllFilmTypeReq,
    GetAllFilmTypeRsp
}

type GetAllFilmTypeReq =
    {}

type GetAllFilmTypeRsp =
    {
        Collection: string[]
    }

async function getAllFilmType(req: GetAllFilmTypeReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_all_film_type`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_all_film_type req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_all_film_type rsp:' + msg)

    return <GetAllFilmTypeRsp>rspParse(msg)
}
