# Tasks & Todos

## table :: super_movie.default_schema.cinema

- [ ] 创建
- [ ] 穷举

## table :: super_movie.default_schema.film

- [ ] 穷举
- [ ] 根据是否预售过滤
- [ ] 根据影片类型过滤
- [ ] 根据上映时间过滤
- [ ] 根据影片名过滤
- [ ] 根据主演过滤
- [ ] 以film_id为主键，对其他字段进行修改

## table :: super_movie.default_schema.order

- [ ] 创建
- [ ] 穷举
- [ ] 根据用户过滤
- [ ] 校验订单合法性(通过比对order_id一致性)
- [ ] 根据film_id过滤
- [ ] 根据影片类型过滤(通过JOIN film表)
- [ ] 根据影片主演过滤(通过JOIN film表)

## table :: super_movie.default_schema.schedule

- [ ] 创建
- [ ] 穷举
- [ ] 根据影厅过滤
- [ ] 根据场次过滤(通过比较目标时间段是否是始末时间的子集)

## table :: supser_movie.default.schema.user

- [ ] 注册
- [ ] 登陆校验(通过比对user_pwd_hash一致性)
- [ ] 修改密码(替换user_pwd_hash即可)
- [ ] 上升到指定vip等级
- [ ] 下降到指定vip等级
- [ ] 获取当前vip的折扣(通过JOIN vip表)

## table ::  super_movie.default_schema.vip

- [ ] 创建
- [ ] 穷举
- [ ] 根据用户修改
