# Laboratorio CRUD de ASP.NET

📅 Fecha: 23/11/2026  
📚 Asignatura: Herramientas de Programación Aplicada III (.NET)  
👩‍🏫 Instructora: Ing. Irina Fong  
👥 Grupo: 1IL133

## 📋 Contenido del Repositorio

Este repositorio contiene una aplicación web desarrollada en **C# con ASP.NET Web Forms (.NET Framework 4.7.2)** y base de datos **MySQL**, a la que solo se accede mediante **procedimientos almacenados**.

La aplicación implementa un **CRUD completo** sobre la tabla `usuarios`:

1. **Create:** formulario para registrar un nuevo usuario, con validación de los datos y control de correos duplicados.
2. **Read:** listado de los usuarios activos y consulta individual de cada registro en modo solo lectura.
3. **Update:** carga de los datos del usuario en el formulario y modificación de los mismos.
4. **Delete:** eliminación lógica con confirmación previa. El registro no se borra: se marca como anulado y se guarda la fecha de eliminación.

La interfaz usa una **página maestra** con Bootstrap, una página de listado (`Index.aspx`) y un formulario (`Crud.aspx`) que cambia de comportamiento según la operación recibida por la URL.

## 🛠 Tecnologías Utilizadas

| Tecnología | Versión |
|---|---|
| **Lenguaje** | C# |
| **Framework** | ASP.NET Web Forms (.NET Framework 4.7.2) |
| **Base de datos** | MySQL 8.x |
| **Conector** | MySql.Data (NuGet) |
| **Estilos** | Bootstrap 5.3.8 (CDN) |
| **IDE** | Visual Studio 2022 |
| **Control de versiones** | Git |

**Conceptos aplicados:**

- Operaciones CRUD
- Páginas maestras (Master Pages)
- Procedimientos almacenados con parámetros tipados
- Eliminación lógica y campos de auditoría
- Paso de datos entre páginas con `Request.QueryString`
- Cadena de conexión centralizada en el `Web.config`
- Manejo de excepciones y validación de datos

## 💻 Arquitectura y Funcionalidades

### Flujo de la aplicación

```text
Index.aspx (listado)  ──►  Crud.aspx?op=C           ──►  insert_user
        │                  Crud.aspx?id=X&op=R      ──►  sp_read
        │                  Crud.aspx?id=X&op=U      ──►  sp_read, sp_update
        └──►  Botón Delete                          ──►  sp_delete
```

### Operaciones

| Operación | Cómo funciona | Procedimiento |
|---|---|---|
| **Create** | `Crud.aspx?op=C` muestra el formulario vacío y el botón Guardar | `insert_user` |
| **Read** | `Index.aspx` lista los usuarios activos; `Crud.aspx?id=X&op=R` consulta un usuario con los campos bloqueados | `sp_load`, `sp_read` |
| **Update** | `Crud.aspx?id=X&op=U` carga los datos y permite modificarlos | `sp_read`, `sp_update` |
| **Delete** | El botón Delete de `Index.aspx` pide confirmación y anula el registro | `sp_delete` |

### Base de datos

El archivo:

```text
bd_crudasp.sql
```

crea la base de datos `crudasp`, la tabla `usuarios`, los procedimientos almacenados y tres registros de ejemplo.

**Tabla `usuarios`:**

| Campo | Tipo | Observación |
|---|---|---|
| `idUsuario` | int | Llave primaria, autoincremental |
| `Nombre` | varchar(150) | |
| `Edad` | int | |
| `Correo` | varchar(150) | Índice único `index_Correo` |
| `FechaNacimiento` | date | |
| `FechaModificacion` | datetime | Se actualiza automáticamente en cada `UPDATE` |
| `FechaDeInsercion` | datetime | Se llena automáticamente al insertar |
| `Anulado` | tinyint(1) | 1 = activo, 0 = eliminado |
| `FechaEliminacion` | datetime | Se llena al eliminar |

**Procedimientos almacenados:** `sp_load`, `sp_read`, `insert_user`, `sp_update` y `sp_delete`.

### Seguridad aplicada

| Riesgo | Medida |
|---|---|
| Inyección SQL | Solo procedimientos almacenados con parámetros tipados |
| XSS | Codificación HTML de los mensajes mostrados al usuario |
| Pérdida de información | Eliminación lógica: el registro se conserva con su fecha de eliminación |
| Eliminación accidental | Confirmación antes de eliminar |
| Correos duplicados | Índice único en la base de datos y mensaje claro al usuario |
| Credenciales en el código | La cadena de conexión está en el `Web.config`, no en las páginas |

## 💻 Capturas de Pantalla

### Listado de usuarios (Read)
<img width="1920" height="1021" alt="image" src="https://github.com/user-attachments/assets/cf0e2e91-0a88-4c65-b76d-27a223baf5a2" />


