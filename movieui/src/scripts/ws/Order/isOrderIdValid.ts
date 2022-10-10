import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {isOrderIdValid}
export type {
    IsOrderIdValidReq,
    IsOrderIdValidRsp
}

type IsOrderIdValidReq =
    {
        OrderId: bigint
    }

type IsOrderIdValidRsp =
    {
        Ok: boolean
    }

async function isOrderIdValid(req: IsOrderIdValidReq) {
    const conn = createWebSocket(`${wsUrlRoot}/is_order_id_valid`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('is_order_id_valid req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('is_order_id_valid rsp:' + msg)

    return <IsOrderIdValidRsp>rspParse(msg)
}
