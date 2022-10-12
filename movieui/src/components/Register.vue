<template>

  <v-card class="login_card">
    <v-card-title>
      超级电影
    </v-card-title>

    <v-card-text>
      <v-text-field
          label="手机号"
          v-model="_UserId"
      />
      <v-text-field
          label="密码(不得少于8位，必须需含有大小写字母)"
          type="password"
          v-model="_UserPwd"
      />
      <v-btn
          variant="text"
          color="blue"
          width="100%"
          @click="register()"
      >
        立即注册
      </v-btn>
    </v-card-text>
  </v-card>

</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"
import {ref} from "vue"
import {addUser} from "@/scripts/ws/User/addUser"
import {UserId, IsUserLogin} from "@/scripts/state/user"

const router = useRouter()

const _UserId = ref('')
const _UserPwd = ref('')

async function register() {
  const user = await addUser({
    UserId: BigInt(_UserId.value),
    UserPwd: _UserPwd.value
  })
  if (user.Ok) {
    UserId.value = BigInt(_UserId.value)
    IsUserLogin.value = true
    await router.push('user_info/' + UserId.value)
  }
}
</script>

<style lang="stylus" scoped>

.login_card
  margin auto
  margin-top 20vh
  width 40%
  padding-left 20px
  padding-right 20px

</style>
