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
-- Table structure for table `transaction`
--

DROP TABLE IF EXISTS `transaction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transaction` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `user_id` char(36) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `amount` decimal(32,2) NOT NULL DEFAULT '0.00',
  `created` datetime NOT NULL,
  `transaction_type` int DEFAULT NULL,
  `detail` varchar(36) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=246 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transaction`
--

LOCK TABLES `transaction` WRITE;
/*!40000 ALTER TABLE `transaction` DISABLE KEYS */;
INSERT INTO `transaction` VALUES (98,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',123.00,'2024-04-27 13:00:08',0,'7536 7247 3527 2367'),(99,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',123.00,'2024-04-27 13:23:52',0,'7536 7247 3527 2367'),(100,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',354.00,'2024-04-27 16:07:00',2,NULL),(101,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',49.00,'2024-04-27 16:47:15',2,NULL),(104,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',10.00,'2024-04-27 18:04:31',2,NULL),(105,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',10.00,'2024-04-27 18:04:48',2,NULL),(106,'ddbe68bc-b237-40bf-a260-b75e593dfddd',123.00,'2024-04-28 10:22:37',0,'2489 4564 8645 6374'),(107,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',12.00,'2024-04-28 12:45:29',2,NULL),(108,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',30.00,'2024-04-28 12:45:45',2,NULL),(109,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',123.00,'2024-04-28 16:42:48',0,'1234 1234 1234 1234'),(110,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',123.00,'2024-04-28 16:43:19',0,'1234 1234 1234 1234'),(111,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:10',0,'2392 3912 9391 2391'),(112,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:11',0,'2392 3912 9391 2391'),(113,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:11',0,'2392 3912 9391 2391'),(114,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:12',0,'2392 3912 9391 2391'),(115,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:12',0,'2392 3912 9391 2391'),(116,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:13',0,'2392 3912 9391 2391'),(117,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:13',0,'2392 3912 9391 2391'),(118,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:13',0,'2392 3912 9391 2391'),(119,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:13',0,'2392 3912 9391 2391'),(120,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:13',0,'2392 3912 9391 2391'),(121,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:14',0,'2392 3912 9391 2391'),(122,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:14',0,'2392 3912 9391 2391'),(123,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:14',0,'2392 3912 9391 2391'),(124,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:14',0,'2392 3912 9391 2391'),(125,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:14',0,'2392 3912 9391 2391'),(126,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:15',0,'2392 3912 9391 2391'),(127,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:15',0,'2392 3912 9391 2391'),(128,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:15',0,'2392 3912 9391 2391'),(129,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:15',0,'2392 3912 9391 2391'),(130,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:15',0,'2392 3912 9391 2391'),(131,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:15',0,'2392 3912 9391 2391'),(132,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:16',0,'2392 3912 9391 2391'),(133,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:16',0,'2392 3912 9391 2391'),(134,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:16',0,'2392 3912 9391 2391'),(135,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:16',0,'2392 3912 9391 2391'),(136,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:16',0,'2392 3912 9391 2391'),(137,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:16',0,'2392 3912 9391 2391'),(138,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:17',0,'2392 3912 9391 2391'),(139,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:17',0,'2392 3912 9391 2391'),(140,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:17',0,'2392 3912 9391 2391'),(141,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:17',0,'2392 3912 9391 2391'),(142,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:17',0,'2392 3912 9391 2391'),(143,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:18',0,'2392 3912 9391 2391'),(144,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:20',0,'2392 3912 9391 2391'),(145,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:20',0,'2392 3912 9391 2391'),(146,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:20',0,'2392 3912 9391 2391'),(147,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:20',0,'2392 3912 9391 2391'),(148,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(149,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(150,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(151,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(152,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(153,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(154,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(155,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(156,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(157,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(158,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:21',0,'2392 3912 9391 2391'),(159,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(160,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(161,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(162,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(163,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(164,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(165,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(166,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(167,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:22',0,'2392 3912 9391 2391'),(168,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:23',0,'2392 3912 9391 2391'),(169,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:23',0,'2392 3912 9391 2391'),(170,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:23',0,'2392 3912 9391 2391'),(171,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:23',0,'2392 3912 9391 2391'),(172,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:23',0,'2392 3912 9391 2391'),(173,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:23',0,'2392 3912 9391 2391'),(174,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:23',0,'2392 3912 9391 2391'),(175,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:23',0,'2392 3912 9391 2391'),(176,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(177,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(178,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(179,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(180,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(181,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(182,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(183,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(184,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(185,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(186,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:24',0,'2392 3912 9391 2391'),(187,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(188,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(189,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(190,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(191,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(192,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(193,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(194,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(195,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:25',0,'2392 3912 9391 2391'),(196,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:26',0,'2392 3912 9391 2391'),(197,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:27',0,'2392 3912 9391 2391'),(198,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:27',0,'2392 3912 9391 2391'),(199,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:27',0,'2392 3912 9391 2391'),(200,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:27',0,'2392 3912 9391 2391'),(201,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:27',0,'2392 3912 9391 2391'),(202,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:27',0,'2392 3912 9391 2391'),(203,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:28',0,'2392 3912 9391 2391'),(204,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:28',0,'2392 3912 9391 2391'),(205,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:28',0,'2392 3912 9391 2391'),(206,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:28',0,'2392 3912 9391 2391'),(207,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:29',0,'2392 3912 9391 2391'),(208,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:29',0,'2392 3912 9391 2391'),(209,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:30',0,'2392 3912 9391 2391'),(210,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:31',0,'2392 3912 9391 2391'),(211,'fa001753-3336-496f-b8f9-fe2f9dce233f',1000.00,'2024-04-28 17:05:31',0,'2392 3912 9391 2391'),(212,'fa001753-3336-496f-b8f9-fe2f9dce233f',50000.00,'2024-04-28 17:06:08',2,NULL),(213,'f79a1dbe-f95d-4c0e-8fac-4ab157d0a033',1000.00,'2024-04-28 20:00:52',0,'0000 0000 0000 0000'),(214,'f79a1dbe-f95d-4c0e-8fac-4ab157d0a033',500.00,'2024-04-28 20:02:04',2,NULL),(215,'8d0bf172-9646-43ba-b29b-1afe8207a37d',1000.00,'2024-04-29 07:57:40',0,'4242 4242 4242 4242'),(216,'8d0bf172-9646-43ba-b29b-1afe8207a37d',500.00,'2024-04-29 07:58:47',2,NULL),(217,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',12.00,'2024-04-29 14:18:55',0,'1234 1234 1234 1234'),(218,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',400.00,'2024-04-29 14:18:58',0,'1234 1234 1234 1234'),(219,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',-781.00,'2024-04-29 14:19:07',1,'1234 1234 1234 1234'),(220,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',1000.00,'2024-04-29 14:19:10',0,'1234 1234 1234 1234'),(221,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',-12.00,'2024-04-29 14:19:12',1,'1234 1234 1234 1234'),(222,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',123.00,'2024-04-29 14:21:36',0,'1234 1234 1234 1234'),(223,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',5.00,'2024-04-29 14:43:57',0,'1234 1234 1234 1234'),(224,'c6675039-c99b-41da-b9d3-d67b7cd7655f',1000.00,'2024-04-29 21:38:47',0,'1020 1212 3213 1124'),(225,'c6675039-c99b-41da-b9d3-d67b7cd7655f',342.00,'2024-04-29 21:43:21',2,'e348b924-94c1-40a8-b1a5-ba14a652aa28'),(226,'c6675039-c99b-41da-b9d3-d67b7cd7655f',342.00,'2024-04-29 21:44:08',2,'e348b924-94c1-40a8-b1a5-ba14a652aa28'),(227,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',12.00,'2024-04-29 22:21:22',0,'5342 5613 4636 1364'),(228,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',123.00,'2024-04-29 22:36:47',2,'d42f298c-173f-4716-ba72-c6bdec14ce2e'),(229,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',12.00,'2024-04-29 22:41:23',2,'a0d91878-d9d7-44b8-ab4c-467b68eb668f'),(230,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',12.00,'2024-04-30 00:29:43',2,'b8590ffe-f7da-48f0-bcef-b101d8f26ff5'),(231,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',12.00,'2024-04-30 00:30:26',2,'bcedf34c-b901-425a-a0d0-77e545cb5cbc'),(232,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',31.00,'2024-04-30 00:30:37',2,'bcedf34c-b901-425a-a0d0-77e545cb5cbc'),(233,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',30.00,'2024-04-30 00:31:31',2,'2efaafb3-3e26-4842-a31d-a758b281bcb6'),(234,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',123.00,'2024-04-30 00:37:04',2,'1149c1da-7d7b-488d-8c89-822d2dcfdf87'),(235,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',321.00,'2024-04-30 00:37:42',2,'e9b41833-9860-413b-bda1-126a160ee8c4'),(236,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',321.00,'2024-04-30 00:38:36',2,'e9b41833-9860-413b-bda1-126a160ee8c4'),(237,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',30.00,'2024-04-30 09:30:16',0,'1234 1234 1234 1234'),(238,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',-45.00,'2024-04-30 09:30:20',1,'1234 1234 1234 1234'),(239,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',420.00,'2024-04-30 09:42:40',2,'c8e5dd38-f538-45e9-a3c6-30483c769db7'),(240,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',42.00,'2024-04-30 09:44:41',2,'678548df-bf43-401e-8e37-4641c42c4d7f'),(241,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',30.00,'2024-04-30 09:46:43',2,'1b966c9e-aaef-4b56-9cd8-f2e15d135d22'),(242,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',1000.00,'2024-04-30 09:50:45',0,'1234 1234 1234 1234'),(243,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',1000.00,'2024-04-30 09:50:46',0,'1234 1234 1234 1234'),(244,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',1000.00,'2024-04-30 09:50:46',0,'1234 1234 1234 1234'),(245,'5c05779f-54b7-4f7f-b80f-d608cd00a1a4',13.00,'2024-04-30 09:51:15',2,'3313fc53-ea9d-4aed-89fd-08e11ad0c39a');
/*!40000 ALTER TABLE `transaction` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-30 20:31:57