# RocketMotor

Geometria base para un motor grande de cohete basada en PicoGK y ShapeKernel.

## Contenido

- `Program.cs` — punto de entrada Task() para PicoGK
- `MotorParameters.cs` — parametros del motor ASE extraidos de `documentation_II_dsgn/important_/notasDeMotorpdf.txt`
- `ChamberGeometry.cs` — geometria de camara de combustion y convergente-divergente
- `NozzleGeometry.cs` — perfil de tobera tipo bell con modulacion
- `InjectorGeometry.cs` — placa de inyectores coaxiales
- `TurbopumpGeometry.cs` — impulsor helicoidal y disco de turbobomba
- `PreburnerGeometry.cs` — preburner toroidal con puertos de alimentacion
- `CoolingChannels.cs` — canales de refrigeracion helicoidales regenerativos
- `LatticeReinforcement.cs` — refuerzo interno con lattice y quasicrystal
- `MotorAssembly.cs` — ensamblaje completo del motor

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

1. Crear Base Shapes (cilindros, tores, pipes)
2. Aplicar modulaciones de radio y superficie
3. Voxelizar con `voxConstruct(fVoxelSize)`
4. Ensamblar con `Sh.voxUnion()` y `Sh.voxSubtract()`
5. Añadir lattice con interfaces `ICellArray`, `ILattice`, `IBeamThickness`
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
