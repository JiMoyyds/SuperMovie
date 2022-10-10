import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {isUserIdMatchPwd}

const conn = new WebSocket(`${wsUrlRoot}/is_user_id_match_pwd`)

type IsUserIdMatchPwdReq =
    {
        UserId: bigint
        UserPwd: string
    }

type IsUserIdMatchPwdRsp =
    {
        Ok: boolean
    }

async function isUserIdMatchPwd(req: IsUserIdMatchPwdReq) {

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <IsUserIdMatchPwdRsp>rspParse(msg)
}