### Crear usuario (Create)
<img width="1920" height="1021" alt="image" src="https://github.com/user-attachments/assets/07509c7a-32f5-4d29-8d6f-db9a9ea78ed0" />


### Modificar usuario (Update)
<img width="1920" height="1021" alt="image" src="https://github.com/user-attachments/assets/fa283208-27f9-4207-ba3d-9f6c27cddc71" />


### Eliminar usuario (Delete)
<img width="1920" height="1025" alt="image" src="https://github.com/user-attachments/assets/d5c2a3a4-9db5-45d7-81de-3da0cb33b9ff" />

<img width="1920" height="1027" alt="image" src="https://github.com/user-attachments/assets/571158df-781e-4f00-b33f-2fe439241601" />


## 📁 Estructura de Carpetas o Directorios

```text
CRUD/
│
├── README.md
├── .gitignore
├── CRUD.sln
├── bd_crudasp.sql
│
├── capturas/         (read.png, create.png, update.png, delete.png)
│
└── CRUD/
    ├── Web.config
    ├── Site.Master              (página maestra con la barra de navegación)
    └── Pages/
        ├── Index.aspx           (listado y botones Read, Update y Delete)
        └── Crud.aspx            (formulario para crear, consultar y modificar)
```

Los directorios `.vs`, `bin`, `obj` y `packages` corresponden a archivos generados por Visual Studio y por el proceso de compilación. No se incluyen en el repositorio (ver `.gitignore`).

## ▶️ Instrucciones de Instalación y Ejecución

### Requisitos

- Windows 10 u 11.
- Visual Studio 2022 con la carga de trabajo **Desarrollo de ASP.NET y web**.
- .NET Framework 4.7.2 (Developer Pack).
- Servidor **MySQL 8.0 o superior** instalado y funcionando.

### Pasos

**1. Clonar el repositorio**

```bash
git clone [URL del repositorio]
cd CRUD
```

**2. Crear la base de datos**

Abrir `bd_crudasp.sql` en MySQL Workbench y ejecutarlo completo, o desde la consola:

```bash
mysql -u root -p < bd_crudasp.sql
```

> ⚠️ El script elimina y vuelve a crear la tabla `usuarios`, por lo que borra los datos si ya existe.

**3. Verificar la creación**

```sql
USE crudasp;
SELECT idUsuario, Nombre, Correo FROM usuarios;
SHOW PROCEDURE STATUS WHERE Db = 'crudasp';
```

**4. Configurar la conexión**

En `CRUD/Web.config`, cambiar `Uid` y `Pwd` por las credenciales de MySQL de tu equipo.

**5. Abrir la solución**

Abrir `CRUD.sln` en Visual Studio. El paquete NuGet `MySql.Data` se restaura automáticamente. Si no lo hace: clic derecho en la solución → **Restaurar paquetes NuGet**.

**6. Compilar y ejecutar**

Compilar con `Ctrl + Shift + B` y ejecutar con `F5`. La página de inicio es `Pages/Index.aspx`.

## ⚠️ Consideraciones

La cadena de conexión se encuentra en:

```text
CRUD/Web.config
```

Los valores por defecto son:

```text
Server=localhost
Database=crudasp
Uid=root
Pwd=TU_CLAVE
```

La contraseña debe ajustarse a la de cada equipo y no debe publicarse en un repositorio público. El uso del usuario `root` es **solo para fines académicos**; en un proyecto real se debe crear un usuario con privilegios mínimos.

Los usuarios eliminados permanecen en la tabla con `Anulado = 0`. Para consultarlos se puede ejecutar `SELECT * FROM usuarios WHERE Anulado = 0;`.

## 👥 Integrante y Contexto

- **Integrante:** Mario Fernandez
- **Institución:** Universidad Tecnológica de Panamá (UTP) — Facultad de Ingeniería en Sistemas Computacionales
- **Asignatura:** Herramientas de Programación Aplicada III (.NET)
- **Grupo:** 1IL133
- **Instructora:** Ing. Irina Fong
- **Fecha de Realización:** 23/11/2026

Este laboratorio fue desarrollado para aplicar el módulo de ASP.NET: páginas maestras, controles de servidor, acceso a MySQL con procedimientos almacenados y la implementación de un CRUD completo con C# y ASP.NET Web Forms.

## 📚 Referencias

- Presentación del curso "ASP.NET: Un Vistazo" — Ing. Irina Fong.
- Microsoft Learn — Documentación oficial de C#.
- Microsoft Learn — ASP.NET Web Forms y .NET Framework 4.7.2.
- MySQL Documentation — Stored Programs.
- Documentación de `MySql.Data` para conectividad entre C# y MySQL.
- Bootstrap 5 — documentación oficial.
