create table if not exists "user"
(
    id       bigserial    not null,
    name     varchar(250) not null,
    login    varchar(50)  not null,
    position varchar(50)  null,
    constraint pk_user_id
        primary key (id)
);