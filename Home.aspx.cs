using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPSnippets.GoogleAPI;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace Web_Cripto_2doParcial
{
    public partial class Home : System.Web.UI.Page
    {
        public DataTable tablaArchivos;

        //---------------------------------------------------------------------------------------------
        //METODO
        //Metodo principal que se ejecuta al cargar la pagina
        //---------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            //ClientID y ClientSecret de Google
            GoogleConnect.ClientId = "88475070629-cteeu4o7sldb3auqf25rt9d5b9j7clg9.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "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";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    //se obtienen los datos del profile de Google
                    string code = Request.QueryString["code"].ToString();
                    GoogleConnect connect = new GoogleConnect();
                    string json = connect.Fetch("me", code.ToString());
                    GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
                    
                    lblId.Text = profile.Id;
                    lblName.Text = profile.Name;
                    lblEmail.Text = profile.Email;
                    lblVerified.Text = profile.Verified_Email;
                    imgProfile.ImageUrl = profile.Picture;
                    pnlProfile.Visible = true;

                    //se obtienen los datos de archivos de Google Drive
                    GoogleDrive gdrive = new GoogleDrive();

                    tablaArchivos = CreaTablaArchivos();
                    //variable para las filas
                    DataRow row;

                    var listFileRequest = gdrive.ReturnDriveServices().Files.List();
                    //establece las propiedades a traer de cada archivo
                    listFileRequest.Fields= "files(id,name,createdTime,modifiedTime,size)";

                    //Se recorren todos los archivos
                    foreach (var file in listFileRequest.Execute().Files)
                    {
                        row = tablaArchivos.NewRow();
                        row["Id"] = file.Id;
                        row["Nombre"] = file.Name;
                        row["Fecha Creacion"] = file.CreatedTimeDateTimeOffset.ToString(); ;
                        row["Fecha Modificacion"] = file.ModifiedTimeDateTimeOffset.ToString();
                        row["Tamaño"] = file.Size.ToString();
                        tablaArchivos.Rows.Add(row);
                    }

                    gvArchivos.DataSource = tablaArchivos;
                    gvArchivos.DataBind();

                }
                else 
                {
                    Login();
                }

            }
            
            if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }
                
        }

        //---------------------------------------------------------------------------------------------
        //METODO
        //Metodo para crear las columnas de la tabla para informacion de archivos de Google Drive
        //---------------------------------------------------------------------------------------------
        public DataTable CreaTablaArchivos()
        {
            DataTable tabla = new DataTable();

            // Variables para las columnas
            DataColumn column;

            // Se crean las columnas  
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Id";
            tabla.Columns.Add(column);
  
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Nombre";
            tabla.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Fecha Modificacion";
            tabla.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Fecha Creacion";
            tabla.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Tamaño";
            tabla.Columns.Add(column);

            return tabla;
        }

        //---------------------------------------------------------------------------------------------
        //METODO
        //Metodo que autoriza el acceso al profile
        //---------------------------------------------------------------------------------------------
        protected void Login()
        {
            GoogleConnect.Authorize("profile", "email");
        }
        
    }

    public class GoogleProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public string Verified_Email { get; set; }
    }

    public class GoogleDrive
    {
        //---------------------------------------------------------------------------------------------
        //METODO
        //Metodo que realiza el login a traves de el clientID y ClientSecret
        //Obtiene una autorizacion para el scope especificado, en este caso readonly
        //---------------------------------------------------------------------------------------------
        public UserCredential Login(string googleClientId, string googleClientSecret)
        {
            ClientSecrets secrets = new ClientSecrets()
            {
                ClientId = googleClientId,
                ClientSecret = googleClientSecret
            };

            return GoogleWebAuthorizationBroker.AuthorizeAsync(secrets,
                    scopes: new[] { "https://www.googleapis.com/auth/drive.readonly" },
                    user: "user", CancellationToken.None).Result;

        }

        //---------------------------------------------------------------------------------------------
        //METODO
        //Metodo que establece el acceso al servicio de drive
        //---------------------------------------------------------------------------------------------
        public DriveService ReturnDriveServices()
        {
            string googleClientId = "88475070629-cteeu4o7sldb3auqf25rt9d5b9j7clg9.apps.googleusercontent.com";
            string googleClientSecret = "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";

            UserCredential credential = Login(googleClientId, googleClientSecret);

            var driveService = new DriveService(new BaseClientService.Initializer() { HttpClientInitializer = credential });

            return driveService;


        }

    }
}