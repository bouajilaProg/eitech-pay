
CREATE TABLE admin (
    AdminId SERIAL PRIMARY KEY,
    Username TEXT NOT NULL,
    Password TEXT NOT NULL,
    ApiKey TEXT NOT NULL,
    isArchived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TYPE BlackListType AS ENUM ('IP', 'User', 'Device');

CREATE TABLE blackListed (
    Id SERIAL PRIMARY KEY,
    Ip TEXT NOT NULL,
    userId INTEGER NOT NULL,
    Type BlackListType NOT NULL,
    BlockedDate TIMESTAMP NOT NULL,
    RecoveryDate TIMESTAMP
);

CREATE TYPE RevenueType AS ENUM ('Subscription', 'Licence');

CREATE TABLE monthlyRevenue (
    Month DATE NOT NULL,
    productId TEXT NOT NULL,
    Type RevenueType NOT NULL,
    Revenue DECIMAL(12, 2) NOT NULL
);

CREATE TABLE stats (
    TotalSales INTEGER NOT NULL,
    TotalRevenue DECIMAL(12, 2) NOT NULL,
    UsersCount INTEGER NOT NULL
);

CREATE TABLE "user" (
    UserId SERIAL PRIMARY KEY,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    Email TEXT NOT NULL,
    Phone TEXT,
    IsArchived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE licence (
    LicenceId TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT NOT NULL,
    MaxDevices INTEGER NOT NULL,
    Duration INTEGER NOT NULL,
    GracePeriod INTEGER NOT NULL,
    PublicKey TEXT NOT NULL,
    Price DECIMAL(12, 2) NOT NULL,
    IsArchived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE licenceActivation (
    ActivationId SERIAL PRIMARY KEY,
    LicenceOrderId INTEGER NOT NULL,
    DevicePrint TEXT NOT NULL,
    UserId INTEGER NOT NULL,
    ActivationDate TIMESTAMP NOT NULL,
    IsArchived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE licenceBundle (
    LicenceOrderId INTEGER NOT NULL,
    OptionId TEXT NOT NULL,
    PRIMARY KEY (LicenceOrderId, OptionId)
);

CREATE TABLE licenceOption (
    OptionId TEXT PRIMARY KEY,
    LicenceId TEXT NOT NULL,
    OptionName TEXT NOT NULL,
    Description TEXT NOT NULL,
    Price DECIMAL(12, 2) NOT NULL,
    IsArchived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TYPE LicenceOrderStatus AS ENUM ('Active', 'Expired', 'Canceled');

CREATE TABLE licenceOrder (
    LicenceOrderId SERIAL PRIMARY KEY,
    UserId INTEGER NOT NULL,
    LicenceId TEXT NOT NULL,
    PrivateKey TEXT NOT NULL,
    PurchaseDate TIMESTAMP NOT NULL,
    Status LicenceOrderStatus NOT NULL,
    Reseller TEXT NOT NULL,
    IsArchived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE subscription (
    SubscriptionId TEXT PRIMARY KEY,
    SubscriptionName TEXT NOT NULL,
    description TEXT NOT NULL,
    IsArchived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TYPE SubscriptionOrderStatus AS ENUM ('Active', 'Expired', 'Canceled');

CREATE TABLE subscriptionOrder (
    OrderId SERIAL PRIMARY KEY,
    SubscriptionId TEXT NOT NULL,
    SubscriptionTierId TEXT NOT NULL,
    UserId INTEGER NOT NULL,
    PurchaseDate TIMESTAMP NOT NULL,
    StartDate TIMESTAMP NOT NULL,
    EndDate TIMESTAMP NOT NULL,
    Status SubscriptionOrderStatus NOT NULL,
    Reseller TEXT NOT NULL,
    IsArchived BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE subscriptionTier (
    TierId TEXT PRIMARY KEY,
    SubscriptionId TEXT NOT NULL,
    TierName TEXT NOT NULL,
    Duration INTEGER NOT NULL,
    GracePeriod INTEGER NOT NULL,
    Price DECIMAL(12, 2) NOT NULL,
    IsArchived BOOLEAN NOT NULL DEFAULT FALSE
);
