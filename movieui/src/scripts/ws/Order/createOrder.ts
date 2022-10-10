import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {createOrder}

const conn = new WebSocket(`${wsUrlRoot}/create_order`)

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
    }

async function createOrder(req: CreateOrderReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <CreateOrderRsp>rspParse(msg)
}