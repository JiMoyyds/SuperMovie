<template>

  <v-snackbar
      v-model="show_snackbar"
      timeout="-1"
      multi-line=""
      location="top"
  >
    <v-responsive :aspect-ratio="16/9">
      <iframe
          class="trailer_player"
          :src="trailer_url"
      />
    </v-responsive>

    <template v-slot:actions>
      <v-btn
          variant="text"
          @click="show_snackbar = false"
      >
        关闭
      </v-btn>
    </template>
  </v-snackbar>

  <v-card
      class="mx-1 mt-2 movie_card"
  >
    <!--
    <v-img :src="cover_url"/>
     -->
    <img
        :src="cover_url"
        style="width:100%;object-fit:contain;"
        alt=""
    />
    <v-card-title>
      {{ name }}
    </v-card-title>

    <v-card-actions>
      <v-btn
          variant="text"
          color="primary"
          @click="router.push('/'+booking_route)"
          v-if="IsUserLogin&&!IsUserAdmin"
      >
        购票
      </v-btn>
      <v-btn
          variant="text"
          color="blue"
          @click="show_snackbar=true"
      >
        观看预告片
      </v-btn>
    </v-card-actions>

  </v-card>

</template>

<script lang="ts" setup>

import {ref} from "vue"
import {useRouter} from "vue-router"
import {IsUserLogin, IsUserAdmin} from "@/scripts/state/user"

const router = useRouter()
defineProps<{
  name: string,
  cover_url: string,
  booking_route: string,
  trailer_url: string
}>()

const show_snackbar = ref(false)

</script>

<style lang="stylus" scoped>

.trailer_player
  width 60vw
  height 70vh

.movie_card
  min-width 240px
  width 18%

</style>