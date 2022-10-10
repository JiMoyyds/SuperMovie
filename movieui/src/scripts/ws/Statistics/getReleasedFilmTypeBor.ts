import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getReleasedFilmTypeBor}

const conn = new WebSocket(`${wsUrlRoot}/get_released_film_type_bor`)

type GetReleasedFilmTypeBorReq =
    {}

type FilmTypeBoxOfficeRsp =
    {
        FilmType: string
        FilmBoxOffice: bigint
    }

type GetReleasedFilmTypeBorRsp =
    {
        Collection: FilmTypeBoxOfficeRsp[]
    }

async function getReleasedFilmTypeBor(req: GetReleasedFilmTypeBorReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <GetReleasedFilmTypeBorRsp>rspParse(msg)
}