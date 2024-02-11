-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Feb 08. 12:55
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.0.30

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

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `jockey`
--

CREATE TABLE `jockey` (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `quality` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `participant`
--

CREATE TABLE `participant` (
  `race_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `horse_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `jockey_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `placement` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `races`
--

CREATE TABLE `races` (
  `id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `race_time` datetime NOT NULL,
  `race_scheduled` datetime NOT NULL,
  `weather` varchar(255) DEFAULT NULL,
  `track_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
('20240208115406_eleventh', '8.0.0');

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
-- A tábla indexei `participant`
--
ALTER TABLE `participant`
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
  ADD KEY `horse_id` (`horse_id`),
  ADD KEY `race_id` (`race_id`);

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
-- Megkötések a táblához `participant`
--
ALTER TABLE `participant`
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
