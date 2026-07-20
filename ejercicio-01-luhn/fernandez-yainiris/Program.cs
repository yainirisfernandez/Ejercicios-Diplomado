int opcion;

int totalValidas = 0;
int totalInvalidas = 0;

int totalVisa = 0;
int totalMastercard = 0;
int totalAmericanExpress = 0;
int totalDiscover = 0;

Random random = new Random();

do
{
    Console.WriteLine("========================================");
    Console.WriteLine("      VALIDADOR DE TARJETAS");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Validar una tarjeta");
    Console.WriteLine("2. Validar desde archivo");
    Console.WriteLine("3. Generar número válido");
    Console.WriteLine("4. Estadísticas");
    Console.WriteLine("5. Salir");
    Console.WriteLine("========================================");
    Console.Write("Seleccione una opción: ");

    if (!int.TryParse(Console.ReadLine(), out opcion))
    {
        opcion = 0;
    }

    Console.WriteLine();

    switch (opcion)
    {
        case 1:
            ValidarTarjetaManual();
            break;

        case 2:
            ValidarArchivo();
            break;

        case 3:
            GenerarTarjetaMenu();
            break;

        case 4:
            MostrarEstadisticas();
            break;

        case 5:
            Console.WriteLine("Gracias por utilizar el sistema.");
            break;

        default:
            Console.WriteLine("Opción inválida.");
            break;
    }

    if (opcion != 5)
    {
        Console.WriteLine();
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

} while (opcion != 5);

void ValidarTarjetaManual()
{
    Console.WriteLine("VALIDACIÓN INDIVIDUAL");
    Console.WriteLine("---------------------");

    Console.Write("Ingrese el número de tarjeta: ");

    string? numero = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(numero))
    {
        Console.WriteLine("Debe ingresar un número.");
        return;
    }

    MostrarResultado(numero);
}

void ValidarArchivo()
{
    Console.WriteLine("VALIDACIÓN DESDE ARCHIVO");
    Console.WriteLine("------------------------");

    Console.Write("Ruta del archivo: ");

    string? ruta = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(ruta))
    {
        Console.WriteLine("Ruta inválida.");
        return;
    }

    if (!File.Exists(ruta))
    {
        Console.WriteLine("No se encontró el archivo.");
        return;
    }

    int validasArchivo = 0;
    int invalidasArchivo = 0;

    string[] lineas = File.ReadAllLines(ruta);

    foreach (string linea in lineas)
    {
        string numero = linea.Trim();

        if (numero.Length == 0)
        {
            continue;
        }

        bool esValida = EsValida(numero);

        string marca = esValida
            ? ObtenerMarca(numero)
            : "Desconocida";

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Número : {numero}");
        Console.WriteLine($"Marca  : {marca}");
        Console.WriteLine($"Estado : {(esValida ? "VÁLIDA" : "INVÁLIDA")}");

        if (esValida)
        {
            validasArchivo++;
            totalValidas++;
            RegistrarMarca(marca);
        }
        else
        {
            invalidasArchivo++;
            totalInvalidas++;
        }
    }

    Console.WriteLine();
    Console.WriteLine("========== RESUMEN ==========");
    Console.WriteLine($"Tarjetas válidas   : {validasArchivo}");
    Console.WriteLine($"Tarjetas inválidas : {invalidasArchivo}");
}

void GenerarTarjetaMenu()
{
    string? marca;

    do
    {
        Console.WriteLine("GENERAR TARJETA");
        Console.WriteLine("---------------");
        Console.WriteLine("Opciones:");
        Console.WriteLine("- Visa");
        Console.WriteLine("- Mastercard");
        Console.WriteLine("- American Express");
        Console.WriteLine("- Discover");
        Console.WriteLine("- Aleatoria");
        Console.WriteLine();

        Console.Write("Marca: ");

        marca = Console.ReadLine()?.Trim().ToLower();

    } while (
        marca != "visa" &&
        marca != "mastercard" &&
        marca != "american express" &&
        marca != "discover" &&
        marca != "aleatoria"
    );

    string numeroGenerado = GenerarTarjeta(marca);

    Console.WriteLine();
    Console.WriteLine("TARJETA GENERADA");
    Console.WriteLine("----------------");

    MostrarResultado(numeroGenerado);
}

