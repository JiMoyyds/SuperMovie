<template>
  <div>

    <div style="display:flex;margin-top:50px">
      <v-card class="mx-auto">

        <v-card-title>
          <h4>
            <v-chip
                color="primary"
                size="x-large"
                variant="text"
            >
              你好！用户{{ user_id }}
            </v-chip>
          </h4>
        </v-card-title>
        <v-card-text>
          <div>
            <v-chip
                size="large"
                variant="text"
            >
              会员等级
            </v-chip>
            <v-chip
                color="orange"
                class="mx-2"
            >
              LEVEL {{ UserVipLevel }}
            </v-chip>
            <v-chip
                color="green"
                class="mx-2"
            >
              享受{{ UserVipLevelDiscount * 10 }}折购票优惠
            </v-chip>
          </div>
          <div>
            <v-chip
                color="gray"
                size="large"
                variant="text"
            >
              绑定手机
            </v-chip>
            <v-chip
                color="orange"
                class="mx-2"
            >
              {{user_id}}
            </v-chip>
          </div>
        </v-card-text>
        <v-card-actions>
          <v-btn
              prepend-icon="mdi-cellphone"
              color="primary"
              @click=""
          >
            改绑手机
          </v-btn>
          <v-btn
              prepend-icon="mdi-lock-reset"
              color="primary"
              @click="router.push('/reset_pwd/'+user_id)"
          >
            修改密码
          </v-btn>
          <v-btn
              prepend-icon="mdi-crown"
              color="primary"
              @click="router.push('/upgrade_vip/1234')"
          >
            升级会员
          </v-btn>
        </v-card-actions>


      </v-card>
    </div>

  </div>
</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"
import {onMounted, ref} from "vue"
import {getUser} from "@/scripts/ws/User/getUser"

const router = useRouter()

const props =
    defineProps<{
      user_id: bigint,
    }>()

const UserVipLevel = ref(-1n)
const UserVipLevelDiscount = ref(-1.0)

onMounted(async () => {
  const user = await getUser({UserId: props.user_id})
  if (user.Ok) {
    UserVipLevel.value = user.UserVipLevel
    UserVipLevelDiscount.value = user.UserVipLevelDiscount
  }
})

</script>

<style lang="stylus" scoped>


</style>