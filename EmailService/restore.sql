CREATE TABLE message(
    id serial not null primary key,
    sender varchar(100) not null,
    recipient varchar(100) not null ,
    carbon_copy_recipients varchar(100)[],
    subject varchar(255) not null,
    text varchar(255) not null,
    is_successfully_sent boolean not null
);