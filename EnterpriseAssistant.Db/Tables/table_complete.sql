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

create table if not exists "user"
(
    id       bigserial    not null,
    name     varchar(250) not null,
    login    varchar(50)  not null,
    position varchar(50)  null,
    constraint pk_user_id
        primary key (id)
);

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

create table if not exists "department_chief_user"
(
    id            bigserial not null,
    department_id bigint    not null,
    chief_user_id bigint    not null,
    constraint pk_department_chief_user_id
        primary key (id),
    constraint fk_department_chief_user_department_id
        foreign key (department_id)
            references "department" (id),
    constraint fk_department_chief_user_chief_user_id
        foreign key (chief_user_id)
            references "user" (id)
);