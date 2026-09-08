# RocketMotor

Geometria base para un motor grande de cohete basada en PicoGK y ShapeKernel.

## Estructura

El proyecto sigue el patron de tutoriales de PicoGK/ShapeKernel:

| Archivo | Descripcion |
|---------|-------------|
| `Program.cs` | Punto de entrada con `Library.Go()` |
| `Ex_RocketMotorChamber.cs` | Camara de combustion basica y modulada |
| `Ex_RocketMotorNozzle.cs` | Perfil de tobera y tobera con pared |
| `Ex_RocketMotorInjector.cs` | Placa de inyectores y elementos lattice |
| `Ex_RocketMotorTurbopump.cs` | Impulsor de turbobomba con alabes |
| `Ex_RocketMotorPreburner.cs` | Preburner toroidal |
| `Ex_RocketMotorCoolingChannels.cs` | 120 canales helicoidales regenerativos |
| `Ex_RocketMotorLattice.cs` | Lattice BCC con interfaces ICellArray/ILatticeType/IBeamThickness |
| `Ex_RocketMotorAssembly.cs` | Motor ensamblado completo |

## Uso

Compila con geometria PicoGK/ShapeKernel:

```text
dotnet build src/RocketMotor/RocketMotor.csproj -p:PicoGKGeometry=true
```

Ejecuta el proyecto:

```text
dotnet run --project src/RocketMotor/RocketMotor.csproj -- [task]
```

Esto abre el visor de PicoGK y genera automaticamente los STL y capturas TGA en `/workspaces/LEAP71_HelixHeatX/output/`.

## Tareas disponibles

| Task | Descripcion |
|------|-------------|
| `chamber` | Camara de combustion basica y modulada |
| `nozzle` | Perfil de tobera y tobera con espesor de pared |
| `injector` | Placa de inyectores y elementos lattice |
| `turbopump` | Impulsor de turbobomba con alabes |
| `preburner` | Preburner toroidal |
| `cooling` | 120 canales helicoidales regenerativos |
| `lattice` | Lattice BCC con interfaces ICellArray/ILatticeType/IBeamThickness |
| `assembly` | Motor ensamblado sin refrigeracion |
| `assemblywithcooling` | Motor ensamblado con canales de refrigeracion restados, post-procesado y exportacion STL lista para impresion |

## Ejemplo

```text
dotnet run --project src/RocketMotor/RocketMotor.csproj -- assemblywithcooling
```

## Exportacion

Los archivos se exportan automaticamente a `/workspaces/LEAP71_HelixHeatX/output/`:

- `RocketMotorAssembly.stl` — motor ensamblado completo
- `RocketMotorChamber_Basic.stl` — camara basica
- `RocketMotorChamber_Modulated.stl` — camara modulada
- `RocketMotorNozzle_Profile.stl` — perfil de tobera
- `RocketMotorNozzle_Wall.stl` — tobera con espesor de pared
- `RocketMotorInjector_Plate.stl` — placa de inyectores
- `RocketMotorInjector_Elements.stl` — elementos de inyectores
- `RocketMotorTurbopump_Hub.stl` — hub de turbobomba
- `RocketMotorTurbopump_Blades.stl` — alabes de turbobomba
- `RocketMotorPreburner_Toroid.stl` — preburner toroidal
- `RocketMotorCoolingChannels.stl` — canales de refrigeracion
- `RocketMotorLattice_BCC.stl` — lattice BCC

Tambien se generan capturas TGA en la misma carpeta.

## Parametros base

| Parametro | Valor | Unidad |
|-----------|-------|--------|
| Radio camara | 190 | mm |
| Radio garganta | 99 | mm |
| Radio salida tobera | 530 | mm |
| Canales refrigeracion | 120 | - |
| Angulo helix | 35 | deg |
| Inyectores | 24 | - |
| RPM turbobomba | 35000 | rpm |

## Referencias

- `documentation_II_dsgn/important_/notasDeMotorpdf.txt` — parametros motor ASE
- `documentation_II_dsgn/important_/otrasInstruccionesLattice.txt` — workflow de lattice
- `documentation_II_dsgn/important_/shapekernelInstructions tutorial.txt` — tutorial ShapeKernel
- `notes/small_notes/notas01 (1).txt` — arquitectura de motor hibrido
- `libs/PicoGK` — voxelizacion y campos escalares
- `libs/LEAP71_ShapeKernel` — superficies spline y contornos
- `lattice/example` — ejemplos originales de lattice
- `lattice_II/others/important` — interfaces ICellArray, ILatticeType, IBeamThickness
- `roverwheels/rocket/refrigeration_channels` — ejemplos de canales de refrigeracion
- `src/RocketCoolingChamber` — canales de refrigeracion regenerativa

## Flujo de trabajo

1. Compilar con `-p:PicoGKGeometry=true`
2. Ejecutar el proyecto
3. El visor de PicoGK mostrara las geometrias generadas
4. Los archivos STL y capturas TGA se guardan en `/output`
5. Cambiar el Task en `Program.cs` para probar diferentes geometrias

## Notas tecnicas

- Resolucion de voxel: 0.25 mm
- Viewer: se abre automaticamente con PicoGK
- Exportacion: STL, TGA, PNG, VDB, CLI
- Coordenadas: mm en espacio 3D
- Convenciones de nombres: prefijos f (float), n (int), vec (Vector3), o (objeto), x (interfaz)
