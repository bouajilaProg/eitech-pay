
CREATE TABLE admin (
    AdminId SERIAL PRIMARY KEY,
    Username TEXT NOT NULL,
CREATE TABLE admin (
    admin_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    api_key VARCHAR(255) NOT NULL,
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE black_listed (
    id INT AUTO_INCREMENT PRIMARY KEY,
    ip VARCHAR(255) NOT NULL,
    user_id INT NOT NULL,
    type ENUM('IP', 'User', 'Device') NOT NULL,
    blocked_date TIMESTAMP NOT NULL,
    recovery_date TIMESTAMP NULL
);

CREATE TABLE monthly_revenue (
    month DATE NOT NULL,
    product_id VARCHAR(255) NOT NULL,
    type ENUM('Subscription', 'Licence') NOT NULL,
    revenue DECIMAL(12, 2) NOT NULL
);

CREATE TABLE stats (
    total_sales INT NOT NULL,
    total_revenue DECIMAL(12, 2) NOT NULL,
    users_count INT NOT NULL
);

CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(50),
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE licence (
    licence_id VARCHAR(255) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    max_devices INT NOT NULL,
    duration INT NOT NULL,
    grace_period INT NOT NULL,
    public_key TEXT NOT NULL,
    price DECIMAL(12, 2) NOT NULL,
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE licence_activation (
    activation_id INT AUTO_INCREMENT PRIMARY KEY,
    licence_order_id INT NOT NULL,
    device_print VARCHAR(255) NOT NULL,
    user_id INT NOT NULL,
    activation_date TIMESTAMP NOT NULL,
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE licence_bundle (
    licence_order_id INT NOT NULL,
    option_id VARCHAR(255) NOT NULL,
    PRIMARY KEY (licence_order_id, option_id)
);

CREATE TABLE licence_option (
    option_id VARCHAR(255) PRIMARY KEY,
    licence_id VARCHAR(255) NOT NULL,
    option_name VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    price DECIMAL(12, 2) NOT NULL,
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE licence_order (
    licence_order_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    licence_id VARCHAR(255) NOT NULL,
    private_key TEXT NOT NULL,
    purchase_date TIMESTAMP NOT NULL,
    status ENUM('Active', 'Expired', 'Canceled') NOT NULL,
    reseller VARCHAR(255) NOT NULL,
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE subscription (
    subscription_id VARCHAR(255) PRIMARY KEY,
    subscription_name VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE subscription_order (
    order_id INT AUTO_INCREMENT PRIMARY KEY,
    subscription_id VARCHAR(255) NOT NULL,
    subscription_tier_id VARCHAR(255) NOT NULL,
    user_id INT NOT NULL,
    purchase_date TIMESTAMP NOT NULL,
    start_date TIMESTAMP NOT NULL,
    end_date TIMESTAMP NOT NULL,
    status ENUM('Active', 'Expired', 'Canceled') NOT NULL,
    reseller VARCHAR(255) NOT NULL,
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE subscription_tier (
    tier_id VARCHAR(255) PRIMARY KEY,
    subscription_id VARCHAR(255) NOT NULL,
    tier_name VARCHAR(255) NOT NULL,
    duration INT NOT NULL,
    grace_period INT NOT NULL,
    price DECIMAL(12, 2) NOT NULL,
    is_archived BOOLEAN NOT NULL DEFAULT FALSE
);
