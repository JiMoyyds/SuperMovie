<template>
  <div>

    <div class="holder">

      <h3>电影名称1</h3>

      <v-divider class="my-4"></v-divider>

      <h4 class="center">
        座位：{{ payment_film_info.seat }}
      </h4>
      <h4 class="center">
        观影日期：{{ payment_film_info.show_time }}
      </h4>
      <h4 class="center">
        场次：{{ payment_film_info.screening }}
      </h4>

      <h4 class="center">
        订单二维码
      </h4>

      <div class="center">
        <canvas ref="qrcode_area"/>
      </div>

      <v-divider class="my-4"></v-divider>

    </div>

  </div>
</template>

<script lang="ts" setup>
import QRCode from 'qrcode'
import {onMounted, ref} from "vue"

const payment_id = ref(1145141919810)
const payment_film_info = ref({
  name: '电影名称',
  seat: '12排13列',
  show_time: '2022 11月11日',
  screening: '19:00-21:00'
})

const qrcode_area = ref()

function setQr() {
  QRCode.toCanvas(qrcode_area.value, String(payment_id.value))
}

onMounted(() => {
  setQr()
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
