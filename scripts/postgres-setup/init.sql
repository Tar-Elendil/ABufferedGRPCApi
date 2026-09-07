CREATE DATABASE Accounts OWNER admin;

CREATE TABLE movements (
    account TEXT NOT NULL,
    externalRef TEXT NOT NULL,
    currency VARCHAR(30) NOT NULL,
    amount NUMERIC(50,2) NOT NULL,
    occurredAt TIMESTAMP,
    narration TEXT,
    PRIMARY KEY (account, externalRef)
);