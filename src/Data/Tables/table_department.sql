create table if not exists "department"
(
    id                  bigserial not null,
    chief_department_id bigint    null,
    name                varchar(250),
    constraint pk_department_id
        primary key (id),
    constraint fk_department_chief_department_id
        foreign key (chief_department_id)
            references "department" (id)
);