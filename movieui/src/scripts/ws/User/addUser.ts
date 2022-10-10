import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {addUser}

const conn = new WebSocket(`${wsUrlRoot}/add_user`)

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

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <AddUserRsp>rspParse(msg)
}