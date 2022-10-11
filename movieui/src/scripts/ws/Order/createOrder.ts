import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {createOrder}
export type {
    CreateOrderReq,
    CreateOrderRsp
}

type CreateOrderReq =
    {
        OrderFilmId: bigint
        OrderUserId: bigint
        OrderCinemaId: bigint
        OrderScheduleId: bigint
        OrderSeat: string
        OrderPayAmount: number
    }

type CreateOrderRsp =
    {
        Ok: boolean
        OrderId: bigint
        AlipayQrCodePath: string
    }

async function createOrder(req: CreateOrderReq) {
    const conn = createWebSocket(`${wsUrlRoot}/create_order`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('create_order req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('create_order rsp:' + msg)

    return <CreateOrderRsp>rspParse(msg)
}