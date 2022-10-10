import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getFilm}
export type {
    GetFilmReq,
    GetFilmRsp
}

type GetFilmReq =
    {
        FilmId: bigint
    }

type GetFilmRsp =
    {
        Ok: boolean
        FilmName: string
        FilmCoverUrl: string
        FilmPreviewVideoUrl: string
        FilmPrice: number
        FilmIsPreorder: boolean
        FilmOnlineTime: Date
    }

async function getFilm(req: GetFilmReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_film`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_film req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_film rsp:' + msg)

    return <GetFilmRsp>rspParse(msg)
}
