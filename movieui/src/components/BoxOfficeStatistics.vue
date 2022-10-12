<template>

  <v-autocomplete
      :items="statistics_items"
      v-model="statistics_values"
      outlined
      dense
      small-chips
      label="统计方式"
  />

  <v-card
      v-if="statistics_values === '按影片'"
      v-for="(item,index) in nameBor.Collection"
      class="mb-2"
  >
    <v-card-title>
      <v-chip variant="text" size="x-large">
        NO.{{ index + 1 }}
        {{ item.FilmName }}
      </v-chip>
    </v-card-title>
    <v-card-text>
      <v-chip size="x-large" variant="text">
        票房共计 {{ item.FilmBoxOffice }}
      </v-chip>
    </v-card-text>
  </v-card>
  <v-card
      v-if="statistics_values === '按类型'"
      v-for="(item,index) in typeBor.Collection"
      class="mb-2"
  >
    <v-card-title>
      <v-chip variant="text" size="x-large">
        NO.{{ index + 1 }}
        {{ item.FilmType }}
      </v-chip>
    </v-card-title>
    <v-card-text>
      <v-chip size="x-large" variant="text">
        票房共计 {{ item.FilmBoxOffice }}
      </v-chip>
    </v-card-text>
  </v-card>
  <v-card
      v-if="statistics_values === '按主演'"
      v-for="(item,index) in actorBor.Collection"
      class="mb-2"
  >
    <v-card-title>
      <v-chip variant="text" size="x-large">
        NO.{{ index + 1 }}
        {{ item.FilmActor }}
      </v-chip>
    </v-card-title>
    <v-card-text>
      <v-chip size="x-large" variant="text">
        票房共计 {{ item.FilmBoxOffice }}
      </v-chip>
    </v-card-text>
  </v-card>

</template>

<script lang="ts" setup>

import {onMounted, ref} from "vue"
import {useRouter} from "vue-router"
import {getReleasedFilmTypeBor, GetReleasedFilmTypeBorRsp} from "@/scripts/ws/Statistics/getReleasedFilmTypeBor"
import {getReleasedFilmNameBor, GetReleasedFilmNameBorRsp} from "@/scripts/ws/Statistics/getReleasedFilmNameBor"
import {
  FilmActorBoxOfficeRsp,
  getReleasedFilmActorBor,
  GetReleasedFilmActorBorRsp
} from "@/scripts/ws/Statistics/getReleasedFilmActorBor"

const router = useRouter()
const statistics_items = ref(['按影片', '按类型', '按主演'])
const statistics_values = ref('按影片')

const actorBor = ref<GetReleasedFilmActorBorRsp>(
    {
      Collection: []
    }
)
const typeBor = ref<GetReleasedFilmTypeBorRsp>(
    {
      Collection: []
    }
)
const nameBor = ref<GetReleasedFilmNameBorRsp>(
    {
      Collection: []
    }
)

function getBor() {
  if (statistics_values.value === '按影片') {
    return nameBor.value
  }
  if (statistics_values.value === '按类型') {
    return typeBor.value
  }
  if (statistics_values.value === '按主演') {
    return actorBor.value
  }
}

onMounted(async () => {
  actorBor.value = (await getReleasedFilmActorBor({}))
  nameBor.value = (await getReleasedFilmNameBor({}))
  typeBor.value = (await getReleasedFilmTypeBor({}))
})

</script>

<style lang="stylus" scoped>

</style>