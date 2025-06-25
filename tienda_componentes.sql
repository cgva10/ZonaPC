-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: tienda_componentes
-- ------------------------------------------------------
-- Server version	9.3.0

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
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) COLLATE utf8mb4_general_ci NOT NULL,
  `ProductVersion` varchar(32) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20250517150123_SeedUsuariosFix','9.0.4'),('20250521193138_AgregarTablaProductoImagen','9.0.4'),('20250529201757_AgregarEstadoPedido','9.0.4'),('20250602133424_CambiarEstadoPedidoAString','9.0.4'),('20250616145355_AgregaCostoEnvioAlPedido','9.0.4'),('20250624133851_Version1.0','9.0.4');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carritoproductos`
--

DROP TABLE IF EXISTS `carritoproductos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carritoproductos` (
  `CarritoId` int NOT NULL,
  `ProductoId` int NOT NULL,
  `Id` int NOT NULL,
  `Cantidad` int NOT NULL,
  PRIMARY KEY (`CarritoId`,`ProductoId`),
  KEY `IX_CarritoProductos_ProductoId` (`ProductoId`),
  CONSTRAINT `FK_CarritoProductos_Carritos_CarritoId` FOREIGN KEY (`CarritoId`) REFERENCES `carritos` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_CarritoProductos_Productos_ProductoId` FOREIGN KEY (`ProductoId`) REFERENCES `productos` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carritoproductos`
--

LOCK TABLES `carritoproductos` WRITE;
/*!40000 ALTER TABLE `carritoproductos` DISABLE KEYS */;
INSERT INTO `carritoproductos` VALUES (3,12,0,1),(6,1,0,1),(8,10,0,1),(9,10,0,1),(12,12,0,1),(16,3,0,1),(18,13,0,1),(24,11,0,1),(28,11,0,1),(29,11,0,1),(30,11,0,1),(31,11,0,1),(32,11,0,1),(33,11,0,1),(34,11,0,1),(35,11,0,1),(36,11,0,1),(37,11,0,1),(38,11,0,1),(40,12,0,3),(41,12,0,3),(42,12,0,11),(44,13,0,3),(57,11,0,1);
/*!40000 ALTER TABLE `carritoproductos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `carritos`
--

DROP TABLE IF EXISTS `carritos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carritos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UsuarioId` int DEFAULT NULL,
  `SesionId` longtext COLLATE utf8mb4_general_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_Carritos_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Carritos_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carritos`
--

LOCK TABLES `carritos` WRITE;
/*!40000 ALTER TABLE `carritos` DISABLE KEYS */;
INSERT INTO `carritos` VALUES (1,2,NULL),(2,NULL,NULL),(3,NULL,NULL),(4,NULL,NULL),(5,1,NULL),(6,NULL,NULL),(7,NULL,NULL),(8,NULL,NULL),(9,NULL,NULL),(10,NULL,NULL),(11,NULL,NULL),(12,NULL,NULL),(13,NULL,NULL),(14,NULL,NULL),(15,NULL,NULL),(16,NULL,NULL),(17,NULL,NULL),(18,NULL,NULL),(19,NULL,NULL),(20,NULL,NULL),(21,NULL,NULL),(22,NULL,NULL),(23,NULL,NULL),(24,NULL,NULL),(25,NULL,NULL),(26,NULL,NULL),(27,NULL,NULL),(28,NULL,NULL),(29,NULL,NULL),(30,NULL,NULL),(31,NULL,NULL),(32,NULL,NULL),(33,NULL,NULL),(34,NULL,NULL),(35,NULL,NULL),(36,NULL,NULL),(37,NULL,NULL),(38,NULL,NULL),(39,NULL,NULL),(40,NULL,NULL),(41,NULL,NULL),(42,NULL,NULL),(43,NULL,NULL),(44,NULL,NULL),(45,NULL,NULL),(46,NULL,NULL),(47,NULL,NULL),(48,NULL,NULL),(49,NULL,NULL),(50,NULL,NULL),(51,NULL,NULL),(52,NULL,NULL),(53,NULL,NULL),(54,NULL,NULL),(55,NULL,NULL),(56,NULL,NULL),(57,NULL,NULL),(58,NULL,NULL),(59,NULL,NULL);
/*!40000 ALTER TABLE `carritos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detallespedido`
--

DROP TABLE IF EXISTS `detallespedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detallespedido` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PedidoId` int NOT NULL,
  `ProductoId` int NOT NULL,
  `Cantidad` int NOT NULL,
  `PrecioUnitario` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_DetallesPedido_PedidoId` (`PedidoId`),
  KEY `IX_DetallesPedido_ProductoId` (`ProductoId`),
  CONSTRAINT `FK_DetallesPedido_Pedidos_PedidoId` FOREIGN KEY (`PedidoId`) REFERENCES `pedidos` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_DetallesPedido_Productos_ProductoId` FOREIGN KEY (`ProductoId`) REFERENCES `productos` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detallespedido`
--

LOCK TABLES `detallespedido` WRITE;
/*!40000 ALTER TABLE `detallespedido` DISABLE KEYS */;
/*!40000 ALTER TABLE `detallespedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pedidos`
--

DROP TABLE IF EXISTS `pedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pedidos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UsuarioId` int NOT NULL,
  `Fecha` datetime(6) NOT NULL,
  `Total` decimal(65,30) NOT NULL,
  `Provincia` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `Localidad` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `Estado` varchar(20) COLLATE utf8mb4_general_ci NOT NULL,
  `CostoEnvio` decimal(65,30) NOT NULL DEFAULT '0.000000000000000000000000000000',
  PRIMARY KEY (`Id`),
  KEY `IX_Pedidos_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Pedidos_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedidos`
--

LOCK TABLES `pedidos` WRITE;
/*!40000 ALTER TABLE `pedidos` DISABLE KEYS */;
/*!40000 ALTER TABLE `pedidos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productoimagenes`
--

DROP TABLE IF EXISTS `productoimagenes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productoimagenes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Url` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `ProductoId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ProductoImagenes_ProductoId` (`ProductoId`),
  CONSTRAINT `FK_ProductoImagenes_Productos_ProductoId` FOREIGN KEY (`ProductoId`) REFERENCES `productos` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=95 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productoimagenes`
--

LOCK TABLES `productoimagenes` WRITE;
/*!40000 ALTER TABLE `productoimagenes` DISABLE KEYS */;
INSERT INTO `productoimagenes` VALUES (1,'https://i.postimg.cc/xTm3WXS1/71-FPIhwpl-IL-AC-SL1500-removebg-preview.png',1),(2,'https://i.postimg.cc/d1DVyKVh/71-Bw3-IPL4e-L-AC-SL1500-removebg-preview.png',1),(3,'https://i.postimg.cc/v81fXgFF/71q3-I4c-Cm-FL-AC-SL1500-removebg-preview.png',2),(4,'https://i.postimg.cc/8kqWYpNF/81c-Adtqu-6-L-AC-SL1500-removebg-preview.png',2),(5,'https://i.postimg.cc/kgCSnTVq/71-Pb-V5-P14-DL-AC-SL1500-removebg-preview.png',3),(6,'https://i.postimg.cc/6phvTCRR/71ps-Wy-Si-MAL-AC-SL1500-removebg-preview.png',3),(7,'https://i.postimg.cc/YSnLzkdC/61-RPELhlxy-L-AC-SL1280-removebg-preview.png',4),(8,'https://i.postimg.cc/pVn5zwzP/61-Rz-HKn-Bxi-L-AC-SL1280-removebg-preview.png',4),(9,'https://i.postimg.cc/wj9tK3PB/61l-Fr6-FEp-L-AC-SL1500-removebg-preview.png',5),(10,'https://i.postimg.cc/9fnzqbf4/71-Sqt8-X-Mf-L-AC-SL1500-removebg-preview.png',5),(11,'https://i.postimg.cc/zfjVXNsk/71-Sj3-WKnl-SL-AC-SL1500-removebg-preview.png',6),(12,'https://i.postimg.cc/2876xr6N/815-M4vy8-JL-AC-SL1500-removebg-preview.png',6),(13,'https://i.postimg.cc/fRWb2ptt/video-geforce-rtx-5070-ti-16gb-msi-ventus-3x-oc-1-removebg-preview.png',7),(14,'https://i.postimg.cc/XJ5NB0hh/video-geforce-rtx-5070-t-removebg-preview.png',7),(15,'https://i.postimg.cc/8cB1wmTm/Placa-Video-Ge-Force-RTX-5080-16-Gb-Zotac-Solid-Oc-48925-2-removebg-preview.png',8),(16,'https://i.postimg.cc/3rgJKY20/Placa-Video-Ge-Force-RTX-5080-16-Gb-Zotac-Solid-Oc-48925-1-removebg-preview.png',8),(17,'https://i.postimg.cc/pLvsgYRn/81kxce-Al-LL-AC-SL1500-removebg-preview.png',9),(18,'https://i.postimg.cc/y8HRdX67/61jk-TDo-KABL-AC-SL1200.jpg',9),(19,'https://i.postimg.cc/MGZj06j4/71-RQr-1-JTj-L-AC-SL1200.jpg',9),(20,'https://i.postimg.cc/4dYPzWbj/81-Gr-Ceu-Czx-L-AC-SL1500-removebg-preview.png',10),(21,'https://i.postimg.cc/JnMD71RK/818s2k-MJ6t-L-AC-SL1500.jpg',10),(22,'https://i.postimg.cc/rp6Rb1bB/81-GYc-CUJDPL-AC-SL1500.jpg',10),(23,'https://i.postimg.cc/hG9QqJRf/81-Xf18-Sxl-PL-AC-SL1500.jpg',10),(28,'https://i.postimg.cc/NGJYGF5M/71-Mf-Zpn-DWRL-AC-SL1500-removebg-preview.png',12),(29,'https://i.postimg.cc/YC1Gg4DM/81e-COC21-L1-L-AC-SL1500.jpg',12),(30,'https://i.postimg.cc/6Qt7h9FR/81-Oy-AEKBg-QL-AC-SL1500.jpg',12),(31,'https://i.postimg.cc/pVFbvCb0/71-IV0-S6-EC8-L-AC-SL1500-removebg-preview.png',13),(32,'https://i.postimg.cc/TYZfVfp9/61-O4-SZC17-CL-AC-SL1500.jpg',13),(33,'https://i.postimg.cc/5tmJWzp7/71-Naaa-TIz-UL-AC-SL1500.jpg',13),(34,'https://i.postimg.cc/zBXrzfcJ/81-G9e-Pr-C0c-L-AC-SL1500.jpg',13),(39,'https://i.postimg.cc/DfSbkFvR/61lz-Lb-V-EHL-AC-SL1500-removebg-preview.png',15),(40,'https://i.postimg.cc/mgscHHYG/71-Tx-Z8-W9pz-L-AC-SL1500-removebg-preview.png',15),(41,'https://i.postimg.cc/K8Jz4VW5/615-KZAmb-Sd-L-AC-SL1000.png',15),(42,'https://i.postimg.cc/VNvNtgQP/716ys-J5-Bnp-L-AC-SL1000.png',15),(43,'https://i.postimg.cc/8PCzJG2n/71-Ll-WLWm24-L-AC-SL1500.jpg',16),(44,'https://i.postimg.cc/4yW4L295/61pp0la-CIb-L-AC-SL1200.jpg',16),(45,'https://i.postimg.cc/y6jN8xbZ/61vzt-Bk-g0-L-AC-SL1500.jpg',16),(46,'https://i.postimg.cc/5NkN8G19/71d1hs-WCs-WL-AC-SL1500.jpg',16),(47,'https://i.postimg.cc/YCDHHcqy/61mhmh-Ht-Ip-L-AC-SL1141-removebg-preview.png',17),(48,'https://i.postimg.cc/Kjwjcgsh/41-U2-Ynfa-Ox-L-AC-SL1272.png',17),(49,'https://i.postimg.cc/bvGjY6ZY/51qah-RCWo-GL-AC-SL1348-removebg-preview.png',17),(50,'https://i.postimg.cc/5yShKTRg/41jc1n-Cj4-DL-AC-SL1131-removebg-preview.png',17),(51,'https://i.postimg.cc/XNktWMsc/61g-Kk-YQn6l-L-AC-SL1500-removebg-preview.png',18),(52,'https://i.postimg.cc/pd2Q8gkc/51-JDZf0zn-WL-AC-SL1000.png',18),(53,'https://i.postimg.cc/9z5ZfSrp/51-Yh-Ii-UU7g-L-AC-SL1000.png',18),(54,'https://i.postimg.cc/KvV6qP9d/61-QDga-B9-z-L-AC-SL1500-removebg-preview.png',19),(55,'https://i.postimg.cc/GmyZZ4V9/61i-Kly-FFd-HL-AC-SL1500-removebg-preview.png',19),(56,'https://i.postimg.cc/rp2vc82j/61bl-C7o-9-OL-AC-SL1500-removebg-preview.png',19),(57,'https://i.postimg.cc/0Qc3pDMX/51-KS-A63-CYL-AC-SL1000-removebg-preview.png',20),(58,'https://i.postimg.cc/8ccYwcLp/51-Sloaj7-q-L-AC-SL1000-removebg-preview.png',20),(59,'https://i.postimg.cc/RhYDYmTc/51vpx-HDs-ML-AC-SL1000-removebg-preview.png',20),(60,'https://i.postimg.cc/WbQy62zV/619foh-Q4g-KL-AC-SL1500-removebg-preview.png',21),(61,'https://i.postimg.cc/ZqLDSpcY/71ec-WXw-UEi-L-AC-SL1500-removebg-preview.png',21),(62,'https://i.postimg.cc/vTC0zzQh/61-Y8jx-GUtm-L-AC-SL1500-removebg-preview.png',21),(63,'https://i.postimg.cc/rwXhRQZF/61d-NQw-Df-Q6-L-AC-SL1500-removebg-preview.png',21),(64,'https://i.postimg.cc/Kvr9Lt6P/71-I4zel-VSf-L-AC-SL1500-removebg-preview.png',22),(65,'https://i.postimg.cc/RZgXp4Vq/71-Dq5-GVb-Y9-L-AC-SL1500-removebg-preview.png',22),(66,'https://i.postimg.cc/d1DyjCXg/718-Bb-PYLJRL-AC-SL1500.png',22),(81,'https://i.postimg.cc/pLYVtVTJ/584cd073456745ca9390f3a6409fe98b-removebg-preview.png',24),(82,'https://i.postimg.cc/W4NNpRqL/0fee75812545413ca807dbf7b47eeda9-removebg-preview.png',24),(83,'https://i.postimg.cc/853NbjKN/97fb32c0c458418189ce13197b0e9c28-removebg-preview.png',24),(88,'https://i.postimg.cc/RFmCtYBZ/71-F-Wcriq4-L-AC-SL1500-removebg-preview.png',11),(89,'https://i.postimg.cc/90FW8qz3/61-oc1-K-Wk-L-AC-SL1500-removebg-preview.png',11),(90,'https://i.postimg.cc/Pf2fLKFd/61c-WY5nx-Kh-L-AC-SL1500-removebg-preview.png',11),(91,'https://i.postimg.cc/ZKBMxZhw/61igk-EY73-KL-AC-SL1000.jpg',11);
/*!40000 ALTER TABLE `productoimagenes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `Descripcion` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `Precio` decimal(18,2) NOT NULL,
  `Stock` int NOT NULL,
  `Categoria` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `Subcategoria` longtext COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (1,'MSI GeForce RTX 4060 Ti Ventus 2X 16GB','Placa de video MSI con chipset Nvidia RTX 4060 Ti. Edición Ventus 2X, 16GB GDDR6.',499999.00,15,'Placas de Video','Nvidia'),(2,'Zotac GeForce RTX 3080 Ti 12GB','Placa de video Zotac con chipset Nvidia RTX 3080 Ti. Edición 12GB GDDR6X.',719999.00,8,'Placas de Video','Nvidia'),(3,'PNY GeForce RTX 2060 XLR8 6GB OC','Placa de video PNY Nvidia RTX 2060 6GB OC Edición XLR8.',299999.00,12,'Placas de Video','Nvidia'),(4,'EVGA GeForce RTX 3070 XC3 Ultra 8GB','Placa de video EVGA GeForce RTX 3070 XC3 Ultra Gaming, 8GB GDDR6.',449999.00,10,'Placas de Video','Nvidia'),(5,'Gigabyte RTX 4070 WINDFORCE 2X OC 12GB','Placa de video Gigabyte GeForce RTX 4070 WINDFORCE 2X OC 12GB GDDR6.',589999.00,9,'Placas de Video','Nvidia'),(6,'ASUS TUF Gaming RTX 5070 12GB GDDR7','Tarjeta gráfica ASUS TUF Gaming RTX 5070 12GB GDDR7 OC Edition. PCIe 5.0, HDMI/DP 2.1.',649999.00,7,'Placas de Video','Nvidia'),(7,'MSI RTX 5070 Ti Ventus 3X OC 16GB','Tarjeta gráfica MSI RTX 5070 Ti Ventus 3X OC 16GB GDDR7, arquitectura Blackwell.',719999.00,8,'Placas de Video','Nvidia'),(8,'Zotac GeForce RTX 5080 16GB Solid OC','Placa de video Zotac GeForce RTX 5080 16GB Solid OC. Edición OC de alto rendimiento.',859999.00,5,'Placas de Video','Nvidia'),(9,'ASUS TUF Gaming A15','Laptop para juegos, FHD 15.6\'\' 144 Hz, RTX 2050, Ryzen 5 7535HS, 8GB DDR5, 512GB SSD, Wi-Fi 6, Win11',589999.00,6,'Notebook','Gaming'),(10,'ASUS ROG Strix G16','165Hz, RTX 4060, Intel i7-13650HX, 16GB DDR5, 1TB SSD, Wi-Fi 6E, Win11',829999.00,12,'Notebook','Gaming'),(11,'Acer Nitro V 15','Intel i5-13420H, RTX 4050, 15.6\'\' FHD 144Hz, 16GB DDR5, 512GB SSD Gen4, Wi-Fi',719999.00,1,'Notebook','Gaming'),(12,'MSI Thin 15 B13VE','Intel i7-13620H, RTX 4050, 15.6\'\' FHD 144Hz, 16GB RAM, 512GB SSD, Wi-Fi 6E',699999.00,11,'Notebook','Gaming'),(13,'Dell G16 7630','QHD+ 240Hz, Intel i9-13900HX, 16GB DDR5, 1TB SSD, RTX 4070, Win11',899999.00,3,'Notebook','Gaming'),(15,'Acer Chromebook Plus 514','Laptop con pantalla táctil IPS Full HD de 14\'\' (1920x1080), Intel Core i3-N305, 8 GB LPDDR5, 128 GB SSD, Wi-Fi 6E, Cámara FHD, Chrome OS',419999.00,0,'Notebook','Oficina'),(16,'Lenovo V15 Ryzen 5 7520U','Notebook para hogar y oficina con pantalla 15.6\'\' FHD, Ryzen 5 7520U, 8 GB RAM, 512 GB SSD, teclado numérico, Wi-Fi 6, cámara web con obturador',479999.00,16,'Notebook','Oficina'),(17,'Samsung Galaxy Book4 2024','Laptop de negocios 15.6\'\' FHD IPS, Intel 7 150U (10 núcleos), 16 GB RAM, 512 GB SSD, Wi-Fi 6, teclado retroiluminado, sensor de huella, Windows 11',559999.00,9,'Notebook','Oficina'),(18,'Acer Aspire 3 A315-24P','Notebook IPS Full HD 15.6\'\', AMD Ryzen 3 7320U, 8 GB LPDDR5, 128 GB SSD NVMe, Wi-Fi 6, Windows 11 Home S',399999.00,18,'Notebook','Oficina'),(19,'XFX Radeon RX 7900XT','Tarjeta gráfica para juegos con 20 GB GDDR6, AMD RDNA 3 RX-79TMBABF9',999999.00,10,'Placas de Video','AMD'),(20,'PowerColor Reaper AMD Radeon RX 9070','Tarjeta gráfica PowerColor con 16 GB GDDR6, ideal para gaming extremo y alto rendimiento',799999.00,8,'Placas de Video','AMD'),(21,'XFX Speedster SWFT210 Radeon RX 7600','Tarjeta gráfica con 8 GB GDDR6, HDMI y 3 DisplayPorts, basada en la arquitectura AMD RDNA 3',529999.00,12,'Placas de Video','AMD'),(22,'Radeon RX 5700 XT','Tarjeta gráfica con 8 GB GDDR6, arquitectura RDNA, base/boost 1755/1905 MHz, con placa trasera metálica. Soporte DirectX 12 y PCIe 4.0',449999.00,4,'Placas de Video','AMD'),(24,'GIGABYTE AMD Radeon RX 6600 EAGLE','Procesador gráfico: Radeon RX 6600\r\nBoost Clock: hasta 2491 MHz\r\nGame Clock: hasta 2044 MHz\r\nMemoria: 8 GB GDDR6 - 128 bit - 14000 MHz\r\nOutput: 2x HDMI 2.1 / 2x DisplayPort 1.4a\r\nResolución máx: 7680x4320 - PSU recomendada: 500W\r\nTamaño: 282x113x41 mm - OpenGL 4.6 / DirectX 12',374999.00,5,'Placas de Video','AMD');
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Rol` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `Nombre` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `Email` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `Contrasena` longtext COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'admin','Admin','admin@zonapc.com','$2a$11$/otiQJfoIzvwesO1oxUtoeoZjUfV944CiNBmw.e7XQA0WoGMBcOGW'),(2,'user','Usuario','usuario@zonapc.com','$2a$11$4KvS64LckxO3SPFlXd.bdeTJu7asqbHtOHxlsjJ2L4EGK1qrVKvdi');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-24 11:24:49
