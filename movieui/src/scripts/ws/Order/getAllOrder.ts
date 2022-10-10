import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getAllOrder}
export type {OrderRsp}

const conn = new WebSocket(`${wsUrlRoot}/get_all_order`)

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

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <GetAllOrderRsp>rspParse(msg)
}