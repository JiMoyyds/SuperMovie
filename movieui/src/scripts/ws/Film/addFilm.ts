import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {addFilm}

const conn = new WebSocket(`${wsUrlRoot}/add_film`)

type  AddFilmReq =
    {
        FilmName: string
        FilmCoverUrl: string
        FilmPreviewVideoUrl: string
        FilmPrice: number
        FilmIsPreorder: boolean
        FilmOnlineTime: Date
    }

type AddFilmRsp =
    {
        Ok: boolean
        FilmId: bigint
    }

async function addFilm(req: AddFilmReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <AddFilmRsp>rspParse(msg)
}