-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: 18-Set-2019 às 05:36
-- Versão do servidor: 8.0.13-4
-- versão do PHP: 7.2.19-0ubuntu0.18.04.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `t433CSHRCf`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `Asset`
--

CREATE TABLE `Asset` (
  `Symbol` varchar(10) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `Name` varchar(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci DEFAULT NULL,
  `Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `Asset`
--

INSERT INTO `Asset` (`Symbol`, `Name`, `Id`) VALUES
('MGLU3', 'Magazine Luiza', 1),
('ABEV3', 'Ambev', 2);

-- --------------------------------------------------------

--
-- Estrutura da tabela `AssetPrice`
--

CREATE TABLE `AssetPrice` (
  `AssetPriceId` int(11) NOT NULL,
  `AssetId` int(11) NOT NULL,
  `Close` decimal(18,4) NOT NULL,
  `Date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `AssetPrice`
--

INSERT INTO `AssetPrice` (`AssetPriceId`, `AssetId`, `Close`, `Date`) VALUES
(1, 1, '10.1600', '2019-09-18');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Asset`
--
ALTER TABLE `Asset`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `AssetPrice`
--
ALTER TABLE `AssetPrice`
  ADD PRIMARY KEY (`AssetPriceId`),
  ADD KEY `AssetId` (`AssetId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Asset`
--
ALTER TABLE `Asset`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `AssetPrice`
--
ALTER TABLE `AssetPrice`
  MODIFY `AssetPriceId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Constraints for dumped tables
--

--
-- Limitadores para a tabela `AssetPrice`
--
ALTER TABLE `AssetPrice`
  ADD CONSTRAINT `AssetPrice_ibfk_1` FOREIGN KEY (`AssetId`) REFERENCES `Asset` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
