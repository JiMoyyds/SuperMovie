import {FilmRsp} from "@/scripts/ws/Film/getAllFilm"
import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {searchFilm}

const conn = new WebSocket(`${wsUrlRoot}/search_film`)

type SearchFilmReq =
    {
        FilmTypes: string[]
        FilmOnlineTime: Date
        FilmScheduleStartTime: Date
        FilmNameKeyWord: string
    }

type SearchFilmRsp =
    {
        Collection: FilmRsp[]
    }

async function searchFilm(req: SearchFilmReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <SearchFilmRsp>rspParse(msg)
}