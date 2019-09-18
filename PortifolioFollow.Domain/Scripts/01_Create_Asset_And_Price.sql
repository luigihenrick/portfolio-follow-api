-- phpMyAdmin SQL Dump
-- version 4.7.1
-- https://www.phpmyadmin.net/
--
-- Host: sql10.freesqldatabase.com
-- Tempo de geração: 18/09/2019 às 04:04
-- Versão do servidor: 5.5.58-0ubuntu0.14.04.1
-- Versão do PHP: 7.0.33-0ubuntu0.16.04.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `sql10305297`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `Asset`
--

CREATE TABLE `Asset` (
  `Id` int(11) NOT NULL,
  `Symbol` varchar(10) CHARACTER SET latin1 NOT NULL,
  `Name` varchar(50) CHARACTER SET latin1 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Fazendo dump de dados para tabela `Asset`
--

INSERT INTO `Asset` (`Id`, `Symbol`, `Name`) VALUES
(1, 'MGLU3', 'Magazine Luiza');

-- --------------------------------------------------------

--
-- Estrutura para tabela `AssetPrice`
--

CREATE TABLE `AssetPrice` (
  `AssetId` int(11) NOT NULL,
  `Close` decimal(18,4) NOT NULL,
  `Date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Índices de tabelas apagadas
--

--
-- Índices de tabela `Asset`
--
ALTER TABLE `Asset`
  ADD PRIMARY KEY (`Id`);

--
-- Índices de tabela `AssetPrice`
--
ALTER TABLE `AssetPrice`
  ADD KEY `AssetId` (`AssetId`);

--
-- Restrições para dumps de tabelas
--

--
-- Restrições para tabelas `AssetPrice`
--
ALTER TABLE `AssetPrice`
  ADD CONSTRAINT `AssetPrice_ibfk_1` FOREIGN KEY (`AssetId`) REFERENCES `Asset` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
