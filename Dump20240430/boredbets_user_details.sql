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
-- Table structure for table `user_details`
--

DROP TABLE IF EXISTS `user_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_details` (
  `user_id` char(36) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `fullname` varchar(255) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `address` varchar(255) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `IsPrivate` tinyint(1) NOT NULL,
  `birth_date` date DEFAULT NULL,
  `PhoneNum` varchar(16) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `Profit` decimal(32,2) DEFAULT '0.00',
  PRIMARY KEY (`user_id`),
  CONSTRAINT `user_details_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_details`
--

LOCK TABLES `user_details` WRITE;
/*!40000 ALTER TABLE `user_details` DISABLE KEYS */;
INSERT INTO `user_details` VALUES ('2cfe7009-685b-4331-93b6-3fd495e7ce8e','-','-',1,'1111-01-01','-',0.00),('52708818-2e16-4eb2-a693-ba84d48ef16a','Apaad','asdgfasdg',1,'1938-04-13','+3671252',0.00),('5c05779f-54b7-4f7f-b80f-d608cd00a1a4','Joska Pista','Ott valahol 9.',0,'1999-04-28','+36705483138',-3275.00),('884c950a-b0f4-41e5-ac69-8cdf9f168bc4','-','-',0,'2000-01-01','-',0.00),('8d0bf172-9646-43ba-b29b-1afe8207a37d','-','-',1,'1111-01-01','-',-1000.00),('aae37905-38c1-4076-8c20-c9aa926bd3ee','-','-',1,'2000-01-01','-',0.00),('c6675039-c99b-41da-b9d3-d67b7cd7655f','-','-',0,'1111-01-01','-',-1026.00),('ea0958e0-fbb1-410c-8988-469b959c7ee1','-','-',1,'1111-01-01','-',0.00),('f79a1dbe-f95d-4c0e-8fac-4ab157d0a033','-','-',1,'1111-01-01','-',-1500.00),('fa001753-3336-496f-b8f9-fe2f9dce233f','-','-',1,'1111-01-01','-',-150000.00);
/*!40000 ALTER TABLE `user_details` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-30 20:32:00
