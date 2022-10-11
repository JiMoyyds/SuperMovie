<template>
  <div>

    <div style="display:flex">
      <v-chip
          size="x-large"
          class="mb-2 mx-auto"
          color="primary"
          prepend-icon="mdi-theater"
      >
        <div>
          {{ CinemaName }}
        </div>
      </v-chip>
    </div>

    <v-card>
      <v-card-title>
        <div
            style="font-size:1.5rem"
            class="mb-3"
        >
          新增排厅
        </div>
      </v-card-title>

      <v-card-text>

        <v-autocomplete
            :items="AllFilmName"
            v-model="SelectedFilmName"
            outlined
            dense
            chips
            small-chips
            label="电影选择"
        />
        <v-text-field label="放映开始(UTC+0)" v-model="NewScheduleStartTime"/>
        <v-text-field label="放映结束(UTC+0)" v-model="NewScheduleEndTime"/>

        <v-btn
            class="float-right"
            prepend-icon="mdi-plus"
            color="primary mb-4"
            @click="add()"
        >
          新增排厅
        </v-btn>
      </v-card-text>

    </v-card>

    <v-card
        class="my-5"
        variant="text"
    >
      <v-card-title>
        <div
            style="font-size:1.5rem"
            class="mb-3"
        >
          当前排厅
        </div>
      </v-card-title>

      <v-card-text>

        <v-card
            class="mb-5"
            v-for="item in AllScheduleInCurrentCinema"
        >
          <v-card-title>
            {{ AllFilm.filter(x => x.FilmId === item.ScheduleFilmId)[0].FilmName }}
          </v-card-title>
          <v-card-text>
            <div style="display:flex">
              <div style="margin-right:auto">
                <v-chip class="mr-2" color="primary">
                  放映开始：{{ item.ScheduleStartTime }}
                </v-chip>
                <v-chip color="primary">
                  放映结束：{{ item.ScheduleEndTime }}
                </v-chip>
              </div>
              <v-btn
                  color="primary"
                  @click="delete_(item.ScheduleId)"
              >
                删除
              </v-btn>
            </div>
          </v-card-text>

        </v-card>

      </v-card-text>
    </v-card>

  </div>
</template>

<script lang="ts" setup>

import {schedule_list} from "@/scripts/schedule_data"
import {onMounted, ref} from "vue"
import {useRouter} from "vue-router"
import {FilmRsp} from "@/scripts/ws/Film/getAllFilm"
import {getAllFilm} from "@/scripts/ws/Film/getAllFilm"
import {addSchedule} from "@/scripts/ws/Schedule/addSchedule"
import {getAllScheduleByCinemaId, ScheduleRsp} from "@/scripts/ws/Schedule/getAllScheduleByCinemaId"
import {deleteSchedule} from "@/scripts/ws/Schedule/deleteSchedule"
import {getAllCinema} from "@/scripts/ws/Cinema/getAllCinema"

const router = useRouter()
const props =
    defineProps<{
      cinema_id: bigint,
    }>()

const CinemaName = ref('')
const AllFilmName = ref<string[]>([])
const SelectedFilmName = ref<string>('')

const AllFilm = ref<FilmRsp[]>([])
const AllScheduleInCurrentCinema = ref<ScheduleRsp[]>([])

const NewScheduleStartTime = ref<string>(new Date().toISOString())
const NewScheduleEndTime = ref<string>(new Date().toISOString())

async function add() {
  const filmId = AllFilm.value.filter(x => x.FilmName === SelectedFilmName.value)[0].FilmId
  const result = await addSchedule({
    ScheduleFilmId: filmId,
    ScheduleCinemaId: props.cinema_id,
    ScheduleStartTime: new Date(NewScheduleStartTime.value),
    ScheduleEndTime: new Date(NewScheduleEndTime.value)
  })
  AllScheduleInCurrentCinema.value =
      (await getAllScheduleByCinemaId({
        CinemaId: props.cinema_id
      })).Collection
}

async function delete_(ScheduleId: bigint) {
  deleteSchedule({
    ScheduleId: ScheduleId
  })
  AllScheduleInCurrentCinema.value =
      (await getAllScheduleByCinemaId({
        CinemaId: props.cinema_id
      })).Collection
}


onMounted(async () => {
  AllFilm.value = (await getAllFilm({})).Collection

  for (const film of AllFilm.value) {
    AllFilmName.value.push(film.FilmName)
  }

  AllScheduleInCurrentCinema.value =
      (await getAllScheduleByCinemaId({
        CinemaId: props.cinema_id
      })).Collection
  if (AllFilmName.value.length > 0)
    SelectedFilmName.value = AllFilmName.value[0]
  const all_cinema = (await getAllCinema({})).Collection
  for (const cinema of all_cinema)
    if (cinema.CinemaId === props.cinema_id)
      CinemaName.value = cinema.CinemaName
})

</script>

<style lang="stylus" scoped>

</style>
