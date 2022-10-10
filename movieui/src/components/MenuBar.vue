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
        <v-btn variant="text" size="large" :to="'/'">首页</v-btn>
        <v-btn variant="text" size="large" :to="'/search'">找电影</v-btn>
        <v-btn variant="text" size="large" :to="'/film_management'">电影管理</v-btn>
        <v-btn variant="text" size="large" :to="'/cinema_management'">排厅管理</v-btn>
        <v-btn variant="text" size="large" :to="'/box_office_statistics'">票房统计</v-btn>
        <v-btn variant="text" size="large" :to="'/recent_orders/1123'">最近订单</v-btn>
        <v-btn variant="text" size="large" :to="'/ticket_checking'">检票</v-btn>
      </v-toolbar-title>


      <div
          class="mr-2 pt-6 search_input"
      >
        <v-text-field
            label="按电影名搜索"
        ></v-text-field>
        <v-btn
            icon="mdi-magnify"
            color="white ml-2 mt-n5"
            class="search_by_film_name_btn"
            :to="'/search'"
        />
      </div>

      <v-btn
          class="mr-2"
          :to="'/login'"
      >
        登录
      </v-btn>
      <v-btn
          icon="mdi-account"
          class="mr-2"
          :to="'/user_info/1234'"
      />

    </v-app-bar>

  </div>
</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"

const router = useRouter()
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