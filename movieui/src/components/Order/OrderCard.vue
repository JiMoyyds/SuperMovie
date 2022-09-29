<template>

  <v-card>
    <v-card-title>
      <v-chip variant="text">订单号 {{ order_id }}</v-chip>
    </v-card-title>
    <v-card-text>
      <v-chip variant="text">
        用户 {{ user_id }}
      </v-chip>
      <v-chip variant="text">
        支付金额 {{ pay_amount }}
      </v-chip>
      <v-chip variant="text">
        订单时间 {{ order_time }}
      </v-chip>
      <v-chip variant="text">
        电影名称 {{ film }}
      </v-chip>
      <v-chip variant="text">
        影厅 {{ cinema }}
      </v-chip>
      <v-chip variant="text">
        场次 {{ screening }}
      </v-chip>
      <v-chip variant="text">
        座位 {{ seat }}
      </v-chip>
    </v-card-text>
    <v-card-actions>
      <v-btn color="primary">
        退票
      </v-btn>
    </v-card-actions>
  </v-card>

</template>

<script lang="ts" setup>

defineProps<{
  order_id: number,
  user_id: number,
  pay_amount: number,
  order_time: string,
  film: string,
  cinema: string,
  screening: string,
  seat: string
}>()

</script>

<style lang="stylus" scoped>

</style>