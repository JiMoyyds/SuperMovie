<template>
  <div>

    <v-switch
        v-model="enable_preview"
        label="包含预售"
        color="indigo"
        hide-details
    />
    <v-autocomplete
        :items="type_items"
        v-model="type_values"
        outlined
        dense
        chips
        small-chips
        label="类型"
        multiple
    />
    <v-autocomplete
        :items="time_items"
        v-model="time_values"
        outlined
        dense
        chips
        small-chips
        label="上映时间"
        multiple
    />
    <v-autocomplete
        :items="screening_items"
        v-model="screening_values"
        outlined
        dense
        chips
        small-chips
        label="场次"
        multiple
    />
    <v-text-field label="电影名关键词"/>

    <div style="display:flex">
      <v-btn class="search_btn" color="primary">查询</v-btn>
    </div>

    <div class="card_field" v-if="search_result_example.length!==0">
      <FilmPreviewCard
          v-for="film in search_result_example"
          :cover_url="film.cover_url"
          :name="film.name"
          :booking_route="film.route"
          :trailer_url="film.trailer_url"
      />
    </div>

  </div>
</template>

<script lang="ts" setup>

import {Ref, ref, watch} from "vue"
import FilmPreviewCard from "@/components/Film/FilmPreviewCard.vue"
import {all_film_on_sale_list} from "@/scripts/film_data"
import {searchFilm} from "@/scripts/ws/Film/searchFilm"
import {useRouter} from "vue-router"
const router = useRouter()

const type_items = ref(['全部', '古装', '科幻', '喜剧', '爱情', '动作', '卡通'])
const type_values = ref(['全部'])
const time_items = ref(['全部', '2023', '2022', '2021', '2020', '2019', '2018'])
const time_values = ref(['全部'])
const screening_items = ref(['全部', '9:00', '11:00', '13:00', '15:00', '18:00', '20:00'])
const screening_values = ref(['全部'])

const enable_preview = ref(false)

function handler(x: string[], target: Ref<string[]>) {
  if (x.length === 0)
    target.value = ['全部']
  if (x.length > 1 && target.value.find(y => y === '全部')) {
    target.value = target.value.filter(x => x !== '全部')
  }
}

watch(type_values, x => handler(x, type_values))
watch(time_values, x => handler(x, time_values))
watch(screening_values, x => handler(x, screening_values))

//const search_result = ref([])

const search_result_example = ref(all_film_on_sale_list)

//TODO
/*
const search_result = await searchFilm({
  FilmTypes: [""],
  FilmOnlineTimeStart: new Date(),
  FilmOnlineTimeEnd: new Date(),
  FilmScheduleTimeStart: new Date(),
  FilmScheduleTimeEnd: new Date(),
  FilmNameKeyWord: ""
})*/

</script>

<style lang="stylus" scoped>

.search_btn
  width: 200px
  margin-left auto
  margin-right auto

.card_field
  margin-top 50px
  display flex
  flex-wrap wrap
  justify-content space-around

.movie_card
  width 20%

</style>
