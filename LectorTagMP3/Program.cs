using LectorTagMP3;
using System.Text;

using (FileStream fs = new FileStream("De musica ligera.mp3", FileMode.Open, FileAccess.Read))
{
    Id3v1Tag datos = new Id3v1Tag();
    fs.Seek(-128, SeekOrigin.End);

    //Encoding.GetEncoding("latin1");

    byte[] buffer = new byte[128];

    fs.Read(buffer, 0, 128);

    if (buffer.Length < 128)
    {
        Console.WriteLine("Archivo invalido");
    }
    else
    {
        datos.Header = Encoding.Latin1.GetString(buffer, 0, 3).TrimEnd('\0');
        datos.Titulo = Encoding.Latin1.GetString(buffer, 3, 30).TrimEnd('\0');
        datos.Artista = Encoding.Latin1.GetString(buffer, 33, 30).TrimEnd('\0');
        datos.Album = Encoding.Latin1.GetString(buffer, 63, 30).TrimEnd('\0');
        
        string anioTexto = Encoding.Latin1.GetString(buffer, 93, 4).TrimEnd('\0');
        if (int.TryParse(anioTexto, out int anio))
        {
            datos.Anio = anio;
        }
        else
        {
            datos.Anio = 0; // Valor por defecto si no se puede convertir
        }

        datos.Comentario = Encoding.Latin1.GetString(buffer, 97, 30).TrimEnd('\0');
        datos.Genero = Encoding.Latin1.GetString(buffer, 127, 1).TrimEnd('\0');
        Console.WriteLine("Título: " + datos.Titulo);
        Console.WriteLine("Artista: " + datos.Artista);
        Console.WriteLine("Álbum: " + datos.Album);
        Console.WriteLine("Año: " + datos.Anio);
    }
}
