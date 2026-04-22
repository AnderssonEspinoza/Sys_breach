# MVP Videojuego 2D en Unity
<img width="1536" height="1024" alt="imagen" src="https://github.com/user-attachments/assets/2ca3b34d-bc69-4a96-b883-066cbb979645" />

Este proyecto es un **MVP** :

- Colisiones de objetos
- Score
- Vidas
- Niveles
- Juego 2D interactivo

## Idea del juego
**Nombre sugerido:** `Coin Escape 2D`

El jugador se mueve por el mapa, recoge monedas para sumar puntos, evita enemigos que quitan vidas y llega a la puerta para pasar al siguiente nivel.

---

## Requisitos
- Unity **2022.3 LTS** o compatible
- TextMeshPro (normalmente Unity lo importa solo)

---

## Estructura incluida
- `Assets/Scripts/` → scripts C# del juego
- `Assets/Art/` → sprites básicos de apoyo
- `Scenes/` → carpeta vacía para crear tus escenas
- `Packages/manifest.json`
- `ProjectSettings/ProjectVersion.txt`

---

## Scripts incluidos

### 1. `GameManager.cs`
Controla:
- score
- vidas
- nivel actual
- cambio de escenas
- mensajes en pantalla
- reinicio del juego

### 2. `PlayerController.cs`
Controla el movimiento del jugador con teclado:
- WASD
- flechas

### 3. `Collectible.cs`
Sirve para monedas o ítems. Cuando el jugador colisiona con ellos:
- suma score
- destruye el objeto

### 4. `EnemyMover.cs`
Hace que un enemigo se mueva entre 2 puntos.
Cuando toca al jugador:
- quita una vida
- devuelve al jugador al punto de respawn

### 5. `ExitDoor.cs`
La puerta revisa si alcanzaste el score mínimo del nivel.
- si sí, pasa al siguiente nivel
- si no, muestra cuántos puntos faltan

### 6. `MenuButtons.cs`
Para botones de:
- reiniciar juego
- salir

### 7. `CameraFollow.cs`
Hace que la cámara siga al jugador.

---

# Cómo montarlo en Unity paso a paso

## PASO 1: Abrir el proyecto
1. Abre Unity Hub.
2. Elige **Add project from disk**.
3. Selecciona la carpeta `unity2d_mvp`.
4. Ábrelo con Unity 2022.3 LTS.

---

## PASO 2: Crear las escenas
Crea estas escenas dentro de `Assets/Scenes/`:

- `Level1`
- `Level2`
- `GameOver`
- `Victory`

Luego agrégalas al Build Settings en este orden:
1. Level1
2. Level2
3. GameOver
4. Victory

---

## PASO 3: Crear el jugador
En `Level1` y `Level2`:

1. `GameObject > 2D Object > Sprites > Square`
2. Renómbralo como `Player`
3. Tag: `Player`
4. Añade componentes:
   - `Rigidbody2D`
   - `BoxCollider2D`
   - `PlayerController`
5. En `Rigidbody2D`:
   - Gravity Scale = `0`
   - Freeze Rotation Z = activado
6. Crea un objeto vacío llamado `RespawnPoint`
7. Arrastra `RespawnPoint` al campo `respawnPoint` del script `PlayerController`

---

## PASO 4: Crear el GameManager
1. Crea un objeto vacío llamado `GameManager`
2. Añade el script `GameManager`
3. Este objeto debe existir desde `Level1`
4. **No pongas otro GameManager en Level2, GameOver o Victory**, porque este usa `DontDestroyOnLoad`

---

## PASO 5: Crear UI
En `Level1` y `Level2` crea un Canvas con estos textos TextMeshPro:

- `ScoreText`
- `LivesText`
- `LevelText`
- `MessageText`

Ejemplo:
- `ScoreText` arriba izquierda
- `LivesText` arriba centro
- `LevelText` arriba derecha
- `MessageText` abajo centro

Importante:
Los nombres deben ser exactamente esos, porque el GameManager los busca por nombre.

---

## PASO 6: Crear monedas
1. Crea varios objetos Sprite (círculo, estrella, cuadrado, lo que quieras)
2. Añade `CircleCollider2D` o `BoxCollider2D`
3. Marca `Is Trigger`
4. Añade script `Collectible`
5. Pon varias monedas por el mapa

En `Level1`, el nivel se desbloquea con **5 puntos**.
En `Level2`, se desbloquea con **8 puntos**.

---

## PASO 7: Crear enemigos
1. Crea un sprite cuadrado o enemigo simple
2. Añade:
   - `Rigidbody2D` (Gravity Scale = 0)
   - `BoxCollider2D`
   - `EnemyMover`
3. Crea dos empty objects:
   - `PointA`
   - `PointB`
4. Colócalos donde quieres que el enemigo se mueva
5. Asigna ambos al script `EnemyMover`

Cuando el jugador choque con el enemigo:
- pierde 1 vida
- vuelve al respawn

---

## PASO 8: Crear la puerta de salida
1. Crea un sprite rectangular
2. Renómbralo `ExitDoor`
3. Añade `BoxCollider2D`
4. Marca `Is Trigger`
5. Añade script `ExitDoor`

Comportamiento:
- si tienes el score mínimo: pasas de nivel
- si no: te dice cuántos puntos faltan

---

## PASO 9: Cámara
1. Selecciona la Main Camera
2. Añade script `CameraFollow`
3. Arrastra el Player al campo `target`

---

## PASO 10: Escena GameOver
Crea una escena simple con:
- texto `GAME OVER`
- botón `Reintentar`
- botón `Salir`

En un objeto vacío añade `MenuButtons.cs`.
Asigna:
- botón reintentar → `RestartGame()`
- botón salir → `QuitGame()`

---

## PASO 11: Escena Victory
Crea una escena simple con:
- texto `¡GANASTE!`
- botón `Jugar otra vez`
- botón `Salir`

También usa `MenuButtons.cs`.

---

# Configuración sugerida del mapa

## Level1
- 5 monedas
- 1 o 2 enemigos
- mapa pequeño
- puerta al final

## Level2
- 8 monedas
- 2 o 3 enemigos
- un poco más difícil
- puerta más alejada


