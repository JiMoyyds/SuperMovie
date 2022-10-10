<template>
  <div class="home_zone">
    <v-card variant="text">
      <template v-slot:title>
        <v-chip color="orange" size="x-large">
          <h2 style="font-weight:400">
            正在热映
          </h2>
        </v-chip>
      </template>

      <v-card-text class="card_field">
        <FilmPreviewCard
            v-for="film in released_film_list"
            :cover_url="film.FilmCoverUrl"
            :name="film.FilmName"
            :booking_route="'booking/'+film.FilmId"
            :trailer_url="film.FilmPreviewVideoUrl"
        />
      </v-card-text>

    </v-card>
    <v-card variant="text" class="mt-5">
      <template v-slot:title>
        <v-chip color="blue" size="x-large">
          <h2 style="font-weight:400">
            即将上映
          </h2>
        </v-chip>
      </template>

      <v-card-text class="card_field">
        <FilmPreviewCard
            v-for="film in preview_film_list"
            :cover_url="film.FilmCoverUrl"
            :name="film.FilmName"
            :booking_route="'booking/'+film.FilmId"
            :trailer_url="film.FilmPreviewVideoUrl"
        />
      </v-card-text>
    </v-card>

  </div>
</template>

<script lang="ts" setup>

import {onMounted, ref} from "vue"
import FilmPreviewCard from "@/components/Film/FilmPreviewCard.vue"
import {
  film_on_show_list,
  film_preview_list
} from "@/scripts/film_data"
import {FilmRsp, getAllFilm, GetAllFilmReq} from "@/scripts/ws/Film/getAllFilm"
import {useRouter} from "vue-router"

const router = useRouter()
let req = <GetAllFilmReq>{
  CinemaName: "hi"
}

const released_film_list = ref<FilmRsp[]>([])
const preview_film_list = ref<FilmRsp[]>([])

onMounted(async () => {
  const all_film_list = (await getAllFilm({})).Collection

  for (const film of all_film_list) {
    if (film.FilmIsPreorder)
      preview_film_list.value.push(film)
    else
      released_film_list.value.push(film)
  }

})

</script>

<style lang="stylus" scoped>

.home_zone
  margin-left 30px
  margin-right 30px

.card_field
  display flex
  flex-wrap wrap
  justify-content center

</style>