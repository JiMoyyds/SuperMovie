import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {refundOrder}

const conn = new WebSocket(`${wsUrlRoot}/refund_order`)

type RefundOrderReq =
    {
        OrderId: bigint
    }

type RefundOrderRsp =
    {
        Ok: boolean
    }

async function refundOrder(req: RefundOrderReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <RefundOrderRsp>rspParse(msg)
}