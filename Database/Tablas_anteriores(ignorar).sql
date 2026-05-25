-- Crear tabla de Estados
CREATE TABLE Estados (
    Clave NVARCHAR(10) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

-- Crear tabla de Municipios
CREATE TABLE Municipios (
    IdMunicipio INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    ContadorTurnos INT DEFAULT 0
);

-- Crear tabla de Estudiantes
CREATE TABLE Estudiantes (
    CURP NVARCHAR(18) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    ApellidoPaterno NVARCHAR(100) NOT NULL,
    ApellidoMaterno NVARCHAR(100) NOT NULL,
    FechaNacimiento DATETIME NOT NULL,
    Sexo CHAR(1) NOT NULL,
    Edad INT NOT NULL,
    EstadoNacimiento NVARCHAR(50),
    MunicipioEstudio NVARCHAR(100),
    TelefonoContacto NVARCHAR(20),
    NivelEducativo NVARCHAR(50),
    Grado INT
);

-- Crear tabla de SolicitudesTurno
CREATE TABLE SolicitudesTurno (
    NumeroTurno INT PRIMARY KEY IDENTITY(1,1),
    CURP NVARCHAR(18) NOT NULL FOREIGN KEY REFERENCES Estudiantes(CURP),
    Municipio NVARCHAR(100) NOT NULL,
    FechaSolicitud DATETIME NOT NULL,
    Asunto NVARCHAR(500),
    PersonaTramitera NVARCHAR(100),
    Parentesco NVARCHAR(50),
    Estatus INT NOT NULL DEFAULT 0
);