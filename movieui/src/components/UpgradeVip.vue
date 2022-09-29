<template>

  <v-card class="mb-1">
    <v-chip
        color="green"
        size="x-large"
        variant="text"
    >
      VIP1
    </v-chip>
    <v-card-text>
      <h1>
        30元/月
        <v-chip color="green">
          享受9折结算优惠
        </v-chip>
      </h1>
    </v-card-text>
    <v-card-actions>
      <v-btn color="primary">申请退费</v-btn>
      <v-btn color="primary">立即充值</v-btn>
    </v-card-actions>
  </v-card>

  <v-card class="mb-1">
    <v-chip
        color="orange"
        size="x-large"
        variant="text"
    >
      VIP2
    </v-chip>
    <v-card-text>
      <h1>
        60元/月
        <v-chip color="orange">
          享受8折结算优惠
        </v-chip>
      </h1>
    </v-card-text>
    <v-card-actions>
      <v-btn color="primary">申请退费</v-btn>
      <v-btn color="primary">立即充值</v-btn>
    </v-card-actions>
  </v-card>

  <v-card class="mb-1">
    <v-chip
        color="purple"
        size="x-large"
        variant="text"
    >
      VIP3
    </v-chip>
    <v-card-text>
      <h1>
        120元/月
        <v-chip color="purple">
          享受7折结算优惠
        </v-chip>
      </h1>
    </v-card-text>
    <v-card-actions>
      <v-btn color="primary">申请退费</v-btn>
      <v-btn color="primary">立即充值</v-btn>
    </v-card-actions>
  </v-card>

</template>

<script lang="ts" setup>

defineProps<{
  user_id: number
}>()

</script>

<style lang="stylus" scoped>

</style>