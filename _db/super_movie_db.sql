--
-- PostgreSQL database dump
--

-- Dumped from database version 14.4
-- Dumped by pg_dump version 14.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'SQL_ASCII';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: default_schema; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA default_schema;


ALTER SCHEMA default_schema OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: cinema; Type: TABLE; Schema: default_schema; Owner: postgres
--

CREATE TABLE default_schema.cinema (
    cinema_id bigint,
    cinema_name text
);


ALTER TABLE default_schema.cinema OWNER TO postgres;

--
-- Name: film; Type: TABLE; Schema: default_schema; Owner: postgres
--

CREATE TABLE default_schema.film (
    film_id bigint,
    film_name text,
    film_online_time timestamp with time zone,
    film_is_preorder boolean,
    film_types text,
    film_price double precision,
    film_actors text,
    film_cover_url text,
    film_preview_video_url text
);


ALTER TABLE default_schema.film OWNER TO postgres;

--
-- Name: order; Type: TABLE; Schema: default_schema; Owner: postgres
--

CREATE TABLE default_schema."order" (
    order_id bigint,
    order_user_id bigint,
    order_pay_amount double precision,
    order_film_id bigint,
    order_cinema_id bigint,
    order_schedule_id bigint,
    order_cinema_seat text,
    order_time timestamp with time zone,
    order_status text
);


ALTER TABLE default_schema."order" OWNER TO postgres;

--
-- Name: schedule; Type: TABLE; Schema: default_schema; Owner: postgres
--

CREATE TABLE default_schema.schedule (
    schedule_cinema_id bigint,
    schedule_film_id bigint,
    schedule_start_time timestamp with time zone,
    schedule_end_time timestamp with time zone,
    schedule_id bigint
);


ALTER TABLE default_schema.schedule OWNER TO postgres;

--
-- Name: user; Type: TABLE; Schema: default_schema; Owner: postgres
--

CREATE TABLE default_schema."user" (
    user_id bigint,
    user_pwd_hash text,
    user_vip_level bigint,
    user_vip_level_expire_time timestamp with time zone
);


ALTER TABLE default_schema."user" OWNER TO postgres;

--
-- Name: vip; Type: TABLE; Schema: default_schema; Owner: postgres
--

CREATE TABLE default_schema.vip (
    vip_level bigint,
    vip_discount double precision,
    vip_month_price double precision
);


ALTER TABLE default_schema.vip OWNER TO postgres;

--
-- Data for Name: cinema; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema.cinema (cinema_id, cinema_name) FROM stdin;
25640864590069760	A
25640867704340480	B
25640869105238016	C
25674385305108480	D
\.


--
-- Data for Name: film; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema.film (film_id, film_name, film_online_time, film_is_preorder, film_types, film_price, film_actors, film_cover_url, film_preview_video_url) FROM stdin;
25673048138977280	小黄人大眼萌3	2023-10-11 17:01:01.812+08	t	喜剧	33		https://bkimg.cdn.bcebos.com/pic/30adcbef76094b367debb0a5a5cc7cd98d109d79?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UyMjA=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1TV4y147bL/?spm_id_from=333.337.search-card.all.click
25672607467573248	星际穿越	2022-10-11 16:52:54.811+08	f	科幻$烧脑	55	马修·麦康纳$安妮·海瑟薇	https://bkimg.cdn.bcebos.com/pic/0df3d7ca7bcb0a46a5a858b76d63f6246a60afb2?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UyMjA=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1Gq4y1e7zW/?spm_id_from=333.337.search-card.all.click
25640853188907008	雷神4	2022-10-11 00:30:20+08	t	科幻	233	不知道	https://bkimg.cdn.bcebos.com/pic/b90e7bec54e736d12f2edc0c980458c2d5628535c321?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UxODA=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1KZ4y1e7n6/?spm_id_from=333.337.search-card.all.click
25673136121843712	EVA：终	2022-10-11 17:02:07.755+08	f	动画	66		https://bkimg.cdn.bcebos.com/pic/d62a6059252dd42a2834f5667b694cb5c9ea14ce12f9?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UyNzI=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1Df4y1V7Ke/?spm_id_from=333.337.search-card.all.click&vd_source=1d3c20abbd901c51b997d416878ecfc0
25673554245718016	中国机长	2022-10-11 17:08:23.574+08	f	改编$传记	87	杜江$欧豪	https://bkimg.cdn.bcebos.com/pic/f703738da97739121c7cfa79f7198618367ae254?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UxMTY=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1aJ411q7HB/?spm_id_from=333.337.search-card.all.click
25673259039068160	万里归途	2022-10-11 17:03:40.02+08	t	战争$爱国	66	张译	https://bkimg.cdn.bcebos.com/pic/8c1001e93901213fb80e76a203b221d12f2eb9383f0a?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UxMTY=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1jD4y1y7Wg/?spm_id_from=333.337.search-card.all.click
25628416035262464	战狼2	2022-10-10 05:12:37+08	f	爱国$感恩	1	吴京	https://bkimg.cdn.bcebos.com/pic/8694a4c27d1ed21b84c524bea76eddc451da3f58?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UxODA=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1Qz4y1f7uP/?spm_id_from=333.337.search-card.all.click
25640856393355264	底线	2022-10-11 00:30:24+08	t	法制$反弹	2	不知道	https://bkimg.cdn.bcebos.com/pic/a5c27d1ed21b0ef41bd52ef5e59146da81cb39db9059?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UyNzI=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1td4y1i7ck/?spm_id_from=333.337.search-card.all.click
25633131652452352	死侍2	2022-10-09 14:27:35+08	t	喜剧	100	瑞安·雷诺兹	https://bkimg.cdn.bcebos.com/pic/3801213fb80e7becce07ef1e232eb9389a506b74?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UxNTA=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1VW411X7t3/?spm_id_from=333.337.search-card.all.click
25673437762555904	熊出没：重返地球	2022-10-11 17:06:59.01+08	f	卡通$儿童$喜剧$动画	34	熊大$熊二$光头强	https://bkimg.cdn.bcebos.com/pic/caef76094b36acaf2edd88264e8b9a1001e938013695?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UxODA=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1jv4y1N7oc/?spm_id_from=333.337.search-card.all.click
25673347356430336	天才游戏	2022-10-11 17:05:33.62+08	t	悬疑	88	定于系	https://bkimg.cdn.bcebos.com/pic/f703738da9773912b31bf76450509118367adab444d6?x-bce-process=image/watermark,image_d2F0ZXIvYmFpa2UyNzI=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV13y4y1176p/?spm_id_from=333.337.search-card.all.click
25672451582070784	独行月球	2022-10-11 16:51:42.621+08	f	喜剧	55	玛丽	https://bkimg.cdn.bcebos.com/pic/30adcbef76094b36acaf81790e986bd98d1001e93750?x-bce-process=image/resize,m_lfit,h_4096,limit_1/watermark,image_d2F0ZXIvYmFpa2U5MzM=,g_7,xp_5,yp_5	https://www.bilibili.com/video/BV1ka411Z7ka/?spm_id_from=333.337.search-card.all.click
\.


