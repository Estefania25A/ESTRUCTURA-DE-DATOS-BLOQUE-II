namespace AplicacionArbolBinario
{
    // Clase nodo genérica que puede almacenar cualquier tipo de dato comparable
    class Nodo<T> where T : IComparable<T>
    {
        public T Valor { get; set; }
        public Nodo<T> Izquierdo { get; set; }
        public Nodo<T> Derecho { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    // Clase ArbolBinario que implementa las operaciones principales
    class ArbolBinario<T> where T : IComparable<T>
    {
        private Nodo<T> raiz;

        public ArbolBinario()
        {
            raiz = null;
        }

        // Método para insertar un valor en el árbol
        public void Insertar(T valor)
        {
            raiz = InsertarRecursivo(raiz, valor);
            Console.WriteLine($"Valor {valor} insertado correctamente.");
        }

        private Nodo<T> InsertarRecursivo(Nodo<T> nodo, T valor)
        {
            // Si el nodo es nulo, creamos un nuevo nodo
            if (nodo == null)
            {
                return new Nodo<T>(valor);
            }

            // Comparamos el valor con el valor del nodo actual
            int comparacion = valor.CompareTo(nodo.Valor);

            // Si el valor es menor, insertamos en el subárbol izquierdo
            if (comparacion < 0)
            {
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            }
            // Si el valor es mayor, insertamos en el subárbol derecho
            else if (comparacion > 0)
            {
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
            }
            // Si el valor ya existe, no hacemos nada (evitamos duplicados)

            return nodo;
        }

        // Método para buscar un valor en el árbol
        public bool Buscar(T valor)
        {
            return BuscarRecursivo(raiz, valor);
        }

        private bool BuscarRecursivo(Nodo<T> nodo, T valor)
        {
            // Si el nodo es nulo, el valor no existe
            if (nodo == null)
            {
                return false;
            }

            // Comparamos el valor con el valor del nodo actual
            int comparacion = valor.CompareTo(nodo.Valor);

            // Si encontramos el valor
            if (comparacion == 0)
            {
                return true;
            }

            // Si el valor es menor, buscamos en el subárbol izquierdo
            if (comparacion < 0)
            {
                return BuscarRecursivo(nodo.Izquierdo, valor);
            }

            // Si el valor es mayor, buscamos en el subárbol derecho
            return BuscarRecursivo(nodo.Derecho, valor);
        }

        // Método para eliminar un valor del árbol
        public void Eliminar(T valor)
        {
            raiz = EliminarRecursivo(raiz, valor);
        }

        private Nodo<T> EliminarRecursivo(Nodo<T> nodo, T valor)
        {
            // Si el nodo es nulo, el valor no existe
            if (nodo == null)
            {
                return null;
            }

            // Comparamos el valor con el valor del nodo actual
            int comparacion = valor.CompareTo(nodo.Valor);

            // Si el valor es menor, eliminamos del subárbol izquierdo
            if (comparacion < 0)
            {
                nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor);
            }
            // Si el valor es mayor, eliminamos del subárbol derecho
            else if (comparacion > 0)
            {
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor);
            }
            // Si encontramos el valor a eliminar
            else
            {
                // Caso 1: Nodo hoja (sin hijos)
                if (nodo.Izquierdo == null && nodo.Derecho == null)
                {
                    return null;
                }
                // Caso 2: Nodo con un solo hijo
                else if (nodo.Izquierdo == null)
                {
                    return nodo.Derecho;
                }
                else if (nodo.Derecho == null)
                {
                    return nodo.Izquierdo;
                }
                // Caso 3: Nodo con dos hijos
                else
                {
                    // Encontramos el valor mínimo en el subárbol derecho
                    nodo.Valor = EncontrarMinimo(nodo.Derecho);
                    // Eliminamos el nodo mínimo del subárbol derecho
                    nodo.Derecho = EliminarRecursivo(nodo.Derecho, nodo.Valor);
                }
            }

            return nodo;
        }

        // Método auxiliar para encontrar el valor mínimo en un subárbol
        private T EncontrarMinimo(Nodo<T> nodo)
        {
            T minimo = nodo.Valor;
            while (nodo.Izquierdo != null)
            {
                minimo = nodo.Izquierdo.Valor;
                nodo = nodo.Izquierdo;
            }
            return minimo;
        }

        // Recorrido en orden (inorden): izquierdo, raíz, derecho
        public void RecorridoInorden()
        {
            Console.WriteLine("Recorrido Inorden:");
            InordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void InordenRecursivo(Nodo<T> nodo)
        {
            if (nodo != null)
            {
                InordenRecursivo(nodo.Izquierdo);
                Console.Write($"{nodo.Valor} ");
                InordenRecursivo(nodo.Derecho);
            }
        }

        // Recorrido preorden: raíz, izquierdo, derecho
        public void RecorridoPreorden()
        {
            Console.WriteLine("Recorrido Preorden:");
            PreordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void PreordenRecursivo(Nodo<T> nodo)
        {
            if (nodo != null)
            {
                Console.Write($"{nodo.Valor} ");
                PreordenRecursivo(nodo.Izquierdo);
                PreordenRecursivo(nodo.Derecho);
            }
        }

        // Recorrido postorden: izquierdo, derecho, raíz
        public void RecorridoPostorden()
        {
            Console.WriteLine("Recorrido Postorden:");
            PostordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void PostordenRecursivo(Nodo<T> nodo)
        {
            if (nodo != null)
            {
                PostordenRecursivo(nodo.Izquierdo);
                PostordenRecursivo(nodo.Derecho);
                Console.Write($"{nodo.Valor} ");
            }
        }

        // Método para calcular la altura del árbol
        public int Altura()
        {
            return AlturaRecursiva(raiz);
        }

        private int AlturaRecursiva(Nodo<T> nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            int alturaIzquierda = AlturaRecursiva(nodo.Izquierdo);
            int alturaDerecha = AlturaRecursiva(nodo.Derecho);

            return Math.Max(alturaIzquierda, alturaDerecha) + 1;
        }

        // Método para contar nodos
        public int ContarNodos()
        {
            return ContarNodosRecursivo(raiz);
        }

        private int ContarNodosRecursivo(Nodo<T> nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return 1 + ContarNodosRecursivo(nodo.Izquierdo) + ContarNodosRecursivo(nodo.Derecho);
        }

        // Método para imprimir el árbol de forma visual
        public void ImprimirArbol()
        {
            Console.WriteLine("Estructura del Árbol:");
            ImprimirArbolRecursivo(raiz, 0);
        }

        private void ImprimirArbolRecursivo(Nodo<T> nodo, int nivel)
        {
            if (nodo == null)
            {
                return;
            }

            ImprimirArbolRecursivo(nodo.Derecho, nivel + 1);

            string espacios = new string(' ', nivel * 4);
            Console.WriteLine($"{espacios}{nodo.Valor}");

            ImprimirArbolRecursivo(nodo.Izquierdo, nivel + 1);
        }
    }

    // Clase Persona para demostrar el uso con objetos
    class Persona : IComparable<Persona>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Persona(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public int CompareTo(Persona otra)
        {
            return Id.CompareTo(otra.Id);
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MostrarMenu();
        }

        static void MostrarMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== APLICACIÓN DE ÁRBOLES BINARIOS ===  \n ");
            Console.WriteLine("Seleccione el tipo de datos para el árbol:  \n ");
            Console.WriteLine("1. Enteros  \n ");
            Console.WriteLine("2. Cadenas  \n ");
            Console.WriteLine("3. Objetos (Persona)  \n ");
            Console.Write("Elija una opción (1-3): ");

            int opcion;
            while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 3)
            {
                Console.Write("Opción inválida. Intente nuevamente (1-3): ");
            }

            switch (opcion)
            {
                case 1:
                    GestionarArbolEnteros();
                    break;
                case 2:
                    GestionarArbolCadenas();
                    break;
                case 3:
                    GestionarArbolPersonas();
                    break;
            }
        }

        static void GestionarArbolEnteros()
        {
            ArbolBinario<int> arbol = new ArbolBinario<int>();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=== ÁRBOL BINARIO DE ENTEROS ===");
                Console.WriteLine("1. Insertar número");
                Console.WriteLine("2. Buscar número");
                Console.WriteLine("3. Eliminar número");
                Console.WriteLine("4. Recorrido Inorden");
                Console.WriteLine("5. Recorrido Preorden");
                Console.WriteLine("6. Recorrido Postorden");
                Console.WriteLine("7. Altura del árbol");
                Console.WriteLine("8. Contar nodos");
                Console.WriteLine("9. Imprimir estructura del árbol");
                Console.WriteLine("0. Salir");
                Console.Write("Elija una opción (0-9): ");

                while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 0 || opcion > 9)
                {
                    Console.Write("Opción inválida. Intente nuevamente (0-9): ");
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el número a insertar: ");
                        if (int.TryParse(Console.ReadLine(), out int valorInsertar))
                        {
                            arbol.Insertar(valorInsertar);
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;
                    case 2:
                        Console.Write("Ingrese el número a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out int valorBuscar))
                        {
                            bool encontrado = arbol.Buscar(valorBuscar);
                            Console.WriteLine(encontrado ? $"El valor {valorBuscar} fue encontrado en el árbol." : $"El valor {valorBuscar} no existe en el árbol.");
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;
                    case 3:
                        Console.Write("Ingrese el número a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int valorEliminar))
                        {
                            arbol.Eliminar(valorEliminar);
                            Console.WriteLine($"Se ha intentado eliminar el valor {valorEliminar}.");
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;
                    case 4:
                        arbol.RecorridoInorden();
                        break;
                    case 5:
                        arbol.RecorridoPreorden();
                        break;
                    case 6:
                        arbol.RecorridoPostorden();
                        break;
                    case 7:
                        Console.WriteLine($"La altura del árbol es: {arbol.Altura()}");
                        break;
                    case 8:
                        Console.WriteLine($"El árbol tiene {arbol.ContarNodos()} nodos.");
                        break;
                    case 9:
                        arbol.ImprimirArbol();
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);

            MostrarMenu();
        }

        static void GestionarArbolCadenas()
        {
            ArbolBinario<string> arbol = new ArbolBinario<string>();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=== ÁRBOL BINARIO DE CADENAS ===  \n ");
                Console.WriteLine("1. Insertar cadena  \n ");
                Console.WriteLine("2. Buscar cadena  \n ");
                Console.WriteLine("3. Eliminar cadena  \n ");
                Console.WriteLine("4. Recorrido Inorden  \n ");
                Console.WriteLine("5. Recorrido Preorden  \n ");
                Console.WriteLine("6. Recorrido Postorden  \n ");
                Console.WriteLine("7. Altura del árbol  \n ");
                Console.WriteLine("8. Contar nodos ");
                Console.WriteLine("9. Imprimir estructura del árbol  \n ");
                Console.WriteLine("0. Salir  \n");
                Console.Write("Elija una opción (0-9):  \n ");

                while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 0 || opcion > 9)
                {
                    Console.Write("Opción inválida. Intente nuevamente (0-9): ");
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese la cadena a insertar: ");
                        string valorInsertar = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(valorInsertar))
                        {
                            arbol.Insertar(valorInsertar);
                        }
                        else
                        {
                            Console.WriteLine("La cadena no puede estar vacía.");
                        }
                        break;
                    case 2:
                        Console.Write("Ingrese la cadena a buscar: ");
                        string valorBuscar = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(valorBuscar))
                        {
                            bool encontrado = arbol.Buscar(valorBuscar);
                            Console.WriteLine(encontrado ? $"La cadena '{valorBuscar}' fue encontrada en el árbol." : $"La cadena '{valorBuscar}' no existe en el árbol.");
                        }
                        else
                        {
                            Console.WriteLine("La cadena no puede estar vacía.");
                        }
                        break;
                    case 3:
                        Console.Write("Ingrese la cadena a eliminar: ");
                        string valorEliminar = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(valorEliminar))
                        {
                            arbol.Eliminar(valorEliminar);
                            Console.WriteLine($"Se ha intentado eliminar la cadena '{valorEliminar}'.");
                        }
                        else
                        {
                            Console.WriteLine("La cadena no puede estar vacía.");
                        }
                        break;
                    case 4:
                        arbol.RecorridoInorden();
                        break;
                    case 5:
                        arbol.RecorridoPreorden();
                        break;
                    case 6:
                        arbol.RecorridoPostorden();
                        break;
                    case 7:
                        Console.WriteLine($"La altura del árbol es: {arbol.Altura()}");
                        break;
                    case 8:
                        Console.WriteLine($"El árbol tiene {arbol.ContarNodos()} nodos.");
                        break;
                    case 9:
                        arbol.ImprimirArbol();
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);

            MostrarMenu();
        }

        static void GestionarArbolPersonas()
        {
            ArbolBinario<Persona> arbol = new ArbolBinario<Persona>();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=== ÁRBOL BINARIO DE PERSONAS ===");
                Console.WriteLine("1. Insertar persona");
                Console.WriteLine("2. Buscar persona (por ID)");
                Console.WriteLine("3. Eliminar persona (por ID)");
                Console.WriteLine("4. Recorrido Inorden");
                Console.WriteLine("5. Recorrido Preorden");
                Console.WriteLine("6. Recorrido Postorden");
                Console.WriteLine("7. Altura del árbol");
                Console.WriteLine("8. Contar nodos");
                Console.WriteLine("9. Imprimir estructura del árbol");
                Console.WriteLine("0. Salir");
                Console.Write("Elija una opción (0-9): ");

                while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 0 || opcion > 9)
                {
                    Console.Write("Opción inválida. Intente nuevamente (0-9): ");
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el ID de la persona: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.Write("Ingrese el nombre de la persona: ");
                            string nombre = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(nombre))
                            {
                                arbol.Insertar(new Persona(id, nombre));
                            }
                            else
                            {
                                Console.WriteLine("El nombre no puede estar vacío.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    case 2:
                        Console.Write("Ingrese el ID de la persona a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out int idBuscar))
                        {
                            bool encontrado = arbol.Buscar(new Persona(idBuscar, ""));
                            Console.WriteLine(encontrado ? $"La persona con ID {idBuscar} fue encontrada en el árbol." : $"La persona con ID {idBuscar} no existe en el árbol.");
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    case 3:
                        Console.Write("Ingrese el ID de la persona a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int idEliminar))
                        {
                            arbol.Eliminar(new Persona(idEliminar, ""));
                            Console.WriteLine($"Se ha intentado eliminar la persona con ID {idEliminar}.");
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    case 4:
                        arbol.RecorridoInorden();
                        break;
                    case 5:
                        arbol.RecorridoPreorden();
                        break;
                    case 6:
                        arbol.RecorridoPostorden();
                        break;
                    case 7:
                        Console.WriteLine($"La altura del árbol es: {arbol.Altura()}");
                        break;
                    case 8:
                        Console.WriteLine($"El árbol tiene {arbol.ContarNodos()} nodos.");
                        break;
                    case 9:
                        arbol.ImprimirArbol();
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);

            MostrarMenu();
        }
    }
}
