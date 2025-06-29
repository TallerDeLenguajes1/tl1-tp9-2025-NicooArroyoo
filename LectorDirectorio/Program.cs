// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

string path;
do
{
    Console.WriteLine("Ingrese un path de directorio:");
    path = Console.ReadLine();
    if (!Directory.Exists(path))
    {
        Console.WriteLine("El directorio no existe. Por favor, intente de nuevo.");
    }
} while (!Directory.Exists(path));

string[] archivos = Directory.GetFiles(path);
string[] subdirectorios = Directory.GetDirectories(path);

if (archivos.Length > 0 || subdirectorios.Length > 0)
{
     Console.WriteLine("\n - Subdirectorios encontrados:");
    foreach (string subdir in subdirectorios)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(subdir);
        Console.WriteLine($"Subdirectorio: {dirInfo.Name}");
    }
    
    Console.WriteLine("\n - Archivos encontrados:");
    for (int i = 0; i < archivos.Length; i++)
    {
        FileInfo fileInfo = new FileInfo(archivos[i]);
        Console.WriteLine($"Archivo: {fileInfo.Name}, Tamaño: {fileInfo.Length/1024} KB");
    }
}
else
{
    Console.WriteLine("No se encontraron archivos ni subdirectorios en el directorio especificado.");
}

string PathNuevo = Path.Combine(path, "reporte_archivos.csv");

if (!File.Exists(PathNuevo))
{
   var nuevo = File.Create(PathNuevo);
    nuevo.Close();
}

StreamWriter escritor = new StreamWriter(PathNuevo);

for (int i = 0; i < archivos.Length; i++)
{
    FileInfo Info = new FileInfo(archivos[i]);
    double tamanioKB = Math.Round(Info.Length / 1024.0, 2);
    escritor.WriteLine($"{Info.Name};{tamanioKB};{Info.LastWriteTime}"); 
}
escritor.Close();
