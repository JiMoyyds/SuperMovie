import {createRouter, createWebHistory} from "vue-router"

import Home from "@/components/Home.vue"
import Login from "@/components/Login.vue"
import Search from "@/components/Search.vue"
import Booking from "@/components/Booking/Booking.vue"
import Register from "@/components/Register.vue"
import UserInfo from "@/components/UserInfo.vue"
import ResetPwd from "@/components/ResetPwd.vue"
import PaySuccess from "@/components/PaySuccess.vue"
import UpgradeVip from "@/components/UpgradeVip.vue"
import FilmEditor from "@/components/Film/FilmEditor.vue"
import RecentOrders from "@/components/Order/RencentOrders.vue"
import CinemaCreator from "@/components/Cinema/CinemaCreator.vue"
import TicketPrinting from "@/components/Ticket/TicketPrinting.vue"
import FilmManagement from "@/components/Film/FilmManagement.vue"
import ScheduleEditor from "@/components/Cinema/ScheduleEditor.vue"
import TicketChecking from "@/components/Ticket/TicketChecking.vue"
import CinemaManagement from "@/components/Cinema/CinemaManagement.vue"
import BoxOfficeStatistics from "@/components/BoxOfficeStatistics.vue"

export default createRouter({
    scrollBehavior: () => ({
        top: 0,
        behavior: 'smooth'
    }),
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: Home,
            props: {}
        },
        {
            path: '/login',
            component: Login,
            props: {}
        },
        {
            path: '/register',
            component: Register,
            props: {}
        },
        {
            path: '/search',
            component: Search,
            props: {}
        },
        {
            path: '/booking/:film_id',
            component: Booking,
            props: r => ({
                filmId: BigInt(r.params.film_id.toString())
            })
        },
        {
            path: '/pay_success/:payment_id/:alipay_qrcode_path',
            component: PaySuccess,
            props: r => ({
                payment_id: BigInt(r.params.payment_id.toString()),
                alipay_qrcode_path: r.params.alipay_qrcode_path
            })
        },
        {
            path: '/ticket_printing/:order_id',
            component: TicketPrinting,
            props: r => ({
                order_id: BigInt(r.params.order_id.toString())
            })
        },
        {
            path: '/film_management',
            component: FilmManagement,
            props: {}
        },
        {
            path: '/film_editor/create',
            component: FilmEditor,
            props: {
                film_id: 0n,
                create_film: true
            }
        },
        {
            path: '/film_editor/:film_id',
            component: FilmEditor,
            props: r => ({
                film_id: BigInt(r.params.film_id.toString()),
                create_film: false
            })
        },
        {
            path: '/cinema_management',
            component: CinemaManagement,
            props: {}
        },
        {
            path: '/schedule_editor/:cinema_id',
            component: ScheduleEditor,
            props: r => ({
                cinema_id: BigInt(r.params.cinema_id.toString())
            })
        },
        {
            path: '/user_info/:user_id',
            component: UserInfo,
            props: r => ({
                user_id: BigInt(r.params.user_id.toString())
            })
        },
        {
            path: '/reset_pwd/:user_id',
            component: ResetPwd,
            props: r => ({
                user_id: BigInt(r.params.user_id.toString())
            })
        },
        {
            path: '/upgrade_vip/:user_id',
            component: UpgradeVip,
            props: r => ({
                user_id: BigInt(r.params.user_id.toString())
            })
        },
        {
            path: '/box_office_statistics',
            component: BoxOfficeStatistics
        },
        {
            path: '/recent_orders/:user_id',
            component: RecentOrders,
            props: r => ({
                user_id: BigInt(r.params.user_id.toString())
            })
        },
        {
            path: '/ticket_checking',
            component: TicketChecking
        }
    ]
})
