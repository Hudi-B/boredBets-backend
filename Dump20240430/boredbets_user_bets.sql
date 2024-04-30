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
-- Table structure for table `user_bets`
--

DROP TABLE IF EXISTS `user_bets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_bets` (
  `Id` char(36) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `user_id` char(36) COLLATE utf8mb4_hungarian_ci NOT NULL,
  `race_id` char(36) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `first` char(36) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `second` char(36) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `third` char(36) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `fourth` char(36) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `fifth` char(36) COLLATE utf8mb4_hungarian_ci DEFAULT NULL,
  `bet_amount` int DEFAULT NULL,
  `bet_type_id` int DEFAULT NULL,
  `outcome` decimal(32,2) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_user_bets_user_id` (`user_id`),
  KEY `race_id1` (`race_id`),
  KEY `user_bets_ibfk_4_idx` (`bet_type_id`),
  CONSTRAINT `user_bets_ibfk_1` FOREIGN KEY (`race_id`) REFERENCES `races` (`id`) ON DELETE CASCADE,
  CONSTRAINT `user_bets_ibfk_3` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
  CONSTRAINT `user_bets_ibfk_4` FOREIGN KEY (`bet_type_id`) REFERENCES `bet_types` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_bets`
--

LOCK TABLES `user_bets` WRITE;
/*!40000 ALTER TABLE `user_bets` DISABLE KEYS */;
INSERT INTO `user_bets` VALUES ('06cd2a40-06b4-4867-9267-8a835b9e7c28','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','3313fc53-ea9d-4aed-89fd-08e11ad0c39a','975e901f-a152-4a83-ba4b-30dcbb48c7a0','ebcc15b9-c0fd-46cb-8e06-6385dd5b0718','244797b4-e544-4298-9f4f-e267219783e2','9ee8c49d-fc7c-4651-8231-c3ba2bc104ed','7493db16-d8b7-44c4-89c1-d859b1001cb4',13,1,-13.00),('08404bf7-091f-4223-9a04-3a2f22179199','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','1841239b-b83d-4891-a2dd-469def6381c3','14f5e582-0db0-4cfa-8d38-5cadfb675681','514fccd8-572a-41a9-8556-0d60220df64b','244797b4-e544-4298-9f4f-e267219783e2','a8857d9d-3ff9-45b6-b6d3-a1737fcce899','38f06ef2-2218-4874-883a-5824ead1bca4',30,1,-120.00),('100593b2-6112-4158-a28a-acfe4c4038be','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','e9b41833-9860-413b-bda1-126a160ee8c4','c948f7ac-6f52-431e-bead-38d6bd5969c7','33c702bc-84ac-4b9f-9dd4-2b329d159277','ca4368df-afe0-4240-9b50-eca96eb68f7d','9ab77112-16a8-4ed2-a164-2cde58075fc2','8f4cab97-df38-4b81-82f1-f06ccc8c3ffd',321,1,-321.00),('131add2a-3909-4dc2-b842-68ab68f77f38','8d0bf172-9646-43ba-b29b-1afe8207a37d','dfc728d3-5ede-41a7-bbbc-26023e88ebcf','5fbb217b-f180-4f7d-8375-4de8cae6104b','9e0cd793-8661-4c97-9916-dee2d9f8a66d','9edb3ff8-d058-4dd9-acfb-3062928ecc18','df53cbf9-c568-4450-ba82-8c327541f367','976dedda-1eae-4856-addb-0f28f0c2e441',500,0,-1000.00),('1f4222b7-1a4a-42e2-9320-f7ed13fb62bb','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','f1def5d8-3f0c-4e31-b505-48682a43c6e2','88d181b8-ad12-4f8f-a4ae-9e634988de2e','9469facb-5c75-49b3-8036-3571b6ca9b24','465e4ace-3970-4a51-954b-a5f433809b50','38ded6ea-df39-4a01-8499-2b24d584e9a6','c6971463-e0ac-411f-b0a8-4a2ec23c45c7',49,1,-147.00),('364ea4d9-13db-4eaf-8049-d05a2042db05','c6675039-c99b-41da-b9d3-d67b7cd7655f','e348b924-94c1-40a8-b1a5-ba14a652aa28','8414cc17-a3b9-41b8-a64f-7528aa24f189','ef38440f-eed5-4535-bb0d-19feeb78b681','9469facb-5c75-49b3-8036-3571b6ca9b24','f08e2525-143e-4df6-a45b-8d2402934f5f','78e72bfc-c3a5-4296-a75f-08f5b8d4d6cc',342,1,-684.00),('40b0239d-0678-43b5-844b-6809d8a06324','f79a1dbe-f95d-4c0e-8fac-4ab157d0a033','003dd8f0-a62b-4df7-bba4-eefe6cd0dfc7','6c4875cd-4ae6-4df5-a610-891e8337c606','435152c3-ba82-4eac-9269-0246a266f405','c141beb7-6a53-47c2-aae5-4c3d18b1751f','1c0aacd9-ae82-4a7f-942c-e69b3e0b1f6c','efc6afe7-272e-4c17-ae57-b3a308def3fa',500,1,-1500.00),('44a645b3-45e1-4cab-8c7d-6a0414f432b3','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','a649ddd8-1717-4a42-ab4f-810654aedef8','09daf00e-ac1a-4f14-b46d-9514ea973f64','4fe5726e-8ebd-40cd-8937-d98ab0bf64ab','d7d5456f-16e3-4f45-9173-18d8a9962596','5d3391b8-573b-4565-aadb-3975a75e12d2','9a6f6050-87e2-4f4d-90fe-bfc4e7726d78',10,0,-30.00),('4f3180bb-a92d-4a89-8919-11291e12bc1d','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','678548df-bf43-401e-8e37-4641c42c4d7f','3ddc1782-dbd6-4338-9bd4-da428ee0790a','6cbdc543-dbcb-4196-aeb2-f7cfa0dcd918','6fb3db55-88fc-458b-b948-1219e2101443','521f87c8-d9f9-4801-86b5-7147ce0f09e2','13d7cc55-0e8a-43b0-9a52-3640c130231b',42,1,-42.00),('51e803dc-4e17-4d70-87fe-cdf2aecc55a7','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','b8590ffe-f7da-48f0-bcef-b101d8f26ff5','9a9db39a-0634-4877-9b93-88caa6d2d682','cb38d4af-4544-42bb-bdd4-0f9a8afbe254','9ba58541-0499-4897-ac21-8bff97f1366a','e7a9a6f8-f413-48bc-b95b-6812c3a776e7','f08e2525-143e-4df6-a45b-8d2402934f5f',12,1,-12.00),('7c29c950-d3e1-4446-b08c-76ca91435094','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','a649ddd8-1717-4a42-ab4f-810654aedef8','911690c6-aaad-4c46-81f6-a85b1f27a330','cf281b6a-6450-447e-88d2-ad8ba1a498a8','66c7b1ff-3325-4c02-81d7-05b877c3ae40','09daf00e-ac1a-4f14-b46d-9514ea973f64','4fe5726e-8ebd-40cd-8937-d98ab0bf64ab',10,0,-40.00),('8ae69a49-5e4a-4bcc-bf1a-4faaf763ecb5','fa001753-3336-496f-b8f9-fe2f9dce233f','fc9204f4-0bfd-469f-b74b-817dddb07d34','567ffb25-60e7-40e2-8c4f-5e197d586c82','44ac2911-1a54-4f65-838e-3a9ed28a167f','1c60336a-e8ec-4428-87dd-9a5bbc5945d8','fcbfaa18-8c9f-42e5-b0a7-f79b40091e68','24be619a-3518-4a30-90c1-56d83dacec32',50000,1,-150000.00),('91b5b02f-b7b8-41af-9fed-10379d953466','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','e9b41833-9860-413b-bda1-126a160ee8c4','c948f7ac-6f52-431e-bead-38d6bd5969c7','33c702bc-84ac-4b9f-9dd4-2b329d159277','ca4368df-afe0-4240-9b50-eca96eb68f7d','9ab77112-16a8-4ed2-a164-2cde58075fc2','8f4cab97-df38-4b81-82f1-f06ccc8c3ffd',321,1,-321.00),('93da3f84-75ff-487b-9896-344cc9ef1cbe','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','bcedf34c-b901-425a-a0d0-77e545cb5cbc','8e4be65b-fde5-4cd2-8b25-5d8bf03d805e','2cedfd6e-1330-4e32-a4c7-ffc02361b28b','4d8484cc-ec19-4f4d-a056-6a8e9ae13b51','e2c58672-dcf8-4af0-8654-2a706674061c','d7929e9e-ab9b-413b-98ba-24277254a30e',31,1,-31.00),('9bbded35-4e9a-4371-8e73-c8e6d4639b27','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','1b966c9e-aaef-4b56-9cd8-f2e15d135d22','6e52023e-7235-4551-8e7e-6ad2e5be8142','d02c8a61-0d67-42d2-8af4-f4b62e79bc3e','c11b28bc-dffb-4bfe-a40b-f36edc051e60','9abad425-041f-41df-b559-6c2024bb558d','6a6d8247-33ed-4cfc-8e06-f84f39920b35',30,1,-30.00),('a1478871-e3e6-466f-a20d-be8e1c2fb5f1','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','c01dc246-6d2c-4db4-a5e9-4e739118ac12','7f488a00-c3ae-449d-827d-6beac9dd005a','3b1b7c67-69be-41e3-8531-873c8cb798bb','5a46b461-0208-47e6-9ea6-871dfe8e9b3b','0aac9f80-3638-459c-a8ab-2ed296d68fed','6797a23e-e81e-4dcc-8f42-18844179967a',12,1,-48.00),('d3c085c5-a51e-4320-923a-f49f0c047513','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','2efaafb3-3e26-4842-a31d-a758b281bcb6','8d589e10-f680-4818-8579-ddebdd388d89','49f7a6c8-9140-45a2-98d8-426f84dcdea8','d80ce3a7-26a3-4492-bcbd-c91d633204a3','96becf03-807f-48e0-845c-1abb86f39d0f','f10bf316-fa5a-4b50-9b92-e20b60d5c798',30,1,-30.00),('e9b7dab0-060c-46ba-855e-951bafc1547e','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','1149c1da-7d7b-488d-8c89-822d2dcfdf87','a855a13e-e2e4-4cf8-9df2-258fb324fd81','74c67805-1870-4b5d-899e-2f67155f7ea5','a1622645-8f23-4ce0-b369-6cc75cc593b1','976dedda-1eae-4856-addb-0f28f0c2e441','65a6a071-bd1b-495b-9269-97733f709480',123,1,-123.00),('ebf79069-5d7a-4d2c-9929-590b5f158e4a','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','bcedf34c-b901-425a-a0d0-77e545cb5cbc','1af0c0d6-f6f4-40c8-a785-a3c3b3d46961','67404a84-fefd-44ab-95f6-d3126ef057b0','6c9d916e-23c4-4952-9188-e16e4fef4a18','b6ace006-a2a5-4b2f-82a1-eefafe11714a','64c5e7de-6afa-492e-89a4-d6037a51df8b',12,1,-12.00),('f322049f-fbce-4782-89be-1d8a69f6afa9','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','c8e5dd38-f538-45e9-a3c6-30483c769db7','1af0c0d6-f6f4-40c8-a785-a3c3b3d46961','f3f62df5-d316-458e-a6e4-1d1f8387173a','2ac91227-f0b9-4d92-8c45-74fdbe528069','23934e62-ba6e-4b87-a706-30d8f935a8ff','d9964c4d-9e76-4107-b9df-d611bf054725',420,1,-420.00),('f49637fc-fc58-4547-ae9d-4e6bce865972','5c05779f-54b7-4f7f-b80f-d608cd00a1a4','d42f298c-173f-4716-ba72-c6bdec14ce2e','83b66164-a055-49e7-9f54-bdd5598f8298','848a9569-b31f-4286-bcf3-93a34264efaf','c35fb07b-426c-4465-92ab-0d80bbcfa95e','87dcdcbd-92c6-4bdb-ad26-3429ce987e0c','d4a5a32e-909b-4b21-b250-f82883479a7b',123,1,-246.00);
/*!40000 ALTER TABLE `user_bets` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-30 20:31:47
