import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllFilm}
export type {
    FilmRsp,
    GetAllFilmReq,
    GetAllFilmRsp
}

type GetAllFilmReq =
    {}

type FilmRsp =
    {
        FilmId: bigint
        FilmName: string
        FilmCoverUrl: string
        FilmPreviewVideoUrl: string
        FilmPrice: number
        FilmIsPreorder: boolean
        FilmOnlineTime: Date
        FilmTypes: string[]
        FilmActors: string[]
    }

type GetAllFilmRsp =
    {
        Collection: FilmRsp[]
    }

async function getAllFilm(req: GetAllFilmReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_all_film`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_all_film req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_all_film rsp:' + msg)

    return <GetAllFilmRsp>rspParse(msg)
}
