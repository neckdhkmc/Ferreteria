using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientesPeticiones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var token = await GetAccessToken("http://localhost:2817/connect/token",  "123456", "XAXX010101000", "api1");
            MessageBox.Show(token);
        }

        private static async Task<string> GetAccessToken(string tokenEndpoint,  string clientId, string clientSecret, string scope)
        {
             var client = new HttpClient();
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
            tokenRequest.Content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("scope", scope)
        });

            var response = await client.SendAsync(tokenRequest);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Extraer el token de acceso de la respuesta JSON
                return JObject.Parse(responseContent).GetValue("access_token").ToString();
            }
            else
            {
                throw new Exception($"Error al obtener el token de acceso: {response.StatusCode}");
            }
        }
    }
}