--
-- Data for Name: order; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema."order" (order_id, order_user_id, order_pay_amount, order_film_id, order_cinema_id, order_schedule_id, order_cinema_seat, order_time, order_status) FROM stdin;
25666020248133632	11451419198	163.1	25640853188907008	25640867704340480	25641009323978752	4排4列	2022-10-11 15:10:24.315489+08	refunded
25755526549741568	11451419198	38.5	25672607467573248	25674385305108480	25674675231141888	6排2列	2022-10-12 14:53:04.176926+08	created
25661756251578368	11451419198	163.1	25640853188907008	25640867704340480	25641009323978752	4排10列	2022-10-11 14:02:37.851371+08	created
25667100935725056	11451419198	163.1	25640853188907008	25640867704340480	25641014384406528	5排9列	2022-10-11 15:27:34.939972+08	paid
25674760707907584	11451419198	38.5	25672607467573248	25674385305108480	25674403371036672	5排3列	2022-10-11 17:29:19.867499+08	paid
25668151689617408	11451419198	163.1	25640853188907008	25640867704340480	25641014384406528	8排8列	2022-10-11 15:44:17.016546+08	paid
\.


--
-- Data for Name: schedule; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema.schedule (schedule_cinema_id, schedule_film_id, schedule_start_time, schedule_end_time, schedule_id) FROM stdin;
25640864590069760	25640853188907008	2022-10-11 08:30:41.457+08	2022-10-11 09:30:41.457+08	25640888427360256
25640864590069760	25640853188907008	2022-10-11 09:30:41.457+08	2022-10-11 10:30:41.457+08	25640894378029056
25640864590069760	25640853188907008	2022-10-11 10:30:41.457+08	2022-10-11 11:30:41.457+08	25640900475498496
25640867704340480	25640853188907008	2022-10-11 08:32:39.114+08	2022-10-11 09:32:39.114+08	25641004010844160
25640869105238016	25640853188907008	2022-10-11 08:33:02.267+08	2022-10-11 09:33:02.267+08	25641031891431424
25640867704340480	25640853188907008	2022-11-11 09:32:39.114+08	2022-11-11 10:32:39.114+08	25641009323978752
25640867704340480	25640853188907008	2022-09-11 14:32:39.114+08	2022-12-11 16:32:39.114+08	25641014384406528
25674385305108480	25672607467573248	2022-10-11 17:23:23.538+08	2022-10-11 18:23:23.538+08	25674403371036672
25674385305108480	25673048138977280	2022-11-11 17:27:51.682+08	2022-12-11 17:27:51.682+08	25674675231141888
\.


--
-- Data for Name: user; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema."user" (user_id, user_pwd_hash, user_vip_level, user_vip_level_expire_time) FROM stdin;
1	$2a$10$b8MdOsozRQlrWM4P9Vkq3.2T3pbN1GfJJWcKPbC4rh7xcRDKfGOlK	0	2022-10-11 12:59:41.394+08
123	$2a$11$x5TgNh6HW7AOdJXBFEKkAO084YOU/vMH3L9aHi6MSKycHa69r/7Yu	0	2022-10-11 14:44:18.500444+08
123	$2a$11$gVaOAy3bGVfDyLDIYvWf..Stu49.Ug0K8HbQE/YlO/u89LapNSJhi	0	2022-10-11 14:44:45.846843+08
11111111111	$2a$11$ptjdjkks6tNhDpIfZa9cCejiE.LoHa/9zNYlILp7fVS4n6F8W3fBq	0	2022-10-11 14:45:12.595978+08
11451419198	$2a$11$GqdQm9vd6mwE2tZhhFhKXeA6yZ9DYfLD0m31MXZI6N7S4H1dSqVxi	2	2022-11-11 14:21:09.133+08
\.


--
-- Data for Name: vip; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema.vip (vip_level, vip_discount, vip_month_price) FROM stdin;
0	1	0
1	0.8	20
2	0.7	40
3	0.6	70
\.


--
-- PostgreSQL database dump complete
--

