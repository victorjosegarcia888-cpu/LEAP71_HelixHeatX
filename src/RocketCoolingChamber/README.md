# RocketCoolingChamber

Modulo paralelo a `src/HelixHeatX`. No modifica el intercambiador original.

## Contenido

- `RocketCoolingParameters.cs`: parametros geometricos y limites iniciales.
- `RocketCoolingProfile.cs`: radio axial de camara, convergente, garganta y tobera.
- `RocketCoolingChamber.cs`: generacion de canales helicoidales conectados.
- `RocketCoolingThermalEstimate.cs`: estimacion 1D preliminar para un canal.

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

El beam circular es una aproximacion inicial. Para fabricar canales rectangulares o trapezoidales sera necesario reemplazarlo por una seccion barrida.
