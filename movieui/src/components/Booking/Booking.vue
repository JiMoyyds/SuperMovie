<template>
  <div>

    <h2>购票 {{ FilmName }} 订单编辑</h2>
    <v-divider class="my-4"></v-divider>
    <h3 class="mb-4">影厅选择</h3>
    <v-select
        v-model="SelectedCinemaId"
        :items="AvailableCinemas"
        item-value="CinemaId"
        item-title="CinemaName"
        label="影厅"
    />
    <v-divider class="my-4"></v-divider>
    <h3 class="mb-4">场次选择</h3>
    <v-select
        :items="SchedulesInSelectedCinema"
        v-model="SelectedSchedule"
        item-title="ScheduleTimeText"
        item-value="ScheduleId"
        label="场次"
    />
    <v-divider class="my-4"></v-divider>
    <h3>座位选择
      <v-chip>{{ selectedSeat() }}</v-chip>
    </h3>
    <div class="seats">
      <div class="screen_chip_holder mb-4">
        <v-chip class="screen_chip px-2" color="grey">首列&emsp;&emsp;&emsp;&emsp;银幕位置&emsp;&emsp;&emsp;&emsp;末列</v-chip>
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
        您是会员Level {{ UserVipLevel }}用户，已享受{{ UserVipDiscount * 10 }}折优惠
      </v-chip>

      <v-chip
          color="red"
          size="large"
          class="pay_price"
          v-if="UserVipDiscount<1"
      >
        <strike>原价 {{ FilmPrice }} 元</strike>
      </v-chip>
      <v-chip
          color="green"
          size="large"
          class="pay_price"
      >
        实付 {{ UserVipDiscount * FilmPrice }} 元
      </v-chip>

      <v-btn
          class="pay-btn"
          color="primary"
          @click="create()"
      >
        创建订单
      </v-btn>
    </div>
  </div>
</template>

<script lang="ts" setup>

import {useRouter} from "vue-router"
import {getAllAvailableCinemaByFilmId} from "@/scripts/ws/Cinema/getAllAvailableCinemaByFilmId"
import {ScheduleRsp} from "@/scripts/ws/Schedule/getAllScheduleByCinemaId"
import {getAllScheduleByCinemaId} from "@/scripts/ws/Schedule/getAllScheduleByCinemaId"
import {UserVipDiscount, UserId, UserVipLevel} from "@/scripts/state/user"

const props =
    defineProps<{
      filmId: bigint
    }>()

const FilmName = ref('')
const FilmPrice = ref(0.0)
const AvailableCinemas = ref<CinemaRsp[]>([])
const SelectedCinemaId = ref<bigint>()
const AvailableSchedules = ref<ScheduleRsp[]>([])
const SelectedSchedule = ref<ScheduleRsp>()
const SchedulesInSelectedCinema = ref<{
  ScheduleId: bigint,
  ScheduleTimeText: string
}[]>([])

watch(SelectedCinemaId, _ => {
  if (SelectedCinemaId.value !== undefined) {
    for (const schedule of AvailableSchedules.value) {
      if (schedule.ScheduleCinemaId === SelectedCinemaId.value) {
        SchedulesInSelectedCinema.value.push({
          ScheduleId: schedule.ScheduleId,
          ScheduleTimeText:
              `${schedule.ScheduleStartTime.toString()} to ${schedule.ScheduleEndTime.toString()}`
        })
      }
    }
  }
})

import {onMounted, Ref, ref, watch} from "vue"
import {CinemaRsp} from "@/scripts/ws/Cinema/getAllCinema"
import {getFilm} from "@/scripts/ws/Film/getFilm"
import {createOrder} from "@/scripts/ws/Order/createOrder"
import {HTMLCanvasElementLuminanceSource} from "html5-qrcode/third_party/zxing-js.umd"

const router = useRouter()

function sqrt(x: number) {
  return Math.sqrt(x)
}

onMounted(async () => {
  const film = await getFilm({FilmId: props.filmId})
  FilmName.value = film.FilmName
  FilmPrice.value = film.FilmPrice

  AvailableCinemas.value =
      (await getAllAvailableCinemaByFilmId(
          {FilmId: props.filmId}
      )).Collection

  for (const cinema of AvailableCinemas.value) {
    const schedules =
        (await getAllScheduleByCinemaId({
          CinemaId: cinema.CinemaId
        })).Collection
    AvailableSchedules.value =
        AvailableSchedules.value.concat(schedules)
  }
})

const selected_seat_col_row =
    ref<[number, number]>([0, 0])

function selectedSeat() {
  return `${selected_seat_col_row.value[1]}排${selected_seat_col_row.value[0]}列`
}

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
      [12, 20]
    ])

async function create() {

  if (SelectedCinemaId.value !== undefined &&
      //@ts-ignore
      SelectedSchedule.value !== undefined &&
      selectedSeat() !== '0排0列'
  ) {
    const order = (await createOrder({
      OrderFilmId: props.filmId,
      OrderUserId: UserId.value,
      OrderCinemaId: SelectedCinemaId.value,
      //@ts-ignore
      OrderScheduleId: SelectedSchedule.value,
      OrderSeat: selectedSeat(),
      OrderPayAmount: UserVipDiscount.value * FilmPrice.value
    }))
    if (order.Ok)
      await router.push('/pay_success/' + order.OrderId)
  }
}
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