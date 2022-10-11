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
      <v-switch v-model="EnableScheduleSearch" label="启用场次检索"/>
      <v-text-field
          :disabled="!EnableScheduleSearch"
          class="mx-1" label="场次月起始"
          v-model="FilmScheduleTimeMonthStart"/>
      <v-text-field
          :disabled="!EnableScheduleSearch"
          class="mx-1" label="场次日起始"
          v-model="FilmScheduleTimeDayStart"/>
      <v-text-field
          :disabled="!EnableScheduleSearch"
          class="mx-1" label="场次时起始"
          v-model="FilmScheduleTimeHourStart"/>

      <v-icon
          icon="mdi-arrow-right"
          class="mx-5 mt-3"
          size="x-large"
          color="grey"
      />

      <v-text-field
          :disabled="!EnableScheduleSearch"
          class="mx-1" label="场次月结束"
          v-model="FilmScheduleTimeMonthEnd"/>
      <v-text-field
          :disabled="!EnableScheduleSearch"
          class="mx-1" label="场次日结束"
          v-model="FilmScheduleTimeDayEnd"/>
      <v-text-field
          :disabled="!EnableScheduleSearch"
          class="mx-1" label="场次时结束"
          v-model="FilmScheduleTimeHourEnd"/>
    </div>

    <v-text-field label="电影名关键词"
                  v-model="FilmNameKeyWord"/>

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
import {getAllFilmType} from "@/scripts/ws/Film/getAllFilmType"

const router = useRouter()

const AllFilmType = ref(['全部'])
const SelectedFilmType = ref(['全部'])

const FilmOnlineTimeYearStart = ref(new Date().getFullYear())
const FilmOnlineTimeYearEnd = ref(new Date().getFullYear())
const FilmOnlineTimeMonthStart = ref(new Date().getMonth() + 1)
const FilmOnlineTimeMonthEnd = ref(new Date().getMonth() + 1)

const FilmScheduleTimeMonthStart = ref(new Date().getMonth() + 1)
const FilmScheduleTimeDayStart = ref(new Date().getDate())
const FilmScheduleTimeHourStart = ref(new Date().getHours())

const FilmScheduleTimeMonthEnd = ref(new Date().getMonth() + 1)
const FilmScheduleTimeDayEnd = ref(new Date().getDate())
const FilmScheduleTimeHourEnd = ref(new Date().getHours())

const FilmNameKeyWord = ref('')
const EnableScheduleSearch = ref(false)

function handler(x: string[], target: Ref<string[]>) {
  if (x.length === 0)
    target.value = ['全部']
  if (x.length > 1 && target.value.find(y => y === '全部')) {
    target.value = target.value.filter(x => x !== '全部')
  }
}

watch(SelectedFilmType, x => handler(x, SelectedFilmType))

const search_result = ref<FilmRsp[]>([])

async function search() {
  //TODO 校验
  const FilmOnlineTimeStart = new Date(FilmOnlineTimeYearStart.value, FilmOnlineTimeMonthStart.value)
  const FilmOnlineTimeEnd = new Date(FilmOnlineTimeYearEnd.value, FilmOnlineTimeMonthEnd.value)
  const FilmScheduleTimeStart = new Date(new Date().getFullYear(), FilmScheduleTimeMonthStart.value, FilmScheduleTimeDayStart.value, FilmScheduleTimeHourStart.value)
  const FilmScheduleTimeEnd = new Date(new Date().getFullYear(), FilmScheduleTimeMonthEnd.value, FilmScheduleTimeDayEnd.value, FilmScheduleTimeHourEnd.value)

  search_result.value = (await searchFilm({
    EnableScheduleSearch: EnableScheduleSearch.value,
    FilmTypes: (SelectedFilmType.value.some(x => x === "全部") ?
        [''] : SelectedFilmType.value),
    FilmOnlineTimeStart: FilmOnlineTimeStart,
    FilmOnlineTimeEnd: FilmOnlineTimeEnd,
    FilmScheduleTimeStart: FilmScheduleTimeStart,
    FilmScheduleTimeEnd: FilmScheduleTimeEnd,
    FilmNameKeyWord: FilmNameKeyWord.value
  })).Collection

}

onMounted(async () => {
  AllFilmType.value = (await getAllFilmType({})).Collection
  AllFilmType.value.push('全部')
})

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
