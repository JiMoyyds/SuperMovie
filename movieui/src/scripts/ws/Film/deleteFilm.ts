import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {deleteFilm}
export type {
    DeleteFilmReq,
    DeleteFilmRsp
}

type DeleteFilmReq =
    {
        FilmId: bigint
    }

type DeleteFilmRsp =
    {
        Ok: boolean
    }

async function deleteFilm(req: DeleteFilmReq) {
    const conn = createWebSocket(`${wsUrlRoot}/delete_film`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('delete_film req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('delete_film rsp:' + msg)

    return <DeleteFilmRsp>rspParse(msg)
}