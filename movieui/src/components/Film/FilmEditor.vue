<template>
  <div>

    <v-card>
      <v-card-title>
        影片编辑
      </v-card-title>
      <v-card-text>
        <v-text-field label="电影名" v-model="FilmName"/>
        <v-text-field label="电影封面URL" v-model="FilmCoverUrl"/>
        <v-text-field label="电影预告片URL" v-model="FilmPreviewVideoUrl"/>
        <v-text-field label="电影上线时间" v-model="FilmOnlineTime"/>
        <v-combobox
            v-model="FilmTypes"
            :items="[]"
            label="电影类型"
            multiple
            chips
        />
        <v-combobox
            v-model="FilmActors"
            :items="[]"
            label="电影演员"
            multiple
            chips
        />
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
            @click="save()"
        >
          保存
        </v-btn>
        <v-btn
            v-else
            prepend-icon="mdi-plus-circle"
            style="float:right"
            color="primary"
            class="mb-4"
            @click="save()"
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
const FilmOnlineTime = ref(new Date().toISOString())
const FilmTypes = ref<string[]>([])
const FilmActors = ref<string[]>([])

onMounted(async () => {
  if (!props.create_film) {
    const film = await getFilm({FilmId: props.film_id})
    FilmName.value = film.FilmName
    FilmCoverUrl.value = film.FilmCoverUrl
    FilmPreviewVideoUrl.value = film.FilmPreviewVideoUrl
    FilmPrice.value = film.FilmPrice
    FilmIsPreorder.value = film.FilmIsPreorder
    FilmOnlineTime.value = film.FilmOnlineTime.toISOString()
    FilmTypes.value = film.FilmTypes
    FilmActors.value = film.FilmActors
  }
})

async function save() {
  if (props.create_film) {
    await addFilm({
      FilmName: FilmName.value,
      FilmCoverUrl: FilmCoverUrl.value,
      FilmPreviewVideoUrl: FilmPreviewVideoUrl.value,
      FilmPrice: FilmPrice.value,
      FilmIsPreorder: FilmIsPreorder.value,
      FilmOnlineTime: new Date(FilmOnlineTime.value),
      FilmTypes: FilmTypes.value,
      FilmActors: FilmActors.value
    })
  } else {
    await updateFilm({
      FilmId: props.film_id,
      FilmName: FilmName.value,
      FilmCoverUrl: FilmCoverUrl.value,
      FilmPreviewVideoUrl: FilmPreviewVideoUrl.value,
      FilmPrice: FilmPrice.value,
      FilmIsPreorder: FilmIsPreorder.value,
      FilmOnlineTime: new Date(FilmOnlineTime.value),
      FilmTypes: FilmTypes.value,
      FilmActors: FilmActors.value
    })
  }
  await router.push('/film_management')
}

</script>

<style lang="stylus" scoped>

</style>