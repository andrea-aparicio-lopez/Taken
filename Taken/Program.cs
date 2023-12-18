namespace Taken
{
    internal class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int[,] tab;
            Genera(out tab);
            Desordena(tab);
            Muestra(tab);

            bool resuelto = Resuelto(tab);
            while (!resuelto)
            {
                bool mov = Mueve(tab, PideMov());
                if (mov)
                {
                    Muestra(tab);
                    resuelto = Resuelto(tab);
                }
            }
            Console.Write("\n¡Has ganado!\n");
        }

        // Genera el tablero y lo rellena con los números del 1 al 15 + el 0 del final
        static void Genera(out int[,] tab)
        {
            tab = new int[4, 4];
            int cont = 1;
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    tab[i, j] = cont;
                    cont++;
                }
            }
            tab[tab.GetLength(0) - 1, tab.GetLength(1) - 1] = 0;
        }

        // Intercambia las posiciones de los valores aleatoriamente
        static void Desordena(int[,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    int indI = rnd.Next(0, tab.GetLength(0));
                    int indJ = rnd.Next(0, tab.GetLength(1));
                    int valor = tab[i, j];

                    tab[i, j] = tab[indI, indJ];
                    tab[indI, indJ] = valor;
                }
            }
        }

        // Imprime tab por consola
        static void Muestra(int[,] tab)
        {
            Console.Clear();
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j] == 0) Console.Write($"{".",3}");
                    else Console.Write($"{tab[i, j],3}");
                }
                Console.WriteLine();
            }
        }

        // Comprueba si los números del tablero están ordenados
        static bool Resuelto(int[,] tab)
        {
            bool resuelto = true;
            int cont = 0, i = 0;

            while (resuelto && i < tab.GetLength(0))
            {
                int j = 0;
                while (resuelto && j < tab.GetLength(1))
                {
                    resuelto = tab[i, j] == cont + 1;
                    j++;
                    cont++;
                }
                i++;
            }
            // Si el contador es igual al valor que correspondería a la última casilla (si no estuviese vacía), entonces es que el bucle ha llegado hasta el final --> tab está ordenado
            return cont == tab.GetLength(0) * tab.GetLength(1);
        }

        // Busca el número n en el tablero y devuelve un booleano. Si encuentra n, modifica la fila (f) y columna (c) por referencia
        static bool Busca(int[,] tab, int n, out int f, out int c)
        {
            // Los inicializo fuera de rango
            f = tab.GetLength(0);
            c = tab.GetLength(1);

            bool encontrado = false;

            for (int i = 0; !encontrado && i < tab.GetLength(0); i++)
            {
                for (int j = 0; !encontrado && j < tab.GetLength(1); j++)
                {
                    encontrado = tab[i, j] == n;
                    if (encontrado)
                    {
                        f = i;
                        c = j;
                    }
                }
            }
            return encontrado;
        }

        // Busca el hueco libre y devuelve por refrencia su posición. Comprueba si es adyacente a la posición [f,c]
        static bool BuscaLibre(int[,] tab, int f, int c, out int lf, out int lc)
        {
            bool adyacente = false;
            bool encontrado = Busca(tab, 0, out lf, out lc);

            if (encontrado)
            {
                adyacente = ((f == lf + 1 || f == lf - 1) && c == lc)
                               ||
                               ((c == lc + 1 || c == lc - 1) && f == lf);
            }
            return adyacente;
        }

        // Recibe el tablero y un número n. Si encuentra ese número y el hueco libre es adyacente, intercambia las posiciones del número y el hueco y devuelve true
        static bool Mueve(int[,] tab, int n)
        {
            int f, c, lf, lc;
            bool mov = false;

            if (Busca(tab, n, out f, out c))
            {
                if (BuscaLibre(tab, f, c, out lf, out lc))
                {
                    int valor = tab[f, c];
                    tab[f, c] = tab[lf, lc];
                    tab[lf, lc] = valor;
                    mov = true;
                }
            }
            return mov;
        }

        static int PideMov()
        {
            Console.Write("Mover.....");
            string input = Console.ReadLine();
            int num;
            int.TryParse(input, out num);
            Console.WriteLine();
            return num;
        }
    }
}