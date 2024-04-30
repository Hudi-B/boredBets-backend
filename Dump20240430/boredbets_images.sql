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
-- Table structure for table `images`
--

DROP TABLE IF EXISTS `images`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `images` (
  `id` char(36) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `image_link` varchar(255) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `image_delete_link` varchar(255) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `images`
--

LOCK TABLES `images` WRITE;
/*!40000 ALTER TABLE `images` DISABLE KEYS */;
INSERT INTO `images` VALUES ('158a4377-01b9-4408-aabd-e30f775c27e2','',''),('1860be84-2d76-47f0-9a11-60c5d7dc2ae7','',''),('19243041-c515-4889-9bcc-e6082fe21fa2','',''),('2029f66f-f906-4183-8f0d-d70556998eca','',''),('29c57b15-5a87-43e2-a35a-067599660745','',''),('29d9fd0f-ee59-47f3-a224-451c95605b92','',''),('2d73045d-5417-4b4d-854d-c8813dcb6c35','https://i.ibb.co/nmMv7rk/torii-black-and-white-wallpaper-5120x2880.jpg','https://ibb.co/h9FjBs8/d8ba0b84241ac348b95aaeb902af3c27'),('2e873f21-7cdb-4016-a853-18fba47f1853','',''),('33e5521a-e82a-4df7-86f7-3c7749bbfb49','',''),('411af792-974a-4f12-be04-a4397de8f1ab','',''),('448dc85e-b4e1-4319-a0ff-f04213cdc055','',''),('4bdc76cb-9465-413a-8ddb-a3899732b9d9','',''),('5dc7b81f-6592-4b99-998a-81c2529fa3c4','',''),('6adb06cc-fd67-4ca3-9fdb-7c71671f8835','',''),('70f508c7-6916-48c2-ab73-eb4834d84d33','https://i.ibb.co/JQ37N6D/mylittlepookiebear.jpg','https://ibb.co/9tZTJKQ/fae4c3290c64fc20d3893d2254dcc000'),('7289cb26-f036-4abe-994e-53a20f525e39','',''),('787c08c5-02fb-4627-b660-b8b51e4a66f6','',''),('803797cc-53b5-47a7-b99c-390289c32905','',''),('9249907c-fbd9-4219-9a30-72e634ab3c0e','',''),('9a293c57-9953-47bf-995f-251fa613b894','',''),('bb3c8adb-cf41-4ef9-808a-5d2b5540beb8','',''),('c68d8b87-c521-4804-9b51-f3794ef61bcf','',''),('c89d89b9-fe9f-4464-8433-5f70b500c6bd','',''),('dcc832c7-653f-4acc-aa52-8ca59b6c571c','https://i.ibb.co/10sBd84/20240314-135638-01.jpg','https://ibb.co/tHZnx81/20270fd746a0f1bc403d505edcf13d0d'),('df1dce3b-d7e2-4596-abd1-3cb81a90c235','',''),('e503c5f3-553f-4c86-b4ae-bd34268b5f97','',''),('f15889e0-5c14-4198-874a-83cb727e936e','',''),('fc6dc0a7-bd9a-4a48-a035-d73ae9eb3200','','');
/*!40000 ALTER TABLE `images` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-30 20:32:07
