// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
ReadFile();

static void ReadFile()
{
    string file = @"C:\Users\digis\OneDrive\Documents\Erick Alejandro Yañez Aguilar\TXT\LayoutUsuarios.txt";

    if (File.Exists(file))
    {
        StreamReader TextFile = new StreamReader(file);
        string line;
        line = TextFile.ReadLine();

        ML.Result resultErrores = new ML.Result();
        resultErrores.Objects = new List<object>();

        while ((line = TextFile.ReadLine()) != null)
        {
            string[] lines = line.Split('|');

            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = lines[0];
            usuario.ApellidoPaterno = lines[1];
            usuario.ApellidoMaterno = lines[2];
            usuario.Sexo = char.Parse(lines[3].Trim());
            usuario.FechaNacimiento = lines[4];
            usuario.Password = lines[5];
            usuario.Telefono = lines[6];
            usuario.Celular = lines[7];
            usuario.CURP = lines[8];
            usuario.UserName = lines[9];
            usuario.Email = lines[10];

            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = byte.Parse(lines[11]);
            usuario.Imagen = null;

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Calle = lines[12];
            usuario.Direccion.NumeroInterior = lines[13];
            usuario.Direccion.NumeroExterior = lines[14];

            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

            ML.Result result = BL.Usuario.AddEF(usuario);

            if (!result.Correct)
            {
                resultErrores.Objects.Add(
                "Mensaje: " + result.Message );
            }
            
        }

        if (resultErrores.Objects != null)
        {

        }
        TextWriter tx = new StreamWriter(@"C:\Users\digis\OneDrive\Documents\Erick Alejandro Yañez Aguilar\TXT\LayoutErrores.txt");
        foreach (string error in resultErrores.Objects)
        {
            tx.WriteLine(error);
        }
        tx.Close();
    }
}