# Validador de Tarjetas de Crédito

Aplicación de consola desarrollada en C# para validar números de tarjetas de crédito mediante el algoritmo de Luhn, identificar la marca de la tarjeta, generar números válidos y mostrar estadísticas de uso.

## Características

* Validación de tarjetas utilizando el algoritmo de Luhn.
* Identificación de la marca de la tarjeta.
* Validación individual desde consola.
* Validación masiva desde archivo de texto.
* Generación de números de tarjeta válidos.
* Estadísticas de validaciones realizadas.

## Marcas Soportadas

| Marca            | Prefijo                          | Longitud        |
| ---------------- | -------------------------------- | --------------- |
| Visa             | 4                                | 13 o 16 dígitos |
| Mastercard       | 51 - 55                          | 16 dígitos      |
| American Express | 34 o 37                          | 15 dígitos      |
| Discover         | 6011, 622126-622925, 644-649, 65 | 16 a 19 dígitos |

## Menú Principal

```text
========================================
      VALIDADOR DE TARJETAS
========================================
1. Validar una tarjeta
2. Validar desde archivo
3. Generar número válido
4. Estadísticas
5. Salir
========================================
```

## Funcionalidades

### 1. Validar una tarjeta

Permite ingresar un número de tarjeta desde la consola y verificar:

* Si cumple con el algoritmo de Luhn.
* La marca correspondiente.
* Su estado (VÁLIDA o INVÁLIDA).

Ejemplo:

```text
Número : 4532015112830366
Marca  : Visa
Estado : VÁLIDA
```

### 2. Validar desde archivo

Permite procesar un archivo `.txt` que contenga un número de tarjeta por línea.

Ejemplo de archivo:

```text
4532015112830366
5500000000000004
371449635398431
1234567890123456
```

Al finalizar se muestra un resumen con la cantidad de tarjetas válidas e inválidas.

### 3. Generar número válido

Genera automáticamente un número de tarjeta válido para una de las siguientes marcas:

* Visa
* Mastercard
* American Express
* Discover
* Aleatoria

Todos los números generados cumplen con el algoritmo de Luhn.

### 4. Estadísticas

Muestra información acumulada durante la ejecución del programa:

* Total de tarjetas válidas.
* Total de tarjetas inválidas.
* Cantidad de tarjetas por marca.

Ejemplo:

```text
ESTADÍSTICAS GENERALES
======================
Tarjetas válidas   : 15
Tarjetas inválidas : 4

Distribución por marca
----------------------
Visa             : 6
Mastercard       : 4
American Express : 2
Discover         : 3
```


## Ejecución

Clonar el repositorio:

```bash
git clone https://github.com/yainirisfernandez/Ejercicios-Diplomado.git
```

Ingresar al directorio del proyecto:

```bash
cd ejercicio-01-luhn/fernandez-yainiris
```

Ejecutar la aplicación:

```bash
dotnet run
```

## Autor

Yainiris María Fernandez Jaraba.