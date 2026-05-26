# 📚 Proyecto Ticket de Turno - Documentación Completa

> Aplicación de escritorio para la gestión integral de turnos escolares en el estado de Coahuila

---

## 📋 Tabla de Contenidos

- [Descripción General](#descripción-general)
- [Especificaciones Técnicas](#especificaciones-técnicas)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Diagrama UML](#diagrama-uml)
- [Diagramas de Flujo](#diagramas-de-flujo)
- [Organigrama de Carpetas](#organigrama-de-carpetas)
- [Dashboards](#dashboards)
- [Instalación y Configuración](#instalación-y-configuración)
- [Guía de Desarrollo](#guía-de-desarrollo)
- [Plan de Desarrollo](#plan-de-desarrollo)
- [Contribución](#contribución)

---

## 📖 Descripción General

**ProyectoTicketTurno_v2** es una solución empresarial desarrollada en C# con .NET Framework 4.8 diseñada para optimizar la gestión de turnos escolares. La aplicación implementa patrones de arquitectura profesionales garantizando escalabilidad, mantenibilidad y rendimiento.

### Características Principales

✅ Gestión centralizada de turnos escolares  
✅ Control de acceso basado en roles  
✅ Seguimiento y generación de reportes  
✅ Base de datos relacional robusta (SQL Server)  
✅ Interfaz amigable con WinForms  
✅ Arquitectura escalable y modular  

---

## 🔧 Especificaciones Técnicas

| Aspecto | Descripción |
|--------|-----------|
| **Lenguaje** | C# con .NET Framework 4.8 |
| **ORM** | Entity Framework 6.4.4 |
| **Base de Datos** | MS SQL Server 2019+ |
| **Arquitectura** | MVC + Repository Pattern |
| **Interfaz de Usuario** | Windows Forms (WinForms) |
| **Patrón de Datos** | Generic Repository Pattern |
| **Testing** | MSTest / NUnit |
| **Control de Versiones** | Git |

### Stack Tecnológico

```
┌─────────────────────────────────────────┐
│      PTT.Presentation (WinForms)        │
│  [UI - Interfaz de Usuario]             │
└────────────────┬────────────────────────┘
                 │
┌────────────────▼────────────────────────┐
│   ProyectoTicketTurno.Business          │
│  [Lógica de Negocio - Models/Services]  │
└────────────────┬────────────────────────┘
                 │
┌────────────────▼────────────────────────┐
│    ProyectoTicketTurno.Data             │
│  [Acceso a Datos - EF6 + Repositories]  │
└────────────────┬────────────────────────┘
                 │
┌────────────────▼────────────────────────┐
│      MS SQL Server 2019                 │
│     [Base de Datos]                     │
└─────────────────────────────────────────┘
```

---

## 📁 Estructura del Proyecto

### Descripción de Capas

#### **ProyectoTicketTurno.Data** (Capa de Acceso a Datos)
- **Responsabilidad**: Gestionar la interacción con la base de datos
- **Tecnologías**: Entity Framework 6, SQL Server
- **Componentes**:
  - `Context/`: DbContext y configuración de EF6
  - `Repositories/`: Implementación del Generic Repository Pattern
  - `Migrations/`: Migraciones de base de datos
  - `Models/`: Entidades del dominio

#### **ProyectoTicketTurno.Business** (Capa de Lógica de Negocio)
- **Responsabilidad**: Implementar las reglas de negocio
- **Componentes**:
  - `Models/`: ViewModels y DTOs
  - `Services/`: Servicios de negocio
  - `Interfaces/`: Contratos de servicios

#### **PTT.Presentation** (Capa de Presentación)
- **Responsabilidad**: Interfaz de usuario
- **Tecnología**: Windows Forms
- **Componentes**:
  - `Forms/`: Formularios principales
  - `UserControls/`: Controles reutilizables
  - `Resources/`: Imágenes y recursos

#### **Infrastructure** (Capa de Infraestructura)
- **Responsabilidad**: Utilidades y servicios transversales
- **Componentes**:
  - Logging
  - Validación
  - Helpers generales
  - Configuración

#### **PTT.Tests** (Capa de Testing)
- **Responsabilidad**: Pruebas unitarias e integración
- **Framework**: MSTest / NUnit
- **Cobertura**: Data, Business y Presentation

---

## 🎨 Diagrama UML

### Diagrama de Clases - Entidades Principales

```
┌─────────────────────────────────────────────────────────┐
│                        USUARIO                          │
├─────────────────────────────────────────────────────────┤
│ - IdUsuario: int [PK]                                   │
│ - NombreUsuario: string                                 │
│ - Contraseña: string (hash)                             │
│ - Email: string                                         │
│ - Activo: bool                                          │
│ - FechaRegistro: DateTime                               │
│ - Rol: Enum [Admin, Docente, Administrativo]           │
├─────────────────────────────────────────────────────────┤
│ + ValidarCredenciales(): bool                           │
│ + CambiarContraseña(): void                             │
│ + ObtenerPermisos(): List<Permiso>                      │
└─────────────────────────────────────────────────────────┘
         │
         │ Realiza
         │
         ▼
┌─────────────────────────────────────────────────────────┐
│                        TURNO                            │
├─────────────────────────────────────────────────────────┤
│ - IdTurno: int [PK]                                     │
│ - NombreTurno: string                                   │
│ - HoraInicio: TimeSpan                                  │
│ - HoraFin: TimeSpan                                     │
│ - FechaCreacion: DateTime                               │
│ - Activo: bool                                          │
├─────────────────────────────────────────────────────────┤
│ + ObtenerDuracion(): TimeSpan                           │
│ + ValidarHorarios(): bool                               │
│ + GenerarReporte(): ReporteTurno                        │
└─────────────────────────────────────────────────────────┘
         │
         │ Asigna a
         │
         ▼
┌─────────────────────────────────────────────────────────┐
│                      ASIGNACION                         │
├─────────────────────────────────────────────────────────┤
│ - IdAsignacion: int [PK]                                │
│ - IdUsuario: int [FK]                                   │
│ - IdTurno: int [FK]                                     │
│ - FechaAsignacion: DateTime                             │
│ - FechaTerminacion: DateTime (nullable)                 │
│ - Estado: Enum [Activo, Completado, Cancelado]         │
├─────────────────────────────────────────────────────────┤
│ + CancelarAsignacion(): void                            │
│ + ObtenerEstado(): string                               │
│ + GetDiasAsignados(): int                               │
└─────────────────────────────────────────────────────────┘
         │
         │ Registra
         │
         ▼
┌─────────────────────────────────────────────────────────┐
│                   REGISTROASISTENCIA                    │
├─────────────────────────────────────────────────────────┤
│ - IdRegistro: int [PK]                                  │
│ - IdAsignacion: int [FK]                                │
│ - FechaRegistro: DateTime                               │
│ - HoraLlegada: TimeSpan                                 │
│ - HoraSalida: TimeSpan (nullable)                       │
│ - Estado: Enum [Presente, Ausente, Permiso]            │
│ - Observaciones: string                                 │
├─────────────────────────────────────────────────────────┤
│ + CalcularTiempoTrabajado(): TimeSpan                   │
│ + EsFueraDeHorario(): bool                              │
│ + RegistrarPermiso(): void                              │
└─────────────────────────────────────────────────────────┘
```

### Diagrama de Componentes

```
┌────────────────────────────────────────────────────────────┐
│                  CAPA DE PRESENTACION                      │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────┐ │
│  │  FormLogin   │  │ FormTurnos   │  │ FormAsignaciones │ │
│  └──────────────┘  └──────────────┘  └──────────────────┘ │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────┐ │
│  │ FormReportes │  │ FormUsuarios │  │  FormDashboard   │ │
│  └──────────────┘  └──────────────┘  └──────────────────┘ │
└────────────────────────┬─────────────────────────────────┘
                         │
┌────────────────────────▼─────────────────────────────────┐
│              CAPA DE LOGICA DE NEGOCIO                  │
│  ┌──────────────────────────────────────────────────┐  │
│  │  UsuarioService      │  TurnoService            │  │
│  │  AsignacionService   │  AsistenciaService       │  │
│  │  ReporteService      │  DashboardService        │  │
│  └──────────────────────────────────────────────────┘  │
└────────────────────────┬────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────┐
│           CAPA DE ACCESO A DATOS (EF6)                │
│  ┌────────────────────────────────────────────────┐   │
│  │         DbContext (AplicacionDbContext)        │   │
│  │  ┌──────────────────────────────────────────┐ │   │
│  │  │  GenericRepository<T>                    │ │   │
│  │  │  - UsuarioRepository                     │ │   │
│  │  │  - TurnoRepository                       │ │   │
│  │  │  - AsignacionRepository                  │ │   │
│  │  │  - RegistroAsistenciaRepository          │ │   │
│  │  └──────────────────────────────────────────┘ │   │
│  └────────────────────────────────────────────────┘   │
└────────────────────────┬────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────┐
│         MS SQL SERVER 2019 (BASE DE DATOS)            │
│  ┌──────────────────────────────────────────────────┐ │
│  │  Usuarios  │  Turnos  │  Asignaciones           │ │
│  │  RegistroAsistencia  │  Reportes  │  Auditoría  │ │
│  └──────────────────────────────────────────────────┘ │
└──────────────────────────────────────────────────────────┘
```

---

## 📊 Diagramas de Flujo

### Flujo de Autenticación

```
┌──────────────┐
│   INICIO     │
└──────┬───────┘
       │
       ▼
┌──────────────────────────────┐
│ Usuario ingresa credenciales │
└──────┬───────────────────────┘
       │
       ▼
┌────────────────────────────────┐
│ Validar formato email/usuario  │
└────┬──────────────┬────────────┘
     │              │
    NO             SÍ
     │              │
     ▼              ▼
  ┌────────┐   ┌──────────────────────┐
  │ Error  │   │ Buscar usuario en BD │
  │Formato │   └────┬─────────────┬───┘
  └────────┘        │             │
                 Existe       No existe
                    │             │
                    ▼             ▼
              ┌────────────┐   ┌─────────┐
              │Validar     │   │ Usuario │
              │Contraseña  │   │No Existe│
              └─┬────────┬─┘   └─────────┘
                │        │
              OK      Error
                │        │
                ▼        ▼
           ┌────────┐ ┌──────┐
           │Cargar  │ │Error │
           │Roles   │ │Login │
           └─┬──────┘ └──────┘
             │
             ▼
        ┌──────────┐
        │ Redirigir│
        │Dashboard │
        └──────────┘
```

### Flujo de Asignación de Turnos

```
┌────────────────────┐
│  FORMULARIO        │
│  ASIGNACIONES      │
└────────┬───────────┘
         │
         ▼
┌─────────────────────────┐
│ Seleccionar Usuario     │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│ Seleccionar Turno       │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│ Establecer Fecha Inicio │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────────────────┐
│ Validar conflictos horarios         │
└────┬──────────────┬──────────────────┘
     │              │
    SÍ             NO
     │              │
     ▼              ▼
┌──────────┐   ┌──────────────────────┐
│ Mostrar  │   │ Crear Asignación     │
│ Alerta   │   │ en Base de Datos     │
└──────────┘   └──────┬───────────────┘
                      │
                      ▼
               ┌──────────────────────┐
               │ Generar Notificación │
               │ al Usuario           │
               └──────┬───────────────┘
                      │
                      ▼
               ┌──────────────────────┐
               │ ASIGNACION           │
               │ COMPLETADA           │
               └──────────────────────┘
```

### Flujo de Registro de Asistencia

```
┌──────────────────────┐
│ Usuario Llega        │
│ a la Institución     │
└──────┬───────────────┘
       │
       ▼
┌────────────────────────────┐
│ Accede a Pantalla de       │
│ Registro de Asistencia     │
└──────┬─────────────────────┘
       │
       ▼
┌─────────────────────────────┐
│ Sistema obtiene hora actual │
└──────┬──────────────────────┘
       │
       ▼
┌─────────────────────────────────┐
│ Verificar asignación activa     │
└────┬──────────────┬─────────────┘
     │              │
    NO             SÍ
     │              │
     ▼              ▼
┌──────────┐  ┌──────────────────┐
│No hay    │  │Validar horario   │
│asignación   │inicio            │
└──────────┘  └─┬─────────────┬──┘
               │             │
            Dentro       Fuera
             │             │
             ▼             ▼
         ┌────────┐   ┌─────────┐
         │Registrar  │Registrar 
         │Normal │   │Retraso  │
         └─┬─────┘   └────┬────┘
           │              │
           └──────┬───────┘
                  │
                  ▼
         ┌──────────────────┐
         │ Guardar en BD    │
         │ Hora Llegada     │
         └────┬─────────────┘
              │
              ▼
         ┌──────────────────┐
         │ ASISTENCIA       │
         │ REGISTRADA       │
         └──────────────────┘
```

### Flujo de Generación de Reportes

```
┌──────────────────┐
│ Usuario solicita │
│ Reporte          │
└─���──────┬─────────┘
         │
         ▼
┌─────────────────────────┐
│ Seleccionar tipo:       │
│ • Asistencia           │
│ • Turnos               │
│ • Desempeño            │
└────────┬────────────────┘
         │
         ▼
┌──────────────────────────┐
│ Definir rango de fechas  │
└────────┬─────────────────┘
         │
         ▼
┌──────────────────────────┐
│ Seleccionar filtros      │
│ (Usuario, Turno, etc)    │
└────────┬─────────────────┘
         │
         ▼
┌──────────────────────────┐
│ Consultar BD             │
│ (Query optimizada)       │
└────────┬─────────────────┘
         │
         ▼
┌──────────────────────────┐
│ Procesar datos           │
│ (Transformación)         │
└────────┬─────────────────┘
         │
         ▼
┌─────────────────────────────┐
│ Generar archivo             │
│ • PDF                       │
│ • Excel                     │
│ • Pantalla                  │
└────────┬────────────────────┘
         │
         ▼
┌──────────────────────────────┐
│ REPORTE DISPONIBLE PARA      │
│ DESCARGA/VISUALIZACION       │
└──────────────────────────────┘
```

---

## 📂 Organigrama de Carpetas

```
ProyectoTicketTurno_v2/
│
├── 📄 ProyectoTicketTurno.sln
├── 📄 README.md
├── 📄 .gitignore
├── 📄 .gitattributes
│
├── 📁 ProyectoTicketTurno.Data/
│   ├── 📄 Data.csproj
│   ├── 📄 App.config
│   ├── 📄 packages.config
│   │
│   ├── 📁 Context/
│   │   ├── AplicacionDbContext.cs
│   │   └── DbConfiguration.cs
│   │
│   ├── 📁 Repositories/
│   │   ├── GenericRepository.cs
│   │   ├── IGenericRepository.cs
│   │   ├── UsuarioRepository.cs
│   │   ├── TurnoRepository.cs
│   │   ├── AsignacionRepository.cs
│   │   └── RegistroAsistenciaRepository.cs
│   │
│   ├── 📁 Models/
│   │   ├── Usuario.cs
│   │   ├── Turno.cs
│   │   ├── Asignacion.cs
│   │   ├── RegistroAsistencia.cs
│   │   └── EntityBase.cs
│   │
│   ├── 📁 Migrations/
│   │   ├── Configuration.cs
│   │   ├── 202405260001_InitialCreate.cs
│   │   └── [Otras migraciones]
│   │
│   └── 📁 Properties/
│       └── AssemblyInfo.cs
│
├── 📁 ProyectoTicketTurno.Business/
│   ├── 📄 Business.csproj
│   ├── 📄 App.config
│   │
│   ├── 📁 Models/
│   │   ├── UsuarioViewModel.cs
│   │   ├── TurnoViewModel.cs
│   │   ├── AsignacionViewModel.cs
│   │   ├── RegistroAsistenciaViewModel.cs
│   │   └── DashboardViewModel.cs
│   │
│   ├── 📁 Services/
│   │   ├── IUsuarioService.cs
│   │   ├── UsuarioService.cs
│   │   ├── ITurnoService.cs
│   │   ├── TurnoService.cs
│   │   ├── IAsignacionService.cs
│   │   ├── AsignacionService.cs
│   │   ├── IAsistenciaService.cs
│   │   ├── AsistenciaService.cs
│   │   ├── IReporteService.cs
│   │   ├── ReporteService.cs
│   │   ├── IDashboardService.cs
│   │   └── DashboardService.cs
│   │
│   ├── 📁 Interfaces/
│   │   └── [Contratos de servicios]
│   │
│   └── 📁 Properties/
│       └── AssemblyInfo.cs
│
├── 📁 PTT.Presentation/
│   ├── 📄 PTT.Presentation.csproj
│   ├── 📄 App.config
│   ├── 📄 Program.cs
│   │
│   ├── 📁 Forms/
│   │   ├── FormLogin.cs/.designer.cs
│   │   ├── FormDashboard.cs/.designer.cs
│   │   ├── FormTurnos.cs/.designer.cs
│   │   ├── FormAsignaciones.cs/.designer.cs
│   │   ├── FormUsuarios.cs/.designer.cs
│   │   ├── FormAsistencia.cs/.designer.cs
│   │   ├── FormReportes.cs/.designer.cs
│   │   └── FormAbout.cs/.designer.cs
│   │
│   ├── 📁 UserControls/
│   │   ├── ControlTurnoItem.cs
│   │   ├── ControlAsignacionItem.cs
│   │   └── ControlEstadisticas.cs
│   │
│   ├── 📁 Resources/
│   │   ├── 📁 Images/
│   │   │   ├── logo.png
│   │   │   ├── icons/
│   │   │   └── backgrounds/
│   │   └── 📁 Strings/
│   │       └── Resources.resx
│   │
│   └── 📁 Properties/
│       ├── AssemblyInfo.cs
│       └── Resources.resx
│
├── 📁 Infrastructure/
│   ├── 📁 Logging/
│   │   ├── Logger.cs
│   │   └── ILogger.cs
│   │
│   ├── 📁 Validation/
│   │   ├── ValidationHelper.cs
│   │   └── ValidationRules.cs
│   │
│   ├── 📁 Helpers/
│   │   ├── DateTimeHelper.cs
│   │   ├── SecurityHelper.cs
│   │   ├── StringHelper.cs
│   │   └── FileHelper.cs
│   │
│   └── 📁 Configuration/
│       └── AppSettings.cs
│
├── 📁 PTT.Tests/
│   ├── 📄 PTT.Tests.csproj
│   │
│   ├── 📁 Unit/
│   │   ├── UsuarioServiceTests.cs
│   │   ├── TurnoServiceTests.cs
│   │   ├── AsignacionServiceTests.cs
│   │   └── AsistenciaServiceTests.cs
│   │
│   ├── 📁 Integration/
│   │   ├── RepositoryTests.cs
│   │   └── DbContextTests.cs
│   │
│   └── 📁 Fixtures/
│       └── TestDataBuilder.cs
│
├── 📁 Database/
│   ├── Ticket_Turno_tablas.sql
│   ├── Ticket_Turno_datos_iniciales.sql
│   └── Ticket_Turno_procedures.sql
│
└── 📁 Documentation/
    ├── ARQUITECTURA.md
    ├── API.md
    ├── INSTALACION.md
    └── GUIA_DESARROLLO.md
```

---

## 📈 Dashboards

### Dashboard Principal

```
╔════════════════════════════════════════════════════════════════════╗
║                    DASHBOARD PRINCIPAL                            ║
╠════════════════════════════════════════════════════════════════════╣
║                                                                    ║
║  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐║
║  │  USUARIOS ACTIVOS │  ��� TURNOS VIGENTES  │  │ASIGNACIONES HOY  ││
║  │      125         │  │       12         │  │       45         ││
║  │      ▲ 5%       │  │      ▼ 2%       │  │      ▲ 8%       ││
║  └──────────────────┘  └──────────────────┘  └──────────────────┘║
║                                                                    ║
║  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐║
║  │ ASISTENCIA HOY   │  │  PERMISOS        │  │  REPORTES        ││
║  │  98% (140/143)   │  │   SOLICITADOS    │  │  GENERADOS       ││
║  │                  │  │        8         │  │        3         ││
║  └──────────────────┘  └──────────────────┘  └──────────────────┘║
║                                                                    ║
║  ┌────────────────────────────────────────────────────────────────║
║  │  ASISTENCIA POR TURNO (ÚLTIMOS 7 DÍAS)                        ║
║  ├────────────────────────────────────────────────────────────────║
║  │                                                                │
║  │  Turno 1 (6:00-14:00)  ████████████████░░  95% (342/360)     │
║  │  Turno 2 (14:00-22:00) ██████████████░░░░  88% (316/360)     │
║  │  Turno 3 (22:00-06:00) ███████████░░░░░░░  78% (280/360)     │
║  │                                                                │
║  └────────────────────────────────────────────────────────────────║
║                                                                    ║
║  ┌────────────────────────────────────────────────────────────────║
║  │  USUARIOS CON MÁS AUSENCIAS                                   ║
║  ├────────────────────────────────────────────────────────────────║
║  │  1. Juan Pérez              8 ausencias                       │
║  │  2. María García            6 ausencias                       │
║  │  3. Carlos López            5 ausencias                       │
║  │  4. Ana Martínez            4 ausencias                       │
║  └────────────────────────────────────────────────────────────────║
║                                                                    ║
║  [← Anterior] [Actualizar] [Exportar PDF] [Siguiente →]         ║
╚════════════════════════════════════════════════════════════════════╝
```

### Dashboard de Turnos

```
╔════════════════════════════════════════════════════════════════════╗
║                    GESTIÓN DE TURNOS                              ║
╠════════════════════════════════════════════════════════════════════╣
║                                                                    ║
║  [Nuevo Turno] [Editar] [Eliminar] [Reportes]                   ║
║                                                                    ║
║  Buscar: _________________ [Filtrar por estado: Todos ▼]         ║
║                                                                    ║
║  ┌────────────────────────────────────────────────────────────────║
║  │ Nombre    │ Hora Inicio │ Hora Fin │ Duración │ Activo │ Acción║
║  ├────────────────────────────────────────────────────────────────║
║  │ Turno 1   │   06:00     │  14:00   │  8 hrs   │  ✓    │ ▼    ║
║  │ Turno 2   │   14:00     │  22:00   │  8 hrs   │  ✓    │ ▼    ║
║  │ Turno 3   │   22:00     │  06:00   │  8 hrs   │  ✓    │ ▼    ║
║  │ Turno Ext │   08:00     │  16:00   │  8 hrs   │  ✗    │ ▼    ║
║  └────────────────────────────────────────────────────────────────║
║                                                                    ║
║  Mostrando 1 - 4 de 4 turnos                                     ║
║                                                                    ║
╚════════════════════════════════════════════════════════════════════╝
```

### Dashboard de Asignaciones

```
╔════════════════════════════════════════════════════════════════════╗
║                   ASIGNACIONES DE TURNOS                          ║
╠════════════════════════════════════════════════════════════════════╣
║                                                                    ║
║  [Nueva Asignación] [Modificar] [Cancelar] [Generar Reportes]   ║
║                                                                    ║
║  Usuario: _________________ Turno: __________ Fecha: ____________ ║
║  [Buscar]                                                         ║
║                                                                    ║
║  ┌────────────────────────────────────────────────────────────────║
║  │ Usuario      │ Turno      │ F. Inicio │ F. Término │ Est. │ ▼ ║
║  ├────────────────────────────────────────────────────────────────║
║  │ Juan Pérez   │ Turno 1    │ 01/05/2025│ En curso  │ ✓   │ ▼ ║
║  │ María García │ Turno 2    │ 01/05/2025│ En curso  │ ✓   │ ▼ ║
║  │ Carlos López │ Turno 3    │ 01/04/2025│31/05/2025 │ ✓   │ ▼ ║
║  │ Ana Martínez │ Turno 1    │ 15/05/2025│ En curso  │ ✓   │ ▼ ║
║  │ Pedro Ruiz   │ Turno 2    │ 01/05/2025│ Cancelada │ ✗   │ ▼ ║
║  └────────────────────────────────────────────────────────────────║
║                                                                    ║
║  Mostrando 1 - 5 de 23 asignaciones                              ║
║                                                                    ║
╚════════════════════════════════════════════════════════════════════╝
```

### Dashboard de Asistencia

```
╔════════════════════════════════════════════════════════════════════╗
║                 REGISTRO DE ASISTENCIA                            ║
╠════════════════════════════════════════════════════════════════════╣
║                                                                    ║
║  Fecha: 26/05/2026  ┌─────────────────────────────────────────┐  ║
║  Hora: 14:32:15     │ Asistencia del día: 142 de 143 (99%)   │  ║
║                     └─────────────────────────────────────────┘  ║
║                                                                    ║
║  [Registrar Llegada] [Registrar Salida] [Registrar Permiso]     ║
║                                                                    ║
║  Búsqueda: _________________ [Buscar]                            ║
║                                                                    ║
║  ┌────────────────────────────────────────────────────────────────║
║  │ Usuario      │ Turno   │ Llegada  │ Salida  │ Trabajado │ Est.║
║  ├────────────────────────────────────────────────────────────────║
║  │ Juan Pérez   │ Turno 1 │  06:02   │ 14:00   │ 7:58 hrs │ ✓  ║
║  │ María García │ Turno 2 │  14:05   │ 22:00   │ 7:55 hrs │ ✓  ║
║  │ Carlos López │ Turno 3 │  22:15   │  ──     │ En curso │ ◐  ║
║  │ Ana Martínez │ Turno 1 │  ──      │  ──     │  ──      │ ✗  ║
║  │ Pedro Ruiz   │ Turno 2 │  14:30   │ 22:00   │ 7:30 hrs │ ✓  ║
║  │ Isabel Díaz  │ Turno 1 │  06:00   │ 14:00   │ Permiso  │ 🏥 ║
║  └────────────────────────────────────────────────────────────────║
║                                                                    ║
║  ✓: Presente | ✗: Ausente | ◐: En proceso | 🏥: Permiso          ║
║                                                                    ║
╚════════════════════════════════════════════════════════════════════╝
```

---

## ⚙️ Instalación y Configuración

### 1. Requisitos Previos

```
✓ Visual Studio 2019 o superior
✓ .NET Framework 4.8 SDK (Target Framework)
✓ SQL Server 2019 Developer Edition o superior
✓ Git para control de versiones
```

### 2. Clonar el Repositorio

```bash
git clone https://github.com/OrbitalDigits791/ProyectoTicketTurno_v2.git
cd ProyectoTicketTurno_v2
```

### 3. Restaurar Dependencias

```bash
nuget restore ProyectoTicketTurno.sln
```

### 4. Configurar Connection String

Editar `ProyectoTicketTurno.Data/App.config`:

```xml
<configuration>
  <connectionStrings>
    <add name="AplicacionDbContext" 
         connectionString="Server=YOUR_SERVER;Database=TicketTurnoDb;Integrated Security=true;" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

### 5. Crear Base de Datos

**Opción A: Usando Package Manager Console**

```powershell
# Abrir Package Manager Console en Visual Studio
# Seleccionar el proyecto: ProyectoTicketTurno.Data

Update-Database -Project ProyectoTicketTurno.Data -Verbose
```

**Opción B: Ejecutar Script SQL**

```bash
sqlcmd -S YOUR_SERVER -d TicketTurnoDb -i Database/Ticket_Turno_tablas.sql
sqlcmd -S YOUR_SERVER -d TicketTurnoDb -i Database/Ticket_Turno_datos_iniciales.sql
```

### 6. Compilar la Solución

```bash
# Desde Visual Studio o terminal
dotnet build ProyectoTicketTurno.sln
```

### 7. Ejecutar la Aplicación

```bash
# Desde Visual Studio: Presionar F5 o Click en Start
# O desde terminal:
dotnet run --project PTT.Presentation/PTT.Presentation.csproj
```

---

## 👨‍💻 Guía de Desarrollo

### Patrones Utilizados

#### 1. **Generic Repository Pattern**

```csharp
// Interfaz
public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void SaveChanges();
}

// Implementación
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    // Implementar métodos...
}
```

#### 2. **Service Layer Pattern**

```csharp
public interface IUsuarioService
{
    IEnumerable<UsuarioViewModel> ObtenerTodos();
    UsuarioViewModel ObtenerPorId(int id);
    void Crear(UsuarioViewModel usuario);
    void Actualizar(UsuarioViewModel usuario);
    void Eliminar(int id);
}

public class UsuarioService : IUsuarioService
{
    private readonly IGenericRepository<Usuario> _usuarioRepository;
    
    public UsuarioService(IGenericRepository<Usuario> usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    
    // Implementar métodos...
}
```

#### 3. **Dependency Injection**

```csharp
// En el punto de entrada (Program.cs o FormLogin)
private void ConfigurarDependencias()
{
    var container = new UnityContainer();
    
    // Registrar repositorios
    container.RegisterType(typeof(IGenericRepository<>), 
                          typeof(GenericRepository<>));
    
    // Registrar servicios
    container.RegisterType<IUsuarioService, UsuarioService>();
    container.RegisterType<ITurnoService, TurnoService>();
}
```

### Convenciones de Código

```csharp
// Nombres de variables
private int _usuarioId;           // Private fields: _camelCase
public int UsuarioId { get; set; } // Properties: PascalCase
var usuario = new Usuario();       // Local variables: camelCase

// Métodos
public async Task<Usuario> ObtenerUsuarioAsync(int id)
{
    // Métodos asincronos terminan con Async
    return await _repository.GetByIdAsync(id);
}

// Comentarios
/// <summary>
/// Obtiene un usuario por su ID
/// </summary>
/// <param name="id">ID del usuario</param>
/// <returns>Datos del usuario</returns>
public Usuario ObtenerPorId(int id)
{
    return _repository.GetById(id);
}
```

### Testing

```csharp
[TestClass]
public class UsuarioServiceTests
{
    private Mock<IGenericRepository<Usuario>> _mockRepository;
    private UsuarioService _usuarioService;
    
    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IGenericRepository<Usuario>>();
        _usuarioService = new UsuarioService(_mockRepository.Object);
    }
    
    [TestMethod]
    public void ObtenerPorId_DebeRetornarUsuario()
    {
        // Arrange
        var usuarioId = 1;
        var usuarioEsperado = new Usuario { Id = usuarioId, Nombre = "Test" };
        _mockRepository.Setup(r => r.GetById(usuarioId))
            .Returns(usuarioEsperado);
        
        // Act
        var resultado = _usuarioService.ObtenerPorId(usuarioId);
        
        // Assert
        Assert.IsNotNull(resultado);
        Assert.AreEqual(usuarioEsperado.Id, resultado.Id);
    }
}
```

---

## 📅 Plan de Desarrollo

### Fases del Proyecto

| Fase | Descripción | Duración | Estado |
|------|-----------|----------|--------|
| **Fase 1** | Fundación (Estructura + EF6 + Repository) | 2 semanas | ✅ Completada |
| **Fase 2** | Lógica de Negocio (Services + Validaciones) | 3 semanas | 🔄 En Progreso |
| **Fase 3** | Interfaz de Usuario (WinForms completa) | 3 semanas | ⏳ Pendiente |
| **Fase 4** | Dashboard y Reportes | 2 semanas | ⏳ Pendiente |
| **Fase 5** | Testing y Optimización | 2 semanas | ⏳ Pendiente |
| **Fase 6** | Documentación y Deployment | 1 semana | ⏳ Pendiente |

### Roadmap Detallado

#### Q2 2026
- [x] Estructura base del proyecto
- [x] Configuración de Entity Framework 6
- [x] Implementación del Generic Repository
- [ ] Crear todos los modelos/entidades
- [ ] Implementar migraciones iniciales

#### Q3 2026
- [ ] Implementar servicios de usuario
- [ ] Implementar servicios de turno
- [ ] Crear validaciones de negocio
- [ ] Implementar manejo de errores
- [ ] Crear servicios de asistencia

#### Q4 2026
- [ ] Interfaz de login
- [ ] Interfaz de gestión de turnos
- [ ] Interfaz de asignaciones
- [ ] Interfaz de asistencia
- [ ] Interfaz de reportes
- [ ] Dashboard completo

#### Q1 2027
- [ ] Temas y personalización
- [ ] Optimización de rendimiento
- [ ] Suite completa de tests
- [ ] Documentación técnica
- [ ] Guía de usuario
- [ ] Entrega final

---

## 🤝 Contribución

### Flujo de Trabajo para Contribuidores

1. **Fork el repositorio** en GitHub

2. **Clonar el fork**
```bash
git clone https://github.com/TU_USUARIO/ProyectoTicketTurno_v2.git
cd ProyectoTicketTurno_v2
git remote add upstream https://github.com/OrbitalDigits791/ProyectoTicketTurno_v2.git
```

3. **Crear una rama para tu feature**
```bash
git checkout -b feature/nombre-del-feature
```

4. **Realizar cambios y commit**
```bash
git add .
git commit -m "feat: Descripción clara del cambio"
```

5. **Subir cambios a tu fork**
```bash
git push origin feature/nombre-del-feature
```

6. **Crear Pull Request**
   - Ve a GitHub
   - Crea PR desde tu rama a `main`
   - Completa la descripción del PR
   - Espera revisión

### Estándares de Código

- ✅ Usar nomenclatura PascalCase para clases
- ✅ Usar camelCase para variables locales
- ✅ Documentar métodos públicos con XML comments
- ✅ Mantener líneas máximo 100 caracteres
- ✅ Incluir tests para nuevas funcionalidades
- ✅ Seguir patrones SOLID

### Tipos de Commits

```
feat:     Nueva funcionalidad
fix:      Corrección de bug
docs:     Cambios en documentación
style:    Formato de código
refactor: Reorganización sin cambiar funcionamiento
test:     Agregar o actualizar tests
chore:    Cambios en configuración
```

---

## 📞 Soporte y Contacto

- **Problemas**: [Abrir un Issue](https://github.com/OrbitalDigits791/ProyectoTicketTurno_v2/issues)
- **Preguntas**: [Crear una Discusión](https://github.com/OrbitalDigits791/ProyectoTicketTurno_v2/discussions)
- **Propuestas**: [Crear una Issue de Feature Request](https://github.com/OrbitalDigits791/ProyectoTicketTurno_v2/issues)

---

## 📜 Licencia

Este proyecto está bajo la Licencia MIT. Ver archivo `LICENSE` para más detalles.

---

## ✨ Agradecimientos

- Equipo de desarrollo de OrbitalDigits791
- Comunidad .NET
- Contribuidores del proyecto

---

**Última actualización**: 26 de Mayo de 2026  
**Versión**: 2.0  
**Status**: 🔄 En Desarrollo Activo
