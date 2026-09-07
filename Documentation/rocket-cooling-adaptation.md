# Adaptacion de camara y canales de refrigeracion

## Alcance

Este documento adapta la geometria de `LEAP71_HelixHeatX` a una primera arquitectura de camara de combustion con refrigeracion regenerativa. No es un diseno certificado de motor ni sustituye CFD, analisis estructural, caracterizacion de material o ensayos.

Los documentos de `notes/small_notes` se usan como referencia conceptual:

- `motor (1).pdf`: motor ASE de 89 kN, ciclo staged-combustion y esquemas de refrigeracion regenerativa.
- `cap4_fundamentos_turbinas (1).pdf`: arquitectura de camara anular, garganta, turbina y limites termicos de una turbina de gas.
- `engineInstrucciones.txt`: parametros de referencia de un Raptor; no deben copiarse sin reescalado.

## Lectura tecnica aplicable

El documento del motor presenta dos decisiones utiles para nuestro caso:

1. Una camisa regenerativa puede dividirse en circuitos y conectarse mediante colectores de entrada, retorno y salida.
2. El esquema `pass-and-a-half` lleva el refrigerante por una parte de la tobera, invierte el sentido en un manifold y lo hace regresar por la zona de camara y garganta. Esto reduce el riesgo de una distribucion de flujo inestable frente a un reparto paralelo mal controlado.

El capitulo de turbinas aporta una distincion que debe mantenerse: una camara anular refrigerada por aire de turbina no es igual que una camara de cohete refrigerada por canales. Podemos reutilizar la idea de doble pared y control de temperatura, pero no sus dimensiones ni sus correlaciones de operacion.

## Mapeo de la geometria actual

| HelixHeatX actual | Camara regenerativa propuesta |
|---|---|
| `voxHotFluidVoid` | volumen de gas caliente o dominio interno de camara/tobera |
| `voxCoolFluidVoid` | volumen de refrigerante que debe sustituirse por canales alrededor de la pared |
| `voxInnerVolume.voxOffset(...)` | pared metalica alrededor del dominio caliente |
| `GetInlet()` y `GetOutlet()` | colectores de entrada, retorno y salida del refrigerante |
| `voxGetTurningFins()` | nervios o separadores entre canales; usar con cuidado en zona de garganta |
| `voxGetStraightFins()` | aletas internas de intercambiador; no deben copiarse automaticamente a una camara |
| `voxGetOuterStructure()` | camisa exterior, refuerzos y manifolds |
| resta booleana final | `solidShell - coolingFluidDomain` |

La idea central es conservar el patron de construccion por volumen:

```text
hotDomain = camara + garganta + tobera
solidShell = outerBody - hotDomain
coolingFluidDomain = canales + colectores + retornos
finalPart = solidShell - coolingFluidDomain
```

Los canales no deben construirse como una decoracion sobre la superficie. Deben ser un dominio continuo, cerrado y conectado a sus colectores.

## Nuevo modelo parametrico minimo

El generador de camara debe recibir parametros, no depender de los valores fijos del cubo actual:

```text
ChamberRadius
ChamberLength
ThroatRadius
ConvergentLength
NozzleExitRadius
NozzleLength
LinerThickness
OuterWallThickness
ChannelCount
ChannelWidth
ChannelHeight
ChannelPitch
HelixAngle
CoolantMassFlow
HotGasTemperature
CoolantInletTemperature
CoolantPressure
```

Las magnitudes geometricas deben permanecer en mm, siguiendo el repositorio actual. Las magnitudes termodinamicas deben usar SI: Pa, K, kg/s, J/(kg K), W/(m K).

Como primera pieza de software conviene separar:

- `RocketCoolingParameters`: entradas y limites geometricos.
- `ChamberProfile`: radio interno y externo en funcion de `z`.
- `CoolingChannelPath`: trayectoria de cada canal.
- `CoolingThermalEstimate`: calculo 1D por segmentos.
- `RegenerativeChamberBuilder`: conversion de los dominios a `Voxels`.

## Perfil radial de camara y tobera

Definir una unica funcion axial `rInner(z)` para evitar que los canales se separen de la pared:

```text
0 <= z < Lc:
    rInner(z) = Rc

Lc <= z <= Lc + Lconv:
    rInner(z) = transicion(Rc, Rt, z)

Lc + Lconv < z <= Ltotal:
    rInner(z) = transicion(Rt, Re, z)
```

La transicion debe ser al menos continua en radio y preferiblemente suave en pendiente. La funcion exterior es:

