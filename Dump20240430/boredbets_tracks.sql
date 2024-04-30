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
-- Table structure for table `tracks`
--

DROP TABLE IF EXISTS `tracks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tracks` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(124) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `country` varchar(124) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `length` float DEFAULT NULL,
  `address` varchar(124) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tracks`
--

LOCK TABLES `tracks` WRITE;
/*!40000 ALTER TABLE `tracks` DISABLE KEYS */;
INSERT INTO `tracks` VALUES (1,'Churchill Downs','United States',1.36,'700 Central Ave, Louisville, KY 40208, United States'),(2,'Del Mar Racetrack','United States',1.27,'2260 Jimmy Durante Blvd, Del Mar, CA 92014, United States'),(3,'Los Alamitos Race Course','United States',1.26,'4961 Katella Ave, Los Alamitos, CA 90720, United States'),(4,'Jacksonville Equestrian Center','United States',1.51,'13611 Normandy Blvd, Jacksonville, FL 32221, United States'),(5,'Epsom Downs Racecourse','United Kingdom',1.28,'Epsom Downs, Epsom KT18 5LQ, United Kingdom'),(6,'York Racecourse','United Kingdom',2.37,'Knavesmire Rd, York YO23 1EX, United Kingdom'),(7,'Flemington Racecourse','Australia',2.49,'448 Epsom Rd, Flemington VIC 3031, Australia'),(8,'Ascot Racecourse','United Kingdom',1.37,'High St, Ascot SL5 7JX, United Kingdom'),(9,'Tokyo Racecourse','Japan',1.35,'230-0012 Tokyo, Fuchu, Morenatsu, 1 Chome−1−1, Japan'),(10,'Nakayama Racecourse','Japan',1.65,'〒274-0051 Chiba Prefecture, Funabashi, Nakayama, 1 Chome−1, Japan'),(11,'Leopardstown Racecourse','Ireland',1.69,'Foxrock, Dublin 18, Ireland'),(12,'Curragh Racecourse','Ireland',2.49,'Co. Kildare, Ireland'),(13,'Woodbine Racetrack','Canada',1.88,'555 Rexdale Blvd, Etobicoke, ON M9W 5L2, Canada'),(14,'Westerner Park','Canada',2.45,'4847A 19 St, Red Deer, AB T4R 2N7, Canada'),(15,'Meydan Racecourse','United Arab Emirates',1.11,'Al Meydan Rd - Meydan City - Dubai - United Arab Emirates'),(16,'Al Ain Racecourse','United Arab Emirates',1.71,'Al Ain - United Arab Emirates'),(17,'Turffontein Racecourse','South Africa',1.2,'Turf Club St, Turffontein, Johannesburg, 2190, South Africa'),(18,'Gosforth Park Racecourse','South Africa',1.37,'Northern Pkwy & Ash St, Germiston, Johannesburg, 2008, South Africa'),(19,'Düsseldorf Racecourse','Germany',2.26,'Rennbahnstraße 20, 40629 Düsseldorf, Germany'),(20,'Riem Racecourse','Germany',1.69,'Riemer Str. 180, 81829 München, Germany'),(21,'Hipódromo da Gávea','Brazil',2.17,'Praça Santos Dumont, 31 - Gávea, Rio de Janeiro - RJ, 22470-060, Brazil'),(22,'Hipódromo do Cristal','Brazil',1.76,'Av. Diário de Notícias, 1000 - Cristal, Porto Alegre - RS, 90810-090, Brazil'),(23,'Hipódromo Argentino de Palermo','Argentina',1.3,'Av. del Libertador 4101, C1426 CABA, Argentina'),(24,'Hipódromo de San Isidro','Argentina',1.72,'Av. Márquez 504, B1642 KBE, Buenos Aires, Argentina'),(25,'Ellerslie Racecourse','New Zealand',2.19,'80-100 Ascot Ave, Remuera, Auckland 1050, New Zealand'),(26,'Trentham Racecourse','New Zealand',1.82,'Racecourse Rd, Trentham, Upper Hutt 5018, New Zealand'),(27,'Royal Western India Turf Club','India',1.5,'Dr E Moses Rd, Royal Western India Turf Club, Royal Western India Turf Club, Mahalakshmi, Mumbai, Maharashtra 400034, India'),(28,'Ooty Racecourse','India',1.07,'Fernhill Post, Ooty, Tamil Nadu 643004, India'),(29,'Täby Galopp','Sweden',1.32,'Djursholmsvägen 30, 183 53 Täby, Sweden'),(30,'Jägersro Racecourse','Sweden',2.42,'Jägersrovägen 159, 215 63 Malmö, Sweden'),(31,'Jarlsberg Travbane','Norway',1.11,'Jarlsbergveien 2, 3159 Melsomvik, Norway'),(32,'Bjerke Travbane','Norway',1.78,'Trondheimsveien 120, 0582 Oslo, Norway'),(33,'Klampenborg Racecourse','Denmark',1.6,'Jægersborg Alle 1, 2930 Klampenborg, Denmark'),(34,'Kobenhavns Galopbane','Denmark',1.67,'Stadionvej 50, 2850 Nærum, Denmark'),(35,'Hipódromo de la Zarzuela','Spain',1.03,'Av. Padre Huidobro, 294, 28023 Madrid, Spain'),(36,'Hipódromo de Zaragoza','Spain',2.14,'Camino de Vistabella, 13, 50009 Zaragoza, Spain'),(37,'Galopprennbahn Belp','Switzerland',2.12,'Muristrasse 62, 3123 Belp, Switzerland'),(38,'Ippodromo Comunale di Lugano','Switzerland',1.67,'Via Campo Marzio, 6925 Gentilino, Switzerland'),(39,'Krieau Racetrack','Austria',1,'Meiereistraße 7, 1020 Wien, Austria'),(40,'Trabrennbahn Krieau','Austria',2.01,'Freudenau 65, 1020 Wien, Austria'),(41,'Hipódromo Manuel Possolo','Portugal',1.51,'6, Av. da República, 2765-290 Estoril, Portugal'),(42,'Hipódromo do Campo Grande','Portugal',2.05,'1300-054 Lisbon, Portugal');
/*!40000 ALTER TABLE `tracks` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-30 20:32:08
