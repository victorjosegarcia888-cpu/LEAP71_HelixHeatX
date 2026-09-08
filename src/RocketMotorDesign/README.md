# RocketMotorDesign

Analisis y diseno de motor de cohete de pikogk y diseno espacial de motor y refrigeracion.

## Contenido

- `EngineParameters.cs`: parametros del motor ASE extraidos de `documentation_II_dsgn/important_/notasDeMotorpdf.txt`
- `ChamberDesign.cs`: perfil radial de camara, convergente, garganta y tobera
- `NozzleDesign.cs`: generacion de perfil de tobera tipo bell
- `InjectorDesign.cs`: geometria de inyectores coaxiales
- `TurbopumpDesign.cs`: diseno de turbobomba (inducer, impeller, disco)
- `PreburnerDesign.cs`: preburner toroidal y resonador acustico
- `ThermalEstimate.cs`: estimacion termica 1D por segmentos
- `MotorAssembly.cs`: ensamblaje del motor completo
- `Program.cs`: smoke test y demostracion

## Uso

```text
dotnet run --project src/RocketMotorDesign/RocketMotorDesign.csproj
```

Con geometria PicoGK:

```text
dotnet build src/RocketMotorDesign/RocketMotorDesign.csproj -p:PicoGKGeometry=true
```

## Referencias

- `documentation_II_dsgn/important_/notasDeMotorpdf.txt`: parametros del motor ASE
- `notes/small_notes/engineInstrucciones.txt`: parametros Raptor
- `notes/small_notes/notas01 (1).txt`: arquitectura de motor hibrido
- `libs/PicoGK`: voxelizacion y campos escalares
- `libs/LEAP71_ShapeKernel`: superficies spline y contornos
- `src/RocketCoolingChamber`: canales de refrigeracion regenerativa
- `roverwheels/rocket/refrigeration_channels`: ejemplos de canales
