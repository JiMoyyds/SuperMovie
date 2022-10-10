<template>
  <div class="mt-5">

    <v-autocomplete
        :items="AllFilmType"
        v-model="SelectedFilmType"
        outlined
        dense
        chips
        small-chips
        label="类型"
        multiple
    />

    <div style="display:flex">
      <v-text-field class="mx-1" label="上映年起始"
                    v-model="FilmOnlineTimeYearStart"/>
      <v-text-field class="mx-1" label="上映月起始"
                    v-model="FilmOnlineTimeMonthStart"/>

      <v-icon
          icon="mdi-arrow-right"
          class="mx-5 mt-3"
          size="x-large"
          color="grey"
      />

      <v-text-field class="mx-1" label="上映年结束"
                    v-model="FilmOnlineTimeYearEnd"/>
      <v-text-field class="mx-1" label="上映月结束"
                    v-model="FilmOnlineTimeMonthEnd"/>
    </div>

    <div style="display:flex">
      <v-text-field class="mx-1" label="场次月起始"
                    v-model="FilmScheduleTimeMonthStart"/>
      <v-text-field class="mx-1" label="场次日起始"
                    v-model="FilmScheduleTimeDayStart"/>
      <v-text-field class="mx-1" label="场次时起始"
                    v-model="FilmScheduleTimeHourStart"/>

      <v-icon
          icon="mdi-arrow-right"
          class="mx-5 mt-3"
          size="x-large"
          color="grey"
      />

      <v-text-field class="mx-1" label="场次月结束"
                    v-model="FilmScheduleTimeMonthEnd"/>
      <v-text-field class="mx-1" label="场次日结束"
                    v-model="FilmScheduleTimeDayEnd"/>
      <v-text-field class="mx-1" label="场次时结束"
                    v-model="FilmScheduleTimeDayEnd"/>
    </div>

    <v-text-field label="电影名关键词"/>

    <div style="display:flex">
      <v-btn
          class="search_btn"
          color="primary"
          @click="search()"
      >
        查询
      </v-btn>
    </div>

    <div class="card_field" v-if="search_result.length!==0">
      <FilmPreviewCard
          v-for="film in search_result"
          :cover_url="film.FilmCoverUrl"
          :name="film.FilmName"
          :booking_route="'booking'+film.FilmId"
          :trailer_url="film.FilmPreviewVideoUrl"
      />
    </div>

  </div>
</template>

<script lang="ts" setup>

import {onMounted, Ref, ref, watch} from "vue"
import FilmPreviewCard from "@/components/Film/FilmPreviewCard.vue"
import {searchFilm} from "@/scripts/ws/Film/searchFilm"
import {useRouter} from "vue-router"
import {FilmRsp} from "@/scripts/ws/Film/getAllFilm"

const router = useRouter()

const AllFilmType = ref(['全部'])
const SelectedFilmType = ref(['全部'])

const FilmOnlineTimeYearStart = ref(new Date().getFullYear())
const FilmOnlineTimeYearEnd = ref(new Date().getFullYear())
const FilmOnlineTimeMonthStart = ref(new Date().getMonth())
const FilmOnlineTimeMonthEnd = ref(new Date().getMonth())

const FilmScheduleTimeMonthStart = ref(new Date().getMonth())
const FilmScheduleTimeMonthEnd = ref(new Date().getMonth())
const FilmScheduleTimeDayStart = ref(new Date().getDay())
const FilmScheduleTimeDayEnd = ref(new Date().getDay())
const FilmScheduleTimeHourStart = ref(new Date().getHours() + 1)
const FilmScheduleTimeHourEnd = ref(new Date().getHours() + 1)

const FilmNameKeyWord = ref('')

const enable_preview = ref(false)

function handler(x: string[], target: Ref<string[]>) {
  if (x.length === 0)
    target.value = ['全部']
  if (x.length > 1 && target.value.find(y => y === '全部')) {
    target.value = target.value.filter(x => x !== '全部')
  }
}

watch(AllFilmType, x => handler(x, AllFilmType))

const search_result = ref<FilmRsp[]>([])

async function search() {
  //TODO 校验
  const FilmOnlineTimeStart = new Date(
      `${FilmOnlineTimeYearStart.value}-${FilmOnlineTimeMonthStart.value}-00T00:00:00`)
  const FilmOnlineTimeEnd = new Date(
      `${FilmOnlineTimeYearEnd.value}-${FilmOnlineTimeMonthEnd.value}-00T00:00:00`)

  const FilmScheduleTimeStart = new Date(
      `${new Date().getFullYear()}-${FilmScheduleTimeMonthStart.value}-${FilmScheduleTimeDayStart.value}T${FilmScheduleTimeHourStart.value}:00:00`)
  const FilmScheduleTimeEnd = new Date(
      `${new Date().getFullYear()}-${FilmScheduleTimeMonthEnd.value}-${FilmScheduleTimeDayEnd.value}T${FilmScheduleTimeHourEnd.value}:00:00`)

  search_result.value = (await searchFilm({
    FilmTypes: (SelectedFilmType.value.some(x => x === "全部") ?
        AllFilmType.value : SelectedFilmType.value),
    FilmOnlineTimeStart: FilmOnlineTimeStart,
    FilmOnlineTimeEnd: FilmOnlineTimeEnd,
    FilmScheduleTimeStart: FilmScheduleTimeStart,
    FilmScheduleTimeEnd: FilmScheduleTimeEnd,
    FilmNameKeyWord: FilmNameKeyWord.value
  })).Collection

}

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
