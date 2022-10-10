import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {refundUserVip}
export type {
    RefundUserVipReq,
    RefundUserVipRsp
}

type RefundUserVipReq =
    {
        UserId: bigint
    }

type RefundUserVipRsp =
    {
        Ok: boolean
    }

async function refundUserVip(req: RefundUserVipReq) {
    const conn = createWebSocket(`${wsUrlRoot}/refund_user_vip`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('refund_user_vip req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('refund_user_vip rsp:' + msg)

    return <RefundUserVipRsp>rspParse(msg)
}