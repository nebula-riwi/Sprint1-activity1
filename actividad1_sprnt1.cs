using System;

class CineIntergalactico
{
    static void Main()
    {
        Console.Write("Edad: ");
        int edad = int.Parse(Console.ReadLine());

        Console.Write("Tipo de película (estreno, clasico, 3D, maraton, funcion_especial): ");
        string tipo_pelicula = Console.ReadLine().ToLower();

        Console.Write("Día (lunes, martes, ..., domingo): ");
        string dia = Console.ReadLine().ToLower();

        Console.Write("Hora (mañana, tarde, noche): ");
        string hora = Console.ReadLine().ToLower();

        Console.Write("Membresía (ninguna, silver, gold, platino): ");
        string membresia = Console.ReadLine().ToLower();

        Console.Write("Promo activa (true/false): ");
        bool promo_activa = bool.Parse(Console.ReadLine());

        Console.Write("Es estudiante (true/false): ");
        bool estudiante = bool.Parse(Console.ReadLine());

        Console.Write("Viene en pareja (true/false): ");
        bool pareja = bool.Parse(Console.ReadLine());

        Console.Write("Precio base: ");
        double precio_base = double.Parse(Console.ReadLine());

        double precio_final = precio_base;
        string descuentos = "";
        if (edad < 12)
        {
            if (tipo_pelicula == "clasico" && (dia == "lunes" || dia == "miércoles" || dia == "miercoles"))
            {
                precio_final = 0;
                descuentos += "Niño + Clásico gratis lunes/miércoles; ";
            }
            else if (tipo_pelicula == "3d" && (hora == "mañana" || hora == "tarde"))
            {
                precio_final *= 0.3;
                descuentos += "Niño paga 30% en 3D hasta las 6pm; ";
            }
            else if (tipo_pelicula == "funcion_especial")
            {
                Console.WriteLine("Niños no pueden entrar a funciones especiales.");
                return;
            }
        }
        else if (edad >= 12 && edad <= 17)
        {
            if (tipo_pelicula == "estreno")
            {
                precio_final *= 1;
            }
            else if (tipo_pelicula == "clasico" && (dia == "miércoles" || dia == "miercoles"))
            {
                precio_final *= 0.5;
                descuentos += "Adolescente 50% en clásicos miércoles; ";
            }
            else if (tipo_pelicula == "3d" && membresia == "silver" && dia != "domingo")
            {
                precio_final *= 0.8;
                descuentos += "Membresía Silver 20% en 3D; ";
            }
        }
        else if (edad >= 18 && edad <= 59)
        {
            if (membresia == "gold")
            {
                if (tipo_pelicula == "clasico" && !(dia == "viernes" || dia == "sábado" || dia == "sabado") && hora != "noche")
                {
                    precio_final *= 0.75;
                    descuentos += "Membresía Gold 25% en clásicos; ";
                }
                else if (tipo_pelicula == "3d" && dia != "domingo")
                {
                    precio_final *= 0.85;
                    descuentos += "Membresía Gold 15% en 3D; ";
                }
            }
            else if (membresia == "platino")
            {
                if (!(tipo_pelicula == "estreno" && (dia == "sábado" || dia == "sabado") && hora == "noche"))
                {
                    precio_final *= 0.65;
                    descuentos += "Membresía Platino 35%; ";
                }
            }
        }
        else if (edad >= 60)
        {
            if (tipo_pelicula == "maraton")
            {
                precio_final *= 0.5;
                descuentos += "Senior paga 50% en maratón; ";
            }
            else if (dia == "domingo" && promo_activa)
            {
                precio_final *= 0.3;
                descuentos += "Senior domingo + promo activa 70%; ";
            }
            else
            {
                precio_final *= 0.6;
                descuentos += "Senior 40% descuento; ";
            }
        }
        if ((dia == "miércoles" || dia == "miercoles") && tipo_pelicula != "funcion_especial")
        {
            precio_final *= 0.8;
            descuentos += "Descuento global miércoles; ";
        }
        if ((dia == "viernes" || dia == "sábado" || dia == "sabado") && hora == "noche")
        {
            descuentos += "Viernes/sábado noche: no aplican membresías; ";
        }
        if (tipo_pelicula == "estreno")
        {
            if (estudiante)
            {
                precio_final *= 0.85;
                descuentos += "Estudiante 15% en estreno; ";
            }
            if (membresia == "platino" && !(dia == "sábado" || dia == "sabado") && hora != "noche")
            {
                precio_final *= 0.65;
                descuentos += "Platino 35% en estreno; ";
            }
        }
        else if (tipo_pelicula == "3d")
        {
            precio_final *= 1.1; // recargo
            descuentos += "Recargo 10% por 3D; ";
        }
        else if (tipo_pelicula == "maraton" && edad < 60)
        {
            precio_final *= 0.8;
            descuentos += "Maratón 20% descuento; ";
        }
        else if (tipo_pelicula == "funcion_especial" && edad < 18)
        {
            Console.WriteLine("Solo adultos y seniors pueden entrar a funciones especiales.");
            return;
        }
        if (estudiante && (dia == "lunes" || dia == "miércoles" || dia == "miercoles"))
        {
            precio_final *= 0.9;
            descuentos += "Estudiante lunes/miércoles extra 10%; ";
        }
        if (pareja && dia != "domingo")
        {
            double precio_pareja = precio_final + (precio_final * 0.5);
            descuentos += "Pareja: 2x con 50% en el segundo; ";
            Console.WriteLine($"\nPrecio base: {precio_base}");
            Console.WriteLine($"Descuentos aplicados: {descuentos}");
            Console.WriteLine($"Precio final pareja: {precio_pareja}");
            return;
        }
        if (promo_activa)
        {
            if (dia == "domingo" && tipo_pelicula != "funcion_especial")
            {
                precio_final *= 0.9;
                descuentos += "Promo activa domingo 10% extra; ";
            }
            else if (membresia != "ninguna")
            {
                precio_final *= 0.95;
                descuentos += "Promo activa +5% por membresía; ";
            }
        }
        Console.WriteLine($"\nPrecio base: {precio_base}");
        Console.WriteLine($"Descuentos aplicados: {descuentos}");
        Console.WriteLine($"Precio final a pagar: {precio_final}");
    }
}
