create table if not exists "department_user"
(
    id            bigserial not null,
    department_id bigint    not null,
    user_id       bigint    not null,
    constraint pk_department_user_id
        primary key (id),
    constraint fk_department_user_department_id
        foreign key (department_id)
            references "department" (id),
    constraint fk_department_user_user_id
        foreign key (user_id)
            references "user" (id)
);