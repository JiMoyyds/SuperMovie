<template>
  <div>

    <v-card>
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
            @click="save();router.push('/cinema_management')"
        >
          新增
        </v-btn>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts" setup>

import {ref} from "vue"
import {useRouter} from "vue-router"
import {addCinema} from "@/scripts/ws/Cinema/addCinema"

const router = useRouter()

const CinemaName = ref('')

function save() {
  addCinema({
    CinemaName: CinemaName.value
  })
}

</script>

<style lang="stylus" scoped>

</style>
