# RocketCoolingChamber

Modulo paralelo a `src/HelixHeatX`. No modifica el intercambiador original.

## Contenido

- `RocketCoolingParameters.cs`: parametros geometricos y limites iniciales.
- `RocketCoolingProfile.cs`: radio axial de camara, convergente, garganta y tobera.
- `RocketCoolingChamber.cs`: generacion de canales helicoidales conectados.
- `RocketCoolingThermalEstimate.cs`: estimacion 1D preliminar para un canal.
- `Program.cs`: smoke test del perfil y del modelo termico, sin inicializar PicoGK.
- `RocketCoolingLattice.cs`: lattice estructural BCC recortado al volumen frontera.
- `RocketCoolingChamber.csproj`: proyecto base y perfil opcional con PicoGK.

## Uso previsto

```csharp
RocketCoolingParameters parameters = new RocketCoolingParameters();
RocketCoolingChamber chamber = new RocketCoolingChamber(parameters);
Voxels coolantDomain = chamber.VoxGetCoolingChannels();
```

Esta primera version genera el dominio de refrigerante. La siguiente fase debe añadir:

1. volumen cerrado de gases calientes;
2. pared derivada por offset;
3. colectores de entrada y salida;
4. ensamblaje `solid - hotGas - coolant`;
5. exportacion separada de dominios para inspeccion.

## Programa de prueba

`Program.cs` comprueba que el perfil tenga camara, garganta y salida divergente, y ejecuta una estimacion con propiedades constantes. Los valores de fluido son solo de prueba y no representan automaticamente LOX, metano o hidrogeno.

El programa de generacion geometrica debe integrarse despues en el proyecto que contenga las referencias de PicoGK y ShapeKernel. No se debe combinar este smoke test con la tarea principal de `CoolCube.HelixHeatX` sin configurar primero el proyecto anfitrion.

El beam circular es una aproximacion inicial. Para fabricar canales rectangulares o trapezoidales sera necesario reemplazarlo por una seccion barrida.

## Compilacion

La configuracion base compila el perfil y el smoke test sin PicoGK:

```text
dotnet run --project src/RocketCoolingChamber/RocketCoolingChamber.csproj
```

La geometria requiere PicoGK y ShapeKernel. El repositorio ya incluye sus fuentes en `libs/PicoGK` y `libs/LEAP71_ShapeKernel`:

```text
dotnet build src/RocketCoolingChamber/RocketCoolingChamber.csproj \
	-p:PicoGKGeometry=true
```

ShapeKernel no trae un `.csproj` independiente en esta revision; sus fuentes se incluyen directamente. PicoGK se referencia mediante `libs/PicoGK/PicoGK.csproj`.
