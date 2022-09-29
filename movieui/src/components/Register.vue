<template>

  <v-card class="login_card">
    <v-card-title>
      超级电影
    </v-card-title>

    <v-card-text>
      <v-text-field label="手机号"></v-text-field>
      <v-text-field label="密码(不得少于8位，必须需含有大小写字母)"></v-text-field>
      <v-btn
          variant="text"
          color="blue"
          width="100%"
      >
        立即注册
      </v-btn>
    </v-card-text>
  </v-card>

</template>

<script lang="ts" setup>

</script>

<style lang="stylus" scoped>

.login_card
  margin auto
  margin-top 20vh
  width 40%
  padding-left 20px
  padding-right 20px

</style>
