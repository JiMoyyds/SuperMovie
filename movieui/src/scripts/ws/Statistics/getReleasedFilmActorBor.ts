import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getReleasedFilmActorBor}
export type {
    FilmActorBoxOfficeRsp,
    GetReleasedFilmActorBorReq,
    GetReleasedFilmActorBorRsp
}

type GetReleasedFilmActorBorReq =
    {}

type FilmActorBoxOfficeRsp =
    {
        FilmActor: string
        FilmBoxOffice: number
    }

type GetReleasedFilmActorBorRsp =
    {
        Collection: FilmActorBoxOfficeRsp[]
    }

async function getReleasedFilmActorBor(req: GetReleasedFilmActorBorReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_released_film_actor_bor`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_released_film_actor_bor req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_released_film_actor_bor rsp:' + msg)

    return <GetReleasedFilmActorBorRsp>rspParse(msg)
}