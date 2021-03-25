import { Injectable } from '@angular/core';
import { ValueAccessor } from '@ionic/angular/directives/control-value-accessors/value-accessor';
import { Tab2PageModule } from '../tab2/tab2.module';
import { Tab2Page } from "../tab2/tab2.page";
import { PlatoApp } from "../objetos/plato-app";
import { HttpClient } from "@angular/common/http";
import { CarritoAlmacena } from "../objetos/carrito-almacena";
import { ObjetosService } from '../services/objetos.service';
import { Carrito } from "../objetos/carrito";
import { Factura } from "../objetos/factura";
import { Pedido } from "../objetos/pedido";
import { CarritoGenera } from "../objetos/carrito-genera";

@Injectable({
  providedIn: 'root'
})
export class DataService {

  pedidoEnProgreso = [];
  // Esta es la lista que almacena lo que esta dentro del carrito
  menu = [
    {
      plato: "Lentejas con platano maduro",
      descripcion: "Deliciosas lentejas en sopa, con pimienta que le da cierto picor.",
      precio: "800",
      calorias: "200",
      tipo: "almuerzo",
      cant: 0
    },
    {
      plato: "Sopa azteca",
      descripcion: "Una sopita tradicional de méxico a base de un caldo de tomate.",
      precio: "650",
      calorias: "250",
      tipo: "cena",
      cant: 0
    },
    {
      plato: "Cereal con leche",
      descripcion: "Kelloggs con banano y leche deslactosada.",
      precio: "300",
      calorias: "150",
      tipo: "desayuno",
      cant: 0
    }
  ]

  constructor(private http: HttpClient, private objetos: ObjetosService) { }

  Url = 'https://192.168.1.2:45455/';

  // Aqui recibe platos para agregarlos al carrito conforme recibe


  //funcion a llamar cuando se compra sirve para cambiar de numero de pedido y crear la factura a mostrar al cliente 
  public comprar(data:PlatoApp[], total:number) {
    var platos: CarritoAlmacena[] = [];
    var factura: Factura = new Factura;
    var platos_con_nombre = []
    var tiempo_total = 0;
    data.forEach(plato_app => {
      tiempo_total += plato_app.tiempo_preparacion * plato_app.cant;
      let plato_almacena: CarritoAlmacena = new CarritoAlmacena;
      plato_almacena.N_plato = plato_app.n_plato;
      plato_almacena.Cantidad = plato_app.cant;
      plato_almacena.Id_carrito = this.objetos.carrito.Id;
      plato_almacena.N_compra = this.objetos.carrito.N_compra;
      platos.push(plato_almacena);
      let plato_con_nombre = { cantidad: plato_almacena.Cantidad, nombre: plato_app.plato };
      platos_con_nombre.push(plato_con_nombre);
    });
    this.http.post<String>(this.Url + "Carrito_almacena", platos).subscribe(car => {
        console.log("platos almacenados");
      }
    );

    this.http.post<Carrito>(this.Url + "Carrito", this.objetos.carrito).subscribe(c => {
      console.log("nuevo carro")
    });
    this.http.post<Factura>(this.Url + "Factura", total).subscribe(fact => {

      factura = fact;
      this.http.post<Pedido>(this.Url + "Pedido", tiempo_total).subscribe(pedido => {
        var carrito_genera: CarritoGenera = new CarritoGenera;
        carrito_genera.N_compra = this.objetos.carrito.N_compra;
        carrito_genera.Id_carrito = this.objetos.carrito.Id;
        carrito_genera.Id_Factura = factura.Id;
        carrito_genera.Id_pedido = pedido.Numero;
        console.log(carrito_genera.Id_pedido);
        console.log(carrito_genera.Id_carrito);
        console.log(carrito_genera.N_compra);
        console.log(carrito_genera.Id_Factura);
        this.http.post<String>(this.Url + "Carrito_genera", carrito_genera).subscribe(g => {
          console.log("todo armado");
        });
        this.objetos.carrito.N_compra += 1;
        this.objetos.carrito.Monto = 0;
      });
    });
    return { detalle: factura.Id, plato_y_cantidad: platos_con_nombre };
  }


  nuevoPedido(platos: PlatoApp[], total: number) {
    const pedido = {
      pedido: platos,
      saldo: total,
      Numero: 1,
      Estado: 'En progreso',
      Tiempo_estimado: 5,
      Tiempo_transcurrido: 0,
      Tiempo_restante: 5,
      Cedula_chef_asignado: 1234
    };
    this.pedidoEnProgreso.push(pedido);
  }

  getPedidoEnProgreso() {
    const tmp = this.pedidoEnProgreso;
    this.pedidoEnProgreso = [];
    return tmp;
  }

}
