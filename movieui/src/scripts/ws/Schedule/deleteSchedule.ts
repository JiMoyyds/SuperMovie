import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {deleteSchedule}
export type {
    DeleteScheduleReq,
    DeleteScheduleRsp
}

type DeleteScheduleReq =
    {
        ScheduleId: bigint
    }

type DeleteScheduleRsp =
    {
        Ok: boolean
    }

async function deleteSchedule(req: DeleteScheduleReq) {
    const conn = createWebSocket(`${wsUrlRoot}/delete_schedule`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('delete_schedule req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('delete_schedule req:' + msg)

    return <DeleteScheduleRsp>rspParse(msg)
}