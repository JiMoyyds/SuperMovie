<template>
  <div>

    <div style="width:100%;display:flex;">
      <v-btn
          prepend-icon="mdi-plus-circle"
          class="mx-auto"
          color="primary"
          @click="router.push('/cinema_creator')"
      >
        新增
      </v-btn>
    </div>

    <v-divider class="my-4"/>

    <f-data
        :provider="async ()=>{
        return (await getAllCinema({})).Collection
      }"
        v-slot="cinema_list"
    >
      <v-banner
          v-for="item in cinema_list"
          color="primary"
      >
        <v-banner-text style="font-size:20px">
          {{ item.CinemaName }}
        </v-banner-text>

        <template v-slot:actions>
          <v-btn @click="router.push('/schedule_editor/'+item.CinemaId)"
          >
            排厅
          </v-btn>
          <v-btn @click="delete_(item.CinemaId)"
          >
            删除
          </v-btn>
        </template>
      </v-banner>
    </f-data>

  </div>
</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"
import {getAllCinema} from "@/scripts/ws/Cinema/getAllCinema"
import {deleteCinema} from "@/scripts/ws/Cinema/deleteCinema"
import {onMounted, ref} from "vue"
import FData from "@/components/field/f-data.vue"

const router = useRouter()

function delete_(CinemaId: bigint) {
  deleteCinema({CinemaId: CinemaId})
}

</script>

<style lang="stylus" scoped>

</style>
