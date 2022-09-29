<template>
  <div>

    <div style="width:100%;display:flex;">
      <v-btn
          prepend-icon="mdi-plus-circle"
          class="mx-auto"
          color="primary"
          @click="$router.push('/cinema_creator')"
      >
        新增
      </v-btn>
    </div>

    <v-divider class="my-4"/>

    <v-banner
        v-for="item in cinema_list"
        color="primary"
    >
      <v-banner-text style="font-size:20px">
        {{ item.cinema_name }}
      </v-banner-text>

      <template v-slot:actions>
        <v-btn @click="$router.push('/schedule_editor/'+item.cinema_id)"
        >
          排厅
        </v-btn>
        <v-btn @click=""
        >
          删除
        </v-btn>
      </template>
    </v-banner>


  </div>
</template>

<script lang="ts" setup>

import {
  cinema_list,
} from "@/scripts/cinema_data"


</script>

<style lang="stylus" scoped>

</style>
