<template>
  <div>

    <div style="width:100%;display:flex;">
      <v-btn
          prepend-icon="mdi-plus-circle"
          class="mx-auto"
          color="primary"
          @click="$router.push('/film_editor/create')"
      >
        新增
      </v-btn>
    </div>

    <v-divider class="my-4"/>

    <v-banner
        v-for="item in all_film_on_sale_list"
        color="primary"
    >
      <v-banner-text style="font-size:20px">
        {{ item.name }}
      </v-banner-text>

      <template v-slot:actions>
        <v-btn
            @click="$router.push('/film_editor/'+item.id)"
        >
          修改
        </v-btn>
        <v-btn
            @click=""
        >
          删除
        </v-btn>
      </template>
    </v-banner>


  </div>
</template>

<script lang="ts" setup>

import {
  all_film_on_sale_list,
} from "@/scripts/film_data"

</script>

<style lang="stylus" scoped>

</style>