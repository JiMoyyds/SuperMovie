import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllCinema}
export type {
    CinemaRsp,
    GetAllCinemaReq,
    GetAllCinemaRsp
}

type GetAllCinemaReq =
    {}

type CinemaRsp = {
    CinemaId: bigint
    CinemaName: string
}
type GetAllCinemaRsp =
    {
        Collection: CinemaRsp[]
    }

async function getAllCinema(req: GetAllCinemaReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_all_cinema`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_all_cinema req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_all_cinema rsp:' + msg)

    return <GetAllCinemaRsp>rspParse(msg)
}
