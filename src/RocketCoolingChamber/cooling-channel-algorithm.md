# Algoritmo recomendado para canales internos

Este algoritmo usa las ideas de las notas y del repositorio HelixHeatX sin copiar valores de un motor grande como si fueran validos para cualquier pieza.

## 1. Variables y unidades

- `p` o `CoolantPressure`: presion del refrigerante en Pa.
- `p_psi` o `CoolantPressurePsi`: la misma presion en psi, solo para informes o datos de referencia.
- `z`: posicion axial en mm.
- `r_inner(z)`: radio del dominio de gas caliente en mm.
- `r_channel(z)`: radio del centro del canal en mm.
- `r_outer(z)`: radio exterior de la camisa en mm.
- `N`: numero de canales.
- `w`, `h`: ancho y altura nominal del canal.
- `t_liner`: espesor entre gas y refrigerante.
- `t_web`: nervio minimo entre canales.
- `voxel`: resolucion espacial del campo voxelizado.

Las coordenadas de PicoGK se mantienen en mm. Las propiedades de fluido y los balances termicos usan SI.

## 2. Perfil de radio

Para cada `z` se calcula primero el radio interior:

```text
r_inner(z) = perfil de camara, convergente, garganta y tobera
r_channel(z) = r_inner(z) + t_liner + h / 2
r_outer(z) = r_inner(z) + t_liner + h + t_web + wall_outer
```

El perfil debe ser continuo. La garganta es la zona de control porque tiene el menor radio y normalmente el mayor flujo termico.

## 3. Condicion de separacion

Antes de crear el lattice se calcula el paso circunferencial minimo en la garganta:

```text
pitch_circ = 2 * pi * r_channel(throat) / N
pitch_circ > w + t_web
```

Si no se cumple, no se genera geometria: hay demasiados canales para el radio elegido. Esta condicion es geometrica, no demuestra que la presion o la temperatura sean admisibles.

## 4. Trayectoria helicoidal

Para el canal `i`:

```text
phi_i(z) = 2 * pi * i / N
         + z * tan(helix_angle) / r_channel(z)

P_i(z) = (
    r_channel(z) * cos(phi_i),
    r_channel(z) * sin(phi_i),
    z
)
```

La representacion anterior conserva los ejes `x`, `y`, `z` del repositorio. Si el sistema anfitrion usa otro eje axial, se aplica una transformacion de frame al final, no se cambian las ecuaciones a mitad del recorrido.

## 5. Muestreo y Lattice

```text
sample_step >= 2 * voxel
for channel in 0 .. N - 1:
    previous = none
    for z in [0 .. total_length] with sample_step:
        point = P_channel(z)
        if previous exists:
            lattice.AddBeam(previous, beam_radius, point, beam_radius)
        previous = point
coolantDomain = new Voxels(lattice)
```

El `beam` circular actual es una primera aproximacion. Para representar canales fabricados con seccion rectangular o trapezoidal, se debe sustituir por un barrido de perfil. El paso axial no debe ser menor que dos voxeles para evitar segmentos mal representados.

## 6. Condiciones de volumen

Antes de restar el dominio de refrigerante del solido:

```text
r_channel - h / 2 > r_inner + t_liner / 2
r_outer - (r_channel + h / 2) >= wall_outer
pitch_circ - w >= t_web
```

Ademas, cada canal debe conectar a un colector de entrada y uno de salida. Un conjunto de beams visualmente helicoidal pero sin colectores no es un circuito de refrigeracion completo.

## 7. Presion y temperatura

La presion no debe modificar directamente el radio con una formula arbitraria. Primero se usa para comprobar el punto de diseno:

```text
DeltaP_total < p_inlet - p_outlet
T_wall < T_max_material
T_coolant_out < T_limite_fluido
```

La perdida de presion puede estimarse por segmentos:

```text
DeltaP_f = f * (L / Dh) * rho * v^2 / 2
DeltaP_K = sum(K) * rho * v^2 / 2
DeltaP_total = DeltaP_f + DeltaP_K
```

El modelo termico actual es una criba preliminar. No incluye propiedades variables, cambio de fase, radiacion, flujo bifasico ni correlaciones especificas de la garganta.

## 8. Orden de implementacion

1. Validar parametros y unidades.
2. Calcular `r_inner(z)`.
3. Calcular radios de canal y camisa.
4. Comprobar paso circunferencial y espesores.
5. Generar puntos helicoidales.
6. Construir `Lattice`.
7. Convertir una vez a `Voxels`.
8. Añadir colectores cerrados.
9. Generar dominio de gas caliente cerrado.
10. Formar `solid - hotGas - coolant`.
11. Ejecutar estimacion termica e hidraulica.
12. Exportar por separado solido, gas y refrigerante.

## 9. Escalado

Los valores tipo Raptor, ASE o NASA de las notas se deben tratar como puntos de referencia. Para una pieza nueva se debe definir primero `r_inner`, `N`, `w`, `h`, `t_liner`, caudal y presion. Despues se comprueba si la geometria cabe y si el margen termico e hidraulico es razonable.
