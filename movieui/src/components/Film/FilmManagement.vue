<template>
  <div>

    <div style="width:100%;display:flex;">
      <v-btn
          prepend-icon="mdi-plus-circle"
          class="mx-auto"
          color="primary"
          @click="router.push('/film_editor/create')"
      >
        新增
      </v-btn>
    </div>

    <v-divider class="my-4"/>

    <f-data
        :provider="async ()=>{
        return (await getAllFilm({})).Collection
      }"
        v-slot="film_list"
    >
      <v-banner
          v-for="item in film_list"
          color="primary"
      >
        <v-banner-text style="font-size:20px">
          {{ item.FilmName }}
        </v-banner-text>

        <template v-slot:actions>
          <v-btn
              @click="router.push('/film_editor/'+item.FilmId)"
          >
            修改
          </v-btn>
          <v-btn
              @click="delete_(item.FilmId)"
          >
            删除
          </v-btn>
        </template>
      </v-banner>
    </f-data>

  </div>
</template>

<script lang="ts" setup>

import {getAllFilm} from "@/scripts/ws/Film/getAllFilm"
import {deleteFilm} from "@/scripts/ws/Film/deleteFilm"
import {useRouter} from "vue-router"
import FData from "@/components/field/f-data.vue"

const router = useRouter()

function delete_(FilmId: bigint) {
  deleteFilm({FilmId: FilmId})
}


</script>

<style lang="stylus" scoped>

</style>