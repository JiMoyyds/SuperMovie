import {OrderRsp} from "@/scripts/ws/Order/getAllOrder"
import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllOrderWithUserId}

const conn = new WebSocket(`${wsUrlRoot}/get_all_order_with_user_id`)

type GetAllOrderWithUserIdReq =
    {
        OrderUserId: bigint
    }

type GetAllOrderWithUserIdRsp =
    {
        Collection: OrderRsp[]
    }

async function getAllOrderWithUserId(req: GetAllOrderWithUserIdReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <GetAllOrderWithUserIdRsp>rspParse(msg)
}