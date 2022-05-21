create table if not exists "user"
(
    id    serial primary key not null,
    name  varchar(250)       not null,
    login varchar(250)       not null
);

create table department
(
    id   serial primary key not null,
    name varchar(250)       not null
);

create table if not exists department_user
(
    id            serial primary key not null,
    department_id int                not null,
    user_id       int                not null,
    unique (department_id, user_id),
    foreign key (department_id) references department (id),
    foreign key (user_id) references "user" (id)
);

create table if not exists permission
(
    id serial primary key not null
);

create table if not exists user_permission
(
    id            serial primary key not null,
    user_id       int                not null,
    permission_id int                not null,
    unique (user_id, permission_id),
    foreign key (user_id) references "user" (id),
    foreign key (permission_id) references permission (id)
);

create table if not exists task
(
    id          serial primary key not null,
    user_id     int                not null,
    title       varchar(250)       not null,
    description varchar(1000)      null,
    foreign key (user_id) references "user" (id)
);