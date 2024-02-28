import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HttpHeaders} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,HttpClientModule, NavbarComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  url = 'https://localhost:5000/api'
  users: any;
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJyb3NlIiwibmJmIjoxNzA1NjI4OTcxLCJleHAiOjE3MDYyMzM3NzEsImlhdCI6MTcwNTYyODk3MX0.vidohpf_cwWUQ8ejZMG1P_XxYyP37HFxnYw76lV2QsWe6DHYJGSWnJTFvHwl7XKrN_9hCAy_TImsQSoiLz1cEg' });
  options = { headers: this.headers };
  
  constructor(private http:HttpClient){
  }

  ngOnInit(): void {

    this.http.get(this.url + '/Users',this.options).subscribe({
      next: reponse => this.users = reponse,
      error: error=>console.log(error),
      complete:()=>console.log(this.users)
    });
  }

}
