<template>

  <v-card class="login_card pt-3">
    <v-card-title>
      <div style="display:flex">
        <v-chip
            color="primary"
            size="x-large"
            variant="text"
            class="mx-auto"
        >
          修改密码
        </v-chip>
      </div>
    </v-card-title>

    <v-card-text>
      <v-text-field
          :append-icon="showOldPwd ? 'mdi-eye' : 'mdi-eye-off'"
          @click:append="showOldPwd = !showOldPwd"
          :type="showOldPwd ? 'text' : 'password'"
          label="原密码"
          v-model="oldPwd"
      />
      <v-text-field
          :append-icon="showNewPwd ? 'mdi-eye' : 'mdi-eye-off'"
          @click:append="showNewPwd = !showNewPwd"
          :type="showNewPwd ? 'text' : 'password'"
          label="新密码"
          v-model="newPwd"
      />

      <v-btn
          color="primary"
          width="100%"
          height="50px"
          size="large"
          @click="reset_()"
      >
        修改
      </v-btn>
    </v-card-text>

  </v-card>

</template>

<script lang="ts" setup>

import {ref} from "vue"
import {useRouter} from "vue-router"
import {resetUserPwd} from "@/scripts/ws/User/resetUserPwd"

const props =
    defineProps<{
      user_id: bigint
    }>()
const router = useRouter()

const oldPwd = ref('')
const newPwd = ref('')
const showOldPwd = ref(false)
const showNewPwd = ref(false)

async function reset_() {
  const ok = await resetUserPwd({
    UserId: props.user_id,
    UserOldPwd: oldPwd.value,
    UserNewPwd: newPwd.value
  })

  if (ok.Yes) {
    await router.push('/user_info/' + props.user_id)
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
