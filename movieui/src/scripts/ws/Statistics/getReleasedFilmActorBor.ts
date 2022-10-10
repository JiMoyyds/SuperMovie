import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getReleasedFilmActorBor}

const conn = new WebSocket(`${wsUrlRoot}/get_released_film_actor_bor`)

type GetReleasedFilmActorReq =
    {}

type FilmActorBoxOfficeRsp =
    {
        FilmActor: string
        FilmBoxOffice: bigint
    }

type GetReleasedFilmActorBorRsp =
    {
        Collection: FilmActorBoxOfficeRsp[]
    }

async function getReleasedFilmActorBor(req: GetReleasedFilmActorReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <GetReleasedFilmActorBorRsp>rspParse(msg)
}