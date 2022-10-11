import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {getUser}
export type {
    GetUserReq,
    GetUserRsp
}

type GetUserReq =
    {
        UserId: bigint
    }

type GetUserRsp =
    {
        Ok: boolean
        UserId: bigint
        UserVipLevel: bigint
        UserVipLevelDiscount: number
        UserVipExpireTime: Date
    }

async function getUser(req: GetUserReq) {
    const conn = createWebSocket(`${wsUrlRoot}/get_user`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('get_user req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('get_user rsp:' + msg)

    return <GetUserRsp>rspParse(msg)
}
