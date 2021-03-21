﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Servidor_API.clases;

namespace Servidor_API.Controllers
{
    [Route("Pedido")]
    public class PedidoController : ApiController
    {
        string path = HttpContext.Current.Server.MapPath(@"~/bases/Pedido.json");
        // GET api/values
        [HttpGet]
        public List<Pedido> Get()
        {
            string jsontext = System.IO.File.ReadAllText(path);
            List<Pedido> lista = new List<Pedido>();
            lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Pedido>>(jsontext);
            return lista;
        }


        // POST api/values

        [HttpPost]
        public string Post([FromBody] Pedido pedido)
        {
            string respuesta = "";

            string jsontext = System.IO.File.ReadAllText(path);
            List<Pedido> lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Pedido>>(jsontext);
            if (lista == null)
            {
                lista = new List<Pedido>();
            }
            bool existe = false;

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Numero == pedido.Numero)
                {
                    existe = true;
                    respuesta = "registro ya existente";
                    break;
                }
            }

            if (pedido.Numero == 0)
            {
                respuesta = "registro necesita un identificador";
            }
            else if (!existe)
            {
                lista.Add(pedido);
                respuesta = "registro ingresado correctamente";
            }

            jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(lista);
            System.IO.File.WriteAllText(path, jsontext);

            return respuesta;
        }

        // PUT api/values/5
        [HttpPut]
        public string Put([FromBody] Pedido pedido)
        {
            string respuesta = "";

            string jsontext = System.IO.File.ReadAllText(path);
            List<Pedido> lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Pedido>>(jsontext);
            if (lista == null)
            {
                lista = new List<Pedido>();
            }
            bool existe = false;

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Numero == pedido.Numero)
                {
                    lista[i] = pedido;
                    jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(lista);
                    System.IO.File.WriteAllText(path, jsontext);
                    existe = true;
                    respuesta = "registro editado exitosamente";
                    break;
                }
            }

            if (!existe)
            {
                respuesta = "el registro no existe";
            }
            return respuesta;

        }

        // DELETE api/values/5
        [HttpDelete]
        public string Delete(Pedido pedido)
        {
            string respuesta = "";

            string jsontext = System.IO.File.ReadAllText(path);
            List<Pedido> lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Pedido>>(jsontext);
            if (lista == null)
            {
                lista = new List<Pedido>();
            }
            bool existe = false;

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Numero == pedido.Numero)
                {
                    lista.RemoveAt(i);
                    jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(lista);
                    System.IO.File.WriteAllText(path, jsontext);
                    existe = true;
                    respuesta = "registro eliminado exitosamente";
                    break;
                }
            }

            if (!existe)
            {
                respuesta = "el registro no existe";
            }
            return respuesta;
        }
    }
}
