import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllFilm}
export type {FilmRsp}

const conn = new WebSocket(`${wsUrlRoot}/get_all_film`)

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
    }

type GetAllFilmRsp =
    {
        Collection: FilmRsp[]
    }

async function getAllFilm(req: GetAllFilmReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <GetAllFilmRsp>rspParse(msg)
}
