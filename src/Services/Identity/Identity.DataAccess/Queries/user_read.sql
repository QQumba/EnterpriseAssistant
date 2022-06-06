select u.*, ids.a as enterprise_ids
from "user" u
         inner join lateral (
    select array_agg(eu.enterprise_id) as a
    from enterprise_user eu
             inner join enterprise e on eu.enterprise_id = e.id
    where eu.user_id = u.id
      and eu.is_soft_deleted = false
      and e.is_soft_deleted = false) ids on true
where email = @Email;