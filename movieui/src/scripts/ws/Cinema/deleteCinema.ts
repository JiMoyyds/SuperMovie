import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {deleteCinema}

const conn = new WebSocket(`${wsUrlRoot}/delete_cinema`)

type DeleteCinemaReq =
    {
        CinemaId: bigint
    }

type DeleteCinemaRsp =
    {
        Ok: boolean
    }


async function deleteCinema(req: DeleteCinemaReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <DeleteCinemaRsp>rspParse(msg)
}