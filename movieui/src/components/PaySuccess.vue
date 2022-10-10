<template>
  <div>

    <div class="holder">

      <h3>等待支付 or 支付完成</h3>


      <v-divider class="my-4"></v-divider>

      <v-chip variant="text">
        <h3>
          订单号：{{ payment_id }}
        </h3>
      </v-chip>

      <v-chip variant="text">
        订单二维码
      </v-chip>

      <canvas ref="qrcode_area"></canvas>

      <v-divider class="my-4"></v-divider>

      <div class="foot">
        <v-btn
            class="go_to_my_orders_btn"
            color="primary"
            prepend-icon="mdi-printer"
            @click="router.push('/ticket_printing/'+payment_id)"
        >
          打印电影票
        </v-btn>
        <v-btn
            class="go_to_my_orders_btn"
            color="primary"
            prepend-icon="mdi-keyboard-return"
        >
          我的订单
        </v-btn>
      </div>
    </div>

  </div>
</template>

<script lang="ts" setup>
import QRCode from 'qrcode'
import {onMounted, ref} from "vue"
import {useRouter} from "vue-router"

const router = useRouter()
defineProps<{
  payment_id: number
}>()

const payment_id = ref(1145141919810)

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

.go_to_my_orders_btn
  width 30%
  float right

.foot
  display flex
  justify-content space-between

</style>