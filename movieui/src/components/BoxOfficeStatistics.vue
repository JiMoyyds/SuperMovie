<template>

  <v-autocomplete
      :items="statistics_items"
      v-model="statistics_values"
      outlined
      dense
      small-chips
      label="统计方式"
  />

  <v-card
      v-for="(item,index) in result_list"
      class="mb-2"
  >
    <v-card-title>
      <v-chip variant="text" size="x-large">
        NO.{{ index + 1 }}
        {{ item.title }}
      </v-chip>
    </v-card-title>
    <v-card-text>
      <v-chip size="x-large" variant="text">
        票房共计 {{ item.count }}
      </v-chip>
    </v-card-text>
  </v-card>

</template>

<script lang="ts" setup>

import {ref} from "vue"

const statistics_items = ref(['按影片', '按类型', '按主演'])
const statistics_values = ref(['按影片'])

const result_list = ref([
  {
    title: '星际穿越',
    count: 114514
  },
  {
    title: '盗梦空间',
    count: 11451
  },
  {
    title: '信条',
    count: 1145
  }
])

</script>

<style lang="stylus" scoped>

</style>