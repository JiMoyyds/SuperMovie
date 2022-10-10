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
      v-for="(item,index) in bor_by_film_name"
      class="mb-2"
  >
    <v-card-title>
      <v-chip variant="text" size="x-large">
        NO.{{ index + 1 }}
        {{ item.filmName }}
      </v-chip>
    </v-card-title>
    <v-card-text>
      <v-chip size="x-large" variant="text">
        票房共计 {{ item.boxOffice }}
      </v-chip>
    </v-card-text>
  </v-card>

</template>

<script lang="ts" setup>

import {ref} from "vue"
import {useRouter} from "vue-router"

const router = useRouter()
const statistics_items = ref(['按影片', '按类型', '按主演'])
const statistics_values = ref(['按影片'])

const bor_by_film_name = ref([
  {
    filmName: '星际穿越',
    boxOffice: 114514
  },
  {
    filmName: '盗梦空间',
    boxOffice: 11451
  },
  {
    filmName: '信条',
    boxOffice: 1145
  }
])

/*
const bor_by_film_name;
const bor_by_film_type;
const bor_by_film_actor;
*/

</script>

<style lang="stylus" scoped>

</style>