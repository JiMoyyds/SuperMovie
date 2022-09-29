<template>

  <v-card class="login_card">
    <v-card-title>
      超级电影
    </v-card-title>

    <v-card-text>
      <v-text-field label="手机号"></v-text-field>
      <v-text-field label="密码"></v-text-field>

      <v-btn
          variant="text"
          color="orange"
          width="100%"
          height="50px"
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

</script>

<style lang="stylus" scoped>

.login_card
  margin auto
  margin-top 10vh
  width 40%
  padding-left 20px
  padding-right 20px

</style>
