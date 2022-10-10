import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {addSchedule}

const conn = new WebSocket(`${wsUrlRoot}/add_schedule`)

type AddScheduleReq =
    {
        ScheduleFilmId: bigint
        ScheduleStartTime: Date
        ScheduleEndTime: Date
    }

type AddScheduleRsp =
    {
        Ok: boolean
        ScheduleId: bigint
    }

async function addSchedule(req: AddScheduleReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <AddScheduleRsp>rspParse(msg)
}