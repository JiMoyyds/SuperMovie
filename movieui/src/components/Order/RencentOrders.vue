<template>

  <OrderCard
      class="mb-2"
      v-for="order in UserOrders"

      :order_id="order.OrderId"
      :user_id="order.OrderUserId"
      :pay_amount="order.OrderPayAmount"
      :order_time="order.OrderTime"
      :film_id="order.OrderFilmId"
      :cinema_id="order.OrderCinemaId"
      :screening_id="order.OrderScheduleId"
      :seat="order.OrderSeat"
      @refund="reload()"
  />

</template>

<script lang="ts" setup>

import {onMounted, ref} from "vue"
import OrderCard from "@/components/Order/OrderCard.vue"
import {useRouter} from "vue-router"
import {OrderRsp} from "@/scripts/ws/Order/getAllOrder"
import {getAllOrderWithUserId} from "@/scripts/ws/Order/getAllOrderWithUserId"
import {getAllOrder} from "@/scripts/ws/Order/getAllOrder"

const router = useRouter()

const props =
    defineProps<{
      user_id: bigint
    }>()

const UserOrders = ref<OrderRsp[]>([])

async function reload() {
  if (props.user_id < 1000n) {
    UserOrders.value =
        (await getAllOrder({})).Collection
  } else {
    UserOrders.value =
        (await getAllOrderWithUserId({
          OrderUserId: props.user_id
        })).Collection
  }
}


onMounted(async () => {
  await reload()
})

</script>

<style lang="stylus" scoped>

</style>