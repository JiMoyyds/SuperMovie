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
    order_time timestamp with time zone
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
    vip_discount double precision
);


ALTER TABLE default_schema.vip OWNER TO postgres;

--
-- Data for Name: cinema; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema.cinema (cinema_id, cinema_name) FROM stdin;
\.


--
-- Data for Name: film; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema.film (film_id, film_name, film_online_time, film_is_preorder, film_types, film_price, film_actors, film_cover_url, film_preview_video_url) FROM stdin;
\.


--
-- Data for Name: order; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema."order" (order_id, order_user_id, order_pay_amount, order_film_id, order_cinema_id, order_schedule_id, order_cinema_seat, order_time) FROM stdin;
\.


--
-- Data for Name: schedule; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema.schedule (schedule_cinema_id, schedule_film_id, schedule_start_time, schedule_end_time, schedule_id) FROM stdin;
\.


--
-- Data for Name: user; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema."user" (user_id, user_pwd_hash, user_vip_level, user_vip_level_expire_time) FROM stdin;
\.


--
-- Data for Name: vip; Type: TABLE DATA; Schema: default_schema; Owner: postgres
--

COPY default_schema.vip (vip_level, vip_discount) FROM stdin;
\.


--
-- PostgreSQL database dump complete
--

