<template>
  <div>

    <v-card
        class="mx-auto mt-10"
        max-width="500"
    >
      <v-card-text>
        <div>{{ order_id }}</div>
        <v-row align="center" hide-gutters>
          <v-col
              class="text-h3"
              cols="9"
          >
            {{ OrderFilmName }}
          </v-col>

          <v-col cols="3" class="text-right">
            <canvas ref="qrcode_area"/>
          </v-col>
        </v-row>
        <p> {{ OrderCinemaName }} 厅 / {{ OrderSeat }}</p>
        <div class="text--primary">
          <h4>FROM : {{ OrderScheduleStartTime }}</h4>
          <h4>TO  : {{ OrderScheduleEndTime }}</h4>
        </div>
      </v-card-text>

    </v-card>

  </div>
</template>

<script lang="ts" setup>
import QRCode from 'qrcode'
import {onMounted, ref} from "vue"
import {useRouter} from "vue-router"
import {getOrder} from "@/scripts/ws/Order/getOrder"
import {getFilm} from "@/scripts/ws/Film/getFilm"
import {getCinema} from "@/scripts/ws/Cinema/getCinema"
import {getSchedule} from "@/scripts/ws/Schedule/getSchedule"

const props =
    defineProps<{
      order_id: bigint
    }>()

const router = useRouter()
const qrcode_area = ref()

const OrderFilmName = ref('')
const OrderSeat = ref('')
const OrderCinemaName = ref('')
const OrderScheduleStartTime = ref(new Date())
const OrderScheduleEndTime = ref(new Date())

function setQr() {
  QRCode.toCanvas(qrcode_area.value, props.order_id.toString())
}

onMounted(async () => {
  setQr()

  const order = await getOrder({OrderId: props.order_id})
  const film = await getFilm({FilmId: order.OrderFilmId})
  const cinema = await getCinema({CinemaId: order.OrderCinemaId})
  const schedule = await getSchedule({ScheduleId: order.OrderScheduleId})

  OrderFilmName.value = film.FilmName
  OrderSeat.value = order.OrderSeat
  OrderCinemaName.value = cinema.CinemaName
  OrderScheduleStartTime.value = schedule.ScheduleStartTime
  OrderScheduleEndTime.value = schedule.ScheduleEndTime
})

</script>

<style lang="stylus" scoped>

.holder
  margin-left auto
  margin-right auto
  width 50%

  > *
    width 100%

.center
  width fit-content
  margin auto

</style>
