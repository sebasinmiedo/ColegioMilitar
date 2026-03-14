# ColegioMilitar - Sistema de Registro de Sanciones

## Estructura de la solución

```
ColegioMilitar.sln
├── src/
│   ├── ColegioMilitar.Domain         → Entidades (equivale a BE en tu ERP)
│   ├── ColegioMilitar.Infrastructure → DbContext + Repositorios (equivale a DA)
│   ├── ColegioMilitar.Application    → Reglas de negocio (equivale a BR)
│   ├── ColegioMilitar.Reports        → Exportación Excel con ClosedXML
│   └── ColegioMilitar.UI             → WinForms (.NET 10)
└── docs/
    └── schema_referencia.sql         → Script SQL de referencia (no usar en producción)
```

## Pasos para arrancar desde cero

### 1. Crear la solución
```bash
# Ejecutar en una carpeta vacía
setup.bat
```

### 2. Copiar los archivos generados
Copiar el contenido de cada carpeta `src/` en el proyecto correspondiente.

### 3. Crear la migración inicial
```bash
cd src/ColegioMilitar.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../ColegioMilitar.UI
```

### 4. Aplicar la migración
La base de datos se crea automáticamente al arrancar la app con `DbFactory.EnsureCreated()`.
También puedes aplicarla manualmente:
```bash
dotnet ef database update --startup-project ../ColegioMilitar.UI
```

### 5. Arrancar la app
```bash
dotnet run --project src/ColegioMilitar.UI
```

## Paquetes NuGet requeridos

| Proyecto | Paquete |
|---|---|
| Infrastructure | `Microsoft.EntityFrameworkCore.Sqlite` |
| Infrastructure | `Microsoft.EntityFrameworkCore.Tools` |
| Infrastructure | `Microsoft.EntityFrameworkCore.Design` |
| Reports | `ClosedXML` |

## Próximos pasos
- [ ] Repositorios (CadeteRepository, SancionRepository, etc.)
- [ ] Servicios en Application (SancionService, ConsolidadoService)
- [ ] Formulario de registro de sanciones (UI)
- [ ] Generación de reportes Excel (Reports)
