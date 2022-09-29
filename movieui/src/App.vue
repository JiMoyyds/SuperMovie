<template>
  <v-app>
    <v-main>

      <MenuBar/>

      <div class="router_view">
        <router-view></router-view>
      </div>

    </v-main>
  </v-app>
</template>

<script lang="ts" setup>

import MenuBar from "@/components/MenuBar.vue"

</script>

<style lang="stylus" scoped>

.router_view
  padding-top 10px
  padding-bottom 50px
  padding-left 20px
  padding-right 20px

</style>