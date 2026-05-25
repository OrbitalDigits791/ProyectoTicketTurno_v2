-- TABLA: Estados (SIN CAMBIOS - Catálogo RENAPO)
CREATE TABLE Estados (
    Clave CHAR(2) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE,
    
    CONSTRAINT chk_Clave_Length CHECK (LEN(Clave) = 2)
);

-- TABLA: Municipios (SIN CAMBIOS - Estructura existente)
CREATE TABLE Municipios (
    IdMunicipio INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    ContadorTurnos INT NOT NULL DEFAULT 0,
    
    CONSTRAINT chk_ContadorTurnos CHECK (ContadorTurnos >= 0)
);

-- TABLA: NivelesEducativos (NUEVA - Catálogo normalizado)
CREATE TABLE NivelesEducativos (
    IdNivelEducativo INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(80) NOT NULL UNIQUE,
    Descripcion VARCHAR(255),
    
    CONSTRAINT chk_Nombre_NotEmpty CHECK (LEN(TRIM(Nombre)) > 0)
);

-- TABLA: Asuntos (NUEVA - Catálogo normalizado)
CREATE TABLE Asuntos (
    IdAsunto INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255) NOT NULL UNIQUE,
    Categoria VARCHAR(50),
    Activo BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT chk_Descripcion_NotEmpty CHECK (LEN(TRIM(Descripcion)) > 0)
);

-- TABLA: Estudiantes (REORGANIZADA - Clave primaria + Claves foráneas)
CREATE TABLE Estudiantes (
    CURP CHAR(18) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    ApellidoPaterno VARCHAR(100) NOT NULL,
    ApellidoMaterno VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Sexo CHAR(1) NOT NULL,
    Edad INT NOT NULL,
    EstadoNacimiento CHAR(2) NOT NULL,
    MunicipioEstudio INT NOT NULL,
    IdNivelEducativo INT NOT NULL,
    Grado TINYINT NOT NULL,
    TelefonoContacto VARCHAR(20),
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT fk_Est_Estado FOREIGN KEY (EstadoNacimiento) 
        REFERENCES Estados(Clave),
    CONSTRAINT fk_Est_Municipio FOREIGN KEY (MunicipioEstudio) 
        REFERENCES Municipios(IdMunicipio),
    CONSTRAINT fk_Est_NivelEducativo FOREIGN KEY (IdNivelEducativo) 
        REFERENCES NivelesEducativos(IdNivelEducativo),
    CONSTRAINT chk_CURP_Length CHECK (LEN(CURP) = 18),
    CONSTRAINT chk_Sexo CHECK (Sexo IN ('H', 'M')),
    CONSTRAINT chk_Edad CHECK (Edad >= 0 AND Edad <= 120),
    CONSTRAINT chk_Grado CHECK (Grado >= 1 AND Grado <= 6)
);

-- TABLA: SolicitudesTurno (REORGANIZADA - Con claves foráneas)
CREATE TABLE SolicitudesTurno (
    NumeroTurno INT PRIMARY KEY IDENTITY(1,1),
    CURP CHAR(18) NOT NULL,
    IdMunicipio INT NOT NULL,
    IdAsunto INT NOT NULL,
    FechaSolicitud DATETIME NOT NULL DEFAULT GETDATE(),
    PersonaTramitera VARCHAR(100) NOT NULL,
    Parentesco VARCHAR(50) NOT NULL,
    Estatus VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    FechaResolucion DATETIME,
    Observaciones VARCHAR(500),
    
    CONSTRAINT fk_Sol_CURP FOREIGN KEY (CURP) 
        REFERENCES Estudiantes(CURP),
    CONSTRAINT fk_Sol_Municipio FOREIGN KEY (IdMunicipio) 
        REFERENCES Municipios(IdMunicipio),
    CONSTRAINT fk_Sol_Asunto FOREIGN KEY (IdAsunto) 
        REFERENCES Asuntos(IdAsunto),
    CONSTRAINT chk_Estatus CHECK (Estatus IN ('Pendiente', 'Resuelto', 'Cancelado')),
    CONSTRAINT chk_PersonaTramitera_NotEmpty CHECK (LEN(TRIM(PersonaTramitera)) > 0)
);