--Tabla  NivelesEducativos
INSERT INTO NivelesEducativos (Nombre, Descripcion) VALUES
    ('Preescolar', 'Educación preescolar o educación inicial'),
    ('Primaria', 'Educación primaria'),
    ('Secundaria', 'Educación secundaria'),
    ('Preparatoria', 'Educación media superior'),
    ('Profesional Técnico', 'Educación profesional técnica');

--Tabla Asuntos
INSERT INTO Asuntos (Descripcion, Categoria, Activo) VALUES
    ('Expediente de alumno', 'Administrativa', 1),
    ('Cambio de escuela', 'Administrativa', 1),
    ('Certificado de estudios', 'Documentos', 1),
    ('Reinscripción', 'Inscripción', 1),
    ('Baja de inscripción', 'Inscripción', 1),
    ('Revalidación de estudios', 'Académico', 1),
    ('Solicitud de beca', 'Económico', 1),
    ('Otras gestiones', 'Miscelánea', 1);

--Tabla Estudiantes
-- Índices en Estudiantes para consultas frecuentes
CREATE INDEX idx_Est_Nombre ON Estudiantes(Nombre);
CREATE INDEX idx_Est_ApellidoPaterno ON Estudiantes(ApellidoPaterno);
CREATE INDEX idx_Est_Municipio ON Estudiantes(MunicipioEstudio);
CREATE INDEX idx_Est_NivelEducativo ON Estudiantes(IdNivelEducativo);

--Tabla SolicitudesTurno
-- Índices en SolicitudesTurno para consultas frecuentes
CREATE INDEX idx_Sol_CURP ON SolicitudesTurno(CURP);
CREATE INDEX idx_Sol_Municipio ON SolicitudesTurno(IdMunicipio);
CREATE INDEX idx_Sol_Asunto ON SolicitudesTurno(IdAsunto);
CREATE INDEX idx_Sol_FechaSolicitud ON SolicitudesTurno(FechaSolicitud);
CREATE INDEX idx_Sol_Estatus ON SolicitudesTurno(Estatus);
CREATE INDEX idx_Sol_Municipio_Estatus ON SolicitudesTurno(IdMunicipio, Estatus);