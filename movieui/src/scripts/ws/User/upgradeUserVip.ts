import {wsUrlRoot} from "@/scripts/ws/meta"
import {recvMsg, reqStringify, rspParse, sendMsg} from "@/scripts/ws/helper"

export {upgradeUserVip}

const conn = new WebSocket(`${wsUrlRoot}/upgrade_user_vip`)

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

    const task = recvMsg(conn)
    sendMsg(conn, reqStringify(req)).then()
    const msg = await task

    return <UpgradeUserVipRsp>rspParse(msg)
}