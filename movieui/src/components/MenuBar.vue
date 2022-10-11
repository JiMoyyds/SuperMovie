<template>
  <div>

    <v-app-bar
        color="primary"
        prominent
    >
      <v-toolbar-title>
        <v-chip
            prepend-icon="mdi-movie-roll"
            variant="text"
            size="x-large"
        >
          超级电影
        </v-chip>
        <v-btn variant="text" size="large"
               :to="'/'">首页
        </v-btn>
        <v-btn variant="text" size="large"
               :to="'/search'"
        >找电影
        </v-btn>
        <v-btn variant="text" size="large"
               :to="'/film_management'"
               v-if="IsUserAdmin&&IsUserLogin"
        >电影管理
        </v-btn>
        <v-btn variant="text" size="large"
               :to="'/cinema_management'"
               v-if="IsUserAdmin&&IsUserLogin"
        >排厅管理
        </v-btn>
        <v-btn variant="text" size="large"
               :to="'/box_office_statistics'"
        >票房排行
        </v-btn>
        <v-btn variant="text" size="large"
               :to="'/recent_orders/1123'"
               v-if="IsUserLogin"
        >我的订单
        </v-btn>
        <v-btn variant="text" size="large"
               :to="'/ticket_checking'"
               v-if="IsUserAdmin&&IsUserLogin"
        >检票
        </v-btn>
      </v-toolbar-title>

      <v-btn
          class="mr-2"
          :to="'/login'"
          v-if="!IsUserLogin"
      >
        登录
      </v-btn>
      <v-btn
          class="mr-2"
          v-else
          @click="logout()"
      >
        登出
      </v-btn>

      <v-btn
          icon="mdi-account"
          class="mr-2"
          :to="'/user_info/'+UserId"
          v-if="IsUserLogin&&!IsUserAdmin"
      />

    </v-app-bar>

  </div>
</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"
import {UserId, IsUserAdmin, IsUserLogin} from "@/scripts/state/user"

const router = useRouter()

function logout() {
  UserId.value = -1n
  IsUserAdmin.value = false
  IsUserLogin.value = false
  router.push('/')
}


</script>

<style lang="stylus" scoped>

/*
.tools
  display flex
  justify-content end
*/
.navi_zone
  margin-right auto

.search_input
  display: flex;
  justify-content: space-between;
  width: 30%;

.search_by_film_name_btn
  align-self center

</style>