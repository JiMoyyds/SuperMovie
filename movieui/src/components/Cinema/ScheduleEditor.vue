<template>
  <div>

    <div style="display:flex">
      <v-chip
          size="x-large"
          class="mb-2 mx-auto"
          color="primary"
          prepend-icon="mdi-theater"
      >
        <div>
          {{ cinema_name }}
        </div>
      </v-chip>
    </div>

    <v-card>
      <v-card-title>
        <div
            style="font-size:1.5rem"
            class="mb-3"
        >
          新增排厅
        </div>
      </v-card-title>

      <v-card-text>
        <v-text-field label="电影ID"/>
        <v-text-field label="放映开始"/>
        <v-text-field label="放映结束"/>

        <v-btn
            class="float-right"
            prepend-icon="mdi-plus"
            color="primary mb-4"
        >
          新增排厅
        </v-btn>
      </v-card-text>

    </v-card>

    <v-card
        class="my-5"
        variant="text"
    >
      <v-card-title>
        <div
            style="font-size:1.5rem"
            class="mb-3"
        >
          当前排厅
        </div>
      </v-card-title>

      <v-card-text>

        <v-card
            class="mb-5"
            v-for="item in schedule_list"
        >
          <v-card-title>
            {{ item.film_name }}
          </v-card-title>
          <v-card-text>
            <div style="display:flex">
              <div style="margin-right:auto">
                <v-chip class="mr-2" color="primary">
                  放映开始：{{ item.start_time }}
                </v-chip>
                <v-chip color="primary">
                  放映结束：{{ item.end_time }}
                </v-chip>
              </div>
              <v-btn
                  color="primary"
              >
                删除
              </v-btn>
            </div>
          </v-card-text>

        </v-card>

      </v-card-text>
    </v-card>

  </div>
</template>

<script lang="ts" setup>

import {schedule_list} from "@/scripts/schedule_data"
import {ref} from "vue";

defineProps<{
  cinema_id: number,
  cinema_name: number
}>()

</script>

<style lang="stylus" scoped>

</style>
