# structure
users: (user_id [PK], first_name, last_name, email [U], phone [U], is_archived)
products: (id [PK], name [U], description, product_type, is_archived)
licenses: (license_id [PK], product_id [FK], max_devices, duration, grace_period, public_key [U], price, is_archived)
license_options: (option_id [PK], license_id [FK], option_name [U], description, price, is_archived)
license_orders: (license_order_id [PK], user_id [FK], license_id [FK], private_key, purchase_date, status, is_archived)
license_bundles: (license_order_id [PK, FK], option_id [PK, FK])
licence_activation: (id [PK], license_order_id [FK], activation_date, is_archived)

subscription_tiers: (tier_id [PK], product_id [FK], tier_name, duration, grace_period, price, is_archived)
subscription_orders: (id [PK], subscription_tier_id [FK], user_id [FK], purchase_date, start_date, end_date, status, is_archived)
black_listed: (id [PK], ip, type, blocked_date, recovery_date, created_at, last_update_at)
admins: (id [PK], username [U], password, api_key [U])


# public

## check licence
{email}
// check if user has an order by checking if user has an order 
SELECT
  COUNT(lo.license_order_id) AS active_orders_count
FROM
  users AS u
JOIN
  license_orders AS lo
  ON u.user_id = lo.user_id
WHERE
  u.email = @email AND lo.license_id = @licenseId
  AND lo.status = 'active'
  AND u.is_archived = FALSE
  AND lo.is_archived = FALSE;

// if user has an order, get the order details (needs transforming in the backend)
SELECT
  lo.license_order_id,
  lo.license_id,
  l.product_id,
  lb.option_id -- This will return multiple rows if there are options
FROM
  license_orders AS lo
JOIN
  licenses AS l
  ON lo.license_id = l.license_id
LEFT JOIN -- Use LEFT JOIN to include orders even if they have no bundles/options
  license_bundles AS lb
  ON lo.license_order_id = lb.license_order_id
WHERE
  lo.license_order_id = @orderId
  AND lo.is_archived = FALSE
  AND l.is_archived = FALSE
  AND (lb.option_id IS NULL OR lb.is_archived = FALSE); -- Handle archived bundles

## activate licence
{email, licenseId, privateKey}
// get order licence
SELECT
  lo.license_order_id,
  lo.private_key,
FROM
  license_orders AS lo
JOIN
  licenses AS l
  ON lo.license_id = l.license_id
WHERE
  lo.user_id = (SELECT user_id FROM users WHERE email = @email AND is_archived = FALSE)
  AND lo.license_id = @licenseId
  AND lo.is_archived = FALSE
  AND l.is_archived = FALSE;


// get the order users count
SELECT COUNT(*) AS activation_count
FROM licence_activation
WHERE license_order_id = ? AND is_archived = FALSE;

// check if the user has already activated the licence
SELECT COUNT(*) AS activation_exists
FROM licence_activation
W AND is_archived = FALSE AND activation_date IS NOT NULL;
