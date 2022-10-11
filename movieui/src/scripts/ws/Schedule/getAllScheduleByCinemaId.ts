import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllScheduleByCinemaId}
export type {
    ScheduleRsp,
    GetAllScheduleByCinemaIdReq,
    GetAllScheduleByCinemaIdRsp
}

type GetAllScheduleByCinemaIdReq =
    {
        CinemaId: bigint
    }

type ScheduleRsp =
    {
        ScheduleId: bigint
        ScheduleCinemaId: bigint
        ScheduleFilmId: bigint
        ScheduleStartTime: Date
        ScheduleEndTime: Date
    }

type GetAllScheduleByCinemaIdRsp =
    {
        Collection: ScheduleRsp[]
    }

async function getAllScheduleByCinemaId(req: GetAllScheduleByCinemaIdReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_all_schedule_by_cinema_id`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_all_schedule_by_cinema_id req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_all_schedule_by_cinema_id rsp:' + msg)

    return <GetAllScheduleByCinemaIdRsp>rspParse(msg)
}
