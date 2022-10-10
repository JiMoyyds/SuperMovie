<template>

  <div class="holder">

    <div
        class="qr-reader-box"
        id="qr-reader"
    />

    <span style="margin:auto" v-text="_decodedText"/>

    <v-icon
        style="margin:auto"
        class="qr-icon mb-5"
        icon="mdi-qrcode-scan"
        color="primary"
    />

    <v-chip
        class="my-2"
        size="x-large"
        color="green"
        style="margin:auto"
    >
      <div class="title">
        检票通过! 观影愉快
      </div>
    </v-chip>
    <v-chip
        class="my-2"
        size="x-large"
        color="red"
        style="margin:auto"
    >
      <div class="title">
        检票失败! 订单无效
      </div>
    </v-chip>

  </div>

</template>

<script lang="ts" setup>

import {onMounted, ref} from "vue"
import {Html5Qrcode} from 'html5-qrcode'
import {Html5QrcodeScanner} from 'html5-qrcode'
import {useRouter} from "vue-router"

const router = useRouter()
const showQrIcon = ref(false)
const _decodedText = ref('_')

function onScanSuccess(decodedText: string, decodedResult: unknown) {
  _decodedText.value = decodedText
}

function onScanFailure(error: string) {
  _decodedText.value = '_'
}

onMounted(() => {
  new Html5QrcodeScanner(
      "qr-reader",
      <any>{
        fps: 2,
        qrbox: {width: 300, height: 300},
        formatsToSupport: 0
      },
      false,
  ).render(onScanSuccess, onScanFailure);
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
