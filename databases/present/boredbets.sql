-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Feb 13. 17:56
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.0.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `boredbets`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `horses`
--

CREATE TABLE `horses` (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `stallion` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `horses`
--

INSERT INTO `horses` (`id`, `name`, `age`, `stallion`) VALUES
('a80a8f02-3c08-4d05-8cd9-29e75e8ac99e', 'paci2', 69, 0),
('d7b63fd3-1203-4eef-96a7-5fff5edacc5b', 'paci', 23, 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `jockey`
--

CREATE TABLE `jockey` (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `quality` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `jockey`
--

INSERT INTO `jockey` (`id`, `name`, `quality`) VALUES
('46a8ad88-a6af-4f30-bf6e-db4583884aa4', 'emese', 2),
('7188e5bb-4630-47d1-a062-5514edbc6782', 'lovag', 5);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `participants`
--

CREATE TABLE `participants` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `race_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `horse_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `jockey_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `placement` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `participants`
--

INSERT INTO `participants` (`Id`, `race_id`, `horse_id`, `jockey_id`, `placement`) VALUES
('08dc28cb-a2b4-413c-8a7b-4e8386b7990b', 'ff01a0f4-44f4-4943-8cca-d9ab6b8551c6', 'd7b63fd3-1203-4eef-96a7-5fff5edacc5b', '46a8ad88-a6af-4f30-bf6e-db4583884aa4', 0),
('08dc28cb-d12f-4b0c-8fbc-bf94d6791664', 'ff01a0f4-44f4-4943-8cca-d9ab6b8551c6', 'd7b63fd3-1203-4eef-96a7-5fff5edacc5b', '46a8ad88-a6af-4f30-bf6e-db4583884aa4', 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `races`
--

CREATE TABLE `races` (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `race_time` double NOT NULL,
  `race_scheduled` datetime NOT NULL,
  `weather` varchar(255) DEFAULT NULL,
  `track_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `races`
--

INSERT INTO `races` (`id`, `race_time`, `race_scheduled`, `weather`, `track_id`) VALUES
('036453a3-d760-4b54-948d-fa3a28f43fa4', 43, '2024-02-03 19:11:31', 'mi', '6951f577-3213-4561-9275-98a3bcfe9844'),
('2c31f557-ed21-44b0-b78b-2fe5713f4b74', 54, '2024-02-14 19:19:02', 'jg', '6951f577-3213-4561-9275-98a3bcfe9844'),
('3cc883ae-7f46-44d9-b1d4-808b5aa80251', 342, '2024-02-28 19:19:02', 'wfwe', '6951f577-3213-4561-9275-98a3bcfe9844'),
('50b50aec-30c5-4406-807d-64bb134c092b', 45, '2024-02-04 18:32:26', 'd', '6951f577-3213-4561-9275-98a3bcfe9844'),
('5bfd7f3e-c9a4-425a-b09b-520994da6c1a', 45, '2024-02-06 18:32:26', 'c', '6951f577-3213-4561-9275-98a3bcfe9844'),
('5dab46f5-3ff1-4666-996d-0548c865b89c', 87, '2024-02-12 19:19:02', 'ouui', '6951f577-3213-4561-9275-98a3bcfe9844'),
('93053cd6-a709-4bc1-8d03-3ee8fc0f1a2f', 23, '2024-02-24 19:19:02', 'oilkj', '6951f577-3213-4561-9275-98a3bcfe9844'),
('9a811ac4-5491-49fa-995e-611e49fc1c42', 43, '2024-02-01 19:11:31', 'et', '6951f577-3213-4561-9275-98a3bcfe9844'),
('adc1d348-ac1f-4e2a-bf01-0373b14eec04', 58, '2024-02-06 18:32:26', 'b', '6951f577-3213-4561-9275-98a3bcfe9844'),
('b76bcb57-581e-43c8-9d5c-f27a8037f412', 34, '2024-02-11 19:19:02', 'asd', '6951f577-3213-4561-9275-98a3bcfe9844'),
('b793627c-debb-4378-acb1-dad20d530f12', 69, '2024-02-18 19:19:02', 'ryr', '6951f577-3213-4561-9275-98a3bcfe9844'),
('c508f4a5-35ae-4e44-b428-14479db3c3eb', 33, '2024-02-06 19:11:31', 'rr', '6951f577-3213-4561-9275-98a3bcfe9844'),
('f474f671-493f-4eca-a771-35e1d28dbd3e', 56, '2024-02-06 18:32:26', 'a', '6951f577-3213-4561-9275-98a3bcfe9844'),
('ff01a0f4-44f4-4943-8cca-d9ab6b8551c6', 65, '2024-02-10 17:29:27', 'esos', '6951f577-3213-4561-9275-98a3bcfe9844');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `tracks`
--

CREATE TABLE `tracks` (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `country` varchar(255) DEFAULT NULL,
  `length` float DEFAULT NULL,
  `surface` varchar(255) DEFAULT NULL,
  `oval` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `tracks`
--

INSERT INTO `tracks` (`id`, `name`, `country`, `length`, `surface`, `oval`) VALUES
('6951f577-3213-4561-9275-98a3bcfe9844', 'monaco', 'Monaco', 500, 'beton(hofi)', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `email` varchar(255) DEFAULT NULL,
  `role` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `created` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_bets`
--

CREATE TABLE `user_bets` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `user_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `race_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `horse_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `bet_amount` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_cards`
--

CREATE TABLE `user_cards` (
  `creditcard_num` int(11) NOT NULL,
  `cvc` int(11) NOT NULL,
  `exp_date` varchar(255) NOT NULL,
  `card_name` varchar(255) NOT NULL,
  `user_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user_details`
--

CREATE TABLE `user_details` (
  `user_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `fullname` varchar(255) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `IsPrivate` tinyint(1) NOT NULL,
  `birth_date` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20240208172217_thirteenth', '8.0.0');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `horses`
--
ALTER TABLE `horses`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `jockey`
--
ALTER TABLE `jockey`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `participants`
--
ALTER TABLE `participants`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `horse_id` (`horse_id`),
  ADD KEY `jockey_id` (`jockey_id`),
  ADD KEY `race_id` (`race_id`);

--
-- A tábla indexei `races`
--
ALTER TABLE `races`
  ADD PRIMARY KEY (`id`),
  ADD KEY `track_id` (`track_id`);

--
-- A tábla indexei `tracks`
--
ALTER TABLE `tracks`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `user_bets`
--
ALTER TABLE `user_bets`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_user_bets_user_id` (`user_id`),
  ADD KEY `horse_id1` (`horse_id`),
  ADD KEY `race_id1` (`race_id`);

--
-- A tábla indexei `user_cards`
--
ALTER TABLE `user_cards`
  ADD PRIMARY KEY (`creditcard_num`),
  ADD KEY `IX_user_cards_user_id` (`user_id`);

--
-- A tábla indexei `user_details`
--
ALTER TABLE `user_details`
  ADD PRIMARY KEY (`user_id`);

--
-- A tábla indexei `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `user_cards`
--
ALTER TABLE `user_cards`
  MODIFY `creditcard_num` int(11) NOT NULL AUTO_INCREMENT;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `participants`
--
ALTER TABLE `participants`
  ADD CONSTRAINT `participant_ibfk_1` FOREIGN KEY (`race_id`) REFERENCES `races` (`id`),
  ADD CONSTRAINT `participant_ibfk_2` FOREIGN KEY (`horse_id`) REFERENCES `horses` (`id`),
  ADD CONSTRAINT `participant_ibfk_3` FOREIGN KEY (`jockey_id`) REFERENCES `jockey` (`id`);

--
-- Megkötések a táblához `races`
--
ALTER TABLE `races`
  ADD CONSTRAINT `races_ibfk_1` FOREIGN KEY (`track_id`) REFERENCES `tracks` (`id`);

--
-- Megkötések a táblához `user_bets`
--
ALTER TABLE `user_bets`
  ADD CONSTRAINT `user_bets_ibfk_1` FOREIGN KEY (`race_id`) REFERENCES `races` (`id`),
  ADD CONSTRAINT `user_bets_ibfk_2` FOREIGN KEY (`horse_id`) REFERENCES `horses` (`id`),
  ADD CONSTRAINT `user_bets_ibfk_3` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Megkötések a táblához `user_cards`
--
ALTER TABLE `user_cards`
  ADD CONSTRAINT `user_cards_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Megkötések a táblához `user_details`
--
ALTER TABLE `user_details`
  ADD CONSTRAINT `user_details_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
