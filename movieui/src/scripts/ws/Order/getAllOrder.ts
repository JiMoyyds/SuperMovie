import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllOrder}
export type {
    OrderRsp,
    GetAllOrderReq,
    GetAllOrderRsp
}

type GetAllOrderReq =
    {}

type OrderRsp =
    {
        OrderId: bigint
        OrderUserId: bigint
        OrderFilmId: bigint
        OrderCinemaId: bigint
        OrderScheduleId: bigint
        OrderSeat: string
        OrderPayAmount: number
    }

type GetAllOrderRsp =
    {
        Collection: OrderRsp[]
    }

async function getAllOrder(req: GetAllOrderReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_all_order`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_all_order req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_all_order rsp:' + msg)

    return <GetAllOrderRsp>rspParse(msg)
}