select u.*, ids.a as enterprise_ids
from "user" u
         inner join lateral (
    select array_agg(eu.enterprise_id) as a
    from enterprise_user eu
    where eu.user_id = u.id) ids on true
where email = @Email;