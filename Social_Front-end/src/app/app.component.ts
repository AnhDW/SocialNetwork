import { Component, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { error } from 'console';
import { stringify } from 'querystring';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule, NavbarComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit{
  title = 'Vũ Ngọc Anh';
  url = 'https://localhost:5000/api'
  user: any;
  users: any;
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJyb3NlIiwibmJmIjoxNzA1NjI4OTcxLCJleHAiOjE3MDYyMzM3NzEsImlhdCI6MTcwNTYyODk3MX0.vidohpf_cwWUQ8ejZMG1P_XxYyP37HFxnYw76lV2QsWe6DHYJGSWnJTFvHwl7XKrN_9hCAy_TImsQSoiLz1cEg' });
  options = { headers: this.headers };
  constructor(private http:HttpClient){
  }

  ngOnInit(): void {
    
    this.http.post(this.url + '/Account/login',{
      "username": "rose",
      "password": "Pa$$w0rd"
    },{
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
      }
    }
    ).subscribe({
      next: reponse => this.user = reponse,
      error: error=>console.log(error),
      complete:()=>console.log( this.user)
    });

    this.http.get(this.url + '/Users',this.options).subscribe({
      next: reponse => this.users = reponse,
      error: error=>console.log(error),
      complete:()=>console.log(this.users)
    });

    this.http.get(this.url + '/Posts?pageNumber=3', this.options).subscribe({
      next: reponse => this.users = reponse,
      error: error=>console.log(error),
      complete:()=>console.log(this.users)
    })
  }
}
