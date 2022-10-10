<template>
  <div>

    <v-card class="mt-12">
      <v-card-title>
        影片编辑
      </v-card-title>
      <v-card-text>
        <v-text-field label="电影名" v-model="FilmName"/>
        <v-text-field label="电影封面URL" v-model="FilmCoverUrl"/>
        <v-text-field label="电影预告片URL" v-model="FilmPreviewVideoUrl"/>
        <v-text-field label="电影价格" v-model="FilmPrice"/>

        <v-switch
            label="预售模式"
            color="primary"
            v-model="FilmIsPreorder"
        />
        <v-btn
            v-if="!create_film"
            prepend-icon="mdi-content-save"
            style="float:right"
            color="primary"
            class="mb-4"
            @click="save();router.push('/film_management')"
        >
          保存
        </v-btn>
        <v-btn
            v-else
            prepend-icon="mdi-plus-circle"
            style="float:right"
            color="primary"
            class="mb-4"
            @click="save();router.push('/film_management')"
        >
          新增
        </v-btn>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts" setup>

import {onMounted, ref} from "vue"
import {addFilm} from "@/scripts/ws/Film/addFilm"
import {updateFilm} from "@/scripts/ws/Film/updateFilm"
import {useRoute, useRouter} from "vue-router"
import {getFilm} from "@/scripts/ws/Film/getFilm"

const route = useRoute()
const router = useRouter()

const props = withDefaults(
    defineProps<{
      film_id: bigint,
      create_film: boolean,
    }>(), {
      create_film: false
    })

const FilmName = ref('电影名')
const FilmCoverUrl = ref('封面URL')
const FilmPreviewVideoUrl = ref('预告片URL')
const FilmPrice = ref(0)
const FilmIsPreorder = ref(false)
const FilmOnlineTime = ref(new Date())//TODO

onMounted(async () => {
  if (!props.create_film) {
    const film = await getFilm({FilmId: props.film_id})
    FilmName.value = film.FilmName
    FilmCoverUrl.value = film.FilmCoverUrl
    FilmPreviewVideoUrl.value = film.FilmPreviewVideoUrl
    FilmPrice.value = film.FilmPrice
    FilmIsPreorder.value = film.FilmIsPreorder
    FilmOnlineTime.value = film.FilmOnlineTime
  }
})

function save() {
  if (props.create_film) {
    addFilm({
      FilmName: FilmName.value,
      FilmCoverUrl: FilmCoverUrl.value,
      FilmPreviewVideoUrl: FilmPreviewVideoUrl.value,
      FilmPrice: FilmPrice.value,
      FilmIsPreorder: FilmIsPreorder.value,
      FilmOnlineTime: FilmOnlineTime.value
    })
  } else {
    updateFilm({
      FilmId: props.film_id,
      FilmName: FilmName.value,
      FilmCoverUrl: FilmCoverUrl.value,
      FilmPreviewVideoUrl: FilmPreviewVideoUrl.value,
      FilmPrice: FilmPrice.value,
      FilmIsPreorder: FilmIsPreorder.value,
      FilmOnlineTime: FilmOnlineTime.value
    })
  }
}

</script>

<style lang="stylus" scoped>

</style>