import {OrderRsp} from "@/scripts/ws/Order/getAllOrder"
import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllOrderWithUserId}
export type {
    GetAllOrderWithUserIdReq,
    GetAllOrderWithUserIdRsp
}

type GetAllOrderWithUserIdReq =
    {
        OrderUserId: bigint
    }

type GetAllOrderWithUserIdRsp =
    {
        Collection: OrderRsp[]
    }

async function getAllOrderWithUserId(req: GetAllOrderWithUserIdReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_all_order_with_user_id`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_all_order_with_user_id req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_all_order_with_user_id rsp:' + msg)

    return <GetAllOrderWithUserIdRsp>rspParse(msg)
}