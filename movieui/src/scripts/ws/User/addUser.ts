import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {addUser}
export type {
    AddUserReq,
    AddUserRsp
}

type AddUserReq =
    {
        UserId: bigint
        UserPwd: string
    }

type AddUserRsp =
    {
        Ok: boolean
    }

async function addUser(req: AddUserReq) {
    const conn = createWebSocket(`${wsUrlRoot}/add_user`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('add_user req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('add_user rsp:' + msg)

    return <AddUserRsp>rspParse(msg)
}