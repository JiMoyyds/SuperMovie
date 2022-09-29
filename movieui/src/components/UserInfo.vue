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
              你好！示例用户
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
              LEVEL 1
            </v-chip>
            <v-chip
                color="green"
                class="mx-2"
            >
              享受8折购票优惠
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
              1145141919810
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
              @click="$router.push('/reset_pwd/'+user_id)"
          >
            修改密码
          </v-btn>
          <v-btn
              prepend-icon="mdi-crown"
              color="primary"
              @click="$router.push('/upgrade_vip/1234')"
          >
            升级会员
          </v-btn>
        </v-card-actions>


      </v-card>
    </div>

  </div>
</template>

<script lang="ts" setup>

defineProps<{
  user_id: number,
}>()

</script>

<style lang="stylus" scoped>


</style>