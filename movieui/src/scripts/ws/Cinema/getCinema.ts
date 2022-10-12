import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getCinema}
export type {
    GetCinemaReq,
    GetCinemaRsp
}

type GetCinemaReq =
    {
        CinemaId: bigint
    }

type GetCinemaRsp =
    {
        Ok: boolean
        CinemaId: bigint
        CinemaName: string
    }

async function getCinema(req: GetCinemaReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_cinema`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_cinema req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_cinema rsp:' + msg)

    return <GetCinemaRsp>rspParse(msg)
}
