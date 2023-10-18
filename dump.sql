-- MariaDB dump 10.19  Distrib 10.11.2-MariaDB, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: chat
-- ------------------------------------------------------
-- Server version	10.11.2-MariaDB-1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Channels`
--

DROP TABLE IF EXISTS `Channels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Channels` (
  `uuid` varchar(64) NOT NULL,
  `accessKey` varchar(256) NOT NULL,
  `dateCreated` date NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Channels`
--

LOCK TABLES `Channels` WRITE;
/*!40000 ALTER TABLE `Channels` DISABLE KEYS */;
INSERT INTO `Channels` VALUES
('ce8a8844c3c1457f88366e0178f9d81d','fYuoijBr+DivIhuabaEVXenA77hWwKWH7yHEOvyOqiiAuq0OlkA1MX92CQ3xr5zbfHq6SDER0bJlE308aNg8UFG8X83I0wgOfbpItn8DQ/kv0kiTt/SZTvXyn3olIKKc','2023-09-21',1);
/*!40000 ALTER TABLE `Channels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Invites`
--

DROP TABLE IF EXISTS `Invites`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Invites` (
  `uuid` varchar(64) NOT NULL,
  `useruuid` varchar(64) NOT NULL,
  `content` varchar(4096) NOT NULL,
  `accessKey` varchar(256) NOT NULL,
  `encryptedKey` varchar(1024) DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  KEY `IX_Invites_useruuid` (`useruuid`),
  CONSTRAINT `FK_Invites_Users_useruuid` FOREIGN KEY (`useruuid`) REFERENCES `Users` (`uuid`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Invites`
--

LOCK TABLES `Invites` WRITE;
/*!40000 ALTER TABLE `Invites` DISABLE KEYS */;
/*!40000 ALTER TABLE `Invites` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Messages`
--

DROP TABLE IF EXISTS `Messages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Messages` (
  `uuid` varchar(64) NOT NULL,
  `content` longtext NOT NULL,
  `channeluuid` varchar(64) NOT NULL,
  `dateCreated` datetime(6) NOT NULL,
  PRIMARY KEY (`uuid`),
  KEY `IX_Messages_channeluuid` (`channeluuid`),
  CONSTRAINT `FK_Messages_Channels_channeluuid` FOREIGN KEY (`channeluuid`) REFERENCES `Channels` (`uuid`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Messages`
--

LOCK TABLES `Messages` WRITE;
/*!40000 ALTER TABLE `Messages` DISABLE KEYS */;
INSERT INTO `Messages` VALUES
('02a579c2e8cf4722b66957b86cf5a6e0','l/1Hrzh+H19ghiOHzYXKof7hX2ICQDuYdhmGcqI1IKRE2R+kRp7iXBjivsJjUuKCriNVldNiBZGTc98B55KsvZ/DziJ3+zmQS1eskiAiVv5Ee7VrX0t3Uc9Y5d44MM6kjwCPKVJOwAtrtZmR3UOu28XshNZYdODpSnYzm8XIBz4wPfrR/DnTSpbvU/Ea','ce8a8844c3c1457f88366e0178f9d81d','2023-09-25 13:35:21.226146'),
('0b16b9284a124ad482359616951e5b2c','Uns/ljLH52oKUo4DUTgD3p56Sn969Kpqdh8SdEZW6E3eAtSywQjEerZCvWUxFbM4fZ2fpNgSPW9tDuMZcv7foOemPKveazWnHD+3Dc1o5o5oJJo1cGYaIFIDYCm1zAa+b6/82cI9JHuJQGmafh8iu+TfOsLeflAdcAMGViXUe1j9','ce8a8844c3c1457f88366e0178f9d81d','2023-09-22 13:30:26.095628'),
('109a8c6a0665444d8eec1e2c07e8fdda','EtSomWNe+MZc3kJ7rwQ9PEPz2DT5OnkghRH9K8dX4P8OTchuCSbt5+NAFbRu0g/jPlv+iNT5LQPPxTaRpceUBUtmxUQKk25FcGqwqZb0Tf7MDqJFUqUcwDcT6EoZeB6Bhy6q+Qtnk3jzJrxSTKXWbvSnCMNzTQfiPuGOsw==','ce8a8844c3c1457f88366e0178f9d81d','2023-09-22 13:52:00.749297'),
('48c0abe1e438424ba4c05309a84e1aee','XvB4bVKwp3TJ+HgT6LawYLhpUGetjxJfWNAdpU3TX1iWsm6GZp+xaYFoTz8p7444TSaUmgt8/zFJlgBA3VFpKjaWV9xmL2v81n4TumBu2B4pQkDai2DYb+cWhzKTC92Rd7gc5DYCOQsnuF6/PiSXj/tj4z8mssnLLvJZkW+FHsikBgBZAMWlaDA4Pb43','ce8a8844c3c1457f88366e0178f9d81d','2023-09-25 13:51:51.096473'),
('56d17b08ea8a43fca2b762b3d0eba288','pObo3ywL0TUun92CoVtluQ4XrrL/gxRrCR9GCQHBmok+4n5Zy6aVFrDBLVf/W72QHkutSJKVLilQNwGVZtt0Cdy8aG+4lHFnw8htjrcqfF7+lDnJ4zh0jSqnyeaxrz7IfdkHZoD45PdkcByuN2atNIVaLlkCZw==','ce8a8844c3c1457f88366e0178f9d81d','2023-09-25 13:01:35.992048'),
('6c452cfc4a0c4ee4906042cf57ab9c21','QqUR7lgg/pBI6xMg5GRjFgTPPMVF02Y/A/Sfh6F4jNRKHh3hkwKd8gYkYOFdfngeQc76r88PZtABH13G03yYjveYTHmsOUM7BrH+iFBuBJ2wEQk/G9QU0AVanV2xq6KCKGfa85SYcUI+wOQahUO3DAOx','ce8a8844c3c1457f88366e0178f9d81d','2023-09-21 15:12:01.975037'),
('a3be23411c764f29bbdc3eb3e3079ca1','UPMkWiJTTvKl03Y0KHPeBR2bgvIAljEVKZUn88AUXdwtph4cPHzpw9ziDhNmc/Y7ZL4VeQMH/a6fnX5A8OM23aNdUvXOJ2OqDH0Agt0nrqkw+OH6c0DjAyvrtiDQuIQXLQ/7sWlgfq36ATQvg92dLP0nhm7cZyMi','ce8a8844c3c1457f88366e0178f9d81d','2023-09-25 13:51:59.878935'),
('c53d35c00a8e45c59b5ed2f8780d8e02','XikTfXvxyDDH6wGB64WtHNssQtL4KxBOj4H5Q6R+cSkMd2NnyjRGsTpnBT80xi3r5JNZvhnJFJpmLrK5hgERaZe+5ImF1axWueKjcNX8jUDiljIOctuHIWVLv2NssBB4rtnwBvnVSBf2TchA0w00kQilNCQgGFOwcCU5Iu/LKsmNoQ==','ce8a8844c3c1457f88366e0178f9d81d','2023-09-22 13:52:08.859191'),
('c7d612328879425d88c09e6706e85806','52NVPh7+FocE7+I6csm1AoOrYr9UCR5IysWWg9Tztiing6mzeg2ZLjehWBxLGD8gLIUhwUab6P6bh6zvJ+xWkUTII98Niuj2+e/zhb3PVegZPG6MbkSdDunYbZ6qq3CSinC+7MOqp8+hrhd8qesPYJhcCw==','ce8a8844c3c1457f88366e0178f9d81d','2023-09-22 13:29:36.876462'),
('cbe3f6a5078d4014b7525b57c5d24dfa','GfhoTDMIkuRX6qvo1lVW/a+hIDpbBP5bfMDBx9ulF7X/4vYbaFd6Qx5W1qX6OAjWa5cZPtuSPqonEy4Lgb74O5ckGqwkrRW52+E6riWngB/KJRyP/SxWSL6LzSm5j3/BcbhZdvW8PqNoPI2frEG6rZNRygrBeeoPY7i8xG+tx4I6ibpXgKx3rXsIqbOpYA==','ce8a8844c3c1457f88366e0178f9d81d','2023-09-25 13:40:28.798125'),
('d84e722963d946379aa72178c2743e69','b3duDygeDW8dHSdagS3lW4YjoYhXwKanxyg29RGxeLad4NLOZlwwOXUwWTbb+hyKRzQ6uAvss1dClQa9Ndqoh0mO+9FoTjHy3CcD+Ka+enlHHvQ6aYrQsSzoj+aliQ8m/gCIfKhWwOgByXXG8q29Sgpde3JZ3MkDprb+91TPaoqtC48=','ce8a8844c3c1457f88366e0178f9d81d','2023-09-25 13:04:32.030512');
/*!40000 ALTER TABLE `Messages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Users` (
  `uuid` varchar(64) NOT NULL,
  `username` varchar(32) NOT NULL,
  `publicKey` varchar(4096) NOT NULL,
  `encryptedUserData` longtext DEFAULT NULL,
  `acceptsInvites` tinyint(1) NOT NULL DEFAULT 0,
  `encryptedInvitesData` longtext DEFAULT NULL,
  PRIMARY KEY (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES
('4f5cb3d6076e4e60842053de77be51a1','user2','-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjaP88rDIh50vK0KK016a\r\n8+q5FJel4lr4n5omV21kQR3ypBnwd734gW0Af/6B895MUl2teNQrWICI1roPfw5W\r\njETG2awY93MakU9/LS88AXYVl/+GDxLZCzLxNSCL11J29QIuDfliZgzVzweIbIH3\r\nNqY7DhXghK7RK+jdM935rUcOHgDw7mJE6N2SZaSMU1vma4WOWLxXeA2n3eM8H+76\r\ndnFOqfJ7A3HrhjaY8gzwmVPr3+FWk3vb/Sc2jn7olS6aP84TU19Rb+ppi1uz2DUM\r\nJSVhfnm5G1MuvNDDNOwf0k7iXWaBp/fezbO5Au8GWruscI/+usI0AP8Ay04/ALJW\r\nLQIDAQAB\r\n-----END PUBLIC KEY-----\r\n','XMScM+g6WIkn0asw235z9Ohs+YxoVi2xT5Vxdf6+Ii/vn145sKIRCa7r2puYXOrjycshf5IFZK3/EWvVDpqiPRGOk9yDp6PnNlwwwkPks3j90YQCOrgeQftM2yGRPw4eBwCOdA1NZ9exhS3dQiCF71rpGMtijpxesvTNjcUuXb6GZLWFU70jBNZoP95/PXh2fEQeeTM7lUZcinswlyVJT83gzwN2nn6/uPooL7RkPsvfEnPsqsvXJuKErU3s8obYqqr84YNwWpYIuawLIrIlDjhTndvxEexXbDZOk7lXamSTiRft04WOKnAV7qwXzHCQP7PNf1W4ujcCa/HlyBsnWlqZH/ls+RbqoXbzu+NAevj78PYHMo8bjGP8nKlakN5vo53nh++OBbIBCWyP3szW6Lt+Z9vEUhJh5Rwz4GTnW2oApmOBEU4RJftvsnV6WlRPD4RtbIAgGOIE00rDI2Nhn3YPumWzY+sn9TBtltOa+6SjQdWMfU1u2ZyqNdQHYwqzt3uFgspMOoUNUxqAvzBKof1KrHorD5/GuyT5sRG1Tg3Yc8BJcttKs//3+alvXMfyA8coUBuYRO9JeenMdM3uySeqPSK3+WTO64uI',1,NULL),
('5cb56bf944c84db2a84daf699a92375e','user1','-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAk/lde4F0/TaG4vSXtko7\r\nW+zldlmOFVX7aCMR1OwU75X3GhbcLaxos7hhjeyJilvtznulfXSTXB4dvC8A+UKY\r\nE7kDwQU22KOU6SB+Mf9pJrc3IlJi4n9ILKLinZD+B/AoPNwI+7okHtgxUce1hKfB\r\nOcOKPY4CsjMxtPAxqe3JBx+O6BUctlzjra4w5YRMyvEItMkYd1UcdF99cTwWFQLs\r\nqNebmHl760lqAcVZxF3I9j4qo2k7XN3Vdv7Muy6i6xcJ6EDKknO6Ewd81d3sSqb2\r\nJh9W+jBuz4GAyYRZycaOy613gkMo8VS21ouRGfR1DQ2PjTLhjCJ57JAWjKMXgi1n\r\nOwIDAQAB\r\n-----END PUBLIC KEY-----\r\n','rbAXW5Iy/5fTwmlwFUwkPEC0HgZJ4olvV5TwdJPyLTxXbD5CJtUx2gbvYCC82voBImLf5FguShYTCeOqRnkBNOlHw0IJ4cXCgh2H8QbaRvC5InxNISvdyieWJiIAmiN2xHX+Ppkm/7CwPSXDZc6YsikxrWzAUmkcJ/cbHMqLcv66khDeggiV7rR4+SSYB7rH7f00jChqEpYqFjMtviTDjEjERKtM0hOxCDaQcRSSkS3Yaln5zYT7oms2fIP++CvlcLynbW1tSov6b5cbiP67mtMQVa8yWjROMVFpqBBtav7TuFNPvAMsvwBF39906cZiA6/VcEBk9JZOEIEHQGncUTPl+Nvherh3bPBeWR7aGgWsdVQHnyKorWMfaHyXJU89ChOow9tMVRp0d2d6ksds+ZweKtdvDWIKbNSdDyAMIeKbt7MJJUAPMuGEcOWS1IRecer79DqD+i5Q22D9bKdOV1RJ0cDYzHQWQLnH3GeHjya89FFzzupKG9u8DeJRhpiW13NBx63Ce6wMOnV5G1xrdE3xHZECQfOsq7Tg0n+/F8AXNxMefkiZeB5LY1VwGe9YpOb5vbwhNR4lxq8igUbhLstbVjFgYFI7jFR4',1,NULL);
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES
('20230423094352_InitialCreate','7.0.5'),
('20230502092210_Invites','7.0.5'),
('20230502094253_UserAcceptsInvites','7.0.5'),
('20230505112438_UserInvites','7.0.5'),
('20230512085609_Channels','7.0.5'),
('20230512104226_UpdateInvites','7.0.5'),
('20230512110048_AddedInvitesData','7.0.5'),
('20230515082349_ChannelActive','7.0.5'),
('20230921111230_AddedMessages','7.0.5'),
('20230921123944_MessagesDateToDateTime','7.0.5');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-10-18 15:43:20