```text
rOuter(z) = rInner(z) + LinerThickness + ChannelHeight + OuterWallThickness
```

La pared caliente entre el gas y el refrigerante es `LinerThickness`. El canal debe quedar completamente dentro del material exterior y no puede cruzar la pared caliente ni la pared externa.

## Trayectoria de canales

Para un canal `i` entre `0` y `N - 1`:

```text
phi_i(z) = 2 * pi * i / N + z * tan(HelixAngle) / rChannel(z)
rChannel(z) = rInner(z) + LinerThickness + 0.5 * ChannelHeight
```

La trayectoria se genera por muestras axiales y se convierte en una `Lattice` de beams o en una seccion barrida. La seccion debe ser rectangular o trapezoidal, no unicamente un beam circular, cuando el objetivo sea representar canales fresados o impresos.

Para mantener fabricabilidad:

- `ChannelWidth` y `ChannelHeight` deben ser mayores que varias veces el voxel seleccionado.
- La distancia entre canales debe conservar un nervio estructural continuo.
- La garganta necesita revisar por separado el espesor de pared y el radio de giro del canal.
- La entrada y el retorno deben conectarse a colectores con transiciones suaves.
- Los extremos deben taparse o conectarse a un manifold; un canal abierto dentro del volumen no es un dominio valido.

## Estimacion termica 1D

Antes de ejecutar CFD se puede evaluar cada segmento axial con un modelo de red termica.

Area hidraulica y diametro hidraulico para un canal rectangular:

```text
A = ChannelWidth * ChannelHeight
P = 2 * (ChannelWidth + ChannelHeight)
Dh = 4 * A / P
```

Con densidad `rho`, velocidad media `v` y viscosidad dinamica `mu`:

```text
Re = rho * v * Dh / mu
```

La potencia absorbida por el refrigerante es:

```text
Qcool = mdot * cp * (Tout - Tin)
```

Para un segmento, el balance de pared puede aproximarse por:

```text
q'' = (Tgas - Tcoolant) /
      (1 / hGas + LinerThickness / kWall + 1 / hCoolant)
```

La perdida de carga debe incluir friccion distribuida y singularidades de entrada, giro y colector:

```text
DeltaP = f * (L / Dh) * (rho * v^2 / 2)
       + sum(K) * (rho * v^2 / 2)
```

Estas ecuaciones son una preseleccion de geometria. `hGas`, `hCoolant`, `f`, propiedades de fluido y flujo bifasico deben provenir de correlaciones apropiadas al propelente y al regimen, no de constantes copiadas del ejemplo HelixHeatX.

## Secuencia de implementacion

1. Extraer la geometria de cubo de `HelixHeatX` en funciones de perfil, dominios y ensamblaje.
2. Crear el perfil axial de camara, convergente, garganta y tobera.
3. Generar primero `hotDomain` y comprobar que es un volumen cerrado.
4. Generar una sola familia de canales de refrigeracion y sus colectores.
5. Crear `solidShell` mediante offset controlado del dominio caliente.
6. Restar `coolingFluidDomain` y comprobar espesores minimos.
7. Añadir refuerzos exteriores fuera de la camisa termica.
8. Ejecutar el estimador 1D y exportar un informe junto con STL/3MF.
9. Solo despues estudiar varias familias, paso variable o un esquema `pass-and-a-half`.

## Criterios de aceptacion de la primera version

- El dominio de gas caliente esta cerrado.
- Todos los canales estan conectados a entrada y salida, sin intersecciones con el dominio caliente.
- La pared caliente, el nervio entre canales y la pared exterior superan el minimo de fabricacion.
- El flujo total de canales coincide con el caudal de diseno dentro de la tolerancia elegida.
- `Qcool` es compatible con la elevacion de temperatura admisible del refrigerante.
- `DeltaP` deja margen respecto a la presion disponible de la bomba.
- La temperatura estimada de pared queda por debajo del limite del material con margen.
- Se exportan por separado solido, dominio caliente y dominio de refrigeracion para inspeccion.

## Advertencias sobre los parametros de referencia

Los valores de `engineInstrucciones.txt`, como 120 canales, 330 bar o 650 kg/s, describen una escala tipo Raptor y no son un punto de partida valido para este cubo de 100 mm. El documento `motor (1).pdf` describe un ASE de 89 kN y usa hidrogeno en un contexto de motor completo. Ambos documentos sirven para arquitectura y ordenes de magnitud, pero el siguiente diseno debe comenzar con un caso de prueba pequeno, propiedades de fluido declaradas y un caudal calculado a partir de una especificacion propia.
