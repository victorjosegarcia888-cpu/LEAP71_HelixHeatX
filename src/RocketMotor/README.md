# RocketMotor

Geometria base para un motor grande de cohete basada en PicoGK y ShapeKernel.

## Tareas disponibles

Cambia el Task en `Program.cs` para ejecutar diferentes geometrias:

| Task | Descripcion |
|------|-------------|
| `Task_ChamberAndNozzle` | Camara y tobera basicas |
| `Task_InjectorPlate` | Placa de inyectores con lattice |
| `Task_TurbopumpImpeller` | Impulsor de turbobomba |
| `Task_PreburnerToroid` | Preburner toroidal |
| `Task_CoolingChannels` | Canales de refrigeracion helicoidales |
| `Task_AperiodicLattice` | Lattice aperiodico en caja |
| `Task_AssembledMotor` | Motor ensamblado completo |
| `Task_NozzleWithWall` | Tobera con espesor de pared |
| `Task_ChamberWithLattice` | Camara con lattice interno |
| `Task_HP_Ducts` | Ductos de alta presion |

## Uso

```text
dotnet run --project src/RocketMotor/RocketMotor.csproj
```

Con geometria PicoGK/ShapeKernel:

```text
dotnet build src/RocketMotor/RocketMotor.csproj -p:PicoGKGeometry=true
```

## Referencias

- `documentation_II_dsgn/important_/notasDeMotorpdf.txt` — parametros motor ASE
- `documentation_II_dsgn/important_/otrasInstruccionesLattice.txt` — workflow de lattice
- `documentation_II_dsgn/important_/shapekernelInstructions tutorial.txt` — tutorial ShapeKernel
- `libs/PicoGK` — voxelizacion y campos escalares
- `libs/LEAP71_ShapeKernel` — superficies spline y contornos
- `src/RocketCoolingChamber` — canales de refrigeracion regenerativa
- `roverwheels/rocket/refrigeration_channels` — ejemplos de canales

## Workflow PicoGK

1. Crear Base Shapes (`BaseCylinder`, `BasePipe`, `BaseRing`, `BaseBox`)
2. Aplicar modulaciones de radio y superficie
3. Voxelizar con `voxConstruct()`
4. Ensamblar con `voxBoolAdd()` y `voxBoolSubtract()`
5. Anadir lattice con interfaces `ICellArray`, `ILatticeType`, `IBeamThickness`
6. Preview con `Sh.PreviewVoxels()`

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

## Estructura

- `Program.cs` — punto de entrada Task() para PicoGK
- `MotorTasks.cs` — tareas de geometria del motor
