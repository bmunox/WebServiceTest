using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using PersonaForm.Models.Request;
using Newtonsoft.Json;

namespace PersonaForm
{
    public partial class Nombre : Form
    {
        public Nombre()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string Send<T>(string url, T objectRequest, string method = "POST")
        {
            string result = "";

            try
            {

                JavaScriptSerializer js = new JavaScriptSerializer();

                //serializamos el objeto
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectRequest);

                //peticion
                WebRequest request = WebRequest.Create(url);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Timeout = 10000; //esto es opcional

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                result = e.Message;

            }

            return result;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:53864//api/Persona";
            //Creamos el objeto para solicitud
            PersonaRequest oPersona = new PersonaRequest();
            oPersona.nombre = txtNombre.Text;
            oPersona.edad = int.Parse(txtEdad.Text);
            //primer parametro url
            //segundo parametro el objeto
            //tercer parametro el metodo: POST, GET, PUT, DELETE
            string resultado = Send<PersonaRequest>(url, oPersona, "POST");
        }

        public void ConsumeApi() {

            string json = "{ 'nombre':'jason', 'edad':25 }";
            string url = "http://localhost:53864//api/Persona";
            string method = "POST";

            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            request.PreAuthenticate = true;
            request.ContentType = "application/json;charset=utf-8'";

            //Inyecta los Parametros de Envio al API
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            //La respuesta que obtiene del API la lee como texto y la convierte en JSON
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                string response = JsonConvert.SerializeObject(result);
            }
        }
    }
}
