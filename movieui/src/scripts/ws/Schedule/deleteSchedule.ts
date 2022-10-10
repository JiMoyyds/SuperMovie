import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {deleteSchedule}

const conn = new WebSocket(`${wsUrlRoot}/delete_schedule`)

type DeleteScheduleReq =
    {
        ScheduleId: bigint
    }

type DeleteScheduleRsp =
    {
        Ok: boolean
    }

async function deleteSchedule(req: DeleteScheduleReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <DeleteScheduleRsp>rspParse(msg)
}