using System;

class CinemaPricing
{
    static void Main()
    {
        Console.WriteLine("Edad (0 - 120): ");
        int edad = LeerEntero(0, 120);

        Console.WriteLine("Tipo de película (premiere, classic, 3d, marathon, special_function): ");
        string tipo = LeerTexto();

        Console.WriteLine("Día (monday, tuesday, wednesday, thursday, friday, saturday, sunday): ");
        string dia = LeerTexto();

        Console.WriteLine("Horario (morning, afternoon, evening): ");
        string horario = LeerTexto();

        Console.WriteLine("Membresía (none, silver, gold, platinum): ");
        string membresia = LeerTexto();

        bool promo = LeerBooleano("¿Hay promoción activa? (yes/no): ");
        bool estudiante = LeerBooleano("¿Eres estudiante? (yes/no): ");
        bool pareja = LeerBooleano("¿Vienes en pareja? (yes/no): ");

        int precioBase = 10000;
        double precioFinal = precioBase;

        Console.WriteLine($"\nPrecio base: {precioBase}");

        // Descuentos por edad
        if (edad < 12)
        {
            if (tipo == "classic" && (dia == "monday" || dia == "wednesday"))
                precioFinal = 0;
            else if (tipo == "3d" && (horario == "morning" || horario == "afternoon"))
                precioFinal *= 0.3;
            else if (tipo == "special_function")
            {
                Console.WriteLine("Niños no pueden entrar a funciones especiales.");
                return;
            }
        }
        else if (edad <= 17)
        {
            if (tipo == "classic" && dia == "wednesday")
                precioFinal *= 0.5;
            if (membresia == "silver" && tipo == "3d" && dia != "sunday")
                precioFinal *= 0.8;
        }
        else if (edad <= 59)
        {
            if (membresia == "gold")
            {
                if (tipo == "classic" && !(dia == "friday" && horario == "evening") && !(dia == "saturday" && horario == "evening"))
                    precioFinal *= 0.75;
                if (tipo == "3d" && dia != "sunday")
                    precioFinal *= 0.85;
            }
            if (membresia == "platinum")
            {
                if (tipo == "premiere" && dia == "saturday" && horario == "evening")
                    Console.WriteLine("Platinum no aplica descuento en premieres sábado noche.");
                else
                    precioFinal *= 0.65;
            }
        }
        else
        {
            if (tipo == "marathon")
                precioFinal *= 0.5;
            else if (dia == "sunday" && promo)
                precioFinal *= 0.3;
            else
                precioFinal *= 0.6;
        }

        // Descuentos generales
        if (dia == "wednesday" && tipo != "special_function")
            precioFinal *= 0.8;

        if ((dia == "friday" || dia == "saturday") && horario == "evening")
            Console.WriteLine("No se aplican descuentos de membresía viernes/sábado noche.");

        if (tipo == "premiere")
        {
            if (estudiante)
                precioFinal *= 0.85;
            else if (membresia == "platinum" && !(dia == "saturday" && horario == "evening"))
                precioFinal *= 0.65;
        }
        else if (tipo == "3d")
            precioFinal *= 1.1;
        else if (tipo == "marathon" && edad < 60)
            precioFinal *= 0.8;
        else if (tipo == "special_function" && edad < 18)
        {
            Console.WriteLine("Solo adultos y mayores pueden entrar a funciones especiales.");
            return;
        }

        if (estudiante && (dia == "monday" || dia == "wednesday"))
            precioFinal *= 0.9;

        if (pareja && dia != "sunday")
            precioFinal += precioFinal * 0.5;

        if (promo)
        {
            if (dia == "sunday" && tipo != "special_function")
                precioFinal *= 0.9;
            else if (dia != "sunday" && membresia != "none")
                precioFinal *= 0.95;
        }

        Console.WriteLine($"\nPrecio final: {Math.Round(precioFinal)}");
    }

    static int LeerEntero(int min, int max)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int valor) && valor >= min && valor <= max)
                return valor;
            Console.WriteLine("Entrada inválida, intenta de nuevo:");
        }
    }

    static string LeerTexto()
    {
        string? entrada;
        do
        {
            entrada = Console.ReadLine()?.ToLower();
            if (string.IsNullOrWhiteSpace(entrada))
                Console.WriteLine("Entrada vacía, intenta de nuevo:");
        } while (string.IsNullOrWhiteSpace(entrada));
        return entrada;
    }

    static bool LeerBooleano(string pregunta)
    {
        Console.WriteLine(pregunta);
        while (true)
        {
            string respuesta = LeerTexto();
            if (respuesta == "yes" || respuesta == "true") return true;
            if (respuesta == "no" || respuesta == "false") return false;
            Console.WriteLine("Responde con yes/no o true/false:");
        }
    }
}
