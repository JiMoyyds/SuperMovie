<template>
  <div>

    <v-card class="mt-16">
      <v-card-title>
        新增影厅
      </v-card-title>
      <v-card-text>
        <v-text-field label="影厅名" v-model="CinemaName"/>
        <v-btn
            prepend-icon="mdi-plus-circle"
            style="float:right"
            color="primary"
            class="mb-4"
            @click="add()"
        >
          新增
        </v-btn>
      </v-card-text>
    </v-card>

    <v-divider class="my-4"/>

    <v-banner
        v-for="item in AllCinema"
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

  </div>
</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"
import {CinemaRsp, getAllCinema} from "@/scripts/ws/Cinema/getAllCinema"
import {deleteCinema} from "@/scripts/ws/Cinema/deleteCinema"
import {onMounted, ref} from "vue"
import {addCinema} from "@/scripts/ws/Cinema/addCinema"

const router = useRouter()

async function delete_(CinemaId: bigint) {
  await deleteCinema({CinemaId: CinemaId})
  AllCinema.value = (await getAllCinema({})).Collection
}

const AllCinema = ref<CinemaRsp[]>([])

onMounted(async () => {
  AllCinema.value = (await getAllCinema({})).Collection
})

const CinemaName = ref('')

async function add() {
  await addCinema({CinemaName: CinemaName.value})
  AllCinema.value = (await getAllCinema({})).Collection
}

</script>

<style lang="stylus" scoped>

</style>
