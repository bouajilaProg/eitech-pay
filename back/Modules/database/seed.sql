-- Disable foreign key checks to allow deletion of data in any order
SET FOREIGN_KEY_CHECKS = 0;

-- Delete all existing data from tables
DELETE FROM `admin`;
DELETE FROM `black_listed`;
DELETE FROM `licence`;
DELETE FROM `licence_activation`;
DELETE FROM `licence_bundle`;
DELETE FROM `licence_option`;
DELETE FROM `licence_order`;
DELETE FROM `monthly_revenue`;
DELETE FROM `stats`;
DELETE FROM `subscription`;
DELETE FROM `subscription_order`;
DELETE FROM `subscription_tier`;
DELETE FROM `users`;

-- Reset auto-increment counters for tables with auto-incrementing primary keys
ALTER TABLE `admin` AUTO_INCREMENT = 1;
ALTER TABLE `black_listed` AUTO_INCREMENT = 1;
ALTER TABLE `licence_activation` AUTO_INCREMENT = 1;
ALTER TABLE `licence_order` AUTO_INCREMENT = 1;
ALTER TABLE `subscription_order` AUTO_INCREMENT = 1;
ALTER TABLE `users` AUTO_INCREMENT = 1;

-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS = 1;

-- Seed data for `admin` table
INSERT INTO `admin` (`admin_id`, `username`, `password`, `api_key`, `is_archived`) VALUES
(1, 'admin_eitech', 'another-hashed-password', 'eitech_admin_key_456', 0);

-- Seed data for `users` table
INSERT INTO `users` (`user_id`, `first_name`, `last_name`, `email`, `phone`, `is_archived`) VALUES
(1, 'Alice', 'Smith', 'alice.smith@example.com', '+1122334455', 0),
(2, 'Bob', 'Johnson', 'bob.j@example.com', '+9988776655', 0);

-- Seed data for `licence` table
INSERT INTO `licence` (`licence_id`, `licence_name`, `description`, `max_devices`, `duration`, `grace_period`, `public_key`, `price`, `is_archived`) VALUES
('LIC-PRO-001', 'Pro Licence', 'Full features, ideal for professionals.', 5, 365, '7 days', 'pubkey_pro_001_xyz', 99.99, 0),
('LIC-BASIC-001', 'Basic Licence', 'Essential features for everyday use.', 1, 180, '3 days', 'pubkey_basic_001_abc', 49.99, 0);

-- Seed data for `licence_option` table
INSERT INTO `licence_option` (`option_id`, `licence_id`, `option_name`, `description`, `price`, `is_archived`) VALUES
('OPT-SUPPORT-PREM', 'LIC-PRO-001', 'Premium Support', '24/7 dedicated support.', 29.99, 0),
('OPT-CLOUD-STORAGE', 'LIC-PRO-001', 'Cloud Storage 50GB', '50GB of secure cloud storage.', 15.00, 0),
('OPT-ADD-DEVICE', 'LIC-BASIC-001', 'Additional Device Slot', 'Add one more device to your licence.', 10.00, 0);

-- Seed data for `licence_order` table
INSERT INTO `licence_order` (`licence_order_id`, `user_id`, `licence_id`, `private_key`, `purchase_date`, `status`, `reseller`, `is_archived`) VALUES
(1, 1, 'LIC-PRO-001', 'privkey_order_001_def', '2025-01-15 10:00:00', 'Active', 'Direct', 0),
(2, 2, 'LIC-BASIC-001', 'privkey_order_002_ghi', '2025-03-20 14:30:00', 'Active', 'Reseller A', 0);

-- Seed data for `licence_bundle` table
INSERT INTO `licence_bundle` (`licence_order_id`, `option_id`) VALUES
(1, 'OPT-SUPPORT-PREM'),
(1, 'OPT-CLOUD-STORAGE'),
(2, 'OPT-ADD-DEVICE');

-- Seed data for `licence_activation` table
INSERT INTO `licence_activation` (`activation_id`, `licence_order_id`, `device_print`, `user_id`, `activation_date`, `is_archived`) VALUES
(1, 1, 'device_hash_alpha', 1, '2025-01-15 10:05:00', 0),
(2, 1, 'device_hash_beta', 1, '2025-01-16 09:00:00', 0),
(3, 2, 'device_hash_gamma', 2, '2025-03-20 14:35:00', 0);

-- Seed data for `black_listed` table
INSERT INTO `black_listed` (`id`, `ip`, `user_id`, `type`, `blocked_date`, `recovery_date`) VALUES
(1, '192.168.1.100', 1, 'IP', '2025-05-01 11:00:00', NULL),
(2, '10.0.0.50', 2, 'User', '2025-06-01 09:00:00', '2025-06-08 09:00:00');

-- Seed data for `subscription` table
INSERT INTO `subscription` (`subscription_id`, `subscription_name`, `description`, `is_archived`) VALUES
('SUB-PREM-SERV', 'Premium Services', 'Access to all premium services and features.', 0),
('SUB-BASIC-SERV', 'Basic Services', 'Access to essential services.', 0);

-- Seed data for `subscription_tier` table
INSERT INTO `subscription_tier` (`tier_id`, `subscription_id`, `tier_name`, `duration`, `grace_period`, `price`, `is_archived`, `description`) VALUES
('TIER-PREM-YR', 'SUB-PREM-SERV', 'Yearly Premium', 365, 14, 199.99, 0, 'Annual subscription for premium services.'),
('TIER-BASIC-MON', 'SUB-BASIC-SERV', 'Monthly Basic', 30, 7, 9.99, 0, 'Monthly subscription for basic services.');

-- Seed data for `subscription_order` table
INSERT INTO `subscription_order` (`order_id`, `subscription_id`, `subscription_tier_id`, `user_id`, `purchase_date`, `start_date`, `end_date`, `status`, `reseller`, `is_archived`) VALUES
(1, 'SUB-PREM-SERV', 'TIER-PREM-YR', 1, '2024-12-01 08:00:00', '2025-01-01 00:00:00', '2026-01-01 00:00:00', 'Active', 'Direct', 0),
(2, 'SUB-BASIC-SERV', 'TIER-BASIC-MON', 2, '2025-05-10 12:00:00', '2025-05-10 12:00:00', '2025-06-10 12:00:00', 'Active', 'Reseller B', 0);

-- Seed data for `monthly_revenue` table
INSERT INTO `monthly_revenue` (`month`, `product_id`, `type`, `revenue`) VALUES
('2025-01-01', 'LIC-PRO-001', 'Licence', 99.99),
('2025-03-01', 'LIC-BASIC-001', 'Licence', 49.99),
('2025-01-01', 'SUB-PREM-SERV', 'Subscription', 199.99),
('2025-05-01', 'SUB-BASIC-SERV', 'Subscription', 9.99);

-- Seed data for `stats` table
-- Note: These values would typically be aggregated from other tables,
-- but for seeding, we'll insert example aggregate values.
INSERT INTO `stats` (`total_sales`, `total_revenue`, `users_count`) VALUES
(5, 359.96, 2);
