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
            v-for="film in film_on_show_list"
            :cover_url="film.cover_url"
            :name="film.name"
            :booking_route="film.route"
            :trailer_url="film.trailer_url"
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
            v-for="film in film_preview_list"
            :cover_url="film.cover_url"
            :name="film.title"
            :booking_route="film.route"
            :trailer_url="film.trailer_url"
        />
      </v-card-text>
    </v-card>

  </div>
</template>

<script lang="ts" setup>

import {ref} from "vue"
import FilmPreviewCard from "@/components/Film/FilmPreviewCard.vue"
import {
  film_on_show_list,
  film_preview_list
} from "@/scripts/film_data"

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