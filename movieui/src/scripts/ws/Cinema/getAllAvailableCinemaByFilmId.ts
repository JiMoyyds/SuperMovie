import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"
import {CinemaRsp} from "@/scripts/ws/Cinema/getAllCinema"

export {getAllAvailableCinemaByFilmId}
export type {
    GetAllAvailableCinemaByFilmIdReq,
    GetAllAvailableCinemaByFilmIdRsp
}

type GetAllAvailableCinemaByFilmIdReq =
    {
        FilmId: bigint
    }

type GetAllAvailableCinemaByFilmIdRsp =
    {
        Collection: CinemaRsp[]
    }

async function getAllAvailableCinemaByFilmId(req: GetAllAvailableCinemaByFilmIdReq) {
    const conn = createWebSocket(
        `${wsUrlRoot}/get_all_available_cinema_by_film_id`
    )

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_all_cinema req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_all_cinema rsp:' + msg)

    return <GetAllAvailableCinemaByFilmIdRsp>rspParse(msg)
}
