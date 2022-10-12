import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getOrder}
export type {
    GetOrderReq,
    GetOrderRsp
}

type GetOrderReq =
    {
        OrderId: bigint
    }

type GetOrderRsp =
    {
        OrderId: bigint
        OrderUserId: bigint
        OrderFilmId: bigint
        OrderCinemaId: bigint
        OrderScheduleId: bigint
        OrderTime: Date
        OrderSeat: string
        OrderPayAmount: number
        OrderStatus: string
    }

async function getOrder(req: GetOrderReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_order`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_order req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_order rsp:' + msg)

    return <GetOrderRsp>rspParse(msg)
}
