import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getReleasedFilmNameBor}

const conn = new WebSocket(`${wsUrlRoot}/get_released_film_name_bor`)

type GetReleasedFilmNameBorReq =
    {}

type FilmNameBoxOfficeRsp =
    {
        FilmName: string
        FilmBoxOffice: bigint
    }

type GetReleasedFilmNameBorRsp =
    {
        Collection: FilmNameBoxOfficeRsp[]
    }

async function getReleasedFilmNameBor(req: GetReleasedFilmNameBorReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <GetReleasedFilmNameBorRsp>rspParse(msg)
}