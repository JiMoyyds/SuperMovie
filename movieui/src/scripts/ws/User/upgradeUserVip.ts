import {wsUrlRoot} from "@/scripts/ws/meta"
import {createWebSocket, recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {upgradeUserVip}
export type {
    UpgradeUserVipReq,
    UpgradeUserVipRsp
}

type UpgradeUserVipReq =
    {
        UserId: bigint
        NewVipLevel: bigint
    }

type UpgradeUserVipRsp =
    {
        Ok: boolean
    }

async function upgradeUserVip(req: UpgradeUserVipReq) {
    const conn = createWebSocket(`${wsUrlRoot}/upgrade_user_vip`)

    const task = recvMsg(conn)
    const reqJson = reqStringify(req)
    console.log('upgrade_user_vip req:' + reqJson)
    sendMsg(conn, reqJson).then()
    const msg = await task
    console.log('upgrade_user_vip rsp:' + msg)

    return <UpgradeUserVipRsp>rspParse(msg)
}