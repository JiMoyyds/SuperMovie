import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {isOrderIdCanCheckIn}
export type {
    IsOrderIdCanCheckInReq,
    IsOrderIdCanCheckInRsp
}

type IsOrderIdCanCheckInReq =
    {
        OrderId: bigint
    }

type IsOrderIdCanCheckInRsp =
    {
        Yes: boolean
    }

async function isOrderIdCanCheckIn(req: IsOrderIdCanCheckInReq) {
    const conn = createWebSocket(`${wsUrlRoot}/is_order_can_check_in`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('is_order_can_check_in req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('is_order_can_check_in rsp:' + msg)

    return <IsOrderIdCanCheckInRsp>rspParse(msg)
}
