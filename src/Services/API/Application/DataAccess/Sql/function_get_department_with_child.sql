create or replace function get_department_with_child(_department_id bigint)
    returns table
            (
                id              int,
                name            text,
                login           text,
                is_soft_deleted boolean,
                created_at      timestamp,
                updated_at      timestamp
            )
    language plpgsql
as
$$
begin
    return query
        select u.id, u.name, u.login, u.is_soft_deleted, u.created_at, u.updated_at
        from department_user du
                 inner join "user" u on u.id = du.user_id
        where du.department_id = _department_id;
end;
$$;
