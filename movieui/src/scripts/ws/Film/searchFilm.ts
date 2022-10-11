import {FilmRsp} from "@/scripts/ws/Film/getAllFilm"
import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {searchFilm}
export type {
    SearchFilmReq,
    SearchFilmRsp
}

type SearchFilmReq =
    {
        EnableScheduleSearch: boolean
        FilmTypes: string[]
        FilmOnlineTimeStart: Date
        FilmOnlineTimeEnd: Date
        FilmScheduleTimeStart: Date
        FilmScheduleTimeEnd: Date
        FilmNameKeyWord: string
    }

type SearchFilmRsp =
    {
        Collection: FilmRsp[]
    }

async function searchFilm(req: SearchFilmReq) {
    const conn = createWebSocket(`${wsUrlRoot}/search_film`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('search_film req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('search_film rsp:' + msg)

    return <SearchFilmRsp>rspParse(msg)
}