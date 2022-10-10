import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {refundUserVip}

const conn = new WebSocket(`${wsUrlRoot}/refund_user_vip`)

type RefundUserVipReq =
    {
        UserId: bigint
    }

type RefundUserVipRsp =
    {
        Ok: boolean
    }

async function refundUserVip(req: RefundUserVipReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <RefundUserVipRsp>rspParse(msg)
}