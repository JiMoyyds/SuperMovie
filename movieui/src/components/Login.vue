<template>

  <v-card class="login_card">
    <v-card-title>
      超级电影
    </v-card-title>

    <v-card-text>
      <v-text-field
          label="手机号"
          type="number"
          v-model="_UserId"
      />
      <v-text-field
          label="密码"
          type="password"
          v-model="_UserPwd"
      />

      <v-btn
          variant="text"
          color="orange"
          width="100%"
          height="50px"
          @click="login()"
      >
        登录
      </v-btn>
      <div style="display: flex">
        <v-btn
            class="mt-2"
            style="margin-left: auto"
            variant="text"
            color="blue"
            width="30%"
            size="small"
            :to="'/register'"
        >
          没有帐号？点此注册
        </v-btn>
      </div>
    </v-card-text>

  </v-card>

</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"
import {ref} from "vue"
import {IsUserAdmin, IsUserLogin, UserId, UserVipDiscount, UserVipLevel} from "@/scripts/state/user"
import {isUserIdMatchPwd} from "@/scripts/ws/User/isUserIdMatchPwd"
import {getUser} from "@/scripts/ws/User/getUser"

const router = useRouter()

const _UserId = ref("")
const _UserPwd = ref("")

async function login() {
  const isMatch = (await isUserIdMatchPwd({
    UserId: BigInt(_UserId.value),
    UserPwd: _UserPwd.value
  })).Yes
  if (isMatch) {
    UserId.value = BigInt(_UserId.value)
    IsUserAdmin.value = BigInt(_UserId.value) < 1000n
    IsUserLogin.value = true

    const user = await getUser({UserId: BigInt(_UserId.value)})
    if (user.Ok) {
      UserVipLevel.value = user.UserVipLevel
      UserVipDiscount.value = user.UserVipLevelDiscount
    }
    if (BigInt(_UserId.value) > 1000n)
      await router.push('/user_info/' + _UserId.value)
    else
      await router.push('/film_management')
  }
}

</script>

<style lang="stylus" scoped>

.login_card
  margin auto
  margin-top 10vh
  width 40%
  padding-left 20px
  padding-right 20px

</style>
