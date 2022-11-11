CREATE TABLE find_my_mate.ad_picture (
id SERIAL PRIMARY KEY,
picture_path TEXT NOT NULL,
picture_name TEXT NOT NULL,
picture_type TEXT NOT NULL
);

CREATE TABLE find_my_mate.ad_type (
id SERIAL PRIMARY KEY,
description TEXT NOT NULL
);

CREATE TABLE find_my_mate.contact_status_type(
id SERIAL PRIMARY KEY,
status TEXT NOT NULL
);

CREATE TABLE find_my_mate.login_type(
id SERIAL PRIMARY KEY,
description TEXT NOT NULL
);

CREATE TABLE find_my_mate.province(
id SERIAL PRIMARY KEY,
province_name TEXT NOT NULL
);

CREATE TABLE find_my_mate.district(
id SERIAL PRIMARY KEY,
district_name TEXT NOT NULL
);

CREATE TABLE find_my_mate.address(
id SERIAL PRIMARY KEY,
zip_code INT4,
province_id INT4 NOT NULL,
district_id INT4 NOT NULL,
CONSTRAINT province_address FOREIGN KEY (province_id) REFERENCES find_my_mate.province(id),
CONSTRAINT district_address  FOREIGN KEY (district_id) REFERENCES find_my_mate.district(id)
);

CREATE TABLE find_my_mate.user (
id SERIAL PRIMARY KEY,
login_type_id INT NOT NULL,
email text NOT NULL,
user_name TEXT NOT NULL,
password text NOT NULL,
login_lastest DATE NOT NULL,
created_at DATE NOT NULL,
is_deleted bool default FALSE NOT NULL,
CONSTRAINT login_type_user FOREIGN KEY (login_type_id) REFERENCES find_my_mate.login_type(id)
);

CREATE TABLE find_my_mate.advertisement (
id SERIAL PRIMARY KEY,
ad_type_id INT4 NOT NULL,
ad_picture_id INT4 NOT NULL,
effective_date DATE NOT NULL,
expired_date DATE NOT NULL,
CONSTRAINT ad_type_advertisement FOREIGN KEY (ad_type_id) REFERENCES find_my_mate.ad_type(id),
CONSTRAINT ad_picture_advertisement FOREIGN KEY (ad_picture_id) REFERENCES find_my_mate.ad_picture(id)
);

CREATE TABLE find_my_mate.university (
id SERIAL PRIMARY KEY,
name_university_th TEXT NOT NULL,
name_university_en TEXT NOT NULL,
address_detail TEXT,
address_id INT4 NOT NULL,
CONSTRAINT address_id_university FOREIGN KEY (address_id) REFERENCES find_my_mate.address(id)
);

CREATE TABLE find_my_mate.user_detail (
user_id INT4 NOT NULL,
first_name TEXT,
last_name TEXT,
phone_number TEXT,
gender TEXT,
address_id INT4,
address_detail TEXT,
university_id INT4,
year INT4,
updated_at DATE DEFAULT NOW(),
created_at DATE DEFAULT NOW(),
CONSTRAINT user_id_details FOREIGN KEY (user_id) REFERENCES find_my_mate.user(id),
CONSTRAINT address_user_details FOREIGN KEY (address_id) REFERENCES find_my_mate.address(id),
CONSTRAINT university_user_detail FOREIGN KEY (university_id) REFERENCES find_my_mate.university(id)
);

CREATE TABLE find_my_mate.residents(
id serial PRIMARY KEY,
user_id INT4 NOT NULL,
resident_name TEXT NOT NULL,
address_detail TEXT NOT NULL,
address_id INT4 NOT NULL,
price INT4 NOT NULL,
capacity INT4 NOT NULL,
created_at DATE NOT NULL,
updated_at DATE,
CONSTRAINT user_residents FOREIGN KEY (user_id) REFERENCES find_my_mate.user(id),
CONSTRAINT address_resident FOREIGN KEY (address_id) REFERENCES find_my_mate.address(id)
);

CREATE TABLE find_my_mate.contact_status(
request_user_id INT4,
response_user_id INT4,
status_id INT4 NOT NULL,
created_at DATE NOT NULL,
updated_at DATE,
CONSTRAINT request_user_contact FOREIGN KEY (request_user_id) REFERENCES find_my_mate.user(id),
CONSTRAINT response_user_contact FOREIGN KEY (response_user_id) REFERENCES find_my_mate.user(id),
CONSTRAINT status_transaction FOREIGN KEY (status_id) REFERENCES find_my_mate.contact_status_type(id)
);

CREATE TABLE find_my_mate.healthcheck (
id SERIAL PRIMARY KEY,
status_message TEXT NOT NULL
);

INSERT INTO find_my_mate.healthcheck
(id, status_message)
VALUES(1, 'I am fine thankyou.');
		
INSERT INTO find_my_mate.contact_status_type
(id, status)
VALUES(1, 'waiting'),
(2, 'pending'),
(3, 'approve'),
(4, 'cancel');

INSERT INTO find_my_mate.login_type
(id, description)
VALUES(1, 'Normal Login'),
(2, 'Google Login'),
(3, 'Facebook Login');

INSERT INTO find_my_mate.ad_type
(id, description)
VALUES(1, 'Home'),
(2, 'Main');
