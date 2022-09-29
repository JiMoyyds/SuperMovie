<template>
  <div>

    <v-card>
      <v-card-title>
        影片编辑
      </v-card-title>
      <v-card-text>
        <v-text-field label="电影名"/>
        <v-text-field label="封面URL"/>
        <v-text-field label="预告片URL"/>
        <v-text-field label="票价"/>

        <v-switch
            label="预售模式"
            color="primary"
            v-model="enable_pre_booking"
        />
        <v-btn
            v-if="!create_film"
            prepend-icon="mdi-content-save"
            style="float:right"
            color="primary"
            class="mb-4"
            @click="$router.push('/film_management')"
        >
          保存
        </v-btn>
        <v-btn
            v-else
            prepend-icon="mdi-plus-circle"
            style="float:right"
            color="primary"
            class="mb-4"
            @click="$router.push('/film_management')"
        >
          新增
        </v-btn>
      </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts" setup>

import {ref} from "vue";

withDefaults(defineProps<{
  film_id: number,
  create_film: boolean,
}>(), {
  create_film: false
})

const enable_pre_booking = ref(false)

</script>

<style lang="stylus" scoped>

</style>