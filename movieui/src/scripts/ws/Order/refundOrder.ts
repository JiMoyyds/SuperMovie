import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {refundOrder}
export type {
    RefundOrderReq,
    RefundOrderRsp
}

type RefundOrderReq =
    {
        OrderId: bigint
    }

type RefundOrderRsp =
    {
        Ok: boolean
    }

async function refundOrder(req: RefundOrderReq) {
    const conn = createWebSocket(`${wsUrlRoot}/refund_order`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('refund_order req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('refund_order rsp:' + msg)

    return <RefundOrderRsp>rspParse(msg)
}