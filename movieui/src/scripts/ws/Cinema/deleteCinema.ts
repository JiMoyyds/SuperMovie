import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {deleteCinema}
export type {
    DeleteCinemaReq,
    DeleteCinemaRsp
}

type DeleteCinemaReq =
    {
        CinemaId: bigint
    }

type DeleteCinemaRsp =
    {
        Ok: boolean
    }


async function deleteCinema(req: DeleteCinemaReq) {
    const conn = createWebSocket(`${wsUrlRoot}/delete_cinema`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('delete_cinema req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('delete_cinema rsp:' + msg)

    return <DeleteCinemaRsp>rspParse(msg)
}