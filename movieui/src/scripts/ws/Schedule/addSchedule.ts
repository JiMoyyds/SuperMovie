import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {addSchedule}
export type {
    AddScheduleReq,
    AddScheduleRsp
}

type AddScheduleReq =
    {
        ScheduleFilmId: bigint
        ScheduleCinemaId: bigint
        ScheduleStartTime: Date
        ScheduleEndTime: Date
    }

type AddScheduleRsp =
    {
        Ok: boolean
        ScheduleId: bigint
    }

async function addSchedule(req: AddScheduleReq) {
    const conn = createWebSocket(`${wsUrlRoot}/add_schedule`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('add_schedule req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('add_schedule rsp:' + msg)

    return <AddScheduleRsp>rspParse(msg)
}