-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: boredomdb.mysql.database.azure.com    Database: boredbets
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `user_cards`
--

DROP TABLE IF EXISTS `user_cards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_cards` (
  `creditcard_num` varchar(20) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `cvc` varchar(4) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `exp_year` varchar(3) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `exp_month` varchar(3) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `card_name` varchar(25) COLLATE utf8mb4_hungarian_ci NOT NULL DEFAULT 'CreditCard',
  `user_id` char(36) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `card_holdername` varchar(25) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  PRIMARY KEY (`creditcard_num`),
  KEY `IX_user_cards_user_id` (`user_id`),
  CONSTRAINT `user_cards_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_cards`
--

LOCK TABLES `user_cards` WRITE;
/*!40000 ALTER TABLE `user_cards` DISABLE KEYS */;
INSERT INTO `user_cards` VALUES ('0000 0000 0000 0000','345','55','11','Enyem','f79a1dbe-f95d-4c0e-8fac-4ab157d0a033','MEZES B ODON'),('1020 1212 3213 1124','231','31','12','gdgffdgdgdgd','c6675039-c99b-41da-b9d3-d67b7cd7655f','ÖLDMEG MAGAD'),('1234 1234 1234 1234','321','32','11','My main card','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','RICSI TEST'),('2392 3912 9391 2391','555','25','06','lac','fa001753-3336-496f-b8f9-fe2f9dce233f','FEKET LAC'),('4242 4242 4242 4242','666','28','01','XD','8d0bf172-9646-43ba-b29b-1afe8207a37d','NÉMETH BENCE'),('5342 5613 4636 1364','142','25','09','bruh','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','JOHH DONE');
/*!40000 ALTER TABLE `user_cards` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-30 20:32:03
