<template>

  <OrderCard
      class="mb-2"
      v-for="order in order_list"

      :order_id="order.order_id"
      :user_id="order.user_id"
      :pay_amount="order.pay_amount"
      :order_time="order.order_time"
      :film="order.film"
      :cinema="order.cinema"
      :screening="order.screening"
      :seat="order.seat"
  />

</template>

<script lang="ts" setup>

import {ref} from "vue"
import OrderCard from "@/components/Order/OrderCard.vue"

defineProps<{
  user_id: number
}>()

const order_list = ref([
  {
    order_id: 1145141919810,
    user_id: 1231231,
    pay_amount: 30,
    order_time: '2022年11月15日 23:00',
    film: '星际穿越',
    cinema: '影厅1',
    screening: '9:00-12:00',
    seat: '2排13列'
  },
  {
    order_id: 14119810,
    user_id: 12231,
    pay_amount: 39,
    order_time: '2022年11月15日 23:00',
    film: '星际穿越',
    cinema: '影厅1',
    screening: '9:00-12:00',
    seat: '2排13列'
  },
  {
    order_id: 114549810,
    user_id: 12331,
    pay_amount: 32,
    order_time: '2022年11月15日 23:00',
    film: '星际穿越',
    cinema: '影厅1',
    screening: '9:00-12:00',
    seat: '2排13列'
  }
])

</script>

<style lang="stylus" scoped>

</style>