<template>
  <div>

    <h3>电影1 订单编辑</h3>
    <v-divider class="my-4"></v-divider>
    <h3 class="mb-4">影厅选择</h3>
    <v-select
        :items="cinema_items"
        label="影厅"
    />
    <v-divider class="my-4"></v-divider>
    <h3 class="mb-4">场次选择</h3>
    <v-select
        :items="screening_items"
        label="场次"
    />
    <v-divider class="my-4"></v-divider>
    <h3>座位选择</h3>
    <div class="seats">
      <div class="screen_chip_holder mb-4">
        <v-chip class="screen_chip px-10" color="grey">银幕位置</v-chip>
      </div>
      <div class="seats_col" v-for="row_col in seats_row_cols">
        <v-icon
            v-for="(_,col_index) in Array(row_col[1])"
            icon="mdi-sofa-single"
            :style="{
              'margin-left':sqrt(row_col[0])+'px',
              'margin-right':sqrt(row_col[0])+'px',
            }"
            :color="selected_seat_col_row[0]===col_index+1&&selected_seat_col_row[1]===row_col[0]?'red':'green'"
            @click="selected_seat_col_row=[col_index+1,row_col[0]]"
        />
      </div>
    </div>

    <v-divider class="my-4"></v-divider>
    <div class="footage">
      <v-chip
          class="discount_message"
          color="orange"
      >
        您是会员L1用户，已享受9折优惠
      </v-chip>

      <v-chip
          color="red"
          size="large"
          class="pay_price"
      >
        32元
      </v-chip>

      <v-btn
          class="pay-btn"
          color="primary"
          @click="$router.push('/pay_success/114514')"
      >
        支付订单
      </v-btn>
    </div>
  </div>
</template>

<script lang="ts" setup>

defineProps<{
  filmId: number
}>()

import {Ref, ref, watch} from "vue";

function sqrt(x: number) {
  return Math.sqrt(x)
}

const cinema_items = ref(['影厅1', '影厅2', '影厅3', '影厅4', '影厅5'])
const screening_items = ref(['9:00', '11:00', '13:00', '15:00', '18:00', '20:00'])
const seats_row_cols =
    ref<[number, number][]>([
      [1, 9],
      [2, 10],
      [3, 11],
      [4, 12],
      [5, 13],
      [6, 14],
      [7, 15],
      [8, 16],
      [9, 17],
      [10, 18],
      [11, 19],
      [12, 20],
    ])

const selected_seat_col_row =
    ref<[number, number]>([0, 0])

</script>

<style lang="stylus" scoped>

.seats
  width 100%

.screen_chip_holder
  width 100%
  display flex
  justify-content center

.screen_chip
  margin-left auto
  margin-right auto

.seats_col
  width fit-content
  margin-left auto
  margin-right auto

.footage
  display flex
  justify-content end

.discount_message
  margin-right auto

.pay_price
  font-weight 600
  margin-right 10px

.pay-btn
  width 20%

</style>