void MostrarResultado(string numero)
{
    bool valida = EsValida(numero);

    string marca = valida
        ? ObtenerMarca(numero)
        : "Desconocida";

    Console.WriteLine("----------------------------------------");
    Console.WriteLine($"Número : {numero}");
    Console.WriteLine($"Marca  : {marca}");
    Console.WriteLine($"Estado : {(valida ? "VÁLIDA" : "INVÁLIDA")}");

    if (valida)
    {
        totalValidas++;
        RegistrarMarca(marca);
    }
    else
    {
        totalInvalidas++;
    }
}

bool EsValida(string numero)
{
    if (!numero.All(char.IsDigit))
    {
        return false;
    }

    if (numero.Length < 13 || numero.Length > 19)
    {
        return false;
    }

    int suma = 0;
    bool duplicar = false;

    for (int i = numero.Length - 1; i >= 0; i--)
    {
        int digito = numero[i] - '0';

        if (duplicar)
        {
            digito *= 2;

            if (digito > 9)
            {
                digito -= 9;
            }
        }

        suma += digito;
        duplicar = !duplicar;
    }

    return suma % 10 == 0;
}

string ObtenerMarca(string numero)
{
    if (numero.StartsWith("4") &&
        (numero.Length == 13 || numero.Length == 16))
    {
        return "Visa";
    }

    if (numero.Length == 16)
    {
        int prefijo = int.Parse(numero.Substring(0, 2));

        if (prefijo >= 51 && prefijo <= 55)
        {
            return "Mastercard";
        }
    }

    if (numero.Length == 15 &&
        (numero.StartsWith("34") || numero.StartsWith("37")))
    {
        return "American Express";
    }

    if (numero.Length >= 16 && numero.Length <= 19)
    {
        if (numero.StartsWith("6011") || numero.StartsWith("65"))
        {
            return "Discover";
        }

        int prefijo3 = int.Parse(numero.Substring(0, 3));

        if (prefijo3 >= 644 && prefijo3 <= 649)
        {
            return "Discover";
        }

        int prefijo6 = int.Parse(numero.Substring(0, 6));

        if (prefijo6 >= 622126 && prefijo6 <= 622925)
        {
            return "Discover";
        }
    }

    return "Desconocida";
}

void RegistrarMarca(string marca)
{
    if (marca == "Visa")
    {
        totalVisa++;
    }
    else if (marca == "Mastercard")
    {
        totalMastercard++;
    }
    else if (marca == "American Express")
    {
        totalAmericanExpress++;
    }
    else if (marca == "Discover")
    {
        totalDiscover++;
    }
}

string GenerarTarjeta(string marca)
{
    if (marca == "aleatoria")
    {
        string[] marcas =
        {
            "visa",
            "mastercard",
            "american express",
            "discover"
        };

        marca = marcas[random.Next(marcas.Length)];
    }

    if (marca == "visa")
    {
        return GenerarNumero("4", 16);
    }

    if (marca == "mastercard")
    {
        return GenerarNumero(random.Next(51, 56).ToString(), 16);
    }

    if (marca == "american express")
    {
        string prefijo = random.Next(2) == 0 ? "34" : "37";
        return GenerarNumero(prefijo, 15);
    }

    if (marca == "discover")
    {
        return GenerarNumero("6011", 16);
    }

    return "";
}

string GenerarNumero(string prefijo, int longitud)
{
    string numero = prefijo;

    while (numero.Length < longitud - 1)
    {
        numero += random.Next(10);
    }

    for (int digito = 0; digito <= 9; digito++)
    {
        string posibilidad = numero + digito;

        if (EsValida(posibilidad))
        {
            return posibilidad;
        }
    }

    return "";
}

void MostrarEstadisticas()
{
    Console.WriteLine("ESTADÍSTICAS GENERALES");
    Console.WriteLine("======================");
    Console.WriteLine($"Tarjetas válidas   : {totalValidas}");
    Console.WriteLine($"Tarjetas inválidas : {totalInvalidas}");
    Console.WriteLine();

    Console.WriteLine("Distribución por marca");
    Console.WriteLine("----------------------");
    Console.WriteLine($"Visa             : {totalVisa}");
    Console.WriteLine($"Mastercard       : {totalMastercard}");
    Console.WriteLine($"American Express : {totalAmericanExpress}");
    Console.WriteLine($"Discover         : {totalDiscover}");
}