import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getSchedule}
export type {
    GetScheduleReq,
    GetScheduleRsp
}

type GetScheduleReq =
    {
        ScheduleId: bigint
    }

type GetScheduleRsp =
    {
        Ok: boolean
        ScheduleId: bigint
        ScheduleCinemaId: bigint
        ScheduleFilmId: bigint
        ScheduleStartTime: Date
        ScheduleEndTime: Date
    }

async function getSchedule(req: GetScheduleReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_schedule`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_schedule req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_schedule rsp:' + msg)

    return <GetScheduleRsp>rspParse(msg)
}
