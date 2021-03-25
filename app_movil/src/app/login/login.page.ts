import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
import { Cliente } from '../objetos/cliente';
import { Menu } from '../objetos/menu';
import { ObjetosService } from '../services/objetos.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  correo: string;
  password: string;
  constructor(private router: Router, private objetos: ObjetosService, private alert: AlertController) { }

  ngOnInit() {
  }

  login(form) {
    let validateClient = this.objetos.validar_cliente(this.correo, this.password);
    let n: number;
    n = setTimeout(() => {
      if (validateClient.Cedula != null && validateClient.Cedula != 0) {
        this.router.navigateByUrl('/menu/tabs/tab2');
      } else {
        this.presentAlert();
      };
    }, 3000) as unknown as number;
  }

  login_aux(validateClient) {
    if (validateClient.Cedula != null && validateClient.Cedula != 0) {
      this.router.navigateByUrl('/menu/tabs/tab2');
    } else {
      this.presentAlert();
    };
  }

  async presentAlert() {
    const alert = await this.alert.create({
      header: 'Datos inválidos',
      subHeader: 'Por favor corrija sus datos',
      buttons: ['OK']
    });

    await alert.present();
  }
}
