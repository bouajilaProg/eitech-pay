-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: mariadb:3306
-- Generation Time: Jun 23, 2025 at 08:07 AM
-- Server version: 11.7.2-MariaDB-ubu2404
-- PHP Version: 8.2.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `eitech`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `admin_id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `api_key` varchar(255) NOT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`admin_id`, `username`, `password`, `api_key`, `is_archived`) VALUES
(1, 'admin', 'hashed-password', 'key123', 0);

-- --------------------------------------------------------

--
-- Table structure for table `black_listed`
--

CREATE TABLE `black_listed` (
  `id` int(11) NOT NULL,
  `ip` varchar(255) NOT NULL,
  `user_id` int(11) NOT NULL,
  `type` enum('IP','User','Device') NOT NULL,
  `blocked_date` timestamp NOT NULL,
  `recovery_date` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `licence`
--

CREATE TABLE `licence` (
  `licence_id` varchar(255) NOT NULL,
  `licence_name` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `max_devices` int(11) NOT NULL,
  `duration` int(11) NOT NULL,
  `grace_period` varchar(11) NOT NULL,
  `public_key` text NOT NULL,
  `price` decimal(12,2) NOT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `licence_activation`
--

CREATE TABLE `licence_activation` (
  `activation_id` int(11) NOT NULL,
  `licence_order_id` int(11) NOT NULL,
  `device_print` varchar(255) NOT NULL,
  `user_id` int(11) NOT NULL,
  `activation_date` timestamp NOT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `licence_bundle`
--

CREATE TABLE `licence_bundle` (
  `licence_order_id` int(11) NOT NULL,
  `option_id` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `licence_option`
--

CREATE TABLE `licence_option` (
  `option_id` varchar(255) NOT NULL,
  `licence_id` varchar(255) NOT NULL,
  `option_name` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `price` decimal(12,2) NOT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `licence_order`
--

CREATE TABLE `licence_order` (
  `licence_order_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `licence_id` varchar(255) NOT NULL,
  `private_key` text NOT NULL,
  `purchase_date` timestamp NOT NULL,
  `status` enum('Active','Expired','Canceled') NOT NULL,
  `reseller` varchar(255) NOT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `monthly_revenue`
--

CREATE TABLE `monthly_revenue` (
  `month` date NOT NULL,
  `product_id` varchar(255) NOT NULL,
  `type` enum('Subscription','Licence') NOT NULL,
  `revenue` decimal(12,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `stats`
--

CREATE TABLE `stats` (
  `total_sales` int(11) NOT NULL,
  `total_revenue` decimal(12,2) NOT NULL,
  `users_count` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `subscription`
--

CREATE TABLE `subscription` (
  `subscription_id` varchar(255) NOT NULL,
  `subscription_name` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `subscription_order`
--

CREATE TABLE `subscription_order` (
  `order_id` int(11) NOT NULL,
  `subscription_id` varchar(255) NOT NULL,
  `subscription_tier_id` varchar(255) NOT NULL,
  `user_id` int(11) NOT NULL,
  `purchase_date` timestamp NOT NULL,
  `start_date` timestamp NOT NULL,
  `end_date` timestamp NOT NULL,
  `status` enum('Active','Expired','Canceled') NOT NULL,
  `reseller` varchar(255) NOT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `subscription_tier`
--

CREATE TABLE `subscription_tier` (
  `tier_id` varchar(255) NOT NULL,
  `subscription_id` varchar(255) NOT NULL,
  `tier_name` varchar(255) NOT NULL,
  `duration` int(11) NOT NULL,
  `grace_period` int(11) NOT NULL,
  `price` decimal(12,2) NOT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0,
  `description` varchar(300) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `first_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `phone` varchar(50) DEFAULT NULL,
  `is_archived` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `first_name`, `last_name`, `email`, `phone`, `is_archived`) VALUES
(1, 'John', 'Doe', 'john@example.com', '+123456789', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`admin_id`);

--
-- Indexes for table `black_listed`
--
ALTER TABLE `black_listed`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `licence`
--
ALTER TABLE `licence`
  ADD PRIMARY KEY (`licence_id`);

--
-- Indexes for table `licence_activation`
--
ALTER TABLE `licence_activation`
  ADD PRIMARY KEY (`activation_id`);

--
-- Indexes for table `licence_bundle`
--
ALTER TABLE `licence_bundle`
  ADD PRIMARY KEY (`licence_order_id`,`option_id`);

--
-- Indexes for table `licence_option`
--
ALTER TABLE `licence_option`
  ADD PRIMARY KEY (`option_id`);

--
-- Indexes for table `licence_order`
--
ALTER TABLE `licence_order`
  ADD PRIMARY KEY (`licence_order_id`);

--
-- Indexes for table `subscription`
--
ALTER TABLE `subscription`
  ADD PRIMARY KEY (`subscription_id`);

--
-- Indexes for table `subscription_order`
--
ALTER TABLE `subscription_order`
  ADD PRIMARY KEY (`order_id`);

--
-- Indexes for table `subscription_tier`
--
ALTER TABLE `subscription_tier`
  ADD PRIMARY KEY (`tier_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin`
--
ALTER TABLE `admin`
  MODIFY `admin_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `black_listed`
--
ALTER TABLE `black_listed`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `licence_activation`
--
ALTER TABLE `licence_activation`
  MODIFY `activation_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `licence_order`
--
ALTER TABLE `licence_order`
  MODIFY `licence_order_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `subscription_order`
--
ALTER TABLE `subscription_order`
  MODIFY `order_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
