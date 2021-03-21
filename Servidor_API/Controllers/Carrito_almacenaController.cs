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
    [Route("Carrito_almacena")]
    public class Carrito_almacenaController : ApiController
    {
        string path = HttpContext.Current.Server.MapPath(@"~/bases/Carrito_almacena.json");
        // GET api/values
        [HttpGet]
        public List<Carrito_almacena> Get()
        {
            string jsontext = System.IO.File.ReadAllText(path);
            List<Carrito_almacena> lista = new List<Carrito_almacena>();
            lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Carrito_almacena>>(jsontext);
            return lista;
        }


        // POST api/values

        [HttpPost]
        public string Post([FromBody] Carrito_almacena carrito_almacena)
        {
            string respuesta = "";

            string jsontext = System.IO.File.ReadAllText(path);
            List<Carrito_almacena> lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Carrito_almacena>>(jsontext);
            if (lista == null)
            {
                lista = new List<Carrito_almacena>();
            }
            bool existe = false;

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Id_carrito == carrito_almacena.Id_carrito && lista[i].N_compra == carrito_almacena.N_compra && lista[i].N_plato == carrito_almacena.N_plato)
                {
                    existe = true;
                    respuesta = "registro ya existente";
                    break;
                }
            }

            if (0 == carrito_almacena.Id_carrito || 0 == carrito_almacena.N_compra || 0 == carrito_almacena.N_plato)
            {
                respuesta = "registro necesita cada identificador";
            }
            else if (!existe)
            {
                lista.Add(carrito_almacena);
                respuesta = "registro ingresado correctamente";
            }

            jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(lista);
            System.IO.File.WriteAllText(path, jsontext);

            return respuesta;
        }

        // PUT api/values/5
        [HttpPut]
        public string Put([FromBody] Carrito_almacena carrito_almacena)
        {
            string respuesta = "";

            string jsontext = System.IO.File.ReadAllText(path);
            List<Carrito_almacena> lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Carrito_almacena>>(jsontext);
            if (lista == null)
            {
                lista = new List<Carrito_almacena>();
            }
            bool existe = false;

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Id_carrito == carrito_almacena.Id_carrito && lista[i].N_compra == carrito_almacena.N_compra && lista[i].N_plato == carrito_almacena.N_plato)
                {
                    lista[i] = carrito_almacena;
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
        public string Delete(Carrito_almacena carrito_almacena)
        {
            string respuesta = "";

            string jsontext = System.IO.File.ReadAllText(path);
            List<Carrito_almacena> lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Carrito_almacena>>(jsontext);
            if (lista == null)
            {
                lista = new List<Carrito_almacena>();
            }
            bool existe = false;

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Id_carrito == carrito_almacena.Id_carrito)
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
