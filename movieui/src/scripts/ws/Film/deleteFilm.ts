import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {deleteFilm}

const conn = new WebSocket(`${wsUrlRoot}/delete_film`)

type DeleteFilmReq =
    {
        FilmId: bigint
    }

type DeleteFilmRsp =
    {
        Ok: boolean
    }

async function deleteFilm(req: DeleteFilmReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <DeleteFilmRsp>rspParse(msg)
}