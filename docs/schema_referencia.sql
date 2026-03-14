-- ============================================================
-- Script SQL equivalente a las migraciones EF Core
-- Solo para referencia / documentación.
-- En producción usar: dotnet ef database update
-- ============================================================

CREATE TABLE IF NOT EXISTS Cadetes (
    DNI                TEXT NOT NULL PRIMARY KEY,
    ApellidosNombres   TEXT NOT NULL,
    Año                INTEGER NOT NULL CHECK (Año IN (3, 4, 5)),
    Division           TEXT
);

CREATE TABLE IF NOT EXISTS Supervisores (
    DNI                TEXT NOT NULL PRIMARY KEY,
    Grado              TEXT NOT NULL,
    ApellidosNombres   TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Castigos (
    Codigo       TEXT NOT NULL PRIMARY KEY,
    Descripcion  TEXT NOT NULL,
    PuntosAño3   INTEGER NOT NULL DEFAULT 0,
    PuntosAño4   INTEGER NOT NULL DEFAULT 0,
    PuntosAño5   INTEGER NOT NULL DEFAULT 0,
    Reincidencia INTEGER NOT NULL DEFAULT 0,
    Nota         TEXT
);

CREATE TABLE IF NOT EXISTS BimestresConfig (
    Id           INTEGER PRIMARY KEY AUTOINCREMENT,
    Bimestre     INTEGER NOT NULL,
    Año          INTEGER NOT NULL,
    NroSemana    INTEGER NOT NULL CHECK (NroSemana BETWEEN 1 AND 5),
    FechaInicio  TEXT NOT NULL,   -- ISO 8601
    FechaFin     TEXT NOT NULL,
    Cerrada      INTEGER NOT NULL DEFAULT 0,
    UNIQUE (Bimestre, Año, NroSemana)
);

CREATE TABLE IF NOT EXISTS Sanciones (
    Id             INTEGER PRIMARY KEY AUTOINCREMENT,
    CadeteDNI      TEXT NOT NULL REFERENCES Cadetes(DNI),
    SupervisorDNI  TEXT NOT NULL REFERENCES Supervisores(DNI),
    CastigoCodigo  TEXT NOT NULL REFERENCES Castigos(Codigo),
    Fecha          TEXT NOT NULL,    -- ISO 8601 "2026-03-14"
    Hora           TEXT NOT NULL,    -- "14:30:00"
    Observaciones  TEXT,
    PuntosAplicados INTEGER NOT NULL,
    SemanaBimestre  INTEGER NOT NULL CHECK (SemanaBimestre BETWEEN 1 AND 5)
);

CREATE INDEX IF NOT EXISTS IX_Sanciones_Cadete_Semana
    ON Sanciones (CadeteDNI, SemanaBimestre);

CREATE INDEX IF NOT EXISTS IX_Sanciones_Fecha
    ON Sanciones (Fecha);

CREATE TABLE IF NOT EXISTS ActitudesMilitares (
    Id             INTEGER PRIMARY KEY AUTOINCREMENT,
    CadeteDNI      TEXT NOT NULL REFERENCES Cadetes(DNI) ON DELETE CASCADE,
    Bimestre       INTEGER NOT NULL,
    AñoAcademico   INTEGER NOT NULL,
    NotaActitud    REAL NOT NULL,
    UNIQUE (CadeteDNI, Bimestre, AñoAcademico)
);
