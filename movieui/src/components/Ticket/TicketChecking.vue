<template>

  <div class="holder">

    <div
        class="qr-reader-box"
        id="qr-reader"
    />

    <span style="margin:auto" v-text="_decodedText"/>

    <v-icon
        v-if="!pass"
        style="margin:auto"
        class="qr-icon mb-5"
        icon="mdi-qrcode-scan"
        color="primary"
    />

    <v-chip
        v-else
        class="my-2"
        size="x-large"
        color="green"
        style="margin:auto"
    >
      <div class="title">
        检票通过! 观影愉快
      </div>
    </v-chip>

  </div>

</template>

<script lang="ts" setup>

import {onMounted, ref} from "vue"
import {Html5Qrcode} from 'html5-qrcode'
import {Html5QrcodeScanner} from 'html5-qrcode'
import {useRouter} from "vue-router"
import {isOrderIdCanCheckIn} from "@/scripts/ws/Order/isOrderCanCheckIn"

const router = useRouter()
const showQrIcon = ref(false)
const pass = ref(false)
const _decodedText = ref('_')

async function onScanSuccess(decodedText: string, decodedResult: unknown) {
  _decodedText.value = decodedText
  const ok = await isOrderIdCanCheckIn({OrderId: BigInt(decodedText)})
  if (ok.Yes) {
    pass.value = true
  } else {
    pass.value = false
  }
}

function onScanFailure(error: string) {
  _decodedText.value = '_'
  pass.value = false
}

onMounted(() => {
  new Html5QrcodeScanner(
      "qr-reader",
      <any>{
        fps: 2,
        qrbox: {width: 300, height: 300},
        formatsToSupport: 0
      },
      false
  ).render(onScanSuccess, onScanFailure)
})

</script>

<style lang="stylus" scoped>

.holder
  display flex
  flex-direction column

.title
  font-size 3rem
  padding 1rem
  margin 1rem

.qr-icon
  font-size 5rem

.qr-reader-box
  width 750px
  margin auto
  margin-bottom 20px

</style>
