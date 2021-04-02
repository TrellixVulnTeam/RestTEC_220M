﻿namespace Servidor_API.clases
{
    public class Factura
    {
        public int Id { get; set; }
        public int Monto { get; set; }
        public string Year { get; set; }
        public string Mes { get; set; }
        public string Dia { get; set; }
        public string Hora { get; set; }


        public Factura()
        {

        }

        public Factura(int id, int monto, string año, string mes, string dia, string hora)
        {
            Id = id;
            Monto = monto;
            Year = año;
            Mes = mes;
            Dia = dia;
            Hora = hora;
        }
    }
}