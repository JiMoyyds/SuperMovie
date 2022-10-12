<template>
  <div>

    <div class="holder mt-5">

      <v-chip
          size="x-large"
          color="primary"
          prepend-icon="mdi-clock-outline"
      >
        订单已被创建，请在 2 小时内完成支付.
      </v-chip>

      <v-chip
          size="x-large"
          color="red"
          class="mt-2"
          prepend-icon="mdi-alert-octagon-outline"
      >
        在支付完成前，请不要关闭该页面.
      </v-chip>

      <v-divider class="my-4"></v-divider>

      <v-chip color="secondary" class="mb-6">
        <h3>
          订单号：{{ payment_id }}
        </h3>
      </v-chip>

      <div style="display: flex;justify-content:space-around">
        <v-card rounded>
          <div style="display: flex;flex-direction: column">
            <v-chip color="primary" style="margin: auto" class="mt-2">
              订单二维码
            </v-chip>
            <canvas ref="order_id_qrcode_area"></canvas>
          </div>
        </v-card>

        <v-card rounded>
          <div style="display: flex;flex-direction: column">
            <v-chip color="primary" style="margin: auto" class="mt-2">
              支付宝支付二维码
            </v-chip>
            <canvas ref="alipay_qrcode_path_qrcode_area"></canvas>
          </div>
        </v-card>
      </div>
      <v-divider class="my-4"></v-divider>
      <div class="foot">
        <v-btn
            class="go_to_my_orders_btn"
            color="primary"
            prepend-icon="mdi-printer"
            @click="router.push('/ticket_printing/'+payment_id)"
        >
          入场凭据
        </v-btn>
        <v-btn
            class="go_to_my_orders_btn"
            color="primary"
            prepend-icon="mdi-keyboard-return"
            @click="router.push('/recent_orders')"
        >
          最近订单
        </v-btn>
      </div>
    </div>

  </div>
</template>

<script lang="ts" setup>
import QRCode from 'qrcode'
import {onMounted, ref} from "vue"
import {useRouter} from "vue-router"
import {b64_to_utf8} from "@/scripts/ws/helper"

const router = useRouter()
const props =
    defineProps<{
      payment_id: bigint
      alipay_qrcode_path: string
    }>()

const order_id_qrcode_area = ref()
const alipay_qrcode_path_qrcode_area = ref()

function setQr() {
  QRCode.toCanvas(
      order_id_qrcode_area.value,
      String(props.payment_id)
  )
  QRCode.toCanvas(
      alipay_qrcode_path_qrcode_area.value,
      String(b64_to_utf8(props.alipay_qrcode_path))
  )
}


onMounted(async () => {
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