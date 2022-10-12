<template>

  <v-card class="mt-2">
    <v-card-title>
      <v-chip variant="text">订单号 {{ order_id }}</v-chip>
      <v-chip color="primary">
        订单状态 {{ OrderStatus }}
      </v-chip>
    </v-card-title>
    <v-card-text>
      <v-chip class="mx-2 mt-2">
        用户 {{ user_id }}
      </v-chip>
      <v-chip class="mx-2 mt-2">
        支付金额 {{ pay_amount }}
      </v-chip>
      <v-chip class="mx-2 mt-2">
        订单时间 {{ order_time.toString() }}
      </v-chip>
      <v-chip class="mx-2 mt-2">
        电影名称 {{ FilmName }}
      </v-chip>
      <v-chip class="mx-2 mt-2">
        影厅 {{ CinemaName }}
      </v-chip>
      <v-chip class="mx-2 mt-2">
        场次 {{ ScheduleStartTime.toString() }} to {{ ScheduleEndTime.toString() }}
      </v-chip>
      <v-chip class="mx-2 mt-2">
        座位 {{ CinemaSeat }}
      </v-chip>
    </v-card-text>
    <v-card-actions>
      <v-btn
          class="mx-4" color="primary"
          prepend-icon="mdi-printer"
          @click="router.push('/ticket_printing/'+order_id)"
      >
        入场凭据
      </v-btn>
      <v-btn class="mx-4" color="primary"
             v-if="IsUserAdmin"
             @click="refund(order_id)"
      >
        退票
      </v-btn>
    </v-card-actions>
  </v-card>

</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"
import {IsUserAdmin} from "@/scripts/state/user"
import {onMounted, ref} from "vue"
import {getAllOrderWithUserId} from "@/scripts/ws/Order/getAllOrderWithUserId"
import {getFilm} from "@/scripts/ws/Film/getFilm"
import {getCinema} from "@/scripts/ws/Cinema/getCinema"
import {getSchedule} from "@/scripts/ws/Schedule/getSchedule"
import {getOrder} from "@/scripts/ws/Order/getOrder"
import {refundOrder} from "@/scripts/ws/Order/refundOrder"

const router = useRouter()

const emits = defineEmits<{
  (e: 'refund'): void
}>()

const props =
    defineProps<{
      order_id: bigint,
      user_id: bigint,
      pay_amount: number,
      order_time: Date,
      film_id: bigint,
      cinema_id: bigint,
      screening_id: bigint,
      seat: string
    }>()

const OrderStatus = ref('')
const FilmName = ref('')
const CinemaName = ref('')
const CinemaSeat = ref('')
const ScheduleStartTime = ref(new Date())
const ScheduleEndTime = ref(new Date())

onMounted(async () => {
  FilmName.value = (await getFilm({FilmId: props.film_id})).FilmName
  const cinema = await getCinema({CinemaId: props.cinema_id})
  CinemaName.value = cinema.CinemaName
  const order = await getOrder({OrderId: props.order_id})
  CinemaSeat.value = order.OrderSeat
  const schedule = await getSchedule({ScheduleId: props.screening_id})
  OrderStatus.value = order.OrderStatus
  ScheduleStartTime.value = schedule.ScheduleStartTime
  ScheduleEndTime.value = schedule.ScheduleEndTime
})


async function refund(order_id: bigint) {
  const rsp = await refundOrder({OrderId: order_id})
  if (rsp.Ok) {
    emits('refund')
  }
  FilmName.value = (await getFilm({FilmId: props.film_id})).FilmName
  const cinema = await getCinema({CinemaId: props.cinema_id})
  CinemaName.value = cinema.CinemaName
  const order = await getOrder({OrderId: props.order_id})
  CinemaSeat.value = order.OrderSeat
  const schedule = await getSchedule({ScheduleId: props.screening_id})
  OrderStatus.value = order.OrderStatus
  ScheduleStartTime.value = schedule.ScheduleStartTime
  ScheduleEndTime.value = schedule.ScheduleEndTime
}

</script>

<style lang="stylus" scoped>

</style>