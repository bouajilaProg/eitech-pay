# front related
- dashboard:
  * stats (users count,revenue,total_orders)
  * revenue (monthly, quartly,yearly) 
  * products 

- product page
  * product details
  * revenue monthly, quarterly, yearly
  > subscription V
    * tiers with price duration total sales 
    * add tier 
    * edit tier 
    * delete tier 
  > licence V
    * see licence options V
    * add licence option V
    * update licence option V
    * delete licence option V
- admin login
  * login

# system related
> licence
  - get licence details
  - activate licence
  - check licence
> subscription
  - get subscription details
  - check subscription

> pay
  - pay for subscription
  - pay for licence

> notification
  - send invoice to user
  - send payment notification to user
  - send product revoked

# db struct
users: (user_id [PK], first_name, last_name, email [U], phone [U], is_archived)
products: (id [PK], name [U], description, product_type, is_archived)
licenses: (license_id [PK], product_id [FK], max_devices, duration, grace_period, public_key [U], price, is_archived)
license_options: (option_id [PK], license_id [FK], option_name [U], description, price, is_archived)
license_orders: (license_order_id [PK], user_id [FK], license_id [FK], private_key, purchase_date, status, is_archived)
license_bundles: (license_order_id [PK, FK], option_id [PK, FK])
licence_activation: (id [PK], license_order_id [FK],userid, activation_date, is_archived)

subscription_tiers: (tier_id [PK], product_id [FK], tier_name, duration, grace_period, price, is_archived)
subscription_orders: (id [PK], subscription_tier_id [FK], user_id [FK], purchase_date, start_date, end_date, status, is_archived)
black_listed: (id [PK], ip, type, blocked_date, recovery_date, created_at, last_update_at)
admins: (id [PK], username [U], password, api_key [U])

stats(total_sales,total_revenu,users_count)
monthly_revenu(month,type[ enum(sub,licence) ],revenu)

