DELETE FROM reservation;
DELETE FROM site;
DELETE FROM campground;
DELETE FROM park;

SET IDENTITY_INSERT park ON;
INSERT INTO park (park_id, name, location, establish_date, area, visitors, description) VALUES (1, 'Test Name', 'North Test', '2018-06-29', 1, 1, 'Test Description');
INSERT INTO park (park_id, name, location, establish_date, area, visitors, description) VALUES (2, 'Test Name 2', 'South Test', '2018-06-29', 1, 1, 'Test Description 2');
SET IDENTITY_INSERT park OFF;

SET IDENTITY_INSERT campground ON;
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (1, 1, 'Test Campground', 1, 12, 1);
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (2, 2, 'Test Campground 2', 1, 12, 1);
SET IDENTITY_INSERT campground OFF;

SET IDENTITY_INSERT site ON;
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (1, 1, 1, 6, 0, 0, 0);
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (2, 2, 2, 12, 1, 1, 1);
SET IDENTITY_INSERT site OFF;

SET IDENTITY_INSERT reservation ON;
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date, create_date) VALUES (1, 1, 'Test Reservation', GETDATE()+1, GETDATE()+5, GETDATE());
SET IDENTITY_INSERT reservation OFF;
