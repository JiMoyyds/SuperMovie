import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {addFilm}
export type {
    AddFilmReq,
    AddFilmRsp
}

type  AddFilmReq =
    {
        FilmName: string
        FilmCoverUrl: string
        FilmPreviewVideoUrl: string
        FilmPrice: number
        FilmIsPreorder: boolean
        FilmOnlineTime: Date
        FilmTypes: string[]
        FilmActors: string[]
    }

type AddFilmRsp =
    {
        Ok: boolean
        FilmId: bigint
    }

async function addFilm(req: AddFilmReq) {
    const conn = createWebSocket(`${wsUrlRoot}/add_film`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('add_film req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('add_film rsp:' + msg)

    return <AddFilmRsp>rspParse(msg)
}