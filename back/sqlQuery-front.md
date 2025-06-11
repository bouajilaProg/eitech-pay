
# product 
### create sub
 
-- Insert into products
INSERT INTO products (name, description, product_type, is_archived)
VALUES (?, ?, 'subscription', false);

-- Insert into subscription_tiers
INSERT INTO subscription_tiers (product_id, tier_name, duration, grace_period, price, is_archived)
VALUES (?, ?, ?, ?, ?, false);

### create licence

-- Insert into products
INSERT INTO products (name, description, product_type, is_archived)
VALUES (?, ?, 'license', false);

-- Insert into licenses
INSERT INTO licenses (product_id, max_devices, duration, grace_period, public_key, price, is_archived)
VALUES (?, ?, ?, ?, ?, ?, false);

### delete product
UPDATE products
SET is_archived = true
WHERE id = ? AND is_archived = false;





# licence

## crud licence

### read by id 
SELECT * FROM licenses WHERE license_id = ? and is_archived = false;

### read all
SELECT * FROM licenses WHERE is_archived = false;

### create
INSERT INTO licenses (product_id, max_devices, duration, grace_period, public_key, price, is_archived)
VALUES (?, ?, ?, ?, ?, ?, false);

### update
UPDATE licenses
SET product_id = ?, max_devices = ?, duration = ?, grace_period = ?, public_key = ?, price = ?
WHERE license_id = ? AND is_archived = false;

### delete by id
UPDATE licenses
SET is_archived = true
WHERE license_id = ? AND is_archived = false;

## crud licence options

### read by id
SELECT * FROM license_options WHERE option_id = ? AND is_archived = false;

### read all
SELECT * FROM license_options WHERE is_archived = false;

### read by licence id 
SELECT * FROM license_options WHERE license_id = ? AND is_archived = false;

### create
INSERT INTO license_options (license_id, option_name, description, price, is_archived)
VALUES (?, ?, ?, ?, false);

### update
UPDATE license_options
SET license_id = ?, option_name = ?, description = ?, price = ?
WHERE option_id = ? AND is_archived = false;

### delete by id
UPDATE license_options
SET is_archived = true
WHERE option_id = ? AND is_archived = false;

## public 
### get subscription details
  { licenseId }

SELECT
  p.name               AS license_name,    
  l.license_id,
  l.price              AS license_price,
  l.max_devices,
  l.duration,
  lo.option_id,
  lo.option_name,
  lo.price              AS option_price,
  lo.description        AS option_description
FROM licenses l
JOIN products p 
  ON p.id = l.product_id
LEFT JOIN license_options lo 
  ON lo.license_id = l.license_id
WHERE
  l.product_id    = @product_id
  AND l.is_archived = FALSE
  AND p.is_archived = FALSE
  AND (lo.is_archived = FALSE OR lo.is_archived IS NULL)
ORDER BY 
  l.license_id, 
  lo.option_id;



# subscription

## crud subscription tiers
### read by id
SELECT * FROM subscription_tiers WHERE tier_id = ? AND is_archived = false;

### read all
SELECT * FROM subscription_tiers WHERE is_archived = false;

### read by product id
SELECT * FROM subscription_tiers WHERE product_id = ? AND is_archived = false;

### create
INSERT INTO subscription_tiers (product_id, tier_name, duration, grace_period, price, is_archived)
VALUES (?, ?, ?, ?, ?, false);

### update
UPDATE subscription_tiers
SET product_id = ?, tier_name = ?, duration = ?, grace_period = ?, price = ?
WHERE tier_id = ? AND is_archived = false;

### delete by id
UPDATE subscription_tiers
SET is_archived = true
WHERE tier_id = ? AND is_archived = false;

## public
### get subscription details
{ product_id }

SELECT
  st.tier_id,
  st.tier_name,
  st.duration,
  st.price,
  p.name AS product_name
FROM subscription_tiers st
JOIN products p 
  ON p.id = st.product_id
WHERE
  st.product_id = @product_id            
  AND st.is_archived = FALSE
  AND p.is_archived = FALSE
ORDER BY st.tier_id;
