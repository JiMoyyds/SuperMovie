import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {addCinema}

const conn = new WebSocket(`${wsUrlRoot}/add_cinema`)

type AddCinemaReq =
    {
        CinemaName: string
    }

type AddCinemaRsp =
    {
        Ok: boolean
        CinemaId: bigint
    }


async function addCinema(req: AddCinemaReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <AddCinemaRsp>rspParse(msg)
}