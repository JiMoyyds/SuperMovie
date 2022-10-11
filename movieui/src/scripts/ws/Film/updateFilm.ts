import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {updateFilm}
export type {
    UpdateFilmReq,
    UpdateFilmRsp
}

type UpdateFilmReq =
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

type UpdateFilmRsp =
    {
        Ok: boolean
    }

async function updateFilm(req: UpdateFilmReq) {
    const conn = createWebSocket(`${wsUrlRoot}/update_film`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('update_film req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('update_film rsp:' + msg)

    return <UpdateFilmRsp>rspParse(msg)
}