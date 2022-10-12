import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {resetUserPwd}
export type {
    ResetUserPwdReq,
    ResetUserPwdRsp
}

type ResetUserPwdReq =
    {
        UserId: bigint
        UserOldPwd: string
        UserNewPwd: string
    }

type ResetUserPwdRsp =
    {
        Yes: boolean
    }

async function resetUserPwd(req: ResetUserPwdReq) {
    const conn = createWebSocket(`${wsUrlRoot}/reset_user_pwd`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('reset_user_pwd req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('reset_user_pwd rsp:' + msg)

    return <ResetUserPwdRsp>rspParse(msg)
}
