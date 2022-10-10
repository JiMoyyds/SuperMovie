import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {updateFilm}

const conn = new WebSocket(`${wsUrlRoot}/update_film`)

type UpdateFilmReq =
    {
        FilmId: bigint
        FilmName: string
        FilmCoverUrl: string
        FilmPreviewVideoUrl: string
        FilmPrice: number
        FilmIsPreorder: boolean
    }

type UpdateFilmRsp =
    {
        Ok: boolean
    }

async function updateFilm(req: UpdateFilmReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <UpdateFilmRsp>rspParse(msg)
}