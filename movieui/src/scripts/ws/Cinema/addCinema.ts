import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {addCinema}
export type {
    AddCinemaReq,
    AddCinemaRsp
}

type AddCinemaReq =
    {
        CinemaName: string
    }

type AddCinemaRsp =
    {
        Ok: boolean
        CinemaId: bigint
    }


async function addCinema(req: AddCinemaReq) {
    const conn = createWebSocket(`${wsUrlRoot}/add_cinema`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('add_cinema req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('add_cinema rsp:' + msg)

    return <AddCinemaRsp>rspParse(msg)
}