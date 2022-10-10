import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {isOrderIdValid}

const conn = new WebSocket(`${wsUrlRoot}/is_order_id_valid`)

type IsOrderIdValidReq =
    {
        OrderId: bigint
    }

type IsOrderIdValidRsp =
    {
        Ok: boolean
    }

async function isOrderIdValid(req: IsOrderIdValidReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <IsOrderIdValidRsp>rspParse(msg)
}
