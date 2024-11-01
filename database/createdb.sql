-- Creat the databse
CREATE DATABASE weatherDB; 
-- Create the location table
CREATE TABLE [location] (
    [zip] BIGINT PRIMARY KEY,  -- Assuming zip is unique for each location
    [name_] VARCHAR(255), 
    [country] VARCHAR(255)
);

-- Create the users table
CREATE TABLE [users] (
    [User_ID] BIGINT PRIMARY KEY, 
    [username] VARCHAR(255), 
    [email] VARCHAR(255), 
    [password_hs] VARCHAR(255),
    [favourite_location_ID] BIGINT,
    FOREIGN KEY ([favourite_location_ID]) REFERENCES [location]([zip])  -- Foreign key to location table
);

-- Create the forecast_weather table
CREATE TABLE [forecast_weather] (
    [id] BIGINT PRIMARY KEY, 
    [location_id] BIGINT, 
    [date] DATETIME, 
    [maxtemp_c] FLOAT, 
    [mintemp_c] FLOAT, 
    [avgtemp_c] FLOAT, 
    [maxwind_kph] FLOAT, 
    [totalprecip_mm] FLOAT, 
    [avgvis_km] FLOAT, 
    [avghumidity] BIGINT, 
    [will_it_rain] TINYINT, 
    [chance_of_rain] BIGINT,
    [condition_text] VARCHAR(255), 
    [uv_index] REAL,
    FOREIGN KEY ([location_id]) REFERENCES [location]([zip])  -- Foreign key to location table
);

-- Create the hourly_forecast table
CREATE TABLE [hourly_forecast] (
    [id] BIGINT PRIMARY KEY, 
    [forecast_id] BIGINT,
    [time_epoch] BIGINT, 
    [temp_c] FLOAT, 
    [is_day] TINYINT,
    [condition_text] VARCHAR(255),
    [wind_kph] FLOAT,
    [precip_mm] FLOAT,
    [humidity] BIGINT,
    [cloud] BIGINT,
    [feelslike_c] FLOAT,
    [dewpoint_c] FLOAT,
    [visibility_km] FLOAT,
    [uv_index] FLOAT,
    [gust_kph] FLOAT,
    [will_it_rain] TINYINT, 
    [chance_of_rain] BIGINT,
    FOREIGN KEY ([forecast_id]) REFERENCES [forecast_weather]([id])  -- Foreign key to forecast_weather table
);

-- Create the current_weather table
CREATE TABLE [current_weather] (
    [id] BIGINT PRIMARY KEY, 
    [location_id] BIGINT, 
    [last_updated] DATETIME, 
    [temp_c] FLOAT, 
    [temp_f] FLOAT, 
    [condition_text] VARCHAR(255), 
    [wind_mph] FLOAT, 
    [wind_kph] FLOAT, 
    [pressure_mb] FLOAT, 
    [precip_mm] FLOAT, 
    [humidity] BIGINT, 
    [cloud] BIGINT, 
    [feelslike_c] FLOAT, 
    [dewpoint_c] FLOAT, 
    [visibility_km] FLOAT, 
    [uv_index] REAL, 
    [gust_mph] FLOAT,
    FOREIGN KEY ([location_id]) REFERENCES [location]([zip])  -- Foreign key to location table
);

-- Indexes for optimization
CREATE INDEX [fk_location_id] ON [current_weather] ([location_id]);
CREATE INDEX [fav_location_id] ON [users] ([favourite_location_ID]);
CREATE INDEX [Rel_4745A427_3977_48AB] ON [hourly_forecast] ([forecast_id]);
