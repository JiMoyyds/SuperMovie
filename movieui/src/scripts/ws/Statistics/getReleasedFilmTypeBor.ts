import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getReleasedFilmTypeBor}
export type {
    FilmTypeBoxOfficeRsp,
    GetReleasedFilmTypeBorReq,
    GetReleasedFilmTypeBorRsp
}

type GetReleasedFilmTypeBorReq =
    {}

type FilmTypeBoxOfficeRsp =
    {
        FilmType: string
        FilmBoxOffice: number
    }

type GetReleasedFilmTypeBorRsp =
    {
        Collection: FilmTypeBoxOfficeRsp[]
    }

async function getReleasedFilmTypeBor(req: GetReleasedFilmTypeBorReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_released_film_type_bor`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_released_film_type_bor req:' + reqJson)
    console.log(reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_released_film_type_bor req:' + msg)

    return <GetReleasedFilmTypeBorRsp>rspParse(msg)
}