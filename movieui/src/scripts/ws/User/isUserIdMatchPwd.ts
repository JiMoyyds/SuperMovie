import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {isUserIdMatchPwd}
export type {
    IsUserIdMatchPwdReq,
    IsUserIdMatchPwdRsp
}

type IsUserIdMatchPwdReq =
    {
        UserId: bigint
        UserPwd: string
    }

type IsUserIdMatchPwdRsp =
    {
        Yes: boolean
    }

async function isUserIdMatchPwd(req: IsUserIdMatchPwdReq) {
    const conn = createWebSocket(`${wsUrlRoot}/is_user_id_match_pwd`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('is_user_id_match_pwd req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('is_user_id_match_pwd rsp:' + msg)

    return <IsUserIdMatchPwdRsp>rspParse(msg)
}