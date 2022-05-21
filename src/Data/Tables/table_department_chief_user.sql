create table if not exists "department_chief_user"
(
    id            bigserial not null,
    department_id bigint    not null,
    chief_user_id bigint    not null,
    constraint pk_department_user_id
        primary key (id),
    constraint fk_department_chief_user_department_id
        foreign key (department_id)
            references "department" (id),
    constraint fk_department_user_user_id
        foreign key (chief_user_id)
            references "user" (id)
);