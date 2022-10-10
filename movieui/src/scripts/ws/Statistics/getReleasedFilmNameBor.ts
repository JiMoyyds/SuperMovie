import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getReleasedFilmNameBor}
export type {
    FilmNameBoxOfficeRsp,
    GetReleasedFilmNameBorReq,
    GetReleasedFilmNameBorRsp
}

type GetReleasedFilmNameBorReq =
    {}

type FilmNameBoxOfficeRsp =
    {
        FilmName: string
        FilmBoxOffice: number
    }

type GetReleasedFilmNameBorRsp =
    {
        Collection: FilmNameBoxOfficeRsp[]
    }

async function getReleasedFilmNameBor(req: GetReleasedFilmNameBorReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_released_film_name_bor`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_released_film_name_bor req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_released_film_name_bor req:' + msg)

    return <GetReleasedFilmNameBorRsp>rspParse(msg)
}