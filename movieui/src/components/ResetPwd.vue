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
      />
      <v-text-field
          :append-icon="showNewPwd ? 'mdi-eye' : 'mdi-eye-off'"
          @click:append="showNewPwd = !showNewPwd"
          :type="showNewPwd ? 'text' : 'password'"
          label="新密码"
      />

      <v-btn
          color="primary"
          width="100%"
          height="50px"
          size="large"
      >
        修改
      </v-btn>
    </v-card-text>

  </v-card>

</template>

<script lang="ts" setup>

import {ref} from "vue"

const showOldPwd = ref(false)
const showNewPwd = ref(false)

</script>

<style lang="stylus" scoped>

.login_card
  margin auto
  margin-top 10vh
  width 40%
  padding-left 20px
  padding-right 20px

</style>
