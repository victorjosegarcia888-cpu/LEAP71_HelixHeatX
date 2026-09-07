# RocketCoolingChamber

Modulo paralelo a `src/HelixHeatX`. No modifica el intercambiador original.

## Contenido

- `RocketCoolingParameters.cs`: parametros geometricos y limites iniciales.
- `RocketCoolingProfile.cs`: radio axial de camara, convergente, garganta y tobera.
- `RocketCoolingChamber.cs`: generacion de canales helicoidales conectados.
- `RocketCoolingThermalEstimate.cs`: estimacion 1D preliminar para un canal.
- `Program.cs`: smoke test del perfil y del modelo termico, sin inicializar PicoGK.

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
