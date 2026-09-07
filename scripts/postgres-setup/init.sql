\c demo

CREATE TABLE Movements (
    account_id TEXT NOT NULL,
    external_ref TEXT NOT NULL,
    currency VARCHAR(30) NOT NULL,
    amount NUMERIC(50,2) NOT NULL,
    occurred_at TIMESTAMP,
    narration TEXT,
    PRIMARY KEY (account_id, external_ref)
);

CREATE INDEX occurred_at ON Movements (occurred_